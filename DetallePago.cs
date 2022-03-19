using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data.Model
{
    public class DetallePago
    {
        public int IdPago { get; set; }
        public string NombreProducto { get; set; }
        public string CantidadProducto { get; set; }
        public string PrecioProducto { get; set; }
        public string Descuento { get; set; }
        public string ValorTotal { get; set; }
    }
}
 