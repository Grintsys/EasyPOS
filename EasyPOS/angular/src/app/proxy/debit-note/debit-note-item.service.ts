import type { CreateUpdateDebitNoteItemDto, DebitNoteItemDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DebitNoteItemService {
  apiName = 'Default';

  create = (input: CreateUpdateDebitNoteItemDto) =>
    this.restService.request<any, DebitNoteItemDto>({
      method: 'POST',
      url: `/api/app/debit-note-item`,
      body: input,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/debit-note-item/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, DebitNoteItemDto>({
      method: 'GET',
      url: `/api/app/debit-note-item/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<DebitNoteItemDto>>({
      method: 'GET',
      url: `/api/app/debit-note-item`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateDebitNoteItemDto) =>
    this.restService.request<any, DebitNoteItemDto>({
      method: 'PUT',
      url: `/api/app/debit-note-item/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
