import { NDDBreadcrumbService } from './../../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.service';
import { Router } from '@angular/router';
import { ProductRemoveCommand, Product, ProductRegisterCommand, ProductUpdateCommand } from './product.model';
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
export class ProductGridService extends BehaviorSubject<GridDataResult> {
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
            .get(`${this.config.apiEndpoint}api/products?${queryStr}`)
            .map((response: any): GridDataResult => ({
                    data: response.items,
                    total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}

@Injectable()
export class ProductService extends BaseService {

    private api: string;

    constructor(@Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);
        this.api = `${config.apiEndpoint}api/products`;
    }

    public delete(entities: Product[]): Observable<boolean> {
        const removeCommand: ProductRemoveCommand = new ProductRemoveCommand(entities);

        return this.deleteRequestWithBody(this.api, removeCommand)
            .map((response: any): boolean => (response));
    }

    public get(id: number): Observable<Product> {
        return this.http.get(`${this.api}/${id}`).map((response: Product) => response);
    }

    public post(product: ProductRegisterCommand): Observable<number> {
        return this.http.post(`${this.api}`, product).map((response: number) => response);
    }

    public put(product: ProductUpdateCommand): Observable<boolean> {
        return this.http.put(`${this.api}`, product).map((response: boolean) => response);
    }

    public queryByDescription(filteredName: string): Observable<Product> {
        const queryStr: string = `$skip=0&$count=true&$filter=contains(tolower(description), tolower('${filteredName}'))`;

        return this.http
            .get(`${this.api}/?${queryStr}`)
            .map((response: any): Product => response.items);
    }
}

@Injectable()
export class ProductResolveService extends AbstractResolveService<Product> {

    constructor(
        public productService: ProductService,
        public breadcrumbService: NDDBreadcrumbService,
        router: Router) {
        super(router);
        this.paramsProperty = 'productId';
    }

    protected loadEntity(entityId: number): Observable<Product> {
        return this.productService
            .get(entityId)
            .take(1)
            .do((product: Product) => {
                this.breadcrumbService.setMetadata({
                    id: 'product',
                    label: product.code,
                    sizeLimit: true,
                });
            });
    }

}
