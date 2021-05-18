import { HttpErrorResponse } from '@angular/common/http';
import { CarrierService } from './../shared/carrier.service';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';
import { Component } from '@angular/core';
import { CarrierGridService } from '../shared/carrier.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { Carrier } from '../shared/carrier.model';

@Component({
    templateUrl: './carrier-list.component.html',
})
export class CarrierListComponent extends GridUtilsComponent {

    constructor(
        public carrierGridService: CarrierGridService,
        public carrierService: CarrierService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.carrierGridService.query(this.createFormattedState());
    }

    public  onClick(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }

    public redirectOpenCarrier(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.carrierGridService.query(state);
    }

    public deleteCarrier(): void {
        this.carrierGridService.loading = true;

        const carriers: Carrier[] = this.getSelectedEntities();

        this.carrierService.delete(carriers)
            .take(1)
            .do(() => this.carrierGridService.loading = false)
            .subscribe((result: boolean) => {
                if (result) {
                    this.selectedRows = [];
                    this.carrierGridService.query(this.state);
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
                this.carrierGridService.loading = false;
                alert(err.message);
            });
    }

    public getPersonTypeDescription(value: string): string {
        if (value === 'LEGAL') {
            return 'Jurídica';
        }

        return 'Física';
    }

    public getFreightResponsabilityDescription(value: string): string {
        if (value === 'SENDER') {
            return 'Emitente';
        }

        return 'Destinatário';
    }

}
