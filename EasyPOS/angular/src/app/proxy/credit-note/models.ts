import type { CreateUpdateDocumentDto, CreateUpdateDocumentItemDto, DocumentDto, DocumentItemDto } from '../document/models';

export interface CreateUpdateCreditNoteDto extends CreateUpdateDocumentDto<CreateUpdateCreditNoteItemDto> {
  orderId?: string;
}

export interface CreateUpdateCreditNoteItemDto extends CreateUpdateDocumentItemDto {
  creditNoteId?: string;
}

export interface CreditNoteDto extends DocumentDto<CreditNoteItemDto> {
  orderId?: string;
}

export interface CreditNoteItemDto extends DocumentItemDto {
  creditNoteId?: string;
}
