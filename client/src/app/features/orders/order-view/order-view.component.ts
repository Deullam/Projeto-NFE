import { Order } from './../shared/order.model';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { OrderResolveService } from '../shared/order.service';

@Component({
    templateUrl: './order-view.component.html',
})
export class OrderViewComponent implements OnInit, OnDestroy {

    public order: Order;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: OrderResolveService) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((order: Order) => {
                this.order = order;
                this.createProperty();
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    private createProperty(): void {
        this.title = this.order.id.toString();
        const productDescription: string = this.order.productName;
        const customerDescription: string = this.order.customer;

        this.infoItems = [
            {
                value: customerDescription,
                description: customerDescription,
            },
            {
                value: productDescription,
                description: productDescription,
            },
        ];
    }

}
