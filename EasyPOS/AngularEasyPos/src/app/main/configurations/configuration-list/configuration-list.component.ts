import { Component, ElementRef, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { fuseAnimations } from '@fuse/animations';
import { FuseTranslationLoaderService } from '@fuse/services/translation-loader.service';
import { Subject } from 'rxjs';
import { Configuration } from '../configuration/configuration.model';

import { locale as english } from '../i18n/en';
import { locale as spanish } from '../i18n/es';

@Component({
  selector: 'app-configuration-list',
  templateUrl: './configuration-list.component.html',
  styleUrls: ['./configuration-list.component.scss'],
  animations   : fuseAnimations,
  encapsulation: ViewEncapsulation.None
})
export class ConfigurationListComponent {

  dataSource = new MatTableDataSource(new Configuration().getInitialData());
  displayedColumns: string[] = ['code', 'key', 'value', 'options'];

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
