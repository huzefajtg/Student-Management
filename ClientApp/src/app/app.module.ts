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
import { TeacherSearcherComponent } from './components/Teachers/teacher-searcher/teacher-searcher.component';
import { ViewStudentComponent } from './components/Teachers/teacher-searcher/view-student/view-student.component';
import { ViewTeacherComponent } from './components/Teachers/teacher-searcher/view-teacher/view-teacher.component';

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
    TeacherProfileComponent,
    TeacherSearcherComponent,
    ViewStudentComponent,
    ViewTeacherComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginPageComponent, pathMatch: 'full' },
      { path: 'login', component: LoginPageComponent },
      { path: 'register-new-user', component: RegisterNewUserComponent },
      { path: 'teacher_home/:id' , component:TeacherHomeComponent },
      { path: 'teacher_home/profile/:id' , component:TeacherProfileComponent },
      { path: 'teacher_home/search/:id', component: TeacherSearcherComponent },
      { path: 'teacher_home/viewStudent/:id/:id2' , component:ViewStudentComponent },
      { path: 'teacher_home/viewTeacher/:id' , component:ViewTeacherComponent },

      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'counter', component: CounterComponent },
      { path: '**', redirectTo: '/login' }//has to remain last

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
