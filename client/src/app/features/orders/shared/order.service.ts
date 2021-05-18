import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { State, toODataString } from '@progress/kendo-data-query';
import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Injectable, Inject } from '@angular/core';

import { CORE_CONFIG_TOKEN, ICoreConfig } from '../../../core/core.config';
import { BaseService } from '../../../core/utils/base-service';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from './../../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.service';

import { OrderRemoveCommand, Order, OrderRegisterCommand, OrderUpdateCommand } from './order.model';

@Injectable()
export class OrderGridService extends BehaviorSubject<GridDataResult> {
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
            .get(`${this.config.apiEndpoint}api/orders?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }

}

@Injectable()
export class OrderService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/orders`;
    }

    public delete(entities: Order[]): Observable<boolean> {
        const removeCommand: OrderRemoveCommand = new OrderRemoveCommand(entities);

        return this.deleteRequestWithBody(this.api, removeCommand)
            .map((response: any): boolean => (response));
    }

    public get(id: number): Observable<Order> {
        return this.http.get(`${this.api}/${id}`).map((response: Order) => response);
    }

    public post(order: OrderRegisterCommand): Observable<number> {
        return this.http.post(`${this.api}`, order).map((response: number) => response);
    }

    public put(order: OrderUpdateCommand): Observable<boolean> {
        return this.http.put(`${this.api}`, order).map((response: boolean) => response);
    }

}

@Injectable()
export class OrderResolveService extends AbstractResolveService<Order> {

    constructor(
        public orderService: OrderService,
        public breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'orderId';
    }

    protected loadEntity(entityId: number): Observable<Order> {
        return this.orderService
            .get(entityId)
            .take(1)
            .do((order: Order) => {
                this.breadcrumbService.setMetadata({
                    id: 'order',
                    label: order.id.toString(),
                    sizeLimit: true,
                });
            });
    }

}
