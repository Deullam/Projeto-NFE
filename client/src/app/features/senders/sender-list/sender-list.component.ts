import { HttpErrorResponse } from '@angular/common/http';
import { SenderService } from './../shared/sender.service';
import { GridUtilsComponent } from './../../../shared/grid-utils/grid-utils-component';
import { Component } from '@angular/core';
import { SenderGridService } from '../shared/sender.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { Sender } from '../shared/sender.model';

@Component({
    templateUrl: './sender-list.component.html',
})
export class SenderListComponent extends GridUtilsComponent {

    constructor(
        public senderGridService: SenderGridService,
        public senderService: SenderService,
        private router: Router,
        private route: ActivatedRoute) {
        super();
        this.senderGridService.query(this.createFormattedState());
    }

    public  onClick(): void {
        this.router.navigate(['./adicionar'],
            { relativeTo: this.route });
    }

    public redirectOpenSender(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`],
            { relativeTo: this.route });
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.senderGridService.query(state);
    }

    public deleteSender(): void {
        this.senderGridService.loading = true;

        const senders: Sender[] = this.getSelectedEntities();

        this.senderService.delete(senders)
            .take(1)
            .do(() => this.senderGridService.loading = false)
            .subscribe((result: boolean) => {
                if (result) {
                    this.selectedRows = [];
                    this.senderGridService.query(this.state);
                } else {
                    alert('Não foi possível realizar a operação');
                }
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }
}
