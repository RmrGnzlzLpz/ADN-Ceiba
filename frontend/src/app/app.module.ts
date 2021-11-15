import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { ParkingConfigurationComponent } from './components/parking-configuration/parking-configuration.component';
import { VehicleHistoryComponent } from './components/vehicle-history/vehicle-history.component';
import { TakeOutVehicleComponent } from './components/take-out-vehicle/take-out-vehicle.component';
import { ParkVehicleComponent } from './components/park-vehicle/park-vehicle.component';
import { FormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

@NgModule({
  declarations: [
    AppComponent,
    VehicleListComponent,
    ParkingConfigurationComponent,
    VehicleHistoryComponent,
    TakeOutVehicleComponent,
    ParkVehicleComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    SweetAlert2Module.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
