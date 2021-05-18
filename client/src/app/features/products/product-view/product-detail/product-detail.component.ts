import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { ProductResolveService } from './../../shared/product.service';
import { Product } from '../../shared/product.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent implements OnInit, OnDestroy {

    public code: string;
    public description: string;
    public currentValue: number;
    public isLoading: boolean = false;

    private product: Product;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: ProductResolveService,
        private router: Router,
        private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((product: Product) => {
                this.isLoading = false;
                this.product = Object.assign(new Product(), product);
                this.code = this.product.code;
                this.description = this.product.description;
                this.currentValue = this.product.currentValue;
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
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
        this.router.navigate(['/produtos'], { relativeTo: this.route });
    }

}
