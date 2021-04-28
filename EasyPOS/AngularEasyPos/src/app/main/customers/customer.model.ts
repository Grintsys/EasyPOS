export class CreateUpdateCustomerDto {
    firstName?: string;
    lastName?: string;
    idNumber?: string;
    rtn?: string;
    address?: string;
    phoneNumber?: string;
    status: CustomerStatus;
    code?: string;
}

export class CustomerDto{
    id?: string;
    firstName?: string;
    lastName?: string;
    idNumber?: string;
    rtn?: string;
    address?: string;
    phoneNumber?: string;
    status: CustomerStatus;
    code?: string;
}

export class CustomerLookupDto{
    id?: string;
    firstName?: string;
    lastName?: string;
    fullName?: string;
}

export enum CustomerStatus {
    Creado = 1,
    Transferido = 2,
}