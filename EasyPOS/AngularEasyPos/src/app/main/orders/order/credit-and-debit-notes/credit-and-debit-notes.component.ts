import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';


import { locale as english } from '../../i18n/en';
import { locale as spanish } from '../../i18n/es';
import { CreditDebitNote } from '../order.model';

@Component({
  selector: 'app-credit-and-debit-notes',
  templateUrl: './credit-and-debit-notes.component.html',
  styleUrls: ['./credit-and-debit-notes.component.scss']
})
export class CreditAndDebitNotesComponent {

  dataSource = new MatTableDataSource(new CreditDebitNote().getInitialData());
  displayedColumns: string[] = ['code', 'customerCode', 'customerName', 'total', 'documentType', 'status', 'options'];

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
