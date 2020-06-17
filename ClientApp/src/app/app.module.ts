import { BrowserModule } from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { LoginServiceService } from './services/login-service.service';
import { RegisterNewUserComponent } from './components/register-new-user/register-new-user.component';
import { TeacherHomeComponent } from './components/Teachers/teacher-home/teacher-home.component';
import { TeacherProfileComponent } from './components/Teachers/teacher-profile/teacher-profile.component';
import { TeacherServiceService } from './services/teacher-services.services';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginPageComponent,
    RegisterNewUserComponent,
    TeacherHomeComponent,
    TeacherProfileComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginPageComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'login', component: LoginPageComponent },
      { path: 'register-new-user', component: RegisterNewUserComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'teacher_home/:id' , component:TeacherHomeComponent },
      { path: 'teacher_home/profile/:id' , component:TeacherProfileComponent },

      { path: '**', redirectTo:'/login' },//has to remain last

    ])

  ],
  providers: [
    LoginServiceService,
    TeacherServiceService,
    RegisterNewUserComponent

  ],
  bootstrap: [AppComponent,

  ]
})
export class AppModule { }
