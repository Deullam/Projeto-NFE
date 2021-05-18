import { InvoiceItemService } from './../../shared/invoice-item.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { GridUtilsComponent } from '../../../../shared/grid-utils/grid-utils-component';

import { Invoice } from '../../shared/invoice.model';
import { InvoiceResolveService } from '../../shared/invoice.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { InvoiceItemRegisterCommand, InvoiceItemUpdateCommand } from '../../shared/invoice-item.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './invoice-itens.component.html',
})

export class InvoiceItensComponent extends GridUtilsComponent implements OnInit, OnDestroy {

    public isLoading: boolean = false;
    public invoice: Invoice;
    public index: number;

    public invoiceItemFormGroup: FormGroup;

    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private fb: FormBuilder,
        private resolver: InvoiceResolveService,
        private invoiceItemService: InvoiceItemService) {
        super();
    }

    public ngOnInit(): void {
        this.invoiceItemFormGroup = this.createEmptyInvoiceItem();
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

    public createEmptyInvoiceItem(): FormGroup {
        return this.fb.group({
            product: new FormControl(null, [Validators.required]),
            amount: ['', [Validators.required]],
        });
    }

    public resetInvoiceItem(): void {
        this.invoiceItemFormGroup.reset();
        this.invoiceItemFormGroup.updateValueAndValidity();

        this.index = -1;
    }

    public onSaveProduct(value: any): void {
        if (this.index > -1) {
            this.updateItem();
        } else {
            this.addItem();
        }
    }

    public onDeleteItem(): void {
        if (this.index > -1) {

            this.invoiceItemService.delete(this.invoice.id, [this.invoice.invoiceItems[this.index]])
            .take(1)
            .do(() => this.isLoading = true)
            .subscribe((result: boolean) => {
                this.isLoading = false;

                if (result) {
                    this.resetInvoiceItem();
                    this.resolver.resolveFromRouteAndNotify();
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
        }

        this.resetInvoiceItem();

        this.index = -1;
    }

    public onSelectedItem(index: number): void {
        if (index < 0) {
            if (this.index > -1) {
                this.resetInvoiceItem();
            }
        } else {
            this.invoiceItemFormGroup.setValue({
                product: this.invoice.invoiceItems[index].product,
                amount: this.invoice.invoiceItems[index].amount,
            });
        }

        this.index = index;
    }

    private addItem(): void {
        this.isLoading = true;

        const registerCommand: InvoiceItemRegisterCommand =
                new InvoiceItemRegisterCommand(this.invoiceItemFormGroup.value);

        this.invoiceItemService
            .post(this.invoice.id, [registerCommand])
            .take(1)
            .subscribe((ids: number[]) => {
                this.isLoading = false;

                if (ids.length >= 0) {
                    this.resetInvoiceItem();
                    this.resolver.resolveFromRouteAndNotify();
                } else {
                    alert('Não foi possível criar o registro');
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    private updateItem(): void {
        this.isLoading = true;

        const updateCommand: InvoiceItemUpdateCommand =
                new InvoiceItemUpdateCommand(this.invoice.invoiceItems[this.index].id,
                    this.invoiceItemFormGroup.value);

        this.invoiceItemService
            .put(this.invoice.id, [updateCommand])
            .take(1)
            .subscribe((success: boolean) => {
                this.isLoading = false;

                if (success) {
                    this.resetInvoiceItem();
                    this.resolver.resolveFromRouteAndNotify();
                } else {
                    alert('Não foi possível atualizar o registro');
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }
}
