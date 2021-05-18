import { Component, Input } from '@angular/core';

import { Address } from '../shared/address.model';

@Component({
    templateUrl: './address-detail.component.html',
    selector: 'ndd-address-detail',
})
export class AddressDetailComponent {

    @Input() public address: Address;

}
