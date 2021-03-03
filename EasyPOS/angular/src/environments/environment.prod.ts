import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'EasyPOS',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44339',
    redirectUri: baseUrl,
    clientId: 'EasyPOS_App',
    responseType: 'code',
    scope: 'offline_access EasyPOS',
  },
  apis: {
    default: {
      url: 'https://localhost:44339',
      rootNamespace: 'Grintsys.EasyPOS',
    },
  },
} as Environment;
