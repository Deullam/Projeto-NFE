import { NDDBreadcrumbService } from './../../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.service';
import { Router } from '@angular/router';
import { SenderRemoveCommand, Sender, SenderRegisterCommand, SenderUpdateCommand } from './sender.model';
import { Observable } from 'rxjs/Observable';
import { State, toODataString } from '@progress/kendo-data-query';
import { CORE_CONFIG_TOKEN, ICoreConfig } from '../../../core/core.config';
import { HttpClient } from '@angular/common/http';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Injectable, Inject } from '@angular/core';
import { BaseService } from '../../../core/utils/base-service';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';

@Injectable()
export class SenderGridService extends BehaviorSubject<GridDataResult> {
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
            .get(`${this.config.apiEndpoint}api/senders?${queryStr}`)
            .map((response: any): GridDataResult => ({
                    data: response.items,
                    total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class SenderService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/senders`;
    }

    public delete(entities: Sender[]): Observable<boolean> {
        const removeCommand: SenderRemoveCommand = new SenderRemoveCommand(entities);

        return this.deleteRequestWithBody(this.api, removeCommand)
            .map((response: any): boolean => (response));
    }

    public get(id: number): Observable<Sender> {
        return this.http.get(`${this.api}/${id}`).map((response: Sender) => response);
    }

    public post(sender: SenderRegisterCommand): Observable<number> {
        return this.http.post(`${this.api}`, sender).map((response: number) => response);
    }

    public put(sender: SenderUpdateCommand): Observable<boolean> {
        return this.http.put(`${this.api}`, sender).map((response: boolean) => response);
    }

    public queryByFancyName(filteredName: string): Observable<Sender> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(fancyName), tolower('${filteredName}'))`;

        return this.http
            .get(`${this.api}/?${queryStr}`)
            .map((response: any): Sender => response.items);
    }
}

@Injectable()
export class SenderResolveService extends AbstractResolveService<Sender> {

    constructor(
        public senderService: SenderService,
        public breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'senderId';
    }

    protected loadEntity(entityId: number): Observable<Sender> {
        return this.senderService
            .get(entityId)
            .take(1)
            .do((sender: Sender) => {
                this.breadcrumbService.setMetadata({
                    id: 'sender',
                    label: sender.companyName,
                    sizeLimit: true,
                });
            });
    }

}
