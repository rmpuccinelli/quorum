import { BillAnalysisComponent } from './components/bill-analysis/bill-analysis.component';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';
import { LegislatorAnalysisComponent } from './components/legislator-analysis/legislator-analysis.component';
import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', component: HomeComponent },
      { path: 'legislators', component: LegislatorAnalysisComponent },
      { path: 'bills', component: BillAnalysisComponent }
    ]
  }
];
