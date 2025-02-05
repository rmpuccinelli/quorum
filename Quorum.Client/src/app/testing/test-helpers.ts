import { Observable, of } from 'rxjs';

export class MockQuorumService {
  getLegislators(): Observable<any[]> {
    return of([]);
  }

  getBills(): Observable<any[]> {
    return of([]);
  }

  getVotes(): Observable<any[]> {
    return of([]);
  }

  getVoteResults(): Observable<any[]> {
    return of([]);
  }
}

export const mockLegislator = {
  id: 1,
  name: 'Test Legislator'
};

export const mockBill = {
  id: 1,
  title: 'Test Bill',
  sponsor_id: 1
};

export const mockVote = {
  id: 1,
  bill_id: 1
};

export const mockVoteResult = {
  id: 1,
  legislator_id: 1,
  vote_id: 1,
  vote_type: 1
};
