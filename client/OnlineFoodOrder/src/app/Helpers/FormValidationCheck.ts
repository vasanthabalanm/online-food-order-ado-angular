import { FormControl, FormGroup } from "@angular/forms";

export default class FormValidationCheck{
    static ValidateallformFields(fg:FormGroup){
        Object.keys(fg.controls).forEach(field => {
            const control = fg.get(field);
            if(control instanceof FormControl){
                control.markAsDirty({onlySelf:true});
            }
            else if(control instanceof FormGroup){
                this.ValidateallformFields(control);
            }
        });
    }
}