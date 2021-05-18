import { NDDTitlebarModule } from './../../shared/ndd-ng-titlebar/component/ndd-ng-titlebar.module';
import { NDDTabsbarModule } from './../../shared/ndd-ng-tabsbar/component/ndd-ng-tabsbar.module';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';

import { GridModule } from '@progress/kendo-angular-grid';
import { TextMaskModule } from 'angular2-text-mask';

import { CarrierRoutingModule } from './carrier-routing.module';
import { CarrierListComponent } from './carrier-list/carrier-list.component';
import { CarrierResolveService } from './shared/carrier.service';
import { CarrierViewComponent } from './carrier-view/carrier-view.component';
import { CarrierDetailComponent } from './carrier-view/carrier-detail/carrier-detail.component';
import { CarrierFormComponent } from './carrier-form/carrier-form.component';
import { CarrierAddComponent } from './carrier-add/carrier-add.component';
import { CarrierEditComponent } from './carrier-view/carrier-edit/carrier-edit.component';
import { CarrierSharedModule } from './shared/carrier-shared.module';
import { AddressModule } from '../addresses/address.module';

@NgModule({
    declarations: [
        CarrierListComponent,
        CarrierDetailComponent,
        CarrierViewComponent,
        CarrierFormComponent,
        CarrierEditComponent,
        CarrierAddComponent,
    ],
    imports: [
        CarrierRoutingModule,
        SharedModule,
        GridModule,
        NDDTabsbarModule,
        NDDTitlebarModule,
        CarrierSharedModule,
        AddressModule,
        TextMaskModule,
    ],
    exports: [],
    providers: [CarrierResolveService],
})
export class CarrierModule {}
