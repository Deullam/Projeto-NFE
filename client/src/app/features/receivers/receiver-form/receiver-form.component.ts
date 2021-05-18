import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'ndd-receiver-form',
    templateUrl: './receiver-form.component.html',
})
export class ReceiverFormComponent{

    @Input() public formModel: FormGroup;

    @Output() public submit: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() public cancel: EventEmitter<void> = new EventEmitter<void>();
    public cpfMask: any[] = [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /\d/, /\d/];
    public cnpjMask: any[] = [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/];

    public onSubmit(event: Event): void {
        event.stopPropagation();
        this.submit.emit(this.formModel);
    }

    public onCancel(): void {
        this.cancel.emit();
    }
}
