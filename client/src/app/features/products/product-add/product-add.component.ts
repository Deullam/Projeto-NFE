import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { ProductService } from './../shared/product.service';
import { ProductRegisterCommand } from './../shared/product.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './product-add.component.html',
})
export class ProductAddComponent {

    private static CODE_MAX_LENGTH_STATE: number = 14;
    private static DESCRIPTION_MAX_LENGTH_STATE: number = 60;
    private static CURRENT_VALUE_MIN_VALUE_STATE: number = 1;
    public title: string = 'Adicionar produto';
    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        code: ['', [Validators.required, Validators.maxLength(ProductAddComponent.CODE_MAX_LENGTH_STATE)]],
        description: ['', [Validators.required, Validators.maxLength(ProductAddComponent.DESCRIPTION_MAX_LENGTH_STATE)]],
        currentValue: ['', [Validators.required, Validators.min(ProductAddComponent.CURRENT_VALUE_MIN_VALUE_STATE)]],

    }, {});

    constructor(
        private fb: FormBuilder,
        private productService: ProductService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const registerCommand: ProductRegisterCommand = new ProductRegisterCommand(formModel.value);

        this.productService
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

    public redirect(): void {
        this.router.navigate(['../'],  { relativeTo: this.route });
    }

}
