import { Component, OnDestroy, OnInit } from '@angular/core';
import { Sender } from '../shared/sender.model';
import { Subject } from 'rxjs/Subject';
import { SenderResolveService } from '../shared/sender.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    templateUrl: './sender-view.component.html',
})
export class SenderViewComponent implements OnInit, OnDestroy {

    public sender: Sender;
    public infoItems: object[];
    public title: string;
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: SenderResolveService) {}

    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((sender: Sender) => {
                this.sender = sender;
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
        this.title = this.sender.companyName;
    }

}
