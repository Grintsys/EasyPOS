import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

import { FuseSharedModule } from '@fuse/shared.module';
import { ProfileComponent } from './profile.component';



const routes = [
    {
        path     : 'profile',
        component: ProfileComponent,
    }
];

@NgModule({
    declarations: [
        ProfileComponent,
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        MatDividerModule,
        MatIconModule,
        MatTabsModule,

        FuseSharedModule
    ],
    exports     : [
        ProfileComponent
    ]
})
export class ProfileModule
{
}
