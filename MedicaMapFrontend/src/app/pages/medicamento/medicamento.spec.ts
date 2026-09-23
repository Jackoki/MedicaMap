import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Medicamento } from './medicamento';

describe('Medicamento', () => {
  let component: Medicamento;
  let fixture: ComponentFixture<Medicamento>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Medicamento],
    }).compileComponents();

    fixture = TestBed.createComponent(Medicamento);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
