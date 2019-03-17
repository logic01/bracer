import { TestBed } from '@angular/core/testing';

import { PhyisicianService } from './phyisician.service';

describe('PhyisicianService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PhyisicianService = TestBed.get(PhyisicianService);
    expect(service).toBeTruthy();
  });
});
