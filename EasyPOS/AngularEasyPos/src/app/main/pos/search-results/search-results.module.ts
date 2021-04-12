import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { SearchResultsComponent } from './search-results.component';
import { ProductCardModule } from '../product-card/product-card.module';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
    declarations: [
        SearchResultsComponent
    ],
    imports     : [
        CommonModule,
        RouterModule,

        MatButtonModule,
        MatIconModule,

        MatMenuModule,

        ProductCardModule,
    ],
    exports     : [
        SearchResultsComponent
    ]
})
export class SearchResultsModule
{
}
