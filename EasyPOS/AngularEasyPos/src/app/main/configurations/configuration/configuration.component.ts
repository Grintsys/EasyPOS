import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ConfigurationDto } from '../configuration.model';
import { ConfigurationService } from '../configuration.service';
import { ConfigurationsModule } from '../configurations.module';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
    selector: 'app-configuration',
    templateUrl: './configuration.component.html',
    styleUrls: ['./configuration.component.scss'],
    animations: fuseAnimations,
    encapsulation: ViewEncapsulation.None
})
export class ConfigurationComponent implements OnInit, OnDestroy {

    configuration: ConfigurationDto;
    pageType: string;
    configurationForm: FormGroup;
    
    // Private
    private _unsubscribeAll: Subject<any>;

    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _configurationService: ConfigurationService,
        private _formBuilder: FormBuilder,
    ) {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        // Set the default
        this.configuration = new ConfigurationDto();

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }

    ngOnInit(): void {
        // Subscribe to update Configuration on changes
        this._configurationService.onConfigurationChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(configId => {
                if (configId) {
                    this.getConfigById(configId);
                }
                else {
                    this.pageType = 'new';
                }
                this.configurationForm = this.createConfigurationForm();
            });
    }

    ngOnDestroy(): void {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    createConfigurationForm(): FormGroup {
        return this._formBuilder.group({
            key: new FormControl({
                value: this.configuration.key, disabled: true
            }),
            value: new FormControl({
                value: this.configuration.value, disabled: true
            })
        });
    }

    getConfigById(id: string) {
        this._configurationService.get(id).then(
            (c) => {
                this.configuration = c;
            },
            (error) => {
                console.log("Promise rejected with " + JSON.stringify(error));
            }
        );
    }
}
