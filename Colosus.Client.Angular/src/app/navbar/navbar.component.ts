import { Component, OnInit } from '@angular/core'
import { AuthService } from '../../services/auth.service';

@Component({
  styleUrl: './navbar.component.css',
  standalone: false,
  selector: 'navbar-component',
  templateUrl: './navbar.component.html'
})
export class NavbarComponent implements OnInit {
  Login: boolean = false;
  constructor(private authService: AuthService) { }
  ngOnInit(): void {
    this.authService.currentLoginStatus.subscribe((status) => {
      this.Login = status;
    });
  }
}  
