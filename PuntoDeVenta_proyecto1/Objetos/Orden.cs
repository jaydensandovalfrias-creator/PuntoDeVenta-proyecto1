using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeVenta_proyecto1.Objetos
{
    class Orden
    {
        public string numOrden;
        public double precioTotal;
        public int cantidad;
        public Boolean pagado;
        public string estado;

        public Provedor proveedor;
        public Producto producto;
        
    }
}
