import { NgModule } from '@angular/core';

import { GridModule, SharedModule } from '@progress/kendo-angular-grid';

import { ProductGridService, ProductService } from './product.service';
import { ProductOffsideScreenService } from '../product-offside-screen/product-offside-screen.service';
import { ProductOffsideScreenComponent } from './../product-offside-screen/product-offside-screen.component';

import { CommonModule } from '@angular/common';

@NgModule({
    declarations: [ProductOffsideScreenComponent],
    imports: [
        GridModule,
        SharedModule,
        CommonModule,
    ],
    exports: [ProductOffsideScreenComponent],
    providers: [
        ProductGridService,
        ProductService,
        ProductOffsideScreenService,
    ],
    entryComponents: [ProductOffsideScreenComponent],
})
export class ProductSharedModule {}
