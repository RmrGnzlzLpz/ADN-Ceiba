import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VehicleParking } from 'src/app/models/vehicle';
import { MessageService } from 'src/app/services/message.service';
import { ParkingService } from 'src/app/services/parking.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-take-out-vehicle',
  templateUrl: './take-out-vehicle.component.html'
})
export class TakeOutVehicleComponent implements OnInit {
  license: string | undefined;
  response: VehicleParking | undefined;

  constructor(
    private route: ActivatedRoute,
    private parkingService: ParkingService,
    private location: Location,
    public messageService: MessageService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const license = this.route.snapshot.paramMap.get('license') ?? '';
    this.license = license;
  }

  takeOut(): void {
    if (this.license?.length) {
      const license = this.license;
      this.parkingService.calculateCost(license)
        .subscribe(calculatedCost => {
          if (!calculatedCost) {
            return;
          }
          Swal.fire({
            title: 'Are you sure?',
            html: `
            <p><strong>Entry Date</strong>: ${new Date(calculatedCost.entryDate).toLocaleString()}</p>
            <p><strong>Exit Date</strong>: ${new Date(calculatedCost.exitDate).toLocaleString()}</p>
            <p><strong>Days</strong>: ${calculatedCost.days}</p>
            <p><strong>Hours</strong>: ${calculatedCost.hours}</p>
            <p><strong>Cost</strong>: ${calculatedCost.value}</p>`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, take out!'
          }).then((result) => {
            if (result.isConfirmed) {
              this.parkingService.takeOutVehicle(license)
                .subscribe(vehicleParking => {
                  this.response = vehicleParking;
                });
            }
          });
        });
    }
  }

  goBack(): void {
    this.location.back();
  }
}
