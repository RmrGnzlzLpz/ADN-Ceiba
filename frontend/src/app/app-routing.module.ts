import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ParkVehicleComponent } from './components/park-vehicle/park-vehicle.component';
import { ParkingConfigurationComponent } from './components/parking-configuration/parking-configuration.component';
import { TakeOutVehicleComponent } from './components/take-out-vehicle/take-out-vehicle.component';
import { VehicleHistoryComponent } from './components/vehicle-history/vehicle-history.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';

const routes: Routes = [
  { path: '', redirectTo: '/parking', pathMatch: 'full' },
  { path: 'vehicles', component: VehicleListComponent },
  { path: 'parking', component: ParkingConfigurationComponent },
  { path: 'history/:license', component: VehicleHistoryComponent },
  { path: 'take-out/:license', component: TakeOutVehicleComponent },
  { path: 'take-out', component: TakeOutVehicleComponent },
  { path: 'park/:license', component: ParkVehicleComponent },
  { path: 'park', component: ParkVehicleComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
