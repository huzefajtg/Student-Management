import { TeacherResource, TeacherSearch } from './../models/models';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TeacherServiceService {

  constructor(private _http: HttpClient) { }

  getTeacher (Tid){
    return this._http.get('/api/teacher/'+Tid).map(res=>res);
  }

  updateTeacher(Tid:number,TeacherDetails){
    return this._http.put('/api/teacher/'+Tid,TeacherDetails).map(res=>res);
  }

  getCourses(){
    return this._http.get('/api/teacher/course').map(res=>res);

  }

  getStudents2(query:TeacherSearch){
    return this._http.post('/api/teacher/getStudent',query).map(res=>res);
  }

  getStudents(query:TeacherSearch){
    return this._http.post('/api/teacher/getStudents',query).map(res=>res);
  }
 

}
