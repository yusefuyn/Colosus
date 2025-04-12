import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { sha256 } from 'js-sha256';
import { RequestParameter } from '../models/RequestParameter';
import { RequestResult, ResultEnum } from '../models/RequestResult';
import { LoginRequestModel } from '../models/Request/login';
import { enviroments } from '../enviroments/enviroment'
import { HttpHeaders } from '@angular/common/http';
import { firstValueFrom, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string = enviroments.apiUrl;
  private loggedInSource = new BehaviorSubject<boolean>(false);
  currentLoginStatus = this.loggedInSource.asObservable();

  constructor(private http: HttpClient) { }
  private async Post(Uri: string, Data: RequestParameter): Promise<RequestResult> {

    Data.Address = Uri;

    const Token = localStorage.getItem('AuthToken');
    if (Token != null)
      Data.Token = Token.toString();
    else
      Data.Token = '';
    Data.RequestToken = '';
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
    });
    const dataString = JSON.stringify(Data);
    try {
      const response = await firstValueFrom(this.http.post<RequestResult>(this.apiUrl + Uri, dataString, { headers }));
      return response;
    } catch (error) {
      var returned = new RequestResult('');
      returned.Result = ResultEnum.Error;
      returned.Description = 'Bir hata oluştu.';
      returned.CreateDate = new Date();
      return returned;
    }
  }


  async login(username: string, password: string): Promise<RequestResult> {
    let hashedPassword: string = sha256(password);
    const body = JSON.stringify(new LoginRequestModel(username, hashedPassword, true));
    const parameter = new RequestParameter(body);
    var res = await this.Post('Api/Login/Login', parameter);

    if (res.Result == ResultEnum.Ok) {
      localStorage.setItem('AuthToken', res.Data);
      this.loggedInSource.next(true);
    }

    return res;
  }

  exit(): void {
    localStorage.removeItem('AuthToken');
    this.loggedInSource.next(false);
  }
}
