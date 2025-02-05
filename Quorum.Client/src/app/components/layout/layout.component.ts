import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive],
  template: `
    <div class="layout">
      <nav class="sidebar">
        <ul class="nav-list">
          <li>
            <a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}">
              Home
            </a>
          </li>
          <li>
            <a routerLink="/legislators" routerLinkActive="active">
              Legislators
            </a>
          </li>
          <li>
            <a routerLink="/bills" routerLinkActive="active">
              Bills
            </a>
          </li>
        </ul>
      </nav>
      <main class="content">
        <router-outlet></router-outlet>
      </main>
    </div>
  `,
  styles: [`
    .layout {
      display: flex;
      min-height: 100vh;
    }

    .sidebar {
      width: 200px;
      background-color: #2c3e50;
      padding: 1rem;
    }

    .nav-list {
      list-style: none;
      padding: 0;
      margin: 0;
    }

    .nav-list li {
      margin-bottom: 0.5rem;
    }

    .nav-list a {
      color: #ecf0f1;
      text-decoration: none;
      padding: 0.5rem 1rem;
      display: block;
      border-radius: 4px;
      transition: background-color 0.2s;
    }

    .nav-list a:hover {
      background-color: #34495e;
    }

    .nav-list a.active {
      background-color: #3498db;
    }

    .content {
      flex: 1;
      padding: 1rem;
      background-color: #f8f9fa;
    }
  `]
})
export class LayoutComponent {}
