import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { SyncsComponent } from './syncs.component';

const routes = [
    {
        path     : 'syncs',
        component: SyncsComponent
    }
];

@NgModule({
    declarations: [
        SyncsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        SyncsComponent
    ]
})

export class SyncsModule
{
}
