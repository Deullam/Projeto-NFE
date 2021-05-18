import { Component } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Receiver } from '../shared/receiver.model';
import { ReceiverResolveService } from '../shared/receiver.service';
import { OnInit, OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './receiver-view.component.html',
})
export class ReceiverViewComponent implements OnInit, OnDestroy{
    public receiver: Receiver;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: ReceiverResolveService) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((receiver: Receiver) => {
                this.receiver = receiver;
                this.createProperty();
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    private createProperty(): void {
        this.title = this.receiver.name;

        let descriptionId: string = '';
        let valueId: string = '';

        if (this.receiver.personType === 'LEGAL') {
            descriptionId = 'CNPJ';
            valueId = this.receiver.cnpj;
        } else {
            descriptionId = 'CPF';
            valueId = this.receiver.cpf;
        }

        this.infoItems = [
            {
                value: valueId,
                description: descriptionId,
            },
        ];
    }
}
