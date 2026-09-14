using PuntoDeVenta_proyecto1;
using PuntoDeVenta_proyecto1.Objetos;

class program
{
    public static void Main()
    {
        int op = 0;

        List<Producto> listInventario = new List<Producto>();
        List<Provedor> listProveedor = new List<Provedor>();
        List<Orden> listPedidos = new List<Orden>();

        while (op != 99)
        {
            Entrada.Titulo("PUNTO DE VENTA - MENU PRINCIPAL");
            Console.WriteLine("  1. Modulo Inventario");
            Console.WriteLine("  2. Modulo Proveedores");
            Console.WriteLine("  3. Modulo Pedidos");
            Console.WriteLine("  99. Salir del programa");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("  Productos: " + listInventario.Count +
                              "  |  Proveedores: " + listProveedor.Count +
                              "  |  Ordenes: " + listPedidos.Count);
            Console.WriteLine("--------------------------------------------------");
            op = Entrada.LeerEntero("  Opcion: ", 1);

            switch (op)
            {
                case 1:
                    new ModuloInventario().menuProducto(listInventario);
                    break;
                case 2:
                    new ModuloProveedores().menuProveedores(listProveedor);
                    break;
                case 3:
                    new ModuloOrdenes().menuOrdenes(listInventario, listProveedor, listPedidos);
                    break;
                case 99:
                    Console.WriteLine();
                    Console.WriteLine("  Saliendo del programa. Hasta luego.");
                    break;
                default:
                    Console.WriteLine("  Esa opcion no existe.");
                    Entrada.Pausa();
                    break;
            }
        }
    }
}