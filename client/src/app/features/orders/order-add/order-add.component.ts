import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { OrderService } from './../shared/order.service';
import { OrderRegisterCommand } from './../shared/order.model';

@Component({
    templateUrl: './order-add.component.html',
})
export class OrderAddComponent {

    public title: string = 'Adicionar ordem de compra';
    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        customer: ['', Validators.required],
        quantity: ['', Validators.required],
        product: new FormControl(null, [Validators.required]),
    });

    constructor(
        private fb: FormBuilder,
        private orderService: OrderService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const registerCommand: OrderRegisterCommand = new OrderRegisterCommand(formModel.value);

        this.orderService
            .post(registerCommand)
            .take(1)
            .subscribe((id: number) => {
                this.isLoading = false;
                if (id > 0) {
                    this.redirect();
                } else {
                    alert('Não foi possível cadastrar o registro');
                }
            });
    }

    public redirect(): void {
        this.router.navigate(['../'],  { relativeTo: this.route });
    }

}
