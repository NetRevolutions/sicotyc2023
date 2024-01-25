import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";


export const fnpasswordsIguales: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {

    const pass1Control = control.get('password');
    const pass2Control = control.get('password2');

    console.log(`pwd: ${pass1Control?.value}, pwd2: ${pass2Control?.value}`);

    if ((pass1Control != null || pass2Control) && pass1Control?.value !== pass2Control?.value) {
        pass2Control?.setErrors({
            contrasenasIguales: false,
            msg: 'Contraseñas no son iguales'
        });
    }

    return ((pass1Control != null || pass2Control) && pass1Control?.value !== pass2Control?.value) ? {
        contrasenasIguales: 'no es correcto',
        msg: 'Revisar validacion de contraseñas'
    }: null;
}