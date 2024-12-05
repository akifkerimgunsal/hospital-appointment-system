import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LiveSupportComponent } from './live-support.component';

describe('LiveSupportComponent', () => {
  let component: LiveSupportComponent;
  let fixture: ComponentFixture<LiveSupportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LiveSupportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LiveSupportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
