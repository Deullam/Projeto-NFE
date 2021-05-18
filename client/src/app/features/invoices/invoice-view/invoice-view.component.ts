import { Invoice } from './../shared/invoice.model';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { InvoiceResolveService } from '../shared/invoice.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './invoice-view.component.html',
})
export class InvoiceViewComponent implements OnInit, OnDestroy {

    public invoice: Invoice;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: InvoiceResolveService) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((invoice: Invoice) => {
                this.invoice = invoice;
                this.createProperty();
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public getStatusInvoice(value: string): string {
        if (value.toString() === '0') {
            return 'Aberta';
        }

        return 'Emitida';
    }

    private createProperty(): void {
        this.title = this.invoice.id.toString();
        const natureOperationDescription: string = this.invoice.natureOperation;
        const statusDescription: string = this.getStatusInvoice(this.invoice.status);

        this.infoItems = [
            {
                value: statusDescription,
                description: statusDescription,
            },
            {
                value: natureOperationDescription,
                description: natureOperationDescription,
            },
        ];
    }

}
