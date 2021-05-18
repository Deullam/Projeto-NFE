import { HttpErrorResponse } from '@angular/common/http';
import { Invoice } from './../shared/invoice.model';
import { SelectionEvent, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Router, ActivatedRoute } from '@angular/router';
import { InvoiceGridService, InvoiceService } from './../shared/invoice.service';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';
import { Component } from '@angular/core';

@Component({
    templateUrl: './invoice-list.component.html',
})

export class InvoiceListComponent extends GridUtilsComponent {

    constructor(
        public invoiceGridService: InvoiceGridService,
        public invoiceService: InvoiceService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.invoiceGridService.query(this.createFormattedState());
    }

    public  onClick(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }

    public redirectOpenInvoice(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.invoiceGridService.query(state);
    }

    public formatDate(dateString: string): string {

        if (dateString !== null && dateString !== '') {
            return new Date(dateString).toISOString();
        }

        return null;
    }

    public getStatusInvoice(value: string): string {
        if (value === null) {
            return '';
        }
        if (value.toString() === '0') {
            return 'Aberta';
        }

        return 'Emitida';
    }

    public deleteInvoice(): void {
        this.invoiceGridService.loading = true;

        const invoices: Invoice[] = this.getSelectedEntities();

        this.invoiceService.delete(invoices)
            .take(1)
            .do(() => this.invoiceGridService.loading = false)
            .subscribe((result: boolean) => {
                if (result) {
                    this.selectedRows = [];
                    this.invoiceGridService.query(this.state);
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

}
