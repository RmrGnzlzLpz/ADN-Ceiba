export enum VehicleType {
    Car = 0,
    Motorcycle = 1
}

export interface Vehicle {
    id: number;
    license: string;
    cylinderCapacity: number;
    type: string;
    isParked: boolean;
}

export interface VehicleParking {
    hours: number;
    days: number;
    value: number;
    entryDate: Date,
    exitDate: Date;
}

export interface ParkVehicleRequest {
    license: string;
    cylinderCapacity: number;
    type: VehicleType;
}

export interface ParkVehicleResponse {
    license: string;
    entryDate: Date;
}

export interface TakeOutVehicleRequest {
    license: string;
}

export interface VehicleHistoryResponse {
    license: string;
    vehicleHistory: VehicleParking[];
}
