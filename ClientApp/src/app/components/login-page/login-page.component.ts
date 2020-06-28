import { RouterModule, Router } from '@angular/router';
import { Component, OnInit, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map'; 
import { LoginServiceService } from '../../services/login-service.service';
import { RegisterNewUserComponent } from '../register-new-user/register-new-user.component';
import { NgModel } from '@angular/forms';

@Component({
  selector: 'login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})


export class LoginPageComponent implements OnInit {

  //Declarations Start
  isHome: boolean = false


  user = {
    username: '',
    password: ''
  };
  usernameIsValid: any;
  userRedirect: any = {};

  constructor(private LoginService: LoginServiceService,
    private router: Router) { }

  ngOnInit() {

    this.user.username = "huzefajtg";
    this.user.password = "admin"
    //this.submit();
  }

  checkUsername() {
    this.LoginService.CheckUsername(this.user.username).subscribe(
      res => {
        this.usernameIsValid = res;
        if (this.usernameIsValid != true)
          alert("Username " + this.user.username + " does not exist ")
      }
    )

  }


  submit() {
    this.LoginService.CheckUser(this.user).subscribe(
      res => {
        if (res != null) {
          this.userRedirect = res;
          let type = this.userRedirect[0].userType;
          console.log("user details from server ", this.userRedirect);
          if (type == "S") {
            console.log("Student")
            this.router.navigateByUrl('/student_home/'+this.userRedirect[0].id);
          }
          else
            if (type == 'T') {
              console.log("Teacher")
              this.router.navigateByUrl('/teacher_home/' + this.userRedirect[0].id);
            }
        }
        else alert("wrong user");
      }
    )
  }

  toHome(){
    
  }

}
