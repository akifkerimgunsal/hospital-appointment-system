import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PastAppointmentComponent } from './past-appointment.component';

describe('PastAppointmentComponent', () => {
  let component: PastAppointmentComponent;
  let fixture: ComponentFixture<PastAppointmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PastAppointmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PastAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
