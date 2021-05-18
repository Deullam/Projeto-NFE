import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { SenderResolveService } from './../../shared/sender.service';
import { Sender } from '../../shared/sender.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './sender-detail.component.html',
})
export class SenderDetailComponent implements OnInit, OnDestroy {

    public freightResponsabilityText: string;
    public isLoading: boolean = false;

    public sender: Sender;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: SenderResolveService,
        private router: Router,
        private route: ActivatedRoute) {}

    public ngOnInit(): void {
        this.isLoading = true;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((sender: Sender) => {
                this.isLoading = false;
                this.sender = Object.assign(new Sender(), sender);
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
        this.router.navigate(['/emitentes'], { relativeTo: this.route });
    }

}
