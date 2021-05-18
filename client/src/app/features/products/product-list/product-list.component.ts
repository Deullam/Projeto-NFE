import { HttpErrorResponse } from '@angular/common/http';
import { ProductService } from './../shared/product.service';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';
import { Component } from '@angular/core';
import { ProductGridService } from '../shared/product.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { Product } from '../shared/product.model';

@Component({
    templateUrl: './product-list.component.html',
})
export class ProductListComponent extends GridUtilsComponent {

    constructor(
        public productGridService: ProductGridService,
        public productService: ProductService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.productGridService.query(this.createFormattedState());
    }

    public  onClick(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }

    public redirectOpenProduct(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.productGridService.query(state);
    }

    public deleteProduct(): void {
        this.productGridService.loading = true;

        const products: Product[] = this.getSelectedEntities();

        this.productService.delete(products)
            .take(1)
            .do(() => this.productGridService.loading = false)
            .subscribe((result: boolean) => {
                if (result) {
                    this.selectedRows = [];
                    this.productGridService.query(this.state);
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

}
