import { Component, OnInit } from '@angular/core';
import { ParkingConfiguration } from '../../models/parking';
import { ParkingService } from '../../services/parking.service';

@Component({
  selector: 'app-parking-configuration',
  templateUrl: './parking-configuration.component.html'
})
export class ParkingConfigurationComponent implements OnInit {
  public parkingConfiguration: ParkingConfiguration | undefined;

  constructor(private parkingService: ParkingService) { }

  ngOnInit(): void {
    this.getConfiguration();
  }

  getConfiguration(): void {
    this.parkingService.getConfiguration()
      .subscribe(configuration => {
        this.parkingConfiguration = configuration;
        console.log(this.parkingConfiguration);
      });
  }
}
