import type { CreateUpdateDocumentDto, CreateUpdateDocumentItemDto, DocumentDto, DocumentItemDto } from '../document/models';
import type { DebitNoteDto } from '../debit-note/models';
import type { CreditNoteDto } from '../credit-note/models';
import type { PaymentMethodDto } from '../payment-method/models';

export interface CreateUpdateOrderDto extends CreateUpdateDocumentDto<CreateUpdateOrderItemDto> {
}

export interface CreateUpdateOrderItemDto extends CreateUpdateDocumentItemDto {
  orderId?: string;
}

export interface OrderDto extends DocumentDto<OrderItemDto> {
  debitNotes: DebitNoteDto[];
  creditNotes: CreditNoteDto[];
  paymentMethods: PaymentMethodDto[];
}

export interface OrderItemDto extends DocumentItemDto {
  orderId?: string;
}
