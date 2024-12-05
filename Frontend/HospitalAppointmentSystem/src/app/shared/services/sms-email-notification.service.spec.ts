import { TestBed } from '@angular/core/testing';

import { SmsEmailNotificationService } from './sms-email-notification.service';

describe('SmsEmailNotificationService', () => {
  let service: SmsEmailNotificationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SmsEmailNotificationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
