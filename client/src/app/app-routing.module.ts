import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './core/layout/layout.component';

const appRoutes: Routes = [
    {
        path: 'page-not-found',
        loadChildren: './features/error-pages/page-not-found/page-not-found.module#PageNotFoundModule',
    },
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: '',
                redirectTo: 'notas',
                pathMatch: 'full',
            },
            {
                path: 'produtos',
                loadChildren: './features/products/product.module.ts#ProductModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Produtos',
                    },
                },
            },
            {
                path: 'notas',
                loadChildren: './features/invoices/invoice.module.ts#InvoiceModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Notas',
                    },
                },
            },
            {
                path: 'destinatarios',
                loadChildren: './features/receivers/receiver.module.ts#ReceiverModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Destinatários',
                    },
                },
            },
            {
                path: 'emitentes',
                loadChildren: './features/senders/sender.module.ts#SenderModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Emitentes',
                    },
                },
            },
            {
                path: 'transportadores',
                loadChildren: './features/carriers/carrier.module.ts#CarrierModule',
                data: {
                    breadcrumbOptions: {
                        breadcrumbLabel: 'Transportadores',
                    },
                },
            },
        ],
    },
    { path: '**', redirectTo: 'page-not-found', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(appRoutes, {
        paramsInheritanceStrategy: 'always',
    })],
    exports: [RouterModule],
})
export class AppRoutingModule { }
