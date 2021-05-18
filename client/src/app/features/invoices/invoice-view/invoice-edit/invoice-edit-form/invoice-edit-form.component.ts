import { FormGroup } from '@angular/forms';
import { Component, Input, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';

import { Receiver } from '../../../../receivers/shared/receiver.model';
import { Carrier } from '../../../../carriers/shared/carrier.model';
import { Sender } from '../../../../senders/shared/sender.model';
import { CarrierService } from '../../../../carriers/shared/carrier.service';
import { ReceiverService } from '../../../../receivers/shared/receiver.service';
import { SenderService } from '../../../../senders/shared/sender.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'ndd-invoice-edit-form',
    templateUrl : './invoice-edit-form.component.html',
})
export class InvoiceEditFormComponent implements OnInit, OnDestroy {

    @Input() public formModel: FormGroup;

    @Output() public submit: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Output() public cancel: EventEmitter<void> = new EventEmitter<void>();

    public receiverData: Receiver[] = [];
    public carrierData: Carrier[] = [];
    public senderData: Sender[] = [];

    private onReceiverFilterChange: Subject<string> = new Subject<string>();
    private ngUnsubscribeReceiver: Subject<void> = new Subject<void>();
    private onCarrierFilterChange: Subject<string> = new Subject<string>();
    private ngUnsubscribeCarrier: Subject<void> = new Subject<void>();
    private onSenderFilterChange: Subject<string> = new Subject<string>();
    private ngUnsubscribeSender: Subject<void> = new Subject<void>();

    constructor(public receiverService: ReceiverService,
        public carrierService: CarrierService,
        public senderService: SenderService) {
    }

    public ngOnInit(): void {
        const timeDelay: number = 300;

        this.onReceiverFilterChange
            .takeUntil(this.ngUnsubscribeReceiver)
            .debounceTime(timeDelay)
            .switchMap((value: string) => this.receiverService.queryByName(value))
            .subscribe((response: any) => {
                this.receiverData = response;
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });

        this.onSenderFilterChange
            .takeUntil(this.ngUnsubscribeSender)
            .debounceTime(timeDelay)
            .switchMap((value: string) => this.senderService.queryByFancyName(value))
            .subscribe((response: any) => {
                this.senderData = response;
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });

        this.onCarrierFilterChange
            .takeUntil(this.ngUnsubscribeSender)
            .debounceTime(timeDelay)
            .switchMap((value: string) => this.carrierService.queryByName(value))
            .subscribe((response: any) => {
                this.carrierData = response;
            }, (err: HttpErrorResponse) => {
                alert(err.message);
            });
    }

    public ngOnDestroy(): void {
        this.ngUnsubscribeReceiver.next();
        this.ngUnsubscribeReceiver.complete();

        this.ngUnsubscribeCarrier.next();
        this.ngUnsubscribeCarrier.complete();

        this.ngUnsubscribeSender.next();
        this.ngUnsubscribeSender.complete();
    }

    public onSubmit(event: Event): void {
        event.stopPropagation();
        this.submit.emit(this.formModel);
    }

    public onCancel(): void {
        this.cancel.emit();
    }

    public onReceiverAutoCompleteChange(value: string): void {
        this.onReceiverFilterChange.next(value);
    }

    public onSenderAutoCompleteChange(value: string): void {
        this.onSenderFilterChange.next(value);
    }

    public onCarrierAutoCompleteChange(value: string): void {
        this.onCarrierFilterChange.next(value);
    }

}
