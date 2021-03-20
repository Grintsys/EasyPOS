import type { CustomerStatus } from '../enums/customer-status.enum';
import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateCustomerDto {
  firstName?: string;
  lastName?: string;
  idNumber?: string;
  rtn?: string;
  address?: string;
  phoneNumber?: string;
  status: CustomerStatus;
  code?: string;
}

export interface CustomerDto extends FullAuditedEntityDto<string> {
  firstName?: string;
  lastName?: string;
  idNumber?: string;
  rtn?: string;
  address?: string;
  phoneNumber?: string;
  status: CustomerStatus;
  code?: string;
}

export interface CustomerLookupDto extends EntityDto<string> {
  firstName?: string;
  lastName?: string;
  fullName?: string;
}
