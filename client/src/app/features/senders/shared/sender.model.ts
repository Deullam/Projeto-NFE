import { Address, AddressCommand } from '../../addresses/shared/address.model';

export class Sender {

    public id: number;
    public fancyName: string;
    public companyName: string;
    public cnpj: string;
    public stateRegistration: string;
    public municipalRegistration: string;
    public address: Address;

}
export class SenderRemoveCommand {

    public senderIds: number[];

    constructor(senders: Sender[]) {
        this.map(senders);
    }

    private map(senders: Sender[]): void {
        this.senderIds = senders.map((sender: Sender) => {
            return sender.id;
        });
    }

}

export class SenderRegisterCommand {

    public fancyName: string;
    public companyName: string;
    public cnpj: string;
    public stateRegistration: string;
    public municipalRegistration: string;
    public address: AddressCommand;

    constructor(value: any) {
        this.fancyName = value.fancyName;
        this.companyName = value.companyName;
        this.cnpj = value.cnpj.replace(/[-./]/g, '');
        this.stateRegistration = value.stateRegistration;
        this.municipalRegistration = value.municipalRegistration;
        this.address = new AddressCommand(value.address);
    }

}

export class SenderUpdateCommand {

    public id: number;
    public fancyName: string;
    public companyName: string;
    public cnpj: string;
    public stateRegistration: string;
    public municipalRegistration: string;
    public address: AddressCommand;

    constructor(id: number, value: Sender) {
        this.id = id;
        this.map(value);
    }

    private map(value: Sender): void {
        this.fancyName = value.fancyName;
        this.companyName = value.companyName;
        this.cnpj = value.cnpj.replace(/[-./]/g, '');
        this.stateRegistration = value.stateRegistration;
        this.municipalRegistration = value.municipalRegistration;
        this.address = new AddressCommand(value.address);
    }

}
