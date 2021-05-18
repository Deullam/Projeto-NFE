import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { BaseService } from '../../../core/utils';
import { CORE_CONFIG_TOKEN, ICoreConfig } from '../../../core/core.config';
import { InvoiceItem, InvoiceItemRegisterCommand, InvoiceItemUpdateCommand, InvoiceItemRemoveCommand } from './invoice-item.model';

@Injectable()
export class InvoiceItemService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/invoices`;
    }

    public delete(idInvoice: number, entities: InvoiceItem[]): Observable<boolean> {
        const removeCommand: InvoiceItemRemoveCommand = new InvoiceItemRemoveCommand(entities);

        return this.deleteRequestWithBody(`${this.api}/${idInvoice}/items`, removeCommand)
            .map((response: any): boolean => (response));
    }

    public post(idInvoice: number, receiver: InvoiceItemRegisterCommand[]): Observable<number[]> {
        return this.http.post(`${this.api}/${idInvoice}/items`, receiver).map((response: number[]) => response);
    }

    public put(idInvoice: number, carrier: InvoiceItemUpdateCommand[]): Observable<boolean> {
        return this.http.put(`${this.api}/${idInvoice}/items`, carrier).map((response: boolean) => response);
    }

}
