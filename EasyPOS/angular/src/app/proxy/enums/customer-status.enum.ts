import { mapEnumToOptions } from '@abp/ng.core';

export enum CustomerStatus {
  Created = 1,
  Transferred = 2,
}

export const customerStatusOptions = mapEnumToOptions(CustomerStatus);
