import { NDDBreadcrumbService } from './../../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.service';
import { Router } from '@angular/router';
import { CarrierRemoveCommand, Carrier, CarrierRegisterCommand, CarrierUpdateCommand } from './carrier.model';
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
export class CarrierGridService extends BehaviorSubject<GridDataResult> {
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
            .get(`${this.config.apiEndpoint}api/carriers?${queryStr}`)
            .map((response: any): GridDataResult => ({
                    data: response.items,
                    total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class CarrierService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/carriers`;
    }

    public delete(entities: Carrier[]): Observable<boolean> {
        const removeCommand: CarrierRemoveCommand = new CarrierRemoveCommand(entities);

        return this.deleteRequestWithBody(this.api, removeCommand)
            .map((response: any): boolean => (response));
    }

    public get(id: number): Observable<Carrier> {
        return this.http.get(`${this.api}/${id}`).map((response: Carrier) => response);
    }

    public post(carrier: CarrierRegisterCommand): Observable<number> {
        return this.http.post(`${this.api}`, carrier).map((response: number) => response);
    }

    public put(carrier: CarrierUpdateCommand): Observable<boolean> {
        return this.http.put(`${this.api}`, carrier).map((response: boolean) => response);
    }

    public queryByName(filteredName: string): Observable<Carrier> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(name), tolower('${filteredName}'))`;

        return this.http
            .get(`${this.api}/?${queryStr}`)
            .map((response: any): Carrier => response.items);
    }
}

@Injectable()
export class CarrierResolveService extends AbstractResolveService<Carrier> {

    constructor(
        public carrierService: CarrierService,
        public breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'carrierId';
    }

    protected loadEntity(entityId: number): Observable<Carrier> {
        return this.carrierService
            .get(entityId)
            .take(1)
            .do((carrier: Carrier) => {
                this.breadcrumbService.setMetadata({
                    id: 'carrier',
                    label: carrier.name,
                    sizeLimit: true,
                });
            });
    }

}
