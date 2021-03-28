import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'OPTIONS',
        title    : 'option_main',
        translate: 'NAV.OPTIONS',
        type     : 'group',
        children : [
            {
                id       : 'POS',
                title    : 'pos',
                translate: 'NAV.POS.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/pos',
            },
            {
                id       : 'ORDER',
                title    : 'Orders',
                translate: 'NAV.ORDERS.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/orders',
            },
            {
                id       : 'CUSTOMERS',
                title    : 'Customers',
                translate: 'NAV.CUSTOMERS.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/customers',
            },
            {
                id       : 'PRODUCTS',
                title    : 'Products',
                translate: 'NAV.PRODUCTS.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/products',
            },
            {
                id       : 'SYNC',
                title    : 'Syncronization',
                translate: 'NAV.SYNC.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/syncronizations',
            },
            {
                id       : 'CONFIG',
                title    : 'Configuration',
                translate: 'NAV.CONFIGURATIONS.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/configurations',
            },
            {
                id       : 'ROLES',
                title    : 'Roles',
                translate: 'NAV.ROLES.TITLE',
                type     : 'item',
                icon     : 'email',
                url      : '/roles',
            }
        ]
    }
];
