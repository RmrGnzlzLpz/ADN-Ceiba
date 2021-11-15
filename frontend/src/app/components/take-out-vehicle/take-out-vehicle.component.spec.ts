import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TakeOutVehicleComponent } from './take-out-vehicle.component';

describe('TakeOutVehicleComponent', () => {
  let component: TakeOutVehicleComponent;
  let fixture: ComponentFixture<TakeOutVehicleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TakeOutVehicleComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TakeOutVehicleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
