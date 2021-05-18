import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs/Subject';

import { Receiver, ReceiverUpdateCommand } from '../../shared/receiver.model';
import { CpfValidators } from './../../../../shared/ndd-form/cpf.validator';
import { CnpjValidators } from './../../../../shared/ndd-form/cnpj.validator';
import { ReceiverService, ReceiverResolveService } from '../../shared/receiver.service';
import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './receiver-edit.component.html',
})
export class ReceiverEditComponent implements OnInit {
    private static MAX_LENGTH_STATE: number = 2;

    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        personType: ['PHYSICAL', [Validators.required]],
        address: this.fb.group({
            streetName: ['', Validators.required],
            number: ['', [Validators.required]],
            neighborhood: ['', Validators.required],
            city: ['', Validators.required],
            state: ['', [Validators.required, Validators.maxLength(ReceiverEditComponent.MAX_LENGTH_STATE)]],
            country: ['', Validators.required],
        }),
    });

    public formGroupPhysical: FormGroup = this.fb.group({
        name: ['', Validators.required],
        cpf: ['', [ Validators.required, CpfValidators.checkCpf ]],
    });

    public formGroupLegal: FormGroup = this.fb.group({
        fancyName: ['', Validators.required],
        companyName: ['', [Validators.required]],
        cnpj: ['', [Validators.required, CnpjValidators.checkCnpj]],
        stateRegistration: ['', [Validators.required]],
    });

    private receiver: Receiver;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private fb: FormBuilder,
        private receiverService: ReceiverService,
        private resolver: ReceiverResolveService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public ngOnInit(): void {
        this.form.addControl('physical', this.formGroupPhysical);
        this.form.controls.personType.valueChanges.subscribe((value: string) => {
            this.updateFormGroup(value.toString());
        });

        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((receiver: Receiver) => {
                this.receiver = Object.assign(new Receiver(), receiver);

                this.updateFormGroup(receiver.personType);

                if (receiver.personType === 'LEGAL') {
                    this.form.setValue({
                        legal: {
                            fancyName: this.receiver.name,
                            companyName: this.receiver.companyName,
                            cnpj: this.receiver.cnpj,
                            stateRegistration: this.receiver.stateRegistration,
                        },
                        personType: this.receiver.personType,
                        address: {
                            streetName: this.receiver.address.streetName,
                            number: this.receiver.address.number,
                            neighborhood: this.receiver.address.neighborhood,
                            city: this.receiver.address.city,
                            state: this.receiver.address.state,
                            country: this.receiver.address.country,
                        },
                    });
                } else {
                    this.form.setValue({
                        physical: {
                            name: this.receiver.name,
                            cpf: this.receiver.cpf,
                        },
                        personType: this.receiver.personType,
                        address: {
                            streetName: this.receiver.address.streetName,
                            number: this.receiver.address.number,
                            neighborhood: this.receiver.address.neighborhood,
                            city: this.receiver.address.city,
                            state: this.receiver.address.state,
                            country: this.receiver.address.country,
                        },
                    });
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const updateCommand: ReceiverUpdateCommand = new ReceiverUpdateCommand(this.receiver.id, formModel.value);

        this.receiverService
            .put(updateCommand)
            .take(1)
            .subscribe((success: boolean) => {
                this.isLoading = false;

                if (success) {
                    this.resolver.resolveFromRouteAndNotify();
                    this.redirect();
                } else {
                    alert('Não foi possível atualizar o registro');
                }
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public redirect(): void {
        this.router.navigate(['../'],  { relativeTo: this.route });
    }

    private updateFormGroup(key: string): void {
        switch (key.toString()) {
            case 'PHYSICAL':
                this.form.removeControl('legal');
                this.form.addControl('physical', this.formGroupPhysical);
                break;
            case 'LEGAL':
                this.form.removeControl('physical');
                this.form.addControl('legal', this.formGroupLegal);
                break;

            default:
                break;
        }
    }
}
