import { APP_INITIALIZER } from '@angular/core';
import { RoutesService, eLayoutType } from '@abp/ng.core';

export const APP_ROUTE_PROVIDER = [
    { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
    return () => {
        routesService.add([
            {
                path: '/pos',
                name: '::Menu:Home',
                iconClass: 'fas fa-home',
                order: 1,
                layout: eLayoutType.application,
            }
        ]);
    };
}