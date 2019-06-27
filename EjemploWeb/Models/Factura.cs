using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploWeb.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<FacturaDetalle> Detalles { get; set; }
    }

    public class FacturaDetalle
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }

}
