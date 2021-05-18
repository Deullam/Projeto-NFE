import { Component, OnDestroy, OnInit } from '@angular/core';
import { Product } from '../shared/product.model';
import { Subject } from 'rxjs/Subject';
import { ProductResolveService } from '../shared/product.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './product-view.component.html',
})
export class ProductViewComponent implements OnInit, OnDestroy {

    public product: Product;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: ProductResolveService) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((product: Product) => {
                this.product = product;
                this.createProperty();
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    private createProperty(): void {
        this.title = this.product.code;
        const codeDescription: string = this.product.code;
        const descriptionDesc: string = this.product.description;
        const currentValueDescription: number = this.product.currentValue;

        this.infoItems = [
            {
                value: codeDescription,
                description: codeDescription,
            },
            {
                value: descriptionDesc,
                description: descriptionDesc,
            },
            {
                value: currentValueDescription,
                description: currentValueDescription,
            },
        ];
    }

}
