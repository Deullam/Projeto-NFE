export class Address {

    public id: number;
    public streetName: string;
    public number: number;
    public neighborhood: string;
    public city: string;
    public state: string;
    public country: string;

}

export class AddressCommand {

    public streetName: string;
    public number: number;
    public neighborhood: string;
    public city: string;
    public state: string;
    public country: string;

    constructor(value: any) {
        this.streetName = value.streetName;
        this.number = value.number;
        this.neighborhood = value.neighborhood;
        this.city = value.city;
        this.state = value.state;
        this.country = value.country;
    }
}
