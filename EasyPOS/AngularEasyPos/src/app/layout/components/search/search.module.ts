import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { SearchComponent } from './search.component';

@NgModule({
    declarations: [
        SearchComponent
    ],
    imports     : [
        CommonModule,
        RouterModule,

        MatButtonModule,
        MatIconModule,
    ],
    exports     : [
        SearchComponent
    ]
})
export class SearchModule
{
}
