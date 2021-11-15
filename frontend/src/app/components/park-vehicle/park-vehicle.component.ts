import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ParkVehicleRequest, ParkVehicleResponse } from 'src/app/models/vehicle';
import { MessageService } from 'src/app/services/message.service';
import { ParkingService } from 'src/app/services/parking.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-park-vehicle',
  templateUrl: './park-vehicle.component.html'
})
export class ParkVehicleComponent implements OnInit {
  response: ParkVehicleResponse | undefined;
  vehicle: ParkVehicleRequest = {
    license: '',
    cylinderCapacity: 0,
    type: 0
  };

  constructor(
    private route: ActivatedRoute,
    private parkingService: ParkingService,
    private location: Location,
    public messageService: MessageService,
  ) { }

  ngOnInit(): void {
    this.getLicense();
  }

  getLicense(): void {
    const license = this.route.snapshot.paramMap.get('license') ?? '';
    this.vehicle.license = license;
  }

  parkVehicle(): void {
    if (!environment.licensePlateValidation.test(this.vehicle.license)) {
      this.messageService.addError(`Invalid license ${this.vehicle.license}`);
    } else {
      this.parkingService.parkVehicle(this.vehicle)
        .subscribe(response => this.response = response);
    }
  }

  goBack(): void {
    this.location.back();
  }
}
