import { StudentService } from './services/student.services';
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
import { StudentHomeComponent } from './components/Student/student-home/student-home.component';
import { StudentDetailsComponent } from './components/Student/student-details/student-details.component';
import { StudentSearchComponent } from './components/Student/student-search/student-search.component';
import { ViewerPageComponent } from './components/Student/viewer-page/viewer-page.component';
import { ShowTableComponent } from './components/show-table/show-table.component';

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
    StudentHomeComponent,
    StudentDetailsComponent,
    StudentSearchComponent,
    ViewerPageComponent,
    ShowTableComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginPageComponent, pathMatch: 'full' },
      { path: 'login', component: LoginPageComponent },
      { path: 'register-new-user', component: RegisterNewUserComponent },
      //Teachers
      { path: 'teacher_home/:id' , component:TeacherHomeComponent },
      { path: 'teacher/profile/:id' , component:TeacherProfileComponent },
      { path: 'teacher/search/:id', component: TeacherSearcherComponent },

      //Students
      { path: 'student_home/:id' , component:StudentHomeComponent },
      { path: 'student/profile/:id' , component:StudentDetailsComponent },
      { path: 'student/search/:id' , component:StudentSearchComponent },
      { path: 'student/viewer/:id/:id2/:type/:type2' , component:ViewerPageComponent },
      


      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'counter', component: CounterComponent },
      { path: '**', redirectTo: '/login' }//has to remain last

    ])

  ],
  providers: [
    LoginServiceService,
    TeacherServiceService,
    StudentService,
    RegisterNewUserComponent

  ],
  bootstrap: [AppComponent,

  ]
})
export class AppModule { }
