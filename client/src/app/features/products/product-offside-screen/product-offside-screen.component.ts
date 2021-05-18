import { Component, EventEmitter } from '@angular/core';

import { SelectionEvent, DataStateChangeEvent } from '@progress/kendo-angular-grid';

import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';

import { ProductOffsideScreenService } from './product-offside-screen.service';
import { ProductGridService } from '../shared/product.service';

@Component({
    selector: 'ndd-product-offside-screen',
    templateUrl: './product-offsise-screen.component.html',
})
export class ProductOffsideScreenComponent extends GridUtilsComponent {

    public onAdd: EventEmitter<void> = new EventEmitter<void>();
    public onAddAndClose: EventEmitter<void> = new EventEmitter<void>();
    public onClose: EventEmitter<void> = new EventEmitter<void>();

    constructor(
        private service: ProductOffsideScreenService,
        public productGridService: ProductGridService,
    ) {
        super();
        this.productGridService.query(this.createFormattedState());
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.productGridService.query(state);
    }

}
