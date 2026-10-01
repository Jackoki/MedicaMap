import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BancoLocal } from './banco-local';

describe('BancoLocal', () => {
  let component: BancoLocal;
  let fixture: ComponentFixture<BancoLocal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BancoLocal],
    }).compileComponents();

    fixture = TestBed.createComponent(BancoLocal);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
