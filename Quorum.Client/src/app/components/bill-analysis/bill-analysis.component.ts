import { AsyncPipe, NgFor } from '@angular/common';
import { Component, inject } from '@angular/core';
import { catchError, EMPTY } from 'rxjs';

import { QuorumService } from '../../services/quorum.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bill-analysis',
  standalone: true,
  imports: [AsyncPipe, NgFor],
  templateUrl: './bill-analysis.component.html',
  styleUrls: ['./bill-analysis.component.scss']
})
export class BillAnalysisComponent {
  private readonly quorumService = inject(QuorumService);
  private readonly toastr = inject(ToastrService);

  protected readonly bills$ = this.quorumService.getBillAnalysis().pipe(
    catchError(error => {
      this.toastr.error('Failed to load bill analysis data');
      return EMPTY;
    })
  );
}
