import { NDDTitlebarModule } from './../../shared/ndd-ng-titlebar/component/ndd-ng-titlebar.module';
import { NDDTabsbarModule } from './../../shared/ndd-ng-tabsbar/component/ndd-ng-tabsbar.module';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';

import { GridModule } from '@progress/kendo-angular-grid';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

import { OrderRoutingModule } from './order-routing.module';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderGridService, OrderService, OrderResolveService } from './shared/order.service';
import { OrderViewComponent } from './order-view/order-view.component';
import { OrderDetailComponent } from './order-view/order-detail/order-detail.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { OrderAddComponent } from './order-add/order-add.component';
import { OrderEditComponent } from './order-view/order-edit/order-edit.component';
import { ProductSharedModule } from '../products/shared/product-shared.module';

@NgModule({
    declarations: [
        OrderListComponent,
        OrderDetailComponent,
        OrderViewComponent,
        OrderFormComponent,
        OrderEditComponent,
        OrderAddComponent,
    ],
    imports: [
        OrderRoutingModule,
        SharedModule,
        GridModule,
        NDDTabsbarModule,
        NDDTitlebarModule,
        DropDownsModule,
        ProductSharedModule,
    ],
    exports: [],
    providers: [OrderGridService, OrderService, OrderResolveService],
})
export class OrderModule {}
