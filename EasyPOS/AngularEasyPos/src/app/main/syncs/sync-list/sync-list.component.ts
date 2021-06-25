import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';
import { SyncDialogComponent } from '../sync-dialog/sync-dialog.component';
import { SyncDto, SyncEstados, Transacciones } from '../sync.model';
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

  updateJson(data: SyncDto) {
    this._syncService.update(data.id, data).then(
      () => {
        this.getSyncList('');
      },
      (error) => {
        console.log("Promise rejected with " + JSON.stringify(error));
      }
    );
  }

  retry(id: string) {
    this._syncService.retry(id).then(
      () => {
        this.getSyncList('');
      },
      (error) => {
        console.log("Promise rejected with " + JSON.stringify(error));
      }
    );
  }

  check(estado: SyncEstados){
    if(estado == SyncEstados.Fallido)
      return true;
    return false;
  }

  openDialogToEditJSON(data: SyncDto): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = "edit-JSON-dialog";
    dialogConfig.data = data;

    this.dialogRef = this._matDialog.open(SyncDialogComponent, dialogConfig);

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (!response) {
          return;
        }
        data.data = response;
        this.updateJson(data);
      });
  }

  mapTransaccionesEnum(value: Transacciones) {
    return Transacciones[value];
  }

  mapSyncEstadosEnum(value: SyncEstados) {
    return SyncEstados[value];
  }
}
