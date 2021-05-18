import { HttpErrorResponse } from '@angular/common/http';
import { SelectionEvent, DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { Router, ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';

import { Receiver } from './../shared/receiver.model';
import { ReceiverGridService, ReceiverService } from './../shared/receiver.service';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';

@Component({
    templateUrl: './receiver-list.component.html',
})

export class ReceiverListComponent extends GridUtilsComponent {

    constructor(
        public receiverGridService: ReceiverGridService,
        public receiverService: ReceiverService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.receiverGridService.query(this.createFormattedState());
    }

    public  onClick(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }

    public redirectOpenReceiver(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.receiverGridService.query(state);
    }

    public deleteReceiver(): void {
        this.receiverGridService.loading = true;

        const receivers: Receiver[] = this.getSelectedEntities();

        this.receiverService.delete(receivers)
            .take(1)
            .do(() => this.receiverGridService.loading = false)
            .subscribe((result: boolean) => {
                if (result) {
                    this.selectedRows = [];
                    this.receiverGridService.query(this.state);
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
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
