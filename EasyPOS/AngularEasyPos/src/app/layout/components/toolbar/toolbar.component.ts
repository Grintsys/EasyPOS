import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { TranslateService } from '@ngx-translate/core';
import * as _ from 'lodash';

import { FuseConfigService } from '@fuse/services/config.service';
import { FuseSidebarService } from '@fuse/components/sidebar/sidebar.service';

import { navigation } from 'app/navigation/navigation';
import { ToolbarService } from './toolbar.service';
import { WarehouseDto } from 'app/main/products/product.model';
import { SharedService } from 'app/shared.service';

@Component({
    selector     : 'toolbar',
    templateUrl  : './toolbar.component.html',
    styleUrls    : ['./toolbar.component.scss'],
    encapsulation: ViewEncapsulation.None
})

export class ToolbarComponent implements OnInit, OnDestroy
{
    horizontalNavbar: boolean;
    rightNavbar: boolean;
    hiddenNavbar: boolean;
    languages: any;
    navigation: any;
    selectedLanguage: any;
    userStatusOptions: any[];
    warehouses: WarehouseDto[];
    selectedWarehouse: string;
    user: string = '';
    // Private
    private _unsubscribeAll: Subject<any>;

    /**
     * Constructor
     *
     * @param {FuseConfigService} _fuseConfigService
     * @param {FuseSidebarService} _fuseSidebarService
     * @param {TranslateService} _translateService
     */
    constructor(
        private _fuseConfigService: FuseConfigService,
        private _fuseSidebarService: FuseSidebarService,
        private _translateService: TranslateService,
        private _toolbarService: ToolbarService,
        private _sharedService: SharedService
    )
    {
        this.navigation = navigation;

        // Set the private defaults
        this._unsubscribeAll = new Subject();

        this.warehouses = [];
        this.selectedWarehouse = '';
        this.user = localStorage.getItem('user/email') ?? '';
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    ngOnInit(): void
    {
        // Subscribe to the config changes
        this._fuseConfigService.config
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((settings) => {
                this.horizontalNavbar = settings.layout.navbar.position === 'top';
                this.rightNavbar = settings.layout.navbar.position === 'right';
                this.hiddenNavbar = settings.layout.navbar.hidden === true;
            });

        // Set the selected language from default languages
        this.selectedLanguage = _.find(this.languages, {id: this._translateService.currentLang});

        this.getWarehouses('');
    }

    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next();
        this._unsubscribeAll.complete();
    }

    toggleSidebarOpen(key): void
    {
        this._fuseSidebarService.getSidebar(key).toggleOpen();
    }

    search(value): void
    {
        console.log(value);
    }

    logOut(){
        localStorage.clear();
    }

    getWarehouses(filter: string){
        this._toolbarService.getWarehouseList(filter).then(
            (data) => {
                this.warehouses = data;
                if(this.warehouses.length > 0){
                    this.selectWarehouse(this.warehouses[0].id)
                }
            },
            (error) => {
                console.log("Toolbar-Component: Error Getting Warehouses List " + 
                    JSON.stringify(error)
                );
            }
        );
    }

    selectWarehouse(warehouseId: string){
        var warehouse = this.warehouses.find(x => x.id == warehouseId)
        this.selectedWarehouse = warehouse.name;
        localStorage.setItem("warehouseId", warehouseId);
        localStorage.setItem("warehouseCode", warehouse.code);

        this._sharedService.updateWarehouse(warehouseId);
    }
}
