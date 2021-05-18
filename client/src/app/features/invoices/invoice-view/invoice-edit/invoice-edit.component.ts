import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';
import { Subject } from 'rxjs/Subject';
import { Component, OnInit } from '@angular/core';

import { Invoice, InvoiceUpdateCommand } from './../../shared/invoice.model';
import { InvoiceService, InvoiceResolveService } from './../../shared/invoice.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './invoice-edit.component.html',
})

export class InvoiceEditComponent implements OnInit {

    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        natureOperation: ['', Validators.required],
        number: ['', Validators.required],
        receiver: new FormControl(null, [Validators.required]),
        carrier: new FormControl(null, [Validators.required]),
        sender: new FormControl(null, [Validators.required]),
    }, {});

    private invoice: Invoice;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private fb: FormBuilder,
        private invoiceService: InvoiceService,
        private resolver: InvoiceResolveService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public ngOnInit(): void {
        this.isLoading = true;

        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((invoice: Invoice) => {
                this.invoice = Object.assign(new Invoice(), invoice);

                this.form.setValue({
                    number: this.invoice.number,
                    natureOperation: this.invoice.natureOperation,
                    receiver: this.invoice.receiver,
                    carrier: this.invoice.carrier,
                    sender: this.invoice.sender,
                });

                this.isLoading = false;
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const updateCommand: InvoiceUpdateCommand = new InvoiceUpdateCommand(this.invoice.id, formModel.value);

        this.invoiceService
            .put(updateCommand)
            .take(1)
            .subscribe((success: boolean) => {
                this.isLoading = false;

                if (success) {
                    this.resolver.resolveFromRouteAndNotify();
                    this.redirect();
                } else {
                    alert('Não foi possível atualizar o registro');
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public redirect(): void {
        this.router.navigate(['../'],  { relativeTo: this.route });
    }

}
