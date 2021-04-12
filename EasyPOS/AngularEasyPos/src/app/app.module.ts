import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';

import { FuseModule } from '@fuse/fuse.module';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseProgressBarModule, FuseSidebarModule, FuseThemeOptionsModule } from '@fuse/components';

import { fuseConfig } from 'app/fuse-config';

import { AppComponent } from 'app/app.component';
import { LayoutModule } from 'app/layout/layout.module';
import { PosModule } from 'app/main/pos/pos.module';
import { OrdersModule } from 'app/main/orders/orders.module';
import { CustomersModule } from 'app/main/customers/customers.module';
import { ProductsModule } from 'app/main/products/products.module';
import { SyncsModule } from 'app/main/syncs/syncs.module';
import { ConfigurationsModule } from 'app/main/configurations/configurations.module';
import { LoginModule } from './main/login/login.module';
import { ProfileModule } from './main/profile/profile.module';

const appRoutes: Routes = [
    {
        path      : '**',
        redirectTo: 'login'
    }
];

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports     : [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes, { relativeLinkResolution: 'legacy' }),

        TranslateModule.forRoot(),

        // Material moment date module
        MatMomentDateModule,

        // Material
        MatButtonModule,
        MatIconModule,

        // Fuse modules
        FuseModule.forRoot(fuseConfig),
        FuseProgressBarModule,
        FuseSharedModule,
        FuseSidebarModule,

        // App modules
        LoginModule,
        LayoutModule,
        PosModule,
        OrdersModule,
        ProductsModule,
        CustomersModule,
        SyncsModule,
        ConfigurationsModule,
        ProfileModule,
    ],
    bootstrap   : [
        AppComponent
    ]
})
export class AppModule
{
}
