import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Carrier, CarrierUpdateCommand } from '../../shared/carrier.model';
import { CarrierResolveService, CarrierService } from './../../shared/carrier.service';
import { CpfValidators } from './../../../../shared/ndd-form/cpf.validator';
import { CnpjValidators } from './../../../../shared/ndd-form/cnpj.validator';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './carrier-edit.component.html',
})
export class CarrierEditComponent implements OnInit {

    private static MAX_LENGTH_STATE: number = 2;

    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        freightResponsability: ['', [Validators.required]],
        personType: ['PHYSICAL', [Validators.required]],
        address: this.fb.group({
            streetName: ['', Validators.required],
            number: ['', [Validators.required]],
            neighborhood: ['', Validators.required],
            city: ['', Validators.required],
            state: ['', [Validators.required, Validators.maxLength(CarrierEditComponent.MAX_LENGTH_STATE)]],
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

    private carrier: Carrier;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(
        private fb: FormBuilder,
        private carrierService: CarrierService,
        private resolver: CarrierResolveService,
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
            .subscribe((carrier: Carrier) => {
                this.carrier = Object.assign(new Carrier(), carrier);

                this.updateFormGroup(carrier.personType);

                if (carrier.personType === 'LEGAL') {
                    this.form.setValue({
                        legal: {
                            fancyName: this.carrier.name,
                            companyName: this.carrier.companyName,
                            cnpj: this.carrier.cnpj,
                            stateRegistration: this.carrier.stateRegistration,
                        },
                        freightResponsability: this.carrier.freightResponsability,
                        personType: this.carrier.personType,
                        address: {
                            streetName: this.carrier.address.streetName,
                            number: this.carrier.address.number,
                            neighborhood: this.carrier.address.neighborhood,
                            city: this.carrier.address.city,
                            state: this.carrier.address.state,
                            country: this.carrier.address.country,
                        },
                    });
                } else {
                    this.form.setValue({
                        physical: {
                            name: this.carrier.name,
                            cpf: this.carrier.cpf,
                        },
                        freightResponsability: this.carrier.freightResponsability,
                        personType: this.carrier.personType,
                        address: {
                            streetName: this.carrier.address.streetName,
                            number: this.carrier.address.number,
                            neighborhood: this.carrier.address.neighborhood,
                            city: this.carrier.address.city,
                            state: this.carrier.address.state,
                            country: this.carrier.address.country,
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

        const updateCommand: CarrierUpdateCommand = new CarrierUpdateCommand(this.carrier.id, formModel.value);

        this.carrierService
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
