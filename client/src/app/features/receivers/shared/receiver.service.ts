import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { State, toODataString } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Injectable, Inject } from '@angular/core';

import { Receiver, ReceiverRemoveCommand, ReceiverRegisterCommand, ReceiverUpdateCommand } from './receiver.model';
import { ICoreConfig, CORE_CONFIG_TOKEN } from './../../../core/core.config';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from './../../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.service';
import { BaseService } from './../../../core/utils/base-service';

@Injectable()
export class ReceiverGridService extends BehaviorSubject<GridDataResult> {
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
            .get(`${this.config.apiEndpoint}api/receivers?${queryStr}`)
            .map((response: any): GridDataResult => ({
                    data: response.items,
                    total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class ReceiverService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/receivers`;
    }

    public delete(entities: Receiver[]): Observable<boolean> {
        const removeCommand: ReceiverRemoveCommand = new ReceiverRemoveCommand(entities);

        return this.deleteRequestWithBody(this.api, removeCommand)
            .map((response: any): boolean => (response));
    }

    public get(id: number): Observable<Receiver> {
        return this.http.get(`${this.api}/${id}`).map((response: Receiver) => response);
    }

    public post(receiver: ReceiverRegisterCommand): Observable<number> {
        return this.http.post(`${this.api}`, receiver).map((response: number) => response);
    }

    public put(carrier: ReceiverUpdateCommand): Observable<boolean> {
        return this.http.put(`${this.api}`, carrier).map((response: boolean) => response);
    }

    public queryByName(filteredName: string): Observable<Receiver> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(name), tolower('${filteredName}'))`;

        return this.http
            .get(`${this.api}/?${queryStr}`)
            .map((response: any): Receiver => response.items);
    }
}

@Injectable()
export class ReceiverResolveService extends AbstractResolveService<Receiver> {

    constructor(
        public receiverService: ReceiverService,
        public breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'receiverId';
    }

    protected loadEntity(entityId: number): Observable<Receiver> {
        return this.receiverService
            .get(entityId)
            .take(1)
            .do((receiver: Receiver) => {
                this.breadcrumbService.setMetadata({
                    id: 'receiver',
                    label: receiver.name,
                    sizeLimit: true,
                });
            });
    }

}
