import type { CreateUpdatePaymentMethodDto, PaymentMethodDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PaymentMethodService {
  apiName = 'Default';

  create = (input: CreateUpdatePaymentMethodDto) =>
    this.restService.request<any, PaymentMethodDto>({
      method: 'POST',
      url: `/api/app/payment-method`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/payment-method/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, PaymentMethodDto>({
      method: 'GET',
      url: `/api/app/payment-method/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<PaymentMethodDto>>({
      method: 'GET',
      url: `/api/app/payment-method`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdatePaymentMethodDto) =>
    this.restService.request<any, PaymentMethodDto>({
      method: 'PUT',
      url: `/api/app/payment-method/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
