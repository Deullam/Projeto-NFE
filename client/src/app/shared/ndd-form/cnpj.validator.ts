import { AbstractControl, ValidationErrors } from '@angular/forms';

export class CnpjValidators {

    public static checkCnpj(cnpjControl: AbstractControl): ValidationErrors | null {
        if (!(cnpjControl)) return null;

        return CnpjValidators.isValid(cnpjControl.value) ?  null : { invalidcnpj: true };
    }

    /* tslint:disable */
    private static isValid(cnpj: string): boolean {
        if (typeof cnpj === 'undefined' || cnpj === null || cnpj.replace(/\s/g, '').length < 1) {
            return false;
        }

        cnpj = cnpj.trim();
        cnpj = cnpj.replace(/[-_./]/g, '');

        if (cnpj.length !== 14) {
            return false;
        }

        const multiplicador1: number[] = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        const multiplicador2: number[] = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        let soma: number;
        let resto: number;
        let digit: string;
        let tempCnpj: string;

        if (cnpj === '00000000000' ||
            cnpj === '11111111111' ||
            cnpj === '22222222222' ||
            cnpj === '33333333333' ||
            cnpj === '44444444444' ||
            cnpj === '55555555555' ||
            cnpj === '66666666666' ||
            cnpj === '77777777777' ||
            cnpj === '88888888888' ||
            cnpj === '99999999999') {
            return false;
        }

        tempCnpj = cnpj.substring(0, 12);

        soma = 0;
        for (let i: number = 0; i < 12; i++) {
            soma += Number(tempCnpj[i].toString()) * multiplicador1[i];
        }

        resto = (soma % 11);
        if (resto < 2) {
            resto = 0;
        } else {
            resto = 11 - resto;
        }

        digit = resto.toString();
        tempCnpj = tempCnpj + digit;
        soma = 0;

        for (let i: number = 0; i < 13; i++) {
            soma += Number(tempCnpj[i].toString()) * multiplicador2[i];
        }

        resto = (soma % 11);
        if (resto < 2) {
            resto = 0;
        } else {
            resto = 11 - resto;
        }

        digit = digit + resto.toString();

        return cnpj.endsWith(digit);
    }

}