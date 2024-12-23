import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import {MatButtonModule} from '@angular/material/button';
import {MatSidenavModule} from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatSidenavModule, MatIconModule, MatButtonModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})

export class AppComponent {
  title = 'frontend';
  isLoggedIn = false;

  constructor(private authService: AuthService, private router:Router) {}

  ngOnInit() {
    this.authService.isLoggedIn$.subscribe((loggedIn) => {
      this.isLoggedIn = loggedIn;
    });
  }

  logout(){
    this.isLoggedIn = false;
    this.authService.logout();
    this.router.navigate(['/login']);
  }
  
  isActiveRoute(route: string): boolean {
    return this.router.url === route;
  }

  redirectsTo(route: string) {
    this.router.navigate([route]);
  }
}
