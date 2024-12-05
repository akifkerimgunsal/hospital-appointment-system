import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorFaqComponent } from './doctor-faq.component';

describe('DoctorFaqComponent', () => {
  let component: DoctorFaqComponent;
  let fixture: ComponentFixture<DoctorFaqComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorFaqComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorFaqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
