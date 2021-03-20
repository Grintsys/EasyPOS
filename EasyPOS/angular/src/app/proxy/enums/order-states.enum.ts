import { mapEnumToOptions } from '@abp/ng.core';

export enum OrderStates {
  Created = 1,
  Cancelled = 2,
}

export const orderStatesOptions = mapEnumToOptions(OrderStates);
