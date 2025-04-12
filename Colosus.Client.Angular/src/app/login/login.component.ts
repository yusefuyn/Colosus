import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service'
import { ResultEnum } from '../../models/RequestResult';
import { Router } from '@angular/router';

@Component({
  styleUrl: './login.component.css',
  standalone: false,
  templateUrl: './login.component.html',
  selector: 'login-component'
})
export class LoginComponent implements OnInit {
  username: string = "";
  password: string = "";




  constructor(private authService: AuthService, private router: Router) { }
  ngOnInit(): void {

  }

  async onLoginSubmit(): Promise<void> {
    var res = await this.authService.login(this.username, this.password);
    if (res.Result == ResultEnum.Ok) {
      this.router.navigate(['/Index']);
    }
    else {
      console.log(res.Description);
    }
  }
}
