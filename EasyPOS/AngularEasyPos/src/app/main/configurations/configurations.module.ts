import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { SearchModule } from 'app/layout/components/search/search.module';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
import { MatChipsModule } from '@angular/material/chips';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';

import { ConfigurationListComponent } from './configuration-list/configuration-list.component';
import { ConfigurationComponent } from './configuration/configuration.component';
import { ConfigurationService } from './configuration.service';

const routes = [
    {
        path     : 'configuration-list',
        component: ConfigurationListComponent
    },
    {
        path     : 'configuration',
        component: ConfigurationComponent,
        resolve  : {
            data: ConfigurationService
        }
    },
    {
        path     : 'configuration/:id',
        component: ConfigurationComponent,
        resolve  : {
            data: ConfigurationService
        }
    },
    {
        path     : 'configuration/:id/:handle',
        component: ConfigurationComponent,
        resolve  : {
            data: ConfigurationService
        }
    },
];

@NgModule({
    declarations: [
        ConfigurationListComponent,
        ConfigurationComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule,

        SearchModule,

        MatTableModule,
        MatSortModule,
        MatPaginatorModule,
        MatMenuModule,

        MatFormFieldModule,
        MatInputModule,
        MatTabsModule,
        MatChipsModule,

        MatButtonModule,
        MatIconModule,

        MatDividerModule,

        MatDialogModule,
        MatToolbarModule,
    ],
    exports     : [
        ConfigurationListComponent,
        ConfigurationComponent
    ],
    providers : [
        ConfigurationService,
    ]
})

export class ConfigurationsModule
{
}
