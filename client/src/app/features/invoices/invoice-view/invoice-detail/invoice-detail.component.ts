import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { InvoiceResolveService } from './../../shared/invoice.service';
import { Invoice } from '../../shared/invoice.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './invoice-detail.component.html',
})
export class InvoiceDetailComponent implements OnInit, OnDestroy {

    public isLoading: boolean = false;
    public invoice: Invoice;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: InvoiceResolveService,
        private router: Router,
        private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((invoice: Invoice) => {
                this.isLoading = false;
                this.invoice = Object.assign(new Invoice(), invoice);
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public formatDate(dateString: string): string {

        if (dateString !== null && dateString !== '') {
            return new Date(dateString).toISOString();
        }

        return null;
    }

    public toStringStatus(status: string): string {
        if (status === null) {
            return '';
        }
        if (status.toString() === '0') {
            return 'Aberta';
        }

        return 'Emitida';
    }

    public toStringKeyAccess(keyAccess: string): string {

        if (keyAccess === null) {
            return 'chave vazia - nota em aberto';
        }

        return keyAccess;
    }

    public toStringNumber(numeral: number): string {

        return numeral.toString();
    }

    public onEdit(): void {
        this.router.navigate(['./editar'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['/notas'], { relativeTo: this.route });
    }
}
