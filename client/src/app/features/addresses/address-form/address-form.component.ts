import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'ndd-address-form',
    templateUrl: './address-form.component.html',
})
export class AddressFormComponent {
    @Input() public formModel: FormGroup;
}
