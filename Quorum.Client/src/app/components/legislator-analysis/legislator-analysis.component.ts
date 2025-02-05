import { AsyncPipe, NgFor } from '@angular/common';
import { Component, inject } from '@angular/core';
import { EMPTY, catchError } from 'rxjs';

import { LegislatorAnalysis } from '../../models/LegislatorAnalysis';
import { MatTableModule } from '@angular/material/table';
import { QuorumService } from '../../services/quorum.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-legislator-analysis',
  standalone: true,
  imports: [MatTableModule, NgFor, AsyncPipe],
  templateUrl: './legislator-analysis.component.html',
  styleUrls: ['./legislator-analysis.component.scss'],
})
export class LegislatorAnalysisComponent {
  private readonly quorumService = inject(QuorumService);
  private readonly toastr = inject(ToastrService);

  protected readonly legislators$ = this.quorumService.getLegislatorAnalysis().pipe(
    catchError(error => {
      this.toastr.error('Failed to load legislator analysis data');
      return EMPTY;
    })
  );

  getDisplayName(legislator: LegislatorAnalysis): string {
    return `${JSON.stringify(legislator)}`;
  }
}
