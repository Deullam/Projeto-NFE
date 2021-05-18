import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { SenderService } from './../shared/sender.service';
import { SenderRegisterCommand } from './../shared/sender.model';
import { CnpjValidators } from './../../../shared/ndd-form/cnpj.validator';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './sender-add.component.html',
})
export class SenderAddComponent {

    private static MAX_LENGTH_STATE: number = 2;

    public title: string = 'Adicionar emitente';
    public isLoading: boolean = false;

    public form: FormGroup = this.fb.group({
        fancyName: ['', Validators.required],
        companyName: ['', Validators.required],
        cnpj: ['', [Validators.required, CnpjValidators.checkCnpj]],
        stateRegistration: ['', Validators.required],
        municipalRegistration: ['', [Validators.required]],
        address: this.fb.group({
            streetName: ['', Validators.required],
            number: ['', [Validators.required]],
            neighborhood: ['', Validators.required],
            city: ['', Validators.required],
            state: ['', [Validators.required, Validators.maxLength(SenderAddComponent.MAX_LENGTH_STATE)]],
            country: ['', Validators.required],
        }),
    });

    constructor(
        private fb: FormBuilder,
        private senderService: SenderService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const registerCommand: SenderRegisterCommand = new SenderRegisterCommand(formModel.value);

        this.senderService
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

}
