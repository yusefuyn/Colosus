import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'button-component',
  templateUrl: './button.component.html',
  styleUrl: './button.component.css',
  standalone: false,
})
export class ButtonComponent {
  constructor(private router: Router) { }
  @Input() Icon: string = '';
  @Input() Text: string = '';
  @Input() Class: string = '';
  @Input() Style: string = '';
  @Input() Route: string = '';

  onclick(): void {
    if (this.Route)  this.router.navigate([this.Route]);
  }
}
