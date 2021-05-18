import { OnInit } from '@angular/core/src/metadata/lifecycle_hooks';
import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { CarrierService } from './../shared/carrier.service';
import { CarrierRegisterCommand } from './../shared/carrier.model';
import { CpfValidators } from './../../../shared/ndd-form/cpf.validator';
import { CnpjValidators } from './../../../shared/ndd-form/cnpj.validator';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './carrier-add.component.html',
})
export class CarrierAddComponent implements OnInit {

    private static MAX_LENGTH_STATE: number = 2;

    public title: string;
    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        freightResponsability: ['', [Validators.required]],
        personType: ['PHYSICAL', [Validators.required]],
        address: this.fb.group({
            streetName: ['', Validators.required],
            number: ['', [Validators.required]],
            neighborhood: ['', Validators.required],
            city: ['', Validators.required],
            state: ['', [Validators.required, Validators.maxLength(CarrierAddComponent.MAX_LENGTH_STATE)]],
            country: ['', Validators.required],
        }),
    });

    public formGroupPhysical: FormGroup = this.fb.group({
        name: ['', Validators.required],
        cpf: ['', [ Validators.required, CpfValidators.checkCpf]],
    });

    public formGroupLegal: FormGroup = this.fb.group({
        fancyName: ['', Validators.required],
        companyName: ['', [Validators.required]],
        cnpj: ['', [Validators.required, CnpjValidators.checkCnpj]],
        stateRegistration: ['', [Validators.required]],
    });

    constructor(
        private fb: FormBuilder,
        private carrierService: CarrierService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public ngOnInit(): void {
        this.title = 'Adicionar Transportador';
        this.isLoading = false;
        this.form.addControl('physical', this.formGroupPhysical);

        this.form.controls.personType.valueChanges.subscribe((value: string) => {
            this.updateFormGroup(value.toString());
        });
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const registerCommand: CarrierRegisterCommand = new CarrierRegisterCommand(formModel.value);

        this.carrierService
            .post(registerCommand)
            .take(1)
            .subscribe((id: number) => {
                this.isLoading = false;

                if (id > 0) {
                    this.redirect();
                } else {
                    alert('Não foi possível cadastrar o registro');
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
