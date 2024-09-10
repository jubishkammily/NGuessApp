import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  loggedIn = false;
  private accountService = inject(AccountService);
  model: any = {};

  login() {
    console.log(this.model);
    this.accountService.login(this.model).subscribe({
      next: (response: any) => {
        console.log(response);
        this.loggedIn = true;
      },
      error: (error: any) => {
        return console.log(error);
      },
    });
  }

  logout() {
    this.loggedIn = false;
  }
}
