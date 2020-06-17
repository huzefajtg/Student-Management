import { TeacherResource } from './../models/models';
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
 

}
