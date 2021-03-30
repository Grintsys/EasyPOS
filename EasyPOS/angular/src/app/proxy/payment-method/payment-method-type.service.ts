import type { CreateUpdatePaymentMethodTypeDto, PaymentMethodTypeDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PaymentMethodTypeService {
  apiName = 'Default';

  create = (input: CreateUpdatePaymentMethodTypeDto) =>
    this.restService.request<any, PaymentMethodTypeDto>({
      method: 'POST',
      url: `/api/app/payment-method-type`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/payment-method-type/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, PaymentMethodTypeDto>({
      method: 'GET',
      url: `/api/app/payment-method-type/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<PaymentMethodTypeDto>>({
      method: 'GET',
      url: `/api/app/payment-method-type`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdatePaymentMethodTypeDto) =>
    this.restService.request<any, PaymentMethodTypeDto>({
      method: 'PUT',
      url: `/api/app/payment-method-type/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
