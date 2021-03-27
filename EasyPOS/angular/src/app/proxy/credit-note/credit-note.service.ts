import type { CreateUpdateCreditNoteDto, CreditNoteDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CreditNoteService {
  apiName = 'Default';

  create = (input: CreateUpdateCreditNoteDto) =>
    this.restService.request<any, CreditNoteDto>({
      method: 'POST',
      url: `/api/app/credit-note`,
      body: input,
    },
    { apiName: this.apiName });

  createCreditNote = (orderId: string) =>
    this.restService.request<any, CreditNoteDto>({
      method: 'POST',
      url: `/api/app/credit-note/credit-note/${orderId}`,
    },
    { apiName: this.apiName });

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/credit-note/${id}`,
    },
    { apiName: this.apiName });

  get = (id: string) =>
    this.restService.request<any, CreditNoteDto>({
      method: 'GET',
      url: `/api/app/credit-note/${id}`,
    },
    { apiName: this.apiName });

  getList = (input: PagedAndSortedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<CreditNoteDto>>({
      method: 'GET',
      url: `/api/app/credit-note`,
      params: { skipCount: input.skipCount, maxResultCount: input.maxResultCount, sorting: input.sorting },
    },
    { apiName: this.apiName });

  update = (id: string, input: CreateUpdateCreditNoteDto) =>
    this.restService.request<any, CreditNoteDto>({
      method: 'PUT',
      url: `/api/app/credit-note/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
