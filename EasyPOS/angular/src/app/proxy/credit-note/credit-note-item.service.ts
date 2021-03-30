import type { CreateUpdateCreditNoteItemDto, CreditNoteItemDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CreditNoteItemService {
  apiName = 'Default';

  create = (input: CreateUpdateCreditNoteItemDto) =>
    this.restService.request<any, CreditNoteItemDto>({
      method: 'POST',
      url: `/api/app/credit-note-item`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/credit-note-item/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, CreditNoteItemDto>({
      method: 'GET',
      url: `/api/app/credit-note-item/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<CreditNoteItemDto>>({
      method: 'GET',
      url: `/api/app/credit-note-item`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateCreditNoteItemDto) =>
    this.restService.request<any, CreditNoteItemDto>({
      method: 'PUT',
      url: `/api/app/credit-note-item/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
