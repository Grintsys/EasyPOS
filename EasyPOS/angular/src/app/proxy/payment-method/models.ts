import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdatePaymentMethodDto {
  orderId?: string;
  paymentMethodTypeId?: string;
  amount: number;
}

export interface CreateUpdatePaymentMethodTypeDto {
  name?: string;
}

export interface PaymentMethodDto extends FullAuditedEntityDto<string> {
  orderId?: string;
  paymentMethodTypeId?: string;
  paymentMethodTypeName?: string;
  amount: number;
}

export interface PaymentMethodTypeDto extends FullAuditedEntityDto<string> {
  name?: string;
}
