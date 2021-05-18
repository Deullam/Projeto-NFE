import { Subject } from 'rxjs/Subject';
import { Component, Output, Input, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { ProductService } from './../../products/shared/product.service';
import { Product } from './../../products/shared/product.model';

@Component({
    selector: 'ndd-order-form',
    templateUrl: './order-form.component.html',
})
export class OrderFormComponent implements OnInit, OnDestroy {

    public data: Product[] = [];

    @Input() public formModel: FormGroup;
    @Output() public submit: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() public cancel: EventEmitter<void> = new EventEmitter<void>();

    private onFilterChange: Subject<string> = new Subject<string>();
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(public productService: ProductService) {}

    public ngOnInit(): void {
        const timeDelay: number = 300;

        this.onFilterChange
            .takeUntil(this.ngUnsubscribe)
            .debounceTime(timeDelay)
            .switchMap((value: string) => this.productService.queryByDescription(value))
            .subscribe((response: any) => {
                this.data = response;
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public onSubmit(event: Event): void {
        event.stopPropagation();
        this.submit.emit(this.formModel);
    }

    public onCancel(): void {
        this.cancel.emit();
    }

    public onAutoCompleteChange(value: string): void {
        this.onFilterChange.next(value);
    }

}
