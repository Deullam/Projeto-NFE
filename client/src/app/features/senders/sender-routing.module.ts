import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SenderAddComponent } from './sender-add/sender-add.component';
import { SenderEditComponent } from './sender-view/sender-edit/sender-edit.component';
import { SenderResolveService } from './shared/sender.service';
import { SenderDetailComponent } from './sender-view/sender-detail/sender-detail.component';
import { SenderViewComponent } from './sender-view/sender-view.component';
import { SenderListComponent } from './sender-list/sender-list.component';

const senderRoutes: Routes = [
    {
        path: '',
        component: SenderListComponent,
    },
    {
        path: 'adicionar',
        component: SenderAddComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Adicionar',
            },
        },
    },
    {
        path: ':senderId',
        resolve: {
            sender: SenderResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbId: 'sender',
            },
        },
        children: [
            {
                path: '',
                component: SenderViewComponent,
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
                                component: SenderDetailComponent,
                            },
                            {
                                path: 'editar',
                                component: SenderEditComponent,
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
    imports: [RouterModule.forChild(senderRoutes)],
    exports: [RouterModule],
    providers: [],
})
export class SenderRoutingModule{}
