using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorApp1.Data.Model
{
    public class Producto
    {

        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Stock { get; set; }
        public string Marca { get; set; }
        public string Precio { get; set; }
        

    }
}
 