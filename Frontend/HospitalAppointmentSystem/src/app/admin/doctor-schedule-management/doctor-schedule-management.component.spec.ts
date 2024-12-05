import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorScheduleManagementComponent } from './doctor-schedule-management.component';

describe('DoctorScheduleManagementComponent', () => {
  let component: DoctorScheduleManagementComponent;
  let fixture: ComponentFixture<DoctorScheduleManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DoctorScheduleManagementComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DoctorScheduleManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
