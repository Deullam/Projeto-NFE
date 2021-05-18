import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { CarrierResolveService } from './../../shared/carrier.service';
import { Carrier } from '../../shared/carrier.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './carrier-detail.component.html',
})
export class CarrierDetailComponent implements OnInit, OnDestroy {

    public freightResponsabilityText: string;
    public personTypeText: string;
    public isLoading: boolean = false;

    private carrier: Carrier;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: CarrierResolveService,
        private router: Router,
        private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((carrier: Carrier) => {
                this.isLoading = false;
                this.carrier = Object.assign(new Carrier(), carrier);
                this.freightResponsabilityText = this.carrier.freightResponsability === 'SENDER' ? 'Emitente' : 'Destinatário';
                this.personTypeText = this.carrier.personType === 'LEGAL' ? 'Jurídica' : 'Física';
            }, (err: HttpErrorResponse) => {
                this.isLoading = false;
                alert(err.message);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }

    public onEdit(): void {
        this.router.navigate(['./editar'], { relativeTo: this.route });
    }

    public redirect(): void {
        this.router.navigate(['/transportadores'], { relativeTo: this.route });
    }

}
