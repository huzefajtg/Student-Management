import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StudentService {

  constructor(private _http: HttpClient) { }

  getStudent(Tid:number) {
    return this._http.get('/api/student/' + Tid).map(res => res);
  }

  addCourse(obj:{studentId:number,courseId:number}) {
    return this._http.post('/api/student/course' , obj).map(res => res);
  }
  updateStudent(id:number,StudentDetails){
    return this._http.put('api/student/'+id,StudentDetails).map(re=>re);
  }

}
