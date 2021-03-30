import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';

import { FuseSharedModule } from '@fuse/shared.module';

import { ConfigurationsComponent } from './configurations.component';

const routes = [
    {
        path     : 'configurations',
        component: ConfigurationsComponent
    }
];

@NgModule({
    declarations: [
        ConfigurationsComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        TranslateModule,

        FuseSharedModule
    ],
    exports     : [
        ConfigurationsComponent
    ]
})

export class ConfigurationsModule
{
}
