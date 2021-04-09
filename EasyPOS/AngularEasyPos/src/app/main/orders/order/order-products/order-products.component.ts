import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { OrderProduct } from '../order.model';

import { locale as english } from '../../i18n/en';
import { locale as spanish } from '../../i18n/es';

@Component({
  selector: 'app-order-products',
  templateUrl: './order-products.component.html',
  styleUrls: ['./order-products.component.scss']
})
export class OrderProductsComponent {

  dataSource = new MatTableDataSource(new OrderProduct().getInitialData());
  displayedColumns: string[] = ['code', 'productName', 'quantity', 'salePrice', 'total'];

  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static: true})
  sort: MatSort;

  @ViewChild('filter', {static: true})
  filter: ElementRef;


  // Private
  private _unsubscribeAll: Subject<any>;

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

      // Set the private defaults
      this._unsubscribeAll = new Subject();
  }

  /**
  * On ngAfterViewInit
  */
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

}
