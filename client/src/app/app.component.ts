import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  // inject http client
  http = inject(HttpClient);
  title = 'NG App';
  users: any;

  // Oninit implements will take care of life cycle function
  ngOnInit(): void {
    this.http.get('http://localhost:5196/API/Users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log('Reques has finished'),
    });

    // to handle request angular uses observable instead of promise like in javascript async and await
  }
}
