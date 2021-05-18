import { TextMaskModule } from 'angular2-text-mask';
import { GridModule } from '@progress/kendo-angular-grid';
import { NgModule } from '@angular/core';

import { ReceiverListComponent } from './receiver-list/receiver-list.component';
import { ReceiverFormComponent } from './receiver-form/receiver-form.component';
import { NDDTitlebarModule } from './../../shared/ndd-ng-titlebar/component/ndd-ng-titlebar.module';
import { SharedModule } from './../../shared/shared.module';
import { ReceiverEditComponent } from './receiver-view/receiver-edit/receiver-edit.component';
import { ReceiverDetailComponent } from './receiver-view/receiver-detail/receiver-detail.component';
import { NDDTabsbarModule } from './../../shared/ndd-ng-tabsbar/component/ndd-ng-tabsbar.module';
import { ReceiverResolveService } from './shared/receiver.service';
import { ReceiverViewComponent } from './receiver-view/receiver-view.component';
import { ReceiverAddComponent } from './receiver-add/receiver-add-component';
import { ReceiverRoutingModule } from './receiver-routing.module';
import { AddressModule } from './../addresses/address.module';
import { ReceiverSharedModule } from './shared/receiver-shared.module';

@NgModule({
    declarations: [
        ReceiverAddComponent,
        ReceiverListComponent,
        ReceiverViewComponent,
        ReceiverDetailComponent,
        ReceiverEditComponent,
        ReceiverFormComponent,
    ],
    imports: [
        ReceiverRoutingModule,
        SharedModule,
        GridModule,
        NDDTabsbarModule,
        NDDTitlebarModule,
        ReceiverSharedModule,
        AddressModule,
        TextMaskModule,
    ],
    exports: [],
    providers: [ReceiverResolveService],
})
export class ReceiverModule {}
