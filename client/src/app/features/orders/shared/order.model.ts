export class Order {

    public id: number;
    public customer: string;
    public quantity: number;
    public productId: number;
    public productName: string;

}

export class OrderRemoveCommand {

    public orderIds: number[];

    constructor(orders: Order[]) {
        this.map(orders);
    }

    private map(orders: Order[]): void {
        this.orderIds = orders.map((order: Order) => {
            return order.id;
        });
    }

}

export class OrderRegisterCommand {

    public customer: string;
    public quantity: number;
    public productId: number;

    constructor(value: Order) {
        this.map(value);
    }

    private map(value: any): void {
        this.customer = value.customer;
        this.quantity = value.quantity;
        this.productId = value.product ? value.product.id : 0;
    }

}

export class OrderUpdateCommand {

    public id: number;
    public customer: string;
    public quantity: number;
    public productId: number;

    constructor(id: number, value: Order) {
        this.id = id;
        this.map(value);
    }

    private map(value: any): void {
        this.customer = value.customer;
        this.quantity = value.quantity;
        this.productId = value.product ? value.product.id : 0;
    }

}
