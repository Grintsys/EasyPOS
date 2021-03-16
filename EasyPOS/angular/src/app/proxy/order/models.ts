import type { OrderStates } from '../enums/order-states.enum';
import type { CreateUpdateOrderItemDto, OrderItemDto } from '../order-item/models';
import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateOrderDto {
  customerId: string;
  orderState: OrderStates;
  orderItems: CreateUpdateOrderItemDto[];
}

export interface OrderDto extends FullAuditedEntityDto<string> {
  customerId?: string;
  orderState: OrderStates;
  subTotal: number;
  isv: number;
  discount: number;
  total: number;
  orderItems: OrderItemDto[];
}
