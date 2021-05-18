import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Product, ProductUpdateCommand } from '../../shared/product.model';
import { ProductResolveService, ProductService } from './../../shared/product.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './product-edit.component.html',
})
export class ProductEditComponent implements OnInit {
    private static CODE_MAX_LENGTH_STATE: number = 14;
    private static DESCRIPTION_MAX_LENGTH_STATE: number = 60;
    private static CURRENT_VALUE_MIN_VALUE_STATE: number = 1;
    public isLoading: boolean = false;

    private product: Product;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    private form: FormGroup = this.fb.group({
        code: ['', [Validators.required, Validators.maxLength(ProductEditComponent.CODE_MAX_LENGTH_STATE)]],
        description: ['', [Validators.required, Validators.maxLength(ProductEditComponent.DESCRIPTION_MAX_LENGTH_STATE)]],
        currentValue: ['', [Validators.required, Validators.min(ProductEditComponent.CURRENT_VALUE_MIN_VALUE_STATE)]],
    });

    constructor(
        private fb: FormBuilder,
        private productService: ProductService,
        private resolver: ProductResolveService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((product: Product) => {
                this.product = Object.assign(new Product(), product);
                this.form.setValue({
                    code: this.product.code,
                    description: this.product.description,
                    currentValue: this.product.currentValue,
                });
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const updateCommand: ProductUpdateCommand = new ProductUpdateCommand(this.product.id, formModel.value);

        this.productService
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
