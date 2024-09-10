import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './services/account.service';
import { HomeComponent } from "./home/home.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NavComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  // inject http client  
  private accountService = inject(AccountService);
  title = 'NG App';


  // Oninit implements will take care of life cycle function
  ngOnInit(): void {
     this.setUser();
    // to handle request angular uses observable instead of promise like in javascript async and await
  }


  setUser(){
    var currentUserString = localStorage.getItem('user');
    if(currentUserString==null)
      return;
    const user = JSON.parse(currentUserString);
    this.accountService.currentUser.set(user);
  }
}
