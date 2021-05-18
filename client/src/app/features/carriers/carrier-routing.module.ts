import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CarrierAddComponent } from './carrier-add/carrier-add.component';
import { CarrierEditComponent } from './carrier-view/carrier-edit/carrier-edit.component';
import { CarrierResolveService } from './shared/carrier.service';
import { CarrierDetailComponent } from './carrier-view/carrier-detail/carrier-detail.component';
import { CarrierViewComponent } from './carrier-view/carrier-view.component';
import { CarrierListComponent } from './carrier-list/carrier-list.component';

const carrierRoutes: Routes = [
    {
        path: '',
        component: CarrierListComponent,
    },
    {
        path: 'adicionar',
        component: CarrierAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: ':carrierId',
        resolve: {
            carrier: CarrierResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'carrier',
            },
        },
        children: [
            {
                path: '',
                component: CarrierViewComponent,
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
                                component: CarrierDetailComponent,
                            },
                            {
                                path: 'editar',
                                component: CarrierEditComponent,
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
    imports: [RouterModule.forChild(carrierRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class CarrierRoutingModule{}
