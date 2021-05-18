import { AbstractControl, ValidationErrors } from '@angular/forms';

export class CpfValidators {
    public static checkCpf(cpfControl: AbstractControl): ValidationErrors | null {
        if (!(cpfControl)) return null;

        return CpfValidators.isValid(cpfControl.value) ? null : { invalidcpf: true };
    }

    /* tslint:disable */
    private static isValid(cpf: string): boolean {
        if (typeof cpf === 'undefined' || cpf === null || cpf.replace(/\s/g, '').length < 1) {
            return false;
        }

        let clearCPF: string;
        clearCPF = cpf.trim();
        clearCPF = clearCPF.replace(/[-_./]/g, '');

        if (clearCPF.length !== 11) {
            return false;
        }

        let cpfArray: number[];
        let totalDigitoI: number = 0;
        let totalDigitoII: number = 0;
        let modI: number;
        let modII: number;

        if (clearCPF === '00000000000' ||
            clearCPF === '11111111111' ||
            clearCPF === '22222222222' ||
            clearCPF === '33333333333' ||
            clearCPF === '44444444444' ||
            clearCPF === '55555555555' ||
            clearCPF === '66666666666' ||
            clearCPF === '77777777777' ||
            clearCPF === '88888888888' ||
            clearCPF === '99999999999') {
            return false;
        }

        const i: string = clearCPF[0];

        if (isNaN(Number(clearCPF))) {
            return false;
        }

        cpfArray = new Array<number>(11);
        for (let i: number = 0; i < clearCPF.length; i++) {
            cpfArray[i] = Number(clearCPF[i].toString());
        }

        for (let posicao: number = 0; posicao < cpfArray.length - 2; posicao++) {
            totalDigitoI += cpfArray[posicao] * (10 - posicao);
            totalDigitoII += cpfArray[posicao] * (11 - posicao);
        }

        modI = totalDigitoI % 11;
        if (modI < 2) {
            modI = 0;
        } else {
            modI = 11 - modI;
        }

        if (cpfArray[9] != modI) {
            return false;
        }

        totalDigitoII += modI * 2;

        modII = totalDigitoII % 11;
        if (modII < 2) {
            modII = 0;
        } else {
            modII = 11 - modII;
        }

        if (cpfArray[10] != modII) {
            return false;
        }

        return true;
    }

}
