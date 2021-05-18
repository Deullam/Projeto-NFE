import { Receiver } from './../../receivers/shared/receiver.model';
import { Sender } from 'src/app/features/senders/shared/sender.model';
import { Carrier } from 'src/app/features/carriers/shared/carrier.model';
import { InvoiceItem, InvoiceItemRegisterCommand } from './invoice-item.model';

export class InvoiceTax {
    public icmsValue: string;
    public freight: number;
    public ipiValue: number;
    public totalValueProducts: string;
    public totalValueInvoice: Date;
}

export class Invoice {
    public id: number;
    public natureOperation: string;
    public keyAccess: number;
    public number: number;
    public status: string;
    public entryDate: Date;
    public issueDate: Date;
    public invoiceTax: InvoiceTax;
    public sender: Sender;
    public receiver: Receiver;
    public carrier: Carrier;
    public invoiceItems: InvoiceItem[];
    public senderName: string;
    public carrierName: string;
    public receiverName: string;
}

export class InvoiceRemoveCommand {

    public invoiceIds: number[];

    constructor(invoice: Invoice[]) {
        this.invoiceIds = invoice.map((invoice: Invoice) => {
            return invoice.id;
        });
    }

}

export class InvoiceRegisterCommand {
    public natureOperation: string;
    public number: number;
    public status: string;
    public senderId: number;
    public receiverId: number;
    public carrierId: number;
    public invoiceItems: InvoiceItemRegisterCommand[];

    constructor(value: Invoice) {
        this.natureOperation = value.natureOperation;
        this.number = value.number;
        this.status = 'OPEN';
        this.senderId = value.sender ? value.sender.id : null;
        this.receiverId = value.receiver ? value.receiver.id : null;
        this.carrierId = value.carrier ? value.carrier.id : null;
        this.invoiceItems = [];

        if (value.invoiceItems) {
            value.invoiceItems.forEach((invoiceItem: any) => {
                this.invoiceItems.push(new InvoiceItemRegisterCommand(invoiceItem));
            });
        }
    }
}
export class InvoiceUpdateCommand {
    public id: number;
    public natureOperation: string;
    public keyAccess: number;
    public number: number;
    public status: string;
    public senderId: number;
    public receiverId: number;
    public carrierId: number;

    constructor(id: number, value: Invoice) {
        this.id = id;
        this.natureOperation = value.natureOperation;
        this.keyAccess = value.keyAccess;
        this.number = value.number;
        this.status = value.status;
        this.status = 'OPEN';
        this.senderId = value.sender ? value.sender.id : null;
        this.receiverId = value.receiver ? value.receiver.id : null;
        this.carrierId = value.carrier ? value.carrier.id : null;
    }
}
