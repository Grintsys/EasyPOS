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
import { SyncDto } from '../sync.model';
import { SyncService } from '../syncs.service';

@Component({
  selector: 'app-sync-list',
  templateUrl: './sync-list.component.html',
  styleUrls: ['./sync-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class SyncListComponent {
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort, { static: true })
  sort: MatSort;

  @ViewChild('filter', { static: true })
  filter: ElementRef;

  dataSource = new MatTableDataSource();
  displayedColumnSyncDown: string[] = ['fecha', 'data', 'informationType', 'status', 'options'];
  
  syncList: SyncDto[];
  dialogRef: any;

  private _unsubscribeAll: Subject<any>;

  constructor(
    private _fuseTranslationLoaderService: FuseTranslationLoaderService,
    private _matDialog: MatDialog,
    private _syncService: SyncService
  ) {
    this._fuseTranslationLoaderService.loadTranslations(english, spanish);
    this._unsubscribeAll = new Subject();
    this.syncList = [];
  }

  ngAfterViewInit() {
    this.getSyncList('');
  }

  search(value): void {
    if (value.target != undefined && value.target.value != undefined) {
      this.getSyncList(value.target.value);
    }
  }

  getSyncList(filter: string) {
    this._syncService.getList(filter).then(
      (d) => {
        this.syncList = d;
        this.dataSource = new MatTableDataSource(d);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      (error) => {
        console.log("Promise rejected with " + JSON.stringify(error));
      }
    );
  }

  openDialogToEditJSON(): void {
    this.dialogRef = this._matDialog.open(SyncDialogComponent, {
      panelClass: 'edit-JSON-dialog'
    });

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }
        const actionType: string = response[0];
        switch (actionType) {
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
