import { Component, Input, EventEmitter, Output, OnInit, OnDestroy, ViewChild, OnChanges, SimpleChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs/Subject';

import { DropDownListComponent } from '@progress/kendo-angular-dropdowns';

import { Product } from '../../../products/shared/product.model';
import { ProductService } from '../../../products/shared/product.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'ndd-invoice-item-form',
    templateUrl: './invoice-item-form.component.html',
})
export class InvoiceItemFormComponent implements OnInit, OnChanges, OnDestroy {

    @Input() public formModelItem: FormGroup;
    @Input() public hasSelectedItem: boolean;

    @Output() public submit: EventEmitter<any> = new EventEmitter<any>();
    @Output() public deleteItem: EventEmitter<void> = new EventEmitter<void>();

    @ViewChild(DropDownListComponent) public dropdown: DropDownListComponent;

    public productData: Product[] = [];

    private onProductFilterChange: Subject<string> = new Subject<string>();
    private ngUnsubscribeProduct: Subject<void> = new Subject<void>();

    constructor(public productService: ProductService) {}

    public ngOnInit(): void {
        const timeDelay: number = 300;

        this.onProductFilterChange
            .takeUntil(this.ngUnsubscribeProduct)
            .debounceTime(timeDelay)
            .switchMap((value: string) => this.productService.queryByDescription(value))
            .subscribe((response: any) => {
                this.productData = response;
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

    public ngOnChanges(changes: SimpleChanges): void {
        if (changes.formModelItem) {
            changes.formModelItem.currentValue.valueChanges
                .takeUntil(this.ngUnsubscribeProduct)
                .subscribe((value: any) => {
                    if (!value.product) {
                        this.dropdown.reset();
                    }
                }, (err: HttpErrorResponse) => {
                    alert(err.message);
                });
        }
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribeProduct.next();
        this.ngUnsubscribeProduct.complete();
    }

    public onProductAutoCompleteChange(value: string): void {
        this.onProductFilterChange.next(value);
    }

    public onSubmit(event: Event): void {
        event.stopPropagation();
        this.submit.emit(this.formModelItem.value);
    }

    public onDeleteItem(): void {
        this.deleteItem.emit();
    }

}
