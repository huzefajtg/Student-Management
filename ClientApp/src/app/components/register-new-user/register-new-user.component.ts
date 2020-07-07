import { Component, OnInit } from '@angular/core';
import { RegisterDetails,Personal } from '../../models/models';
import { LoginServiceService } from '../../services/login-service.service';

@Component({
  selector: 'app-register-new-user',
  templateUrl: './register-new-user.component.html',
  styleUrls: ['./register-new-user.component.css']
})
export class RegisterNewUserComponent implements OnInit {

  user: RegisterDetails = {
    PersonalInfo: {
      firstName: '',
      secondName: '',
      lastname: '',
      isReg: true,

      gender: '',
      emailId: '',
      contactNumber: '',
      contactAddress: '',
      dob: '',
      type: ''
    },

    UserInfo: {
      username: '',
      password: ''
    }
  }
  constructor(private LoginService: LoginServiceService) { }

  ngOnInit() {
  }

  Register() {
    console.log(this.user);
    this.LoginService.Register(this.user).subscribe(res => {
      console.log(res);
    })
  }
}
