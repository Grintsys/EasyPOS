import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { LoginService } from './login.service';
import { LoginInput } from './login.input';
import { Router } from "@angular/router";

@Component({
    selector     : 'app-login',
    templateUrl  : './login.component.html',
    styleUrls    : ['./login.component.scss'],
    encapsulation: ViewEncapsulation.None,
    animations   : fuseAnimations
})
export class LoginComponent implements OnInit
{
    loginForm: FormGroup;
    invalidPassword: string;
    loginInProgress: Boolean;

    constructor(
        private _fuseConfigService: FuseConfigService,
        private _formBuilder: FormBuilder,
        private _loginService: LoginService,
        private router: Router
    )
    {
        // Configure the layout
        this._fuseConfigService.config = {
            layout: {
                navbar   : {
                    hidden: true
                },
                toolbar  : {
                    hidden: true
                },
                footer   : {
                    hidden: true
                },
                sidepanel: {
                    hidden: true
                }
            }
        };

        this.invalidPassword = "";
        this.loginInProgress = false;
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this.loginForm = this._formBuilder.group({
            email   : ['', [Validators.required]],
            password: ['', Validators.required]
        });

        this.animateShapes();
    }

    animateShapes(): void
    {
        var html = '';
        for (var i = 1; i <= 50; i ++) {
            html += '<div class="shape-container--'+i+' shape-animation"><div class="random-shape"></div></div>';
        }
        document.querySelector('.shape').innerHTML += html;
    }

    doLogin() {
        const email = this.loginForm.get("email").value;
        const password = this.loginForm.get("password").value;
        const loginInput = new LoginInput(email, password);
        this.invalidPassword = "";
        this.loginInProgress = true;

        this._loginService.authorize(loginInput).then(
            () => {
                this.loginInProgress = false;
                this.router.navigate(["/pos"]);
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
                this.invalidPassword = "Contraseña o Usuario incorrecto!";
                this.loginForm.reset();
                this.loginInProgress = false;
            }
        );
    }
}
