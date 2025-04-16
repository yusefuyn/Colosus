import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
@Component({
  selector: 'input-component',
  templateUrl: './input.component.html',
  styleUrls: ['./input.component.css'],
  standalone: false,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputComponent),
      multi: true
    }
  ]
})
export class InputComponent {

  @Input() placeholder: string = '';
  @Input() type: string = 'text';
  @Input() class: string = '';
  value: any = '';

  onChange = (value: any) => { };
  onTouched = () => { };

  // Yazarken tetiklenir
  onInput(event: any) {
    const value = event.target.value;
    this.value = value;
    this.onChange(value);
  }

  // Form kontrolü yeni değer gönderdiğinde tetiklenir
  writeValue(value: any): void {
    this.value = value;
  }

  // ngModel -> component arasındaki bağlantı
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  // Touched olayı için
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  // (opsiyonel) disable için
  setDisabledState?(isDisabled: boolean): void {
    // burada input disable edebilirsin
  }



  constructor() {
    this.placeholder = '';
    this.type = 'text';
  }
  focused: boolean = false;
}
