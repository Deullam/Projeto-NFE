import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs/Subject';

import { ReceiverResolveService } from '../../shared/receiver.service';
import { Receiver } from '../../shared/receiver.model';
import { OnInit, OnDestroy } from '@angular/core/src/metadata/lifecycle_hooks';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './receiver-detail.component.html',
})
export class ReceiverDetailComponent implements OnInit, OnDestroy{
    public personTypeText: string;
    public isLoading: boolean = false;

    private receiver: Receiver;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: ReceiverResolveService,
        private router: Router,
        private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((receiver: Receiver) => {
                this.isLoading = false;
                this.receiver = Object.assign(new Receiver(), receiver);
                this.personTypeText = this.receiver.personType === 'LEGAL' ? 'Jurídica' : 'Física';
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
        this.router.navigate(['/destinatarios'], { relativeTo: this.route });
    }
}
