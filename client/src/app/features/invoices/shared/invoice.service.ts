import { Injectable, Inject } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { State, toODataString } from '@progress/kendo-data-query';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

import { Invoice, InvoiceRemoveCommand, InvoiceRegisterCommand, InvoiceUpdateCommand } from './invoice.model';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { BaseService } from '../../../core/utils';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';
import { CORE_CONFIG_TOKEN, ICoreConfig } from '../../../core/core.config';

@Injectable()
export class InvoiceGridService extends BehaviorSubject<GridDataResult> {
    public loading: boolean;

    constructor(private http: HttpClient, @Inject(CORE_CONFIG_TOKEN) private config: ICoreConfig) {
        super(null);
    }

    public query(state: State): void {
        this.fetch(state).subscribe((x: GridDataResult) => super.next(x));
    }

    protected fetch(state: State): Observable<GridDataResult> {
        const queryStr: string = `${toODataString(state)}&$count=true`;
        this.loading = true;

        return this.http
            .get(`${this.config.apiEndpoint}api/invoices?${queryStr}`)
            .map((response: any): GridDataResult => ({
                    data: response.items,
                    total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class InvoiceService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/invoices`;
    }

    public delete(entities: Invoice[]): Observable<boolean> {
        const removeCommand: InvoiceRemoveCommand = new InvoiceRemoveCommand(entities);

        return this.deleteRequestWithBody(this.api, removeCommand)
            .map((response: any): boolean => (response));
    }

    public get(id: number): Observable<Invoice> {
        return this.http.get(`${this.api}/${id}`).map((response: Invoice) => response);
    }

    public post(receiver: InvoiceRegisterCommand): Observable<number> {
        return this.http.post(`${this.api}`, receiver).map((response: number) => response);
    }

    public put(carrier: InvoiceUpdateCommand): Observable<boolean> {
        return this.http.put(`${this.api}`, carrier).map((response: boolean) => response);
    }

    public queryByName(filteredName: string): Observable<Invoice> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(name), tolower('${filteredName}'))`;

        return this.http
            .get(`${this.api}/?${queryStr}`)
            .map((response: any): Invoice => response.items);
    }
}

@Injectable()
export class InvoiceResolveService extends AbstractResolveService<Invoice> {

    constructor(
        public invoiceService: InvoiceService,
        public breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'invoiceId';
    }

    protected loadEntity(entityId: number): Observable<Invoice> {
        return this.invoiceService
            .get(entityId)
            .take(1)
            .do((invoice: Invoice) => {
                this.breadcrumbService.setMetadata({
                    id: 'invoice',
                    label: invoice.id.toString(),
                    sizeLimit: true,
                });
            });
    }

}
