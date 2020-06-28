import { TeacherResource, TeacherSearch } from './../models/models';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StudentService {

  constructor(private _http: HttpClient) { }

  getStudent(Tid) {
    return this._http.get('/api/student/' + Tid).map(res => res);
  }

}
