import { NgModule } from '@angular/core';

import { InvoiceGridService, InvoiceService } from './invoice.service';
import { InvoiceItemService } from './invoice-item.service';

@NgModule({
    declarations: [],
    imports: [],
    exports: [],
    providers: [InvoiceGridService, InvoiceService, InvoiceItemService],
})

export class InvoiceSharedModule{

}
