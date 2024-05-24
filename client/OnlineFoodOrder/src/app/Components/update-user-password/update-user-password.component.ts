import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import FormValidationCheck from '../../Helpers/FormValidationCheck';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AdminServiceService } from '../../Services/admin-service.service';

@Component({
  selector: 'app-update-user-password',
  templateUrl: './update-user-password.component.html',
  styleUrls: ['./update-user-password.component.css']
})
export class UpdateUserPasswordComponent implements OnInit {
  updateDetails: FormGroup;
  Email!: string;
  Id!: number;
  Oldpass!: string

  constructor(private fb: FormBuilder, private router: Router, private toast: NgToastService, private adminser: AdminServiceService) {
    this.updateDetails = this.fb.group({
      TempPassword: ['', Validators.required],
      NewPassword: ['', [Validators.required]],
      UserPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }
  ngOnInit() {
    this.initializeUserData();

  }

  get tempPass() {
    return this.updateDetails.get('TempPassword');
  }

  get newPass() {
    return this.updateDetails.get('NewPassword');
  }

  get userPassword() {
    return this.updateDetails.get('UserPassword');
  }

  passwordMatchValidator(formGroup: FormGroup): ValidationErrors | null {
    const password = formGroup.get('NewPassword')?.value;
    const confirmPassword = formGroup.get('UserPassword')?.value;

    // Check if New Password and Confirm Password match
    if (password !== confirmPassword) {
      formGroup.get('UserPassword')?.setErrors({ mismatchPassword: true });
    } else {
      // Reset the error if they match
      formGroup.get('UserPassword')?.setErrors(null);
    }
    return null;
  }

  formsubmit() {
    if (this.updateDetails.valid) {
      const TempPassword = this.updateDetails.get('TempPassword')?.value;
      const userPassword = this.updateDetails.get('UserPassword')?.value;
      const values = { email: this.Email, id: this.Id,TempPassword, userPassword }
      this.adminser.UpdatePassword(values).subscribe((response) => {
        console.log(response);
        this.adminser.logout();
        this.router.navigate(['login']);
        this.toast.success({ detail: "SUCCESS", summary: response.message, duration: 5000 });
      },
        (err) => {
          this.toast.error({ detail: "ERROR", summary: "Something when wrong!", duration: 3000 });
          console.log(err);
        }
      )
    } else {
      FormValidationCheck.ValidateallformFields(this.updateDetails);
    }
  }

  //role //id //email 
  private initializeUserData() {
    const idFromStorage = this.adminser.getId();
    const emailFromStorage = this.adminser.getemail();
    this.Email = emailFromStorage || 'null';
    this.Id = idFromStorage || 'null';
  }
}
