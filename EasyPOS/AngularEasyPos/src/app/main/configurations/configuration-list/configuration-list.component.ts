import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { ConfigurationDto } from '../configuration.model';
import { ConfigurationService } from '../configuration.service';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
  selector: 'app-configuration-list',
  templateUrl: './configuration-list.component.html',
  styleUrls: ['./configuration-list.component.scss'],
  animations: fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ConfigurationListComponent {
  @ViewChild(MatPaginator, { static: true })
  paginator: MatPaginator;

  @ViewChild(MatSort, { static: true })
  sort: MatSort;

  @ViewChild('filter', { static: true })
  filter: ElementRef;

  dataSource = new MatTableDataSource();
  displayedColumns: string[] = ['key', 'value'];

  configList: ConfigurationDto[];

  // Private
  private _unsubscribeAll: Subject<any>;

  constructor(
    private _fuseTranslationLoaderService: FuseTranslationLoaderService,
    private _configService: ConfigurationService
  ) {
    this._fuseTranslationLoaderService.loadTranslations(english, spanish);

    // Set the private defaults
    this._unsubscribeAll = new Subject();

    this.configList = [];
  }

  ngAfterViewInit() {
    this.getConfigList('');
  }

  search(value): void {
    if (value.target != undefined && value.target.value != undefined) {
      this.getConfigList(value.target.value);
    }
  }

  getConfigList(filter: string) {
    this._configService.getList(filter).then(
      (d) => {
        this.configList = d;
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
