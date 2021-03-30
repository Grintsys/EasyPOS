import type { DocumentState } from '../enums/document-state.enum';
import type { FullAuditedEntityDto } from '@abp/ng.core';

export interface CreateUpdateDocumentDto<T> {
  id?: string;
  customerId?: string;
  state: DocumentState;
  items: T[];
}

export interface CreateUpdateDocumentItemDto {
  name?: string;
  description?: string;
  code?: string;
  salePrice: number;
  taxes: number;
  quantity: number;
  discount: number;
  totalItem: number;
}

export interface DocumentDto<T> extends FullAuditedEntityDto<string> {
  customerId?: string;
  customerName?: string;
  state: DocumentState;
  subTotal: number;
  isv: number;
  discount: number;
  total: number;
  items: T[];
}

export interface DocumentItemDto extends FullAuditedEntityDto<string> {
  name?: string;
  description?: string;
  code?: string;
  salePrice: number;
  taxes: number;
  discount: number;
  quantity: number;
  totalItem: number;
}
