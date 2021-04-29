import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'
import { FuseSharedModule } from '@fuse/shared.module';
import { LoginComponent } from './login.component';
import { MatSelectModule } from '@angular/material/select';
import { LoginService } from './login.service';
import { AppSettingsService } from 'app/app-settings/app-settings.service';

const routes = [
    {
        path     : 'login',
        component: LoginComponent
    }
];

@NgModule({
    declarations: [
        LoginComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatCheckboxModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        MatProgressSpinnerModule,
        FuseSharedModule
    ],
    exports     : [
        LoginComponent
    ],
    providers : [
        LoginService,
        AppSettingsService,
    ]
})
export class LoginModule {   
}
