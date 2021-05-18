import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { Order, OrderUpdateCommand } from '../../shared/order.model';
import { OrderResolveService, OrderService } from './../../shared/order.service';

@Component({
    templateUrl: './order-edit.component.html',
})
export class OrderEditComponent implements OnInit {

    public isLoading: boolean = false;
    private order: Order;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    private form: FormGroup = this.fb.group({
        customer: ['', Validators.required],
        quantity: ['', Validators.required],
        product: new FormControl(null, [Validators.required]),
    });

    constructor(
        private fb: FormBuilder,
        private orderService: OrderService,
        private resolver: OrderResolveService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((order: Order) => {
                this.order = Object.assign(new Order(), order);
                this.form.setValue({
                    customer: this.order.customer,
                    quantity: this.order.quantity,
                    product: {
                        id: this.order.id,
                        name: this.order.productName,
                    },
                });
            });
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const updateCommand: OrderUpdateCommand = new OrderUpdateCommand(this.order.id, formModel.value);

        this.orderService
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
            });
    }

    public redirect(): void {
        this.router.navigate(['../'],  { relativeTo: this.route });
    }

}
