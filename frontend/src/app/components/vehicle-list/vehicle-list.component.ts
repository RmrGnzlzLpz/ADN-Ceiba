import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Vehicle } from '../../models/vehicle';
import { ParkingService } from '../../services/parking.service';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html'
})
export class VehicleListComponent implements OnInit {
  private vehicles: Vehicle[] = [];
  filteredEntities: Vehicle[] = [];
  search: string = "";

  currentPage: number = 1;
  pageSize: number = environment.page_size;

  totalPages = () => Math.floor(((this.filteredEntities.length + this.pageSize - 1) / this.pageSize));
  getFilteredEntities() {
    if (this.search.length > 0) {
      this.filteredEntities = this.vehicles.filter(x => x.license.toUpperCase().indexOf(this.search.toUpperCase()) > -1);
    } else {
      this.filteredEntities = this.vehicles;
    }
  }

  getCurrentPage(): Vehicle[] {
    return this.filteredEntities.slice((this.currentPage - 1) * this.pageSize, this.currentPage * this.pageSize);
  }

  constructor(private parkingService: ParkingService) { }

  ngOnInit(): void {
    this.getVehicles();
  }

  getVehicles(): void {
    this.parkingService.getVehicles().subscribe(vehicles => {
      this.vehicles = vehicles;
      this.getFilteredEntities();
    });
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

  filter(): void {
    this.getFilteredEntities();
  }
}
