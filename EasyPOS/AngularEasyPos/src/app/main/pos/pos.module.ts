import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseSearchBarModule } from 'app/layout/components/search-bar/search-bar.module';
import { PosComponent } from './pos.component';

const routes = [
    {
        path     : 'pos',
        component: PosComponent
    }
];

@NgModule({
    declarations: [
        PosComponent
    ],
    imports     : [
        RouterModule.forChild(routes),
        TranslateModule,
        FuseSharedModule,
        FuseSearchBarModule,
    ],
    exports     : [
        PosComponent,
    ]
})

export class PosModule
{
}
