import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { AccountService } from '../services/account.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
private accountService = inject(AccountService);
@Output() cancelRegister = new EventEmitter();
cancel() {
this.cancelRegister.emit(false);
}
  model:any = {};
register() {
  console.log("register")
 this.accountService.register(this.model).subscribe({
  next: response => {
    console.log(response)
    this.cancel();
  },
  error: error =>{console.log(error);}
 });
 
}



}
