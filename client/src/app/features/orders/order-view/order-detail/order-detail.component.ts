import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { OrderResolveService } from './../../shared/order.service';
import { Order } from '../../shared/order.model';

@Component({
    templateUrl: './order-detail.component.html',
})
export class OrderDetailComponent implements OnInit, OnDestroy {

    public isLoading: boolean = false;
    public order: Order;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: OrderResolveService,
        private router: Router,
        private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((order: Order) => {
                this.isLoading = false;
                this.order = Object.assign(new Order(), order);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public onEdit(): void {
        this.router.navigate(['./editar'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['/ordens'], { relativeTo: this.route });
    }

}
