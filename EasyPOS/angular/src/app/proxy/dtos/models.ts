import type { FullAuditedEntityDto } from '@abp/ng.core';
import type { CustomerStatus } from '../enums/customer-status.enum';

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

export interface ProductDto extends FullAuditedEntityDto<string> {
  name?: string;
  description?: string;
  code?: string;
  salePrice: number;
  taxes: number;
  isActive: boolean;
  imageUrl?: string;
}
