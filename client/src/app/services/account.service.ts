import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5197/api/';
  login(model: any) {
    console.log('login service');
    var url = this.baseUrl + 'Account/login';
    console.log('url : ', url);
    return this.http.post(this.baseUrl + 'account/login', model);
  }
}
