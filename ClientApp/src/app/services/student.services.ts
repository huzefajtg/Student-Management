import { TeacherResource, TeacherSearch } from './../models/models';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StudentService {

  constructor(private _http: HttpClient) { }

  getStudent(Tid:number) {
    return this._http.get('/api/student/' + Tid).map(res => res);
  }

  addCourse(obj:{StudentId:number,TeacherId:number}) {
    return this._http.post('/api/student/course' , obj).map(res => res);
  }


}
