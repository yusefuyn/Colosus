import { HttpHeaders } from '@angular/common/http';
import { HttpClient } from '@angular/common/http'
import { firstValueFrom, BehaviorSubject } from 'rxjs';
import { RequestParameter } from '../models/RequestParameter';
import { RequestResult, ResultEnum } from '../models/RequestResult';
import { enviroments } from '../enviroments/enviroment';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(private http: HttpClient) { }
  private apiUrl: string = enviroments.apiUrl;

  public async Post(Uri: string, Data: RequestParameter): Promise<RequestResult> {
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
}
