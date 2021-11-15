import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VehicleHistoryResponse } from 'src/app/models/vehicle';
import { ParkingService } from 'src/app/services/parking.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-vehicle-history',
  templateUrl: './vehicle-history.component.html',
})
export class VehicleHistoryComponent implements OnInit {
  vehicle: VehicleHistoryResponse | undefined;
  currentPage: number = 1;
  pageSize: number = environment.page_size;
  totalPages = () => Math.floor((((this.vehicle?.vehicleHistory ?? []).length + this.pageSize - 1) / this.pageSize));
  currentEntities = () => {
    if (this.vehicle?.vehicleHistory) {
      return this.vehicle.vehicleHistory.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize);
    }
    return [];
  }


  constructor(
    private route: ActivatedRoute,
    private parkingService: ParkingService,
    private location: Location
  ) { }

  ngOnInit(): void {
    this.getVehicle();
  }

  getVehicle(): void {
    const license = this.route.snapshot.paramMap.get('license') ?? '';
    this.parkingService.getVehicleHistory(license)
      .subscribe(history => {
        this.vehicle = history;
      });
  }

  goBack(): void {
    this.location.back();
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages()) {
      this.currentPage += 1;
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage -= 1;
    }
  }
}
