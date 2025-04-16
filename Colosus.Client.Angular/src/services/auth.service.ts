import { sha256 } from 'js-sha256';
import { RequestParameter } from '../models/RequestParameter';
import { RequestResult, ResultEnum } from '../models/RequestResult';
import { LoginRequestModel } from '../models/Request/login';
import { BehaviorSubject } from 'rxjs';
import { HttpService } from './http.service';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root' 
})
export class AuthService {
  private loggedInSource = new BehaviorSubject<boolean>(false);
  currentLoginStatus = this.loggedInSource.asObservable();

  constructor(private httpService: HttpService) { }

  async login(username: string, password: string): Promise<RequestResult> {
    let hashedPassword: string = sha256(password);
    const body = JSON.stringify(new LoginRequestModel(username, hashedPassword, true));
    const parameter = new RequestParameter(body);
    var res = await this.httpService.Post('Api/Login/Login', parameter);

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
