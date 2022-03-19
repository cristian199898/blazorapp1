using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorApp1.Data.Model
{
    public class Pedido
    {

    
        public int IdPeido { get; set; }
        public string NombreProducto { get; set; }
        public string FechaPedido { get; set; }
        public string fechaEntrega { get; set; }
        public string Estado { get; set; }
        public string Descripcionl { get; set; }
         
    }
}
 