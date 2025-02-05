import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="welcome-container">
      <h1>Welcome to Quorum</h1>
      <p>Your collaborative platform for meaningful discussions</p>

      <div class="features">
        <div class="feature-card">
          <h3>Connect</h3>
          <p>Join discussions with like-minded individuals</p>
        </div>

        <div class="feature-card">
          <h3>Share</h3>
          <p>Share your thoughts and ideas with the community</p>
        </div>

        <div class="feature-card">
          <h3>Grow</h3>
          <p>Learn from others and expand your perspectives</p>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .welcome-container {
      max-width: 1200px;
      margin: 0 auto;
      padding: 2rem;
      text-align: center;
    }

    h1 {
      font-size: 2.5rem;
      margin-bottom: 1rem;
      color: #2c3e50;
    }

    .features {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
      gap: 2rem;
      margin-top: 3rem;
    }

    .feature-card {
      padding: 1.5rem;
      background: #ffffff;
      border-radius: 8px;
      box-shadow: 0 2px 4px rgba(0,0,0,0.1);
      transition: transform 0.2s ease;
    }

    .feature-card:hover {
      transform: translateY(-5px);
    }

    .feature-card h3 {
      color: #3498db;
      margin-bottom: 0.5rem;
    }

    .feature-card p {
      color: #666;
      line-height: 1.6;
    }
  `]
})
export class WelcomeComponent {}
