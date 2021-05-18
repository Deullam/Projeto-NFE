import { HttpErrorResponse } from '@angular/common/http';
import { OrderService } from './../shared/order.service';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';
import { Component } from '@angular/core';
import { OrderGridService } from '../shared/order.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { Order } from '../shared/order.model';

@Component({
    templateUrl: './order-list.component.html',
})
export class OrderListComponent extends GridUtilsComponent {

    constructor(
        public orderGridService: OrderGridService,
        public orderService: OrderService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.orderGridService.query(this.createFormattedState());
    }

    public  onClick(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }

    public redirectOpenOrder(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.orderGridService.query(state);
    }

    public deleteOrder(): void {
        this.orderGridService.loading = true;

        const orders: Order[] = this.getSelectedEntities();

        this.orderService.delete(orders)
            .take(1)
            .do(() => this.orderGridService.loading = false)
            .subscribe((result: boolean) => {
                if (result) {
                    this.selectedRows = [];
                    this.orderGridService.query(this.state);
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

}
