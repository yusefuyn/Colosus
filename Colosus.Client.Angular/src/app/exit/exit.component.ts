import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
@Component({
  styleUrl: './exit.component.css',
  templateUrl: './exit.component.html',
  standalone: false,
  selector: 'exit-component',
})

export class ExitComponent implements OnInit, OnDestroy {

  seconds: number = 3;
  private intervalId: any;


  ngOnInit(): void {
    if (this.authService) this.authService.exit();
    this.startTimer();
  }

  startTimer(): void {
    this.intervalId = setInterval(() => {
      this.seconds--;
      if (this.seconds == 0) {
        if (this.router) this.router.navigate(['/Index']);
      }
    }, 1000);
  }

  ngOnDestroy(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }
  constructor(private authService: AuthService,private router: Router) { }
}
