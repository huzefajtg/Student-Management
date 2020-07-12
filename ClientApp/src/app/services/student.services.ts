import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class StudentService {

  constructor(private _http: HttpClient) { }

  getStudent(Tid:number) {
    return this._http.get('/api/student/' + Tid).map(res => res);
  }

  addCourse(obj:{studentId:number,otherId:number}) {
    return this._http.post('/api/student/course' , obj).map(res => res);
  }
  
  updateStudent(id:number,StudentDetails){
    return this._http.put('api/student/'+id,StudentDetails).map(re=>re);
  }


  deleteCourse(obj:{studentId:number,otherId:number}){
    return this._http.post('/api/student/deleteCourse' , obj).map(res => res);
  }

  getTeachersList(){
    return this._http.get('/api/student/getTeachers').map(res => res);
  }

  
  getNotification(id:number){
    return this._http.get('/api/snoti/' + id).map(res => res);
  }

  changeViwed(id:number){
    return this._http.get('/api/snoti/vChange/' + id).map(res => res);
  }

}
