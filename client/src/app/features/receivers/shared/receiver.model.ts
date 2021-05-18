import { Address, AddressCommand } from './../../addresses/shared/address.model';

export class Receiver {
    public id: number;
    public name: string;
    public companyName: string;
    public cpf: string;
    public cnpj: string;
    public stateRegistration: string;
    public personType: string;
    public address: Address;
}

export class ReceiverRemoveCommand {

    public receiverIds: number[];

    constructor(carriers: Receiver[]) {
        this.map(carriers);
    }

    private map(receiver: Receiver[]): void {
        this.receiverIds = receiver.map((receiver: Receiver) => {
            return receiver.id;
        });
    }

}

export class ReceiverRegisterCommand {

    public name: string;
    public companyName: string;
    public cpf: string;
    public cnpj: string;
    public stateRegistration: string;
    public personType: string;
    public address: AddressCommand;

    constructor(value: any) {
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

export class ReceiverUpdateCommand {

    public id: number;
    public name: string;
    public companyName: string;
    public cpf: string;
    public cnpj: string;
    public stateRegistration: string;
    public personType: string;
    public address: AddressCommand;

    constructor(id: number, value: any) {
        this.id = id;
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
