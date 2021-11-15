import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParkingConfigurationComponent } from './parking-configuration.component';

describe('ParkingConfigurationComponent', () => {
  let component: ParkingConfigurationComponent;
  let fixture: ComponentFixture<ParkingConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParkingConfigurationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ParkingConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
