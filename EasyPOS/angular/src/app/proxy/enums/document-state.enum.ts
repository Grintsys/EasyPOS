import { mapEnumToOptions } from '@abp/ng.core';

export enum DocumentState {
  Created = 1,
  Edited = 2,
  Transferred = 3,
  Cancelled = 3,
}

export const documentStateOptions = mapEnumToOptions(DocumentState);
