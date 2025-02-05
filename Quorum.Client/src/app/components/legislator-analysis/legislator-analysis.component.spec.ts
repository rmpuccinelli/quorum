import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LegislatorAnalysisComponent } from './legislator-analysis.component';
import { QuorumService } from '../../services/quorum.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('LegislatorAnalysisComponent', () => {
  let component: LegislatorAnalysisComponent;
  let fixture: ComponentFixture<LegislatorAnalysisComponent>;
  let quorumService: QuorumService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      declarations: [ LegislatorAnalysisComponent ],
      providers: [QuorumService]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LegislatorAnalysisComponent);
    component = fixture.componentInstance;
    quorumService = TestBed.inject(QuorumService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // Add more tests for component methods
});
