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
                title    : 'Ventas',
                translate: 'NAV.POS.TITLE',
                type     : 'item',
                icon     : 'shopping_cart',
                url      : '/pos',
                role     : ['admin', 'vendor']
            },
            {
                id       : 'DEBITNOTE',
                title    : 'Nota de Debito',
                translate: 'NAV.DEBITNOTE.TITLE',
                type     : 'item',
                icon     : 'assignment',
                url      : '/debit-note',
                role     : ['admin', 'vendor']
            },
            {
                id       : 'ORDER',
                title    : 'Documents',
                translate: 'NAV.DOCUMENTS.TITLE',
                type     : 'item',
                icon     : 'list_alt',
                url      : '/document-list',
                role     : ['admin', 'vendor']
            },
            {
                id       : 'CUSTOMERS',
                title    : 'Customers',
                translate: 'NAV.CUSTOMERS.TITLE',
                type     : 'item',
                icon     : 'person_outline',
                url      : '/customer-list',
                role     : ['admin', 'vendor']
            },
            {
                id       : 'PRODUCTS',
                title    : 'Products',
                translate: 'NAV.PRODUCTS.TITLE',
                type     : 'item',
                icon     : 'shopping_basket',
                url      : '/product-list',
                role     : ['admin', 'vendor']
            },
            {
                id       : 'SYNC',
                title    : 'Syncronization',
                translate: 'NAV.SYNC.TITLE',
                type     : 'item',
                icon     : 'sync',
                url      : '/sync-list',
                role     : ['admin']
            },
            {
                id       : 'CONFIG',
                title    : 'Configuration',
                translate: 'NAV.CONFIGURATIONS.TITLE',
                type     : 'item',
                icon     : 'settings',
                url      : '/configuration-list',
                role     : ['admin']
            },
            /* {
                id       : 'ROLES',
                title    : 'Roles',
                translate: 'NAV.ROLES.TITLE',
                type     : 'item',
                icon     : 'person_pin',
                url      : '/roles',
            } */
        ]
    }
];
