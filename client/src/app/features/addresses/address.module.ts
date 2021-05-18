import { NgModule } from '@angular/core';
import { AddressFormComponent } from './address-form/address-form.component';
import { SharedModule } from '../../shared/shared.module';
import { AddressDetailComponent } from './address-detail/address-detail.component';

@NgModule({
    declarations: [
        AddressFormComponent,
        AddressDetailComponent,
    ],
    imports: [
        SharedModule,
    ],
    exports: [
        AddressFormComponent,
        AddressDetailComponent,
    ],
    providers: [

    ],
})
export class AddressModule {}
