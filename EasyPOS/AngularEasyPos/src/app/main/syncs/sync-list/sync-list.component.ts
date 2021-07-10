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
  displayedColumnSyncDown: string[] = ['fecha', 'data', 'informationType', 'message', 'status', 'options'];

  syncList: SyncDto[];
  dialogRef: any;
  userRoles: string[];
  private _unsubscribeAll: Subject<any>;

  constructor(
    private _fuseTranslationLoaderService: FuseTranslationLoaderService,
    private _matDialog: MatDialog,
    private _syncService: SyncService
  ) {
    this._fuseTranslationLoaderService.loadTranslations(english, spanish);
    this._unsubscribeAll = new Subject();
    this.syncList = [];
    this.userRoles = [];
  }

  ngAfterViewInit() {
    var userData = localStorage.getItem('id_token_claims_obj');
    var roles = JSON.parse(userData).role ?? '';

    if (!Array.isArray(roles)) {
      roles = [roles];
    }

    this.userRoles = roles;

    this.getSyncList('');
  }

  search(value): void {
    if (value.target != undefined && value.target.value != undefined) {
      this.getSyncList(value.target.value);
    }
  }

  getSyncList(filter: string) {
    if (this.userRoles.indexOf('admin') > -1) {
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

  validateRetry(syncDto: SyncDto) {
    if (syncDto.estado == 0
      && (syncDto.tipoTransaccion == Transacciones.CreacionCliente
        || syncDto.tipoTransaccion == Transacciones.CreacionNotaDebito
        || syncDto.tipoTransaccion == Transacciones.CreacionOrden
        || syncDto.tipoTransaccion == Transacciones.CreacionNotaCredito)) {
      return true;
    }
    return false;
  }

  check(estado: SyncEstados) {
    if (estado == SyncEstados.Fallido)
      return true;
    return false;
  }

  openDialogToEditJSON(syncData: SyncDto): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = "edit-JSON-dialog";
    dialogConfig.data = {
      syncDto: syncData,
      mode: 'edit'
    };
    this.dialogRef = this._matDialog.open(SyncDialogComponent, dialogConfig);

    this.dialogRef.afterClosed()
      .subscribe(response => {
        if (response != undefined) {
          syncData.data = response;
          this.updateJson(syncData);
        }

      });
  }

  openViewData(syncData: SyncDto): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.panelClass = "edit-JSON-dialog";
    dialogConfig.data = {
      syncDto: syncData,
      mode: 'view'
    };

    this.dialogRef = this._matDialog.open(SyncDialogComponent, dialogConfig);
  }

  mapTransaccionesEnum(value: Transacciones) {
    return Transacciones[value];
  }

  mapSyncEstadosEnum(value: SyncEstados) {
    return SyncEstados[value];
  }
}
