import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProductAddComponent } from './product-add/product-add.component';
import { ProductEditComponent } from './product-view/product-edit/product-edit.component';
import { ProductResolveService } from './shared/product.service';
import { ProductDetailComponent } from './product-view/product-detail/product-detail.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductListComponent } from './product-list/product-list.component';

const productRoutes: Routes = [
    {
        path: '',
        component: ProductListComponent,
    },
    {
        path: 'adicionar',
        component: ProductAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: ':productId',
        resolve: {
            product: ProductResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'product',
            },
        },
        children: [
            {
                path: '',
                component: ProductViewComponent,
                children: [
                    {
                        path: '',
                        redirectTo: 'info',
                        pathMatch: 'full',
                    },
                    {
                        path: 'info',
                        children: [
                            {
                                path: '',
                                component: ProductDetailComponent,
                            },
                            {
                                path: 'editar',
                                component: ProductEditComponent,
                                data: {
                                    breadcrumbOptions: {
                                        breadcrumbLabel: 'Editar',
                                    },
                                },
                            },
                        ],
                    },
                ],
            },
        ],
    },
];

@NgModule({
    declarations: [],
    imports: [RouterModule.forChild(productRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class ProductRoutingModule{}
