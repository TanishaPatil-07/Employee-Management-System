import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Navbr } from './navbr';

describe('Navbr', () => {
  let component: Navbr;
  let fixture: ComponentFixture<Navbr>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Navbr]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Navbr);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
