import { ReceiverDetailComponent } from './receiver-view/receiver-detail/receiver-detail.component';
import { ReceiverViewComponent } from './receiver-view/receiver-view.component';
import { ReceiverEditComponent } from './receiver-view/receiver-edit/receiver-edit.component';
import { ReceiverListComponent } from './receiver-list/receiver-list.component';
import { ReceiverAddComponent } from './receiver-add/receiver-add-component';
import { ReceiverResolveService } from './shared/receiver.service';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

const carrierRoutes: Routes = [
    {
        path: '',
        component: ReceiverListComponent,
    },
    {
        path: 'adicionar',
        component: ReceiverAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: ':receiverId',
        resolve: {
            carrier: ReceiverResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'receiver',
            },
        },
        children: [
            {
                path: '',
                component: ReceiverViewComponent,
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
                                component: ReceiverDetailComponent,
                            },
                            {
                                path: 'editar',
                                component: ReceiverEditComponent,
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
export class ReceiverRoutingModule{}
