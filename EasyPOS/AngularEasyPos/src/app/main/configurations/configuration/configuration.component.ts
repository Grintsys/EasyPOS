import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Configuration } from './configuration.model';
import { ConfigurationService } from './configuration.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ConfigurationComponent implements OnInit, OnDestroy {

    configuration: Configuration;
    pageType: string;
    configurationForm: FormGroup;

    // Private
    private _unsubscribeAll: Subject<any>;

    /**
    * Constructor
    *
    * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
    * @param {FuseTranslationLoaderService} _configurationService
    * @param {FormBuilder} _formBuilder
   */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService,
        private _configurationService: ConfigurationService,
        private _formBuilder: FormBuilder,
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
        // Set the default
        this.configuration = new Configuration();

        // Set the private defaults
        this._unsubscribeAll = new Subject();
    }


    /**
     * On init
     */
    ngOnInit(): void
    {
        // Subscribe to update Configuration on changes
        this._configurationService.onConfigurationChanged
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe(configuration => {

                if ( configuration )
                {
                    this.configuration = new Configuration({key: 'Key01', value: 'Value01'}),
                    this.pageType = 'edit';
                }
                else
                {
                    this.pageType = 'new';
                }
                this.configurationForm = this.createConfigurationForm();
            });
    }


    /**
    * On destroy
    */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Create configuration form
     *
     * @returns {FormGroup}
    */
    createConfigurationForm(): FormGroup
    {
        return this._formBuilder.group({
            key     : [this.configuration.key],
            value   : [this.configuration.value],
        });
    }

}
