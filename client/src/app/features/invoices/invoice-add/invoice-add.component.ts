import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormBuilder, FormControl, FormArray } from '@angular/forms';
import { Component } from '@angular/core';

import { InvoiceValidators } from '../shared/invoice.validator';
import { InvoiceRegisterCommand } from './../shared/invoice.model';
import { InvoiceService } from './../shared/invoice.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './invoice-add.component.html',
})

export class InvoiceAddComponent implements OnInit {

    public title: string = 'Adicionar Nota Fiscal';
    public isLoading: boolean = false;

    public invoiceItemFormGroup: FormGroup;

    public form: FormGroup = this.fb.group({
        natureOperation: ['', Validators.required],
        number: ['', Validators.required],
        receiver: new FormControl(null, [Validators.required]),
        carrier: new FormControl(null, [Validators.required]),
        sender: new FormControl(null, [Validators.required]),
        invoiceItems: this.fb.array([], InvoiceValidators.minLengthArray(1)),
    }, {});

    private index: number = -1;

    constructor(
        private fb: FormBuilder,
        private invoiceService: InvoiceService,
        private router: Router,
        private route: ActivatedRoute,
    ) { }

    public ngOnInit(): void {
        this.invoiceItemFormGroup = this.createEmptyInvoiceItem();
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const registerCommand: InvoiceRegisterCommand = new InvoiceRegisterCommand(formModel.value);

        this.invoiceService
            .post(registerCommand)
            .take(1)
            .subscribe((id: number) => {
                this.isLoading = false;

                if (id > 0) {
                    this.redirect();
                } else {
                    alert('Não foi possível cadastrar o registro');
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public createEmptyInvoiceItem(): FormGroup {
        return this.fb.group({
            product: new FormControl(null, [Validators.required]),
            amount: ['', [Validators.required]],
        });
    }

    public createInvoiceItem(value: any): FormGroup {
        return this.fb.group({
            product: new FormControl(value.product, [Validators.required]),
            amount: [value.amount, [Validators.required]],
        });
    }

    public resetInvoiceItem(): void {
        this.invoiceItemFormGroup.reset();
        this.invoiceItemFormGroup.updateValueAndValidity();

        this.index = -1;

        this.form.get('invoiceItems').updateValueAndValidity();
    }

    public onSaveProduct(value: any): void {
        const items: FormArray = this.form.get('invoiceItems') as FormArray;

        if (this.index > -1) {
            items.controls[this.index] = this.createInvoiceItem(value);
        } else {
            items.push(this.createInvoiceItem(value));
        }

        this.resetInvoiceItem();
        this.index = -1;
    }

    public onDeleteItem(): void {
        const items: FormArray = this.form.get('invoiceItems') as FormArray;

        if (this.index > -1) {
            items.removeAt(this.index);
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
            const items: FormArray = this.form.get('invoiceItems') as FormArray;
            this.invoiceItemFormGroup.setValue(items.controls[index].value);
        }

        this.index = index;
    }

    public redirect(): void {
        this.router.navigate(['../'], { relativeTo: this.route });
    }

}
