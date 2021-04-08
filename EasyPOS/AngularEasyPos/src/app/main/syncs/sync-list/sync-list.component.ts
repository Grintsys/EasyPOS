import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { SyncDialogComponent } from '../sync-dialog/sync-dialog.component';
import { SyncDown, SyncUp } from '../sync/sync.module';

@Component({
  selector: 'app-sync-list',
  templateUrl: './sync-list.component.html',
  styleUrls: ['./sync-list.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class SyncListComponent {

  dataSourceSyncDown = new MatTableDataSource(new SyncDown().getInitialData());
  displayedColumnSyncDown: string[] = ['code', 'informationType', 'status', 'options'];

  dataSourceSyncUp = new MatTableDataSource(new SyncUp().getInitialData());
  displayedColumnSyncUp: string[] = ['code', 'customerCode', 'customerName', 'total', 'documentType', 'status', 'options'];

  @ViewChild(MatPaginator, {static: true})
  paginator: MatPaginator;

  @ViewChild(MatSort, {static: true})
  sort: MatSort;

  @ViewChild('filter', {static: true})
  filter: ElementRef;

  dialogRef: any;


  // Private
  private _unsubscribeAll: Subject<any>;

  /**
  * Constructor
  *
  * @param {FuseTranslationLoaderService} _fuseTranslationLoaderService
  */
  constructor(
    private _fuseTranslationLoaderService: FuseTranslationLoaderService,
    private _matDialog: MatDialog,
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
    this.dataSourceSyncDown.paginator = this.paginator;
    this.dataSourceSyncDown.sort = this.sort;

    this.dataSourceSyncUp.paginator = this.paginator;
    this.dataSourceSyncUp.sort = this.sort;
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

  openDialogToEditJSON(): void
    {
        this.dialogRef = this._matDialog.open(SyncDialogComponent, {
            panelClass: 'edit-JSON-dialog'
        });

        this.dialogRef.afterClosed()
            .subscribe(response => {
                if ( !response )
                {
                    return;
                }
                const actionType: string = response[0];
                switch ( actionType )
                {
                    /**
                     * Send
                     */
                    case 'send':
                        console.log('new Mail');
                        break;
                    /**
                     * Delete
                     */
                    case 'delete':
                        console.log('delete Mail');
                        break;
                }
            });
    }
}
