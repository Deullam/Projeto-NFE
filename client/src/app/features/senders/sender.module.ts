import { NDDTitlebarModule } from './../../shared/ndd-ng-titlebar/component/ndd-ng-titlebar.module';
import { NDDTabsbarModule } from './../../shared/ndd-ng-tabsbar/component/ndd-ng-tabsbar.module';
import { SharedModule } from './../../shared/shared.module';
import { NgModule } from '@angular/core';

import { GridModule } from '@progress/kendo-angular-grid';
import { TextMaskModule } from 'angular2-text-mask';

import { SenderRoutingModule } from './sender-routing.module';
import { SenderListComponent } from './sender-list/sender-list.component';
import { SenderResolveService } from './shared/sender.service';
import { SenderViewComponent } from './sender-view/sender-view.component';
import { SenderDetailComponent } from './sender-view/sender-detail/sender-detail.component';
import { SenderFormComponent } from './sender-form/sender-form.component';
import { SenderAddComponent } from './sender-add/sender-add.component';
import { SenderEditComponent } from './sender-view/sender-edit/sender-edit.component';
import { SenderSharedModule } from './shared/sender-shared.module';
import { AddressModule } from '../addresses/address.module';

@NgModule({
    declarations: [
        SenderListComponent,
        SenderDetailComponent,
        SenderViewComponent,
        SenderFormComponent,
        SenderEditComponent,
        SenderAddComponent,
    ],
    imports: [
        SenderRoutingModule,
        SharedModule,
        GridModule,
        NDDTabsbarModule,
        NDDTitlebarModule,
        SenderSharedModule,
        AddressModule,
        TextMaskModule,
    ],
    exports: [],
    providers: [SenderResolveService],
})
export class SenderModule {}
