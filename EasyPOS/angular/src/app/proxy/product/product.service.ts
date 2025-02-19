import type { CreateUpdateProductDto, ProductDto, ProductLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  apiName = 'Default';

  create = (input: CreateUpdateProductDto) =>
    this.restService.request<any, ProductDto>({
      method: 'POST',
      url: `/api/app/product`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/product/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, ProductDto>({
      method: 'GET',
      url: `/api/app/product/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<ProductDto>>({
      method: 'GET',
      url: `/api/app/product`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  getProductFilteredQuery = (input: string) =>
    this.restService.request<any, ProductDto[]>({
      method: 'GET',
      url: `/api/app/product/product-filtered-query`,
      params: { input: input },
    },
    { apiName: this.apiName });

  getProductLookup = () =>
    this.restService.request<any, ListResultDto<ProductLookupDto>>({
      method: 'GET',
      url: `/api/app/product/product-lookup`,
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateProductDto) =>
    this.restService.request<any, ProductDto>({
      method: 'PUT',
      url: `/api/app/product/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
