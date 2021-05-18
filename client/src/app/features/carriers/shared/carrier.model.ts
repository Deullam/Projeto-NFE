import { Address, AddressCommand } from '../../addresses/shared/address.model';

export class Carrier {

    public id: number;
    public name: string;
    public companyName: string;
    public cpf: string;
    public cnpj: string;
    public stateRegistration: string;
    public freightResponsability: string;
    public personType: string;
    public address: Address;

}

export class CarrierRemoveCommand {

    public carrierIds: number[];

    constructor(carriers: Carrier[]) {
        this.map(carriers);
    }

    private map(carriers: Carrier[]): void {
        this.carrierIds = carriers.map((carrier: Carrier) => {
            return carrier.id;
        });
    }

}

export class CarrierRegisterCommand {

    public name: string;
    public companyName: string;
    public cpf: string;
    public cnpj: string;
    public stateRegistration: string;
    public freightResponsability: string;
    public personType: string;
    public address: AddressCommand;

    constructor(value: any) {
        this.freightResponsability = value.freightResponsability;
        this.personType = value.personType;
        this.address = new AddressCommand(value.address);

        if (this.personType === 'LEGAL') {
            this.name = value.legal.fancyName;
            this.companyName = value.legal.companyName;
            this.cnpj = value.legal.cnpj.replace(/[-_./]/g, '');
            this.stateRegistration = value.legal.stateRegistration;
        } else {
            this.name = value.physical.name;
            this.cpf = value.physical.cpf.replace(/[-_./]/g, '');
        }
    }

}

export class CarrierUpdateCommand {

    public id: number;
    public name: string;
    public companyName: string;
    public cpf: string;
    public cnpj: string;
    public stateRegistration: string;
    public freightResponsability: string;
    public personType: string;
    public address: AddressCommand;

    constructor(id: number, value: any) {
        this.id = id;
        this.freightResponsability = value.freightResponsability;
        this.personType = value.personType;
        this.address = new AddressCommand(value.address);

        if (this.personType === 'LEGAL') {
            this.name = value.legal.fancyName;
            this.companyName = value.legal.companyName;
            this.cnpj = value.legal.cnpj.replace(/[-./]/g, '');
            this.stateRegistration = value.legal.stateRegistration;
        } else {
            this.name = value.physical.name;
            this.cpf = value.physical.cpf.replace(/[-./]/g, '');
        }
    }

}
