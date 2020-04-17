import { TestBed } from '@angular/core/testing';

import { ConsultorioService } from './consultorio.service';

describe('ConsultorioService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ConsultorioService = TestBed.get(ConsultorioService);
    expect(service).toBeTruthy();
  });
});
