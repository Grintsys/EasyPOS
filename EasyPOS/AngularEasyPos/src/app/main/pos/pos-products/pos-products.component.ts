import { Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { MatTableDataSource } from '@angular/material/table';
import { PosProduct } from './pos-product.model';

@Component({
    selector   : 'pos-products',
    templateUrl: './pos-products.component.html',
    styleUrls  : ['./pos-products.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class PosProductsComponent
{

    dataSource = new MatTableDataSource(new PosProduct().getInitialData());
    displayedColumns: string[] = ['id', 'code', 'name', 'quantity', 'salePrice', 'total', 'options'];

    @ViewChild(MatPaginator, {static: true})
    paginator: MatPaginator;

    @ViewChild(MatSort, {static: true})
    sort: MatSort;

    @ViewChild('filter', {static: true})
    filter: ElementRef;


    /**
     * Constructor
     *
     * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
     */
    constructor(
        private _fuseTranslationLoaderService: FuseTranslationLoaderService
    )
    {
        this._fuseTranslationLoaderService.loadTranslations(english, spanish);
    }

    /**
    * On ngAfterViewInit
    */
    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
    }
}
