import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RegisterDetails } from '../models/models';

@Injectable()
export class LoginServiceService {

  constructor(private _http: HttpClient) { }

  CheckUsername(username){
    return this._http.get('/api/login/'+username).map(res=>res);
  }

  CheckUser(user)
  {
    return this._http.post('/api/login',user).map(res=>res)
  }

  Register(user:RegisterDetails){
    return this._http.post('/api/register',user).map(res=>res);
  }

}
