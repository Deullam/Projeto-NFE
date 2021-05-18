import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Sender, SenderUpdateCommand } from '../../shared/sender.model';
import { SenderResolveService, SenderService } from './../../shared/sender.service';
import { CnpjValidators } from './../../../../shared/ndd-form/cnpj.validator';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './sender-edit.component.html',
})
export class SenderEditComponent implements OnInit {

    private static MAX_LENGTH_STATE: number = 2;
    public isLoading: boolean = false;

    private sender: Sender;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    private form: FormGroup = this.fb.group({
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
            state: ['', [Validators.required, Validators.maxLength(SenderEditComponent.MAX_LENGTH_STATE)]],
            country: ['', Validators.required],
        }),
    });

    constructor(
        private fb: FormBuilder,
        private senderService: SenderService,
        private resolver: SenderResolveService,
        private router: Router,
        private route: ActivatedRoute,
    ) {}

    public ngOnInit(): void {
        this.isLoading = true;

        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((sender: Sender) => {
                this.sender = Object.assign(new Sender(), sender);
                this.form.setValue({
                    fancyName: this.sender.fancyName,
                    companyName: this.sender.companyName,
                    cnpj: this.sender.cnpj,
                    stateRegistration: this.sender.stateRegistration,
                    municipalRegistration: this.sender.municipalRegistration,
                    address: ({
                        streetName: this.sender.address.streetName,
                        number: this.sender.address.number,
                        neighborhood: this.sender.address.neighborhood,
                        city: this.sender.address.city,
                        state: this.sender.address.state,
                        country: this.sender.address.country,
                    }),
                });

                this.isLoading = false;
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public onSubmit(formModel: FormGroup): void {
        this.isLoading = true;

        const updateCommand: SenderUpdateCommand = new SenderUpdateCommand(this.sender.id, formModel.value);

        this.senderService
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

}
