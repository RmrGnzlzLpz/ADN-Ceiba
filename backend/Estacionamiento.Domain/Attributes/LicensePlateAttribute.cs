using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estacionamiento.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LicensePlateAttribute : RegularExpressionAttribute
    {
        public LicensePlateAttribute() : base(@"[a-zA-Z]{3}[0-9]{3}|[CcDdMmOoAa]{2}[0-9]{4}|[RrSs]{1}[0-9]{5}|[Tt]{1}[0-9]{4}|[a-zA-Z]{3}[0-9]{2}[a-zA-Z]{1}")
        {
        }
    }
}
