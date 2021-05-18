import { AbstractControl, ValidationErrors } from '@angular/forms';
export class ReceiverValidators {

    public static verifyCNPJRequirement(control: AbstractControl): ValidationErrors | null {
        const personType: AbstractControl = control.get('personType');
        const cnpj: AbstractControl = control.get('cnpj');

        if (!(personType && cnpj)) return null;

        return personType.hasError('required') || personType.value !== 'LEGAL' ||
                cnpj.value ? null : { cnpjrequired: true };
    }

    public static verifyCPFRequirement(control: AbstractControl): ValidationErrors | null {
        const personType: AbstractControl = control.get('personType');
        const cpf: AbstractControl = control.get('cpf');

        if (!(personType && cpf)) return null;

        return personType.hasError('required') || personType.value === 'LEGAL' ||
                cpf.value ? null : { cpfrequired: true };
    }

    public static verifyCompanyNameRequirement(control: AbstractControl): ValidationErrors | null {
        const personType: AbstractControl = control.get('personType');
        const companyName: AbstractControl = control.get('companyName');

        if (!(personType && companyName)) return null;

        return personType.hasError('required') || personType.value !== 'LEGAL' ||
                companyName.value ? null : { companynamerequired: true };
    }

    public static verifyStateRegistrationRequirement(control: AbstractControl): ValidationErrors | null {
        const personType: AbstractControl = control.get('personType');
        const stateRegistration: AbstractControl = control.get('stateRegistration');

        if (!(personType && stateRegistration)) return null;

        return personType.hasError('required') || personType.value !== 'LEGAL' ||
                stateRegistration.value ? null : { stateregistrationrequired: true };
    }
}
