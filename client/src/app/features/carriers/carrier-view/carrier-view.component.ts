import { Component, OnDestroy, OnInit } from '@angular/core';
import { Carrier } from '../shared/carrier.model';
import { Subject } from 'rxjs/Subject';
import { CarrierResolveService } from '../shared/carrier.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './carrier-view.component.html',
})
export class CarrierViewComponent implements OnInit, OnDestroy {

    public carrier: Carrier;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: CarrierResolveService) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((carrier: Carrier) => {
                this.carrier = carrier;
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
        this.title = this.carrier.name;

        let descriptionId: string = '';
        let valueId: string = '';

        if (this.carrier.personType === 'LEGAL') {
            descriptionId = 'CNPJ';
            valueId = this.carrier.cnpj;
        } else {
            descriptionId = 'CPF';
            valueId = this.carrier.cpf;
        }

        this.infoItems = [
            {
                value: valueId,
                description: descriptionId,
            },
        ];
    }

}
