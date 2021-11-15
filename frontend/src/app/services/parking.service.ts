import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ParkingConfiguration } from '../models/parking';
import { ParkVehicleRequest, ParkVehicleResponse, TakeOutVehicleRequest, Vehicle, VehicleHistoryResponse, VehicleParking } from '../models/vehicle';
import { MessageService } from './message.service';

@Injectable({
  providedIn: 'root'
})
export class ParkingService {
  private apiUrl = environment.api_url;
  private httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private messageService: MessageService) { }

  parkVehicle(vehicle: ParkVehicleRequest): Observable<ParkVehicleResponse> {
    return this.http.post<ParkVehicleResponse>(`${this.apiUrl}/Vehicles/Park`, vehicle, this.httpOptions)
      .pipe(
        tap(_ => this.log(`Vehicle successfully parked`)),
        catchError(this.handleError<ParkVehicleResponse>('Park vehicle'))
      );
  }

  calculateCost(license: string): Observable<VehicleParking> {
    return this.http.get<VehicleParking>(`${this.apiUrl}/Vehicles/CalculateCost/${license}`, this.httpOptions)
      .pipe(
        tap(_ => this.log(`Calculated cost for ${license}`)),
        catchError(this.handleError<VehicleParking>('Calculate cost'))
      );
  }

  takeOutVehicle(license: string): Observable<VehicleParking> {
    const request: TakeOutVehicleRequest = { license };
    return this.http.post<VehicleParking>(`${this.apiUrl}/Vehicles/Exit/`, request, this.httpOptions)
      .pipe(
        tap(_ => this.log(`Taken out vehicle ${license}`)),
        catchError(this.handleError<VehicleParking>('Take out vehicle'))
      );
  }

  getVehicleHistory(license: string): Observable<VehicleHistoryResponse> {
    return this.http.get<VehicleHistoryResponse>(`${this.apiUrl}/Vehicles/History/${license}`, this.httpOptions)
      .pipe(
        tap(_ => this.log(`Obtainer history of vehicle ${license}`)),
        catchError(this.handleError<VehicleHistoryResponse>('Get vehicle history'))
      );
  }

  getVehicles(): Observable<Vehicle[]> {
    return this.http.get<Vehicle[]>(`${this.apiUrl}/Vehicles`, this.httpOptions)
      .pipe(
        tap(_ => this.log('Fetched vehicles')),
        catchError(this.handleError<Vehicle[]>('Get vehicles', []))
      );
  }

  getConfiguration(): Observable<ParkingConfiguration> {
    return this.http.get<ParkingConfiguration>(`${this.apiUrl}/Parking/Configuration`)
      .pipe(
        tap(_ => this.log('Obtained parking configuration')),
        catchError(this.handleError<ParkingConfiguration>('Get parking configuration'))
      );
  }

  private logError(error: string): void {
    this.messageService.addError(error);
  }

  private log(message: string): void {
    this.messageService.addMessage(message);
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      if (error.error?.message) {
        this.logError(`${operation} failed: ${error.error.message}`);
      } else {
        this.logError(`${operation} failed: ${error.message}`);
      }

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
