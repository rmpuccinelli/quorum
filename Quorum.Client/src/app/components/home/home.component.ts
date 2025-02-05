import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <div class="home-container">
      <h1>Welcome to Quorum</h1>
      <p>Select a section to begin</p>

      <div class="nav-links">
        <a routerLink="/legislators" class="nav-link">
          <h2>Legislators</h2>
          <p>View and analyze legislator data</p>
        </a>

        <a routerLink="/bills" class="nav-link">
          <h2>Bills</h2>
          <p>Explore and analyze bills</p>
        </a>
      </div>
    </div>
  `,
  styles: [`
    .home-container {
      max-width: 800px;
      margin: 0 auto;
      padding: 2rem;
      text-align: center;
    }

    h1 {
      font-size: 2.5rem;
      margin-bottom: 1rem;
      color: #2c3e50;
    }

    .nav-links {
      display: grid;
      grid-template-columns: repeat(2, 1fr);
      gap: 2rem;
      margin-top: 3rem;
    }

    .nav-link {
      padding: 2rem;
      background: #ffffff;
      border-radius: 8px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      transition: transform 0.2s ease;
      text-decoration: none;
      color: inherit;
    }

    .nav-link:hover {
      transform: translateY(-5px);
      background: #f8f9fa;
    }

    .nav-link h2 {
      color: #3498db;
      margin-bottom: 0.5rem;
    }

    .nav-link p {
      color: #666;
      line-height: 1.6;
    }
  `]
})
export class HomeComponent {}
