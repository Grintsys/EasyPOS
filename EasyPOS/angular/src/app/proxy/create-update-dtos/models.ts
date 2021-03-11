import type { CustomerStatus } from '../enums/customer-status.enum';

export interface CreateUpdateCustomerDto {
  firstName: string;
  lastName: string;
  idNumber: string;
  rtn: string;
  address: string;
  phoneNumber: string;
  status: CustomerStatus;
  code: string;
}

export interface CreateUpdateProductDto {
  name: string;
  description: string;
  code: string;
  salePrice: number;
  taxes: number;
  isActive: boolean;
  imageUrl: string;
}
