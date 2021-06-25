import { FuseUtils } from '@fuse/utils';
import { ProductDto } from '../products/product.model';

export class OrderProduct {
    code: number;
    productName: string;
    quantity: string;
    salePrice: string;
    total: number;

    constructor(orderProduct?)
    {
        orderProduct = orderProduct || {};
        this.code = orderProduct.code || FuseUtils.generateGUID();
        this.productName = orderProduct.productName || '';
        this.quantity = orderProduct.quantity || 0;
        this.salePrice = orderProduct.salePrice || 0;
        this.total = orderProduct.total || 0;
    }
}

export enum DocumentState {
    Creada = 1,
    Editada = 2,
    Transferida = 4,
    Cancelada = 3,
}

export enum OrderType {
    Ninguno = 0,
    Credito = 1,
    Contado = 2
}

export class CreateUpdateDocumentDto<T> {
    id?: string;
    customerId?: string;
    state: DocumentState;
    items: T[] = [];
    salesPersonId: number;
    warehouseCode: string;
}

export class CreateUpdateDocumentItemDto {
    productId: string;
    name?: string;
    description?: string;
    code?: string;
    salePrice: number;
    taxes: boolean;
    taxAmount: number;
    quantity: number;
    discount: number;
    totalItem: number;
}

export class DocumentDto<T> {
    id?: string;
    customerId?: string;
    customerName?: string;
    customerCode?: string;
    state: DocumentState;
    subTotal: number = 0;
    isv: number = 0;
    discount: number = 0;
    total: number = 0;
    items: T[] = [];
    salesPersonId: number;
    warehouseCode: string;
}

export class DocumentItemDto {
    productId?: string;
    name?: string;
    description?: string;
    code?: string;
    salePrice: number;
    taxes: boolean;
    taxAmount: number;
    discount: number;
    quantity: number;
    totalItem: number;
}

export class CreateUpdateOrderDto extends CreateUpdateDocumentDto<CreateUpdateOrderItemDto> {
    paymentMethods: CreateUpdatePaymentMethodDto;
    orderType: OrderType;
}

export class CreateUpdateOrderItemDto extends CreateUpdateDocumentItemDto {
    orderId?: string;
}

export class OrderDto extends DocumentDto<OrderItemDto> {
    debitNotes: DebitNoteDto[] = [];
    creditNotes: CreditNoteDto[] = [];
    paymentMethods: PaymentMethodDto[] = [];
    paymentAmount: number = 0;
    orderType: OrderType;
}

export class OrderItemDto extends DocumentItemDto {
    orderId?: string;

    constructor(product: ProductDto){
        super();
        product = product || new ProductDto,
        this.productId = product.id || '',
        this.name = product.name || '',
        this.description = product.description || '',
        this.code = product.code || '',
        this.salePrice = product.salePrice || 0,
        this.taxes = product.taxes || false,
        this.discount = 0,
        this.quantity = 0,
        this.totalItem = this.quantity * this.salePrice
    }
}

export class CreateUpdateDebitNoteDto extends CreateUpdateDocumentDto<CreateUpdateDebitNoteItemDto> {
    orderId?: string;
}

export class CreateUpdateDebitNoteItemDto extends CreateUpdateDocumentItemDto {
    debitNoteId?: string;
}

export class DebitNoteDto extends DocumentDto<DebitNoteItemDto> {
}

export class DebitNoteItemDto extends DocumentItemDto {
    debitNoteId?: string;
}

export class CreateUpdateCreditNoteDto extends CreateUpdateDocumentDto<CreateUpdateCreditNoteItemDto> {
    orderId?: string;
}

export class CreateUpdateCreditNoteItemDto extends CreateUpdateDocumentItemDto {
    creditNoteId?: string;
}

export class CreditNoteDto extends DocumentDto<CreditNoteItemDto> {
    orderId?: string;
}

export class CreditNoteItemDto extends DocumentItemDto {
    creditNoteId?: string;
}

export class CreateUpdatePaymentMethodDto {
    id?: string;
    orderId: string;
    creditDebitCard?: any = new CreateUpdateCreditDebitCardDto;
    cash?: any = new CreateUpdateCashDto;
    wireTransfer?: any = new CreateUpdateWireTransferDto;
    bankChecks?: any[] = new Array;
}

export class PaymentMethodDto {
    id?: string;
    creditDebitCardId?: string;
    cashId?: string;
    wireTransferId?: string;
    bankChecks?: string;
    amount: number;
}

export class CreateUpdateCreditDebitCardDto {
    total: number = 0;
    name: string;
    validThru: Date;
    personId: string;
    certificateRetentionNumber: string;
    paymentMethodId: string;
}

export class CreditDebitCardDto {
    total: number;
    name: string;
    validThru: Date;
    personId: string;
    certificateRetentionNumber: string;
    paymentMethodId: string;
}

export class CreateUpdateCashDto {
    total: number = 0;
    paymentMethodId: string;
}

export class CashDto {
    total: number;
    paymentMethodId: string;
}

export class CreateUpdateWireTransferDto {
    total: number = 0;
    paymentMethodId: string;
    account: string;
    dateTime: Date;
    reference: string;
}

export class WireTransferDto {
    total: number;
    paymentMethodId: string;
    account: string;
    dateTime: Date;
    reference: string;
}

export class CreateUpdateBankCheckDto {
    total: number = 0;
    paymentMethodId: string;
    bank: string;
    date: Date;
}

export class BankCheckDto {
    total: number;
    paymentMethodId: string;
    reference: string;
    bank: string;
    date: Date;
}