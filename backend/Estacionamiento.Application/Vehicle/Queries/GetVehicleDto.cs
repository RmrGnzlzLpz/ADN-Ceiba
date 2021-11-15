namespace Estacionamiento.Application.Vehicle.Queries
{
    public class GetVehicleDto
    {
        public int Id { get; init; }
        public string License { get; init; } = default!;
        public short CylinderCapacity { get; set; }
        public string Type { get; set; } = default!;
        public bool IsParked { get; set; }
    }
}
