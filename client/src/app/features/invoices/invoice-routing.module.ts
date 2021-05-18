import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { InvoiceListComponent } from './invoice-list/invoice-list.component';
import { InvoiceAddComponent } from './invoice-add/invoice-add.component';
import { InvoiceResolveService } from './shared/invoice.service';
import { InvoiceViewComponent } from './invoice-view/invoice-view.component';
import { InvoiceDetailComponent } from './invoice-view/invoice-detail/invoice-detail.component';
import { InvoiceEditComponent } from './invoice-view/invoice-edit/invoice-edit.component';
import { InvoiceItensComponent } from './invoice-view/invoice-itens/invoice-itens.component';

const invoicesRoutes: Routes = [
    {
        path: '',
        component: InvoiceListComponent,
    },
    {
        path: 'adicionar',
        component: InvoiceAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: ':invoiceId',
        resolve: {
            carrier: InvoiceResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'invoice',
            },
        },
        children: [
            {
                path: '',
                component: InvoiceViewComponent,
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
                                component: InvoiceDetailComponent,
                            },
                            {
                                path: 'editar',
                                component: InvoiceEditComponent,
                                data: {
                                    breadcrumbOptions: {
                                        breadcrumbLabel: 'Editar',
                                    },
                                },
                            },
                        ],
                    },
                    {
                        path: 'itens',
                        children: [
                            {
                                path: '',
                                component: InvoiceItensComponent,
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
    imports: [RouterModule.forChild(invoicesRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class InvoiceRoutingModule{}
