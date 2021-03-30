import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { CreateUpdateOrderItemDto, OrderItemDto } from '../order/models';

@Injectable({
  providedIn: 'root',
})
export class OrderItemService {
  apiName = 'Default';

  create = (input: CreateUpdateOrderItemDto) =>
    this.restService.request<any, OrderItemDto>({
      method: 'POST',
      url: `/api/app/order-item`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/order-item/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, OrderItemDto>({
      method: 'GET',
      url: `/api/app/order-item/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<OrderItemDto>>({
      method: 'GET',
      url: `/api/app/order-item`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateOrderItemDto) =>
    this.restService.request<any, OrderItemDto>({
      method: 'PUT',
      url: `/api/app/order-item/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
