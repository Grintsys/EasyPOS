import type { CreateUpdateOrderDto, OrderDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  apiName = 'Default';

  create = (input: CreateUpdateOrderDto) =>
    this.restService.request<any, OrderDto>({
      method: 'POST',
      url: `/api/app/order`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/order/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, OrderDto>({
      method: 'GET',
      url: `/api/app/order/${id}`,
    },
    { apiName: this.apiName });

  getAllOrders = () =>
    this.restService.request<any, PagedResultDto<OrderDto>>({
      method: 'GET',
      url: `/api/app/order/orders`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<OrderDto>>({
      method: 'GET',
      url: `/api/app/order`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  getOrderById = (id: string) =>
    this.restService.request<any, OrderDto>({
      method: 'GET',
      url: `/api/app/order/${id}/order-by-id`,
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateOrderDto) =>
    this.restService.request<any, OrderDto>({
      method: 'PUT',
      url: `/api/app/order/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
