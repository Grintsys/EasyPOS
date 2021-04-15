import { FuseUtils } from "@fuse/utils";

export class Customer {
    code: number;
    firstName: string;
    lastName: string;
    identity: string;
    rtn: string;
    address: string;
    phone: string;
    status: string;

    /**
     * Constructor
     *
     * @param customer
     */
    constructor(customer?) {
        customer = customer || {};
        this.code = customer.id || FuseUtils.generateGUID();
        this.firstName = customer.firstName || "";
        this.lastName = customer.lastName || "";
        this.identity = customer.identity || "";
        this.rtn = customer.rtn || "";
        this.address = customer.address || "";
        this.phone = customer.phone || "";
        this.status = customer.status || "";
    }

    /**
     * Add category
     *
     * @param {MatChipInputEvent} event
     */
    getInitialData(): Customer[] {
        return [
            new Customer({
                firstName: "Nombre1",
                lastName: "Apellido1",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre2",
                lastName: "Apellido2",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre3",
                lastName: "Apellido3",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre4",
                lastName: "Apellido4",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre5",
                lastName: "Apellido5",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre6",
                lastName: "Apellido6",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre7",
                lastName: "Apellido7",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre8",
                lastName: "Apellido8",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre9",
                lastName: "Apellido9",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
            new Customer({
                firstName: "Nombre10",
                lastName: "Apellido10",
                identity: "1614-1885-00178",
                rtn: "0501-1885-01458",
                address: "San Pedro Sula",
                phone: "8741-8958",
                status: "Activo",
            }),
        ];
    }
}

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
    Created = 1,
    Transferred = 2,
}