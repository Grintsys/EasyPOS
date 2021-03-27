import type { CreateUpdateDocumentDto, CreateUpdateDocumentItemDto, DocumentDto, DocumentItemDto } from '../document/models';

export interface CreateUpdateDebitNoteDto extends CreateUpdateDocumentDto<CreateUpdateDebitNoteItemDto> {
  orderId?: string;
}

export interface CreateUpdateDebitNoteItemDto extends CreateUpdateDocumentItemDto {
  debitNoteId?: string;
}

export interface DebitNoteDto extends DocumentDto<DebitNoteItemDto> {
  orderId?: string;
}

export interface DebitNoteItemDto extends DocumentItemDto {
  debitNoteId?: string;
}
