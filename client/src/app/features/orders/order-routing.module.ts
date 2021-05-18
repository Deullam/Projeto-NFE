import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { OrderAddComponent } from './order-add/order-add.component';
import { OrderEditComponent } from './order-view/order-edit/order-edit.component';
import { OrderResolveService } from './shared/order.service';
import { OrderDetailComponent } from './order-view/order-detail/order-detail.component';
import { OrderViewComponent } from './order-view/order-view.component';
import { OrderListComponent } from './order-list/order-list.component';

const orderRoutes: Routes = [
    {
        path: '',
        component: OrderListComponent,
    },
    {
        path: 'adicionar',
        component: OrderAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: ':orderId',
        resolve: {
            order: OrderResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'order',
            },
        },
        children: [
            {
                path: '',
                component: OrderViewComponent,
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
                                component: OrderDetailComponent,
                            },
                            {
                                path: 'editar',
                                component: OrderEditComponent,
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
    imports: [RouterModule.forChild(orderRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class OrderRoutingModule{}
