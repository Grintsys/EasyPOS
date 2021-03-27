import type { CreateUpdateDebitNoteDto, DebitNoteDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DebitNoteService {
  apiName = 'Default';

  create = (input: CreateUpdateDebitNoteDto) =>
    this.restService.request<any, DebitNoteDto>({
      method: 'POST',
      url: `/api/app/debit-note`,
      body: input,
    },
    { apiName: this.apiName });

  createDebitNote = (orderId: string) =>
    this.restService.request<any, DebitNoteDto>({
      method: 'POST',
      url: `/api/app/debit-note/debit-note/${orderId}`,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/debit-note/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, DebitNoteDto>({
      method: 'GET',
      url: `/api/app/debit-note/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<DebitNoteDto>>({
      method: 'GET',
      url: `/api/app/debit-note`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateDebitNoteDto) =>
    this.restService.request<any, DebitNoteDto>({
      method: 'PUT',
      url: `/api/app/debit-note/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
