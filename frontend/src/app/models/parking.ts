export interface ParkingConfiguration {
    hourCost: ParkingProperty[],
    dayCost: ParkingProperty[],
    extraCost: ParkingProperty[],
    extraCostCylinderCapacity: ParkingProperty[],
    numberOfHoursAsDay: number,
    numberOfHoursPerDay: number,
    totalCells: ParkingProperty[]
}

export interface ParkingProperty {
    key: string,
    value: number
}
