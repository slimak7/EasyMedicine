import { TestBed } from '@angular/core/testing';

import { MedicinesServiceService } from './medicines-service.service';

describe('MedicinesServiceService', () => {
  let service: MedicinesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MedicinesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
