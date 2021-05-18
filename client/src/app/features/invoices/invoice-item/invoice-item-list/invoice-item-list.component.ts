import { ProductOffsideScreenComponent } from './../../../products/product-offside-screen/product-offside-screen.component';
import { ProductOffsideScreenService } from './../../../products/product-offside-screen/product-offside-screen.service';
import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { SelectionEvent } from '@progress/kendo-angular-grid';

import { GridUtilsComponent } from '../../../../shared/grid-utils/grid-utils-component';

@Component({
    templateUrl: './invoice-item-list.component.html',
    selector: 'ndd-invoice-item-list',
})
export class InvoiceItemListComponent extends GridUtilsComponent implements OnInit {

    @Input() public invoiceItems: any[];
    @Input() public height: string;
    @Output() public selectItem: EventEmitter<number> = new EventEmitter<number>();

    constructor(private productOffsideScreenService: ProductOffsideScreenService) {
        super();
    }

    public ngOnInit(): void {
        this.selectableSettings.mode = 'single';
    }

    public teste(): void {
        this.productOffsideScreenService.createComponent<ProductOffsideScreenComponent>(ProductOffsideScreenComponent);
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);

        if (this.getSelectedEntities().length === 1) {
            this.selectItem.emit(this.selectedRows[0].index);
        } else {
            this.selectItem.emit(-1);
        }
    }

}
