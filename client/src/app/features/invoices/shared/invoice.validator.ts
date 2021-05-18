import { AbstractControl, ValidatorFn } from '@angular/forms';

export class InvoiceValidators {

    public static minLengthArray(min: number): ValidatorFn | null {
        return (c: AbstractControl): {[key: string]: any} => {
            if (c.value.length >= min) {
                 return null;
            }

            return { minarraylength: {valid: false }};
        };
    }

}
