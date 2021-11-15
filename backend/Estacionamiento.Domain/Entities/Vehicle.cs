using Estacionamiento.Domain.Attributes;
using Estacionamiento.Domain.Enums;
using Estacionamiento.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Estacionamiento.Domain.Entities
{
    public class Vehicle : EntityBase<int>
    {
        [Required]
        private string _license;
        public string License { 
            get {
                return _license;
            } 
            set {
                if (!new LicensePlateAttribute().IsValid(value))
                {
                    throw new AppException($"invalid license {value}");
                }
                _license = value;
            }
        }

        public short CylinderCapacity { get; set; }
        public VehicleType Type { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public bool IsParked { get; set; }
        public ICollection<VehicleHistory> VehicleHistory { get; private set; }

        private Vehicle()
        {
            _license = default!;
            VehicleHistory = new HashSet<VehicleHistory>();
        }

        public Vehicle(string license, short cylinderCapacity, VehicleType vehicleType) : this()
        {
            License = license;
            CylinderCapacity = cylinderCapacity;
            Type = vehicleType;
        }

        public void TakeOut(DateTime endDate)
        {
            if (endDate < EntryDate)
            {
                throw new AppException("date inconsistent");
            }
            if (!IsParked)
            {
                throw new NotParkedException();
            }
            ExitDate = endDate;
            IsParked = false;
        }

        public void Park(DateTime date)
        {
            if (IsParked)
            {
                throw new AlreadyParkedException();
            }

            EntryDate = date;
            ExitDate = null;
            IsParked = true;
        }

        public void NewHistory(DateTime endDate, int days, int hours, double value)
        {
            VehicleHistory.Add(new VehicleHistory(this, endDate, days, hours, value));
        }
    }
}
