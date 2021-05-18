export class Product {

    public id: number;
    public code: string;
    public description: string;
    public currentValue: number;
}

export class ProductRemoveCommand {

    public productIds: number[];

    constructor(products: Product[]) {
        this.map(products);
    }

    private map(products: Product[]): void {
        this.productIds = products.map((product: Product) => {
            return product.id;
        });
    }

}

export class ProductRegisterCommand {

    public code: string;
    public description: string;
    public currentValue: number;

    constructor(value: Product) {
        this.map(value);
    }

    private map(value: Product): void {
        this.code = value.code;
        this.description = value.description;
        this.currentValue = value.currentValue;
    }

}

export class ProductUpdateCommand {

    public id: number;
    public code: string;
    public description: string;
    public currentValue: number;

    constructor(id: number, value: Product) {
        this.id = id;
        this.map(value);
    }

    private map(value: Product): void {
        this.code = value.code;
        this.description = value.description;
        this.currentValue = value.currentValue;
    }

}
