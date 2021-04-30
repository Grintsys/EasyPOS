import { CLASS_NAME } from '@angular/flex-layout';
import { FuseUtils } from '@fuse/utils';
import { ProductDto } from '../products/product.model';

export class OrderProduct {
    code: number;
    productName: string;
    quantity: string;
    salePrice: string;
    total: number;

    /**
     * Constructor
     *
     * @param orderProduct
    */
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

export class CreditDebitNote {
    code: number;
    customerCode: string;
    customerName: string;
    total: number;
    documentType: string;
    state: string;

    constructor(creditDebitNote?)
    {
        creditDebitNote = creditDebitNote || {};
        this.code = creditDebitNote.id || FuseUtils.generateGUID();
        this.customerCode = creditDebitNote.customerCode || '';
        this.customerName = creditDebitNote.customerName || '';
        this.total = creditDebitNote.total || 0;
        this.documentType = creditDebitNote.documentType || '';
        this.state = creditDebitNote.state || '';
    }
}

export enum DocumentState {
    Creada = 1,
    Editada = 2,
    Transferida = 3,
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
}

export class CreateUpdateDocumentItemDto {
    productId: string;
    name?: string;
    description?: string;
    code?: string;
    salePrice: number;
    taxes: number;
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
}

export class DocumentItemDto {
    productId?: string;
    name?: string;
    description?: string;
    code?: string;
    salePrice: number;
    taxes: number;
    discount: number;
    quantity: number;
    totalItem: number;
}

export class CreateUpdateOrderDto extends CreateUpdateDocumentDto<CreateUpdateOrderItemDto> {
    paymentMethods: PaymentMethodDto[];
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

    // constructor(order){
    //     super();
    //     order = order || {};
    //     this.id = order.id || FuseUtils.generateGUID();
    //     this.customerId = order.customerId || FuseUtils.generateGUID();
    //     this.customerName = order.customerName || '';
    //     this.customerCode = order.customerCode || '';
    //     this.state = order.state || '';
    //     this.subTotal = order.subTotal || '';
    //     this.isv = order.isv || '';
    //     this.discount = order.discount || '';
    //     this.total = order.total || '';
    //     this.items = order.items || [];
    //     this.debitNotes = order.debitNotes || [];
    //     this.creditNotes = order.creditNotes || [];
    //     this.paymentMethods = order.paymentMethods || [];
    // }
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
        this.taxes = product.taxes || 0,
        this.discount = product.taxes || 0, //TODO
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
    orderId?: string;
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
    orderId?: string;
    paymentMethodTypeId?: string;
    amount: number;
}

export class CreateUpdatePaymentMethodTypeDto {
    name?: string;
    imageUrl: string;
}

export class PaymentMethodDto {
    id?: string;
    orderId?: string;
    paymentMethodTypeId?: string;
    paymentMethodTypeName?: string;
    amount: number;
}

export class PaymentMethodTypeDto {
    id?: string;
    name?: string;
    imageUrl?: string;
}