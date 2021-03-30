import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { CustomersComponent } from './customers.component';

const routes = [
    {
        path     : 'customers',
        component: CustomersComponent
    }
];

@NgModule({
    declarations: [
        CustomersComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        CustomersComponent
    ]
})

export class CustomersModule
{
}
