import { Product } from 'src/app/features/products/shared/product.model';
import { Invoice } from './invoice.model';

export class InvoiceItem {

    public id: number;
    public invoice: Invoice;
    public product: Product;
    public amount: number;

}

export class InvoiceItemRegisterCommand {

    public productId: number;
    public amount: number;

    constructor(value: any) {
        this.productId = value.product ? value.product.id : null;
        this.amount = value.amount;
    }
}

export class InvoiceItemUpdateCommand {

    public id: number;
    public productId: number;
    public amount: number;

    constructor(id: number, value: any) {
        this.id = id;
        this.productId = value.product ? value.product.id : null;
        this.amount = value.amount;
    }
}

export class InvoiceItemRemoveCommand {

    public invoiceItemIds: number[];

    constructor(invoice: InvoiceItem[]) {
        this.invoiceItemIds = invoice.map((invoiceItem: InvoiceItem) => {
            return invoiceItem.id;
        });
    }

}
