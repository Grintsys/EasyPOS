import { Component, ElementRef, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';

import { locale as english } from './i18n/en';
import { locale as spanish } from './i18n/es';
import { MatTableDataSource } from '@angular/material/table';

export interface DataTableProductTemp {
    id: number;
    code: string;
    name: string;
    quantity: number;
    salePrice: number;
    total: number;
}

const PRODUCT_DATA_TEMP: DataTableProductTemp[] = [
    {id: 1, code: 'P001', name: 'Producto 1', quantity: 122, salePrice: 10, total: 1021},
    {id: 2, code: 'P002', name: 'Producto 2', quantity: 266, salePrice: 123, total: 1155},
    {id: 3, code: 'P003', name: 'Producto 3', quantity: 345, salePrice: 132, total: 15454},
    {id: 4, code: 'P004', name: 'Producto 4', quantity: 56, salePrice: 155, total: 1544},
    {id: 5, code: 'P005', name: 'Producto 5', quantity: 321, salePrice: 177, total: 1454},
    {id: 6, code: 'P006', name: 'Producto 6', quantity: 143, salePrice: 1456, total: 154},
    {id: 7, code: 'P007', name: 'Producto 7', quantity: 133, salePrice: 1321, total: 145},
    {id: 8, code: 'P008', name: 'Producto 8', quantity: 144, salePrice: 1456, total: 148},
    {id: 9, code: 'P009', name: 'Producto 9', quantity: 1156, salePrice: 1555, total: 187},
    {id: 10, code: 'P010', name: 'Producto 10', quantity: 1789, salePrice: 1555, total: 21},
  ];

@Component({
    selector   : 'pos',
    templateUrl: './pos.component.html',
    styleUrls  : ['./pos.component.scss']
})
export class PosComponent
{

    dataSource = new MatTableDataSource(PRODUCT_DATA_TEMP);
    displayedColumns: string[] = ['id', 'code', 'name', 'quantity', 'salePrice', 'total'];

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


    /**
     * Search
     *
     * @param value
     */
    search(value): void
    {
        // Do your search here...
        console.log(value);
    }
}
