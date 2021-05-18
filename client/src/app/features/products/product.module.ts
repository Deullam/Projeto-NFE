import { NDDTitlebarModule } from './../../shared/ndd-ng-titlebar/component/ndd-ng-titlebar.module';
import { NDDTabsbarModule } from './../../shared/ndd-ng-tabsbar/component/ndd-ng-tabsbar.module';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';

import { GridModule } from '@progress/kendo-angular-grid';
import { UiSwitchModule } from 'angular2-ui-switch';

import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductResolveService } from './shared/product.service';
import { ProductViewComponent } from './product-view/product-view.component';
import { ProductDetailComponent } from './product-view/product-detail/product-detail.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductAddComponent } from './product-add/product-add.component';
import { ProductEditComponent } from './product-view/product-edit/product-edit.component';
import { ProductSharedModule } from './shared/product-shared.module';
import { AddressModule } from '../addresses/address.module';

@NgModule({
    declarations: [
        ProductListComponent,
        ProductDetailComponent,
        ProductViewComponent,
        ProductFormComponent,
        ProductEditComponent,
        ProductAddComponent,
    ],
    imports: [
        ProductRoutingModule,
        SharedModule,
        GridModule,
        NDDTabsbarModule,
        NDDTitlebarModule,
        UiSwitchModule,
        ProductSharedModule,
        AddressModule,
    ],
    exports: [],
    providers: [ProductResolveService],
})
export class ProductModule {}
