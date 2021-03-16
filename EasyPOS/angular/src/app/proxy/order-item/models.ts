import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateOrderItemDto {
  orderId?: string;
  name?: string;
  description?: string;
  code?: string;
  salePrice: number;
  taxes: number;
  quantity: number;
  discount: number;
  totalItem: number;
}

export interface OrderItemDto extends FullAuditedEntityDto<string> {
  orderId?: string;
  name?: string;
  description?: string;
  code?: string;
  salePrice: number;
  taxes: number;
  discount: number;
  quantity: number;
  totalItem: number;
}
