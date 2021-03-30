import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { ProductsComponent } from './products.component';

const routes = [
    {
        path     : 'products',
        component: ProductsComponent
    }
];

@NgModule({
    declarations: [
        ProductsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        ProductsComponent
    ]
})

export class ProductsModule
{
}
