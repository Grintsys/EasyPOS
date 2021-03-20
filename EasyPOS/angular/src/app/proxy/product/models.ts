import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateProductDto {
  name?: string;
  description?: string;
  code?: string;
  salePrice: number;
  taxes: number;
  isActive: boolean;
  imageUrl?: string;
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

export interface ProductLookupDto extends EntityDto<string> {
  name?: string;
}
