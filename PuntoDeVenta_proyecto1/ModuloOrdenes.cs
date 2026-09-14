using PuntoDeVenta_proyecto1.Objetos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeVenta_proyecto1
{
    class ModuloOrdenes
    {
        private const string EN_ESPERA = "En espera";
        private const string EN_TRANSITO = "En transito";
        private const string COMPLETADA = "Completada";

        public void menuOrdenes(List<Producto> listProd, List<Provedor> listProveedor, List<Orden> listOrden)
        {
            int op = 0;
            while (op != 99)
            {
                Entrada.Titulo("MODULO PEDIDOS");
                Console.WriteLine("  1. Agregar Orden");
                Console.WriteLine("  2. Borrar Orden");
                Console.WriteLine("  3. Modificar Orden");
                Console.WriteLine("  4. Mostrar Ordenes");
                Console.WriteLine("  5. Buscar Orden");
                Console.WriteLine("  6. Pagar Orden");
                Console.WriteLine("  7. Completar Orden");
                Console.WriteLine("  99. Regresar al menu principal");
                Console.WriteLine("--------------------------------------------------");
                op = Entrada.LeerEntero("  Opcion: ", 1);

                switch (op)
                {
                    case 1:
                        AgregarOrden(listProd, listProveedor, listOrden);
                        break;
                    case 2:
                        BorrarOrden(listOrden);
                        break;
                    case 3:
                        ModificarOrden(listProd, listProveedor, listOrden);
                        break;
                    case 4:
                        MostrarOrdenes(listOrden);
                        Entrada.Pausa();
                        break;
                    case 5:
                        BuscarOrden(listOrden);
                        break;
                    case 6:
                        PagarOrden(listOrden);
                        break;
                    case 7:
                        CompletarOrden(listProd, listOrden);
                        break;
                    case 99:
                        break;
                    default:
                        Console.WriteLine("  Esa opcion no existe.");
                        Entrada.Pausa();
                        break;
                }
            }
        }
        private void AgregarOrden(List<Producto> listProd, List<Provedor> listProveedor, List<Orden> listOrden)
        {
            Entrada.Titulo("AGREGAR ORDEN");

            if (listProd.Count == 0)
            {
                Console.WriteLine("  No puedes crear una orden sin productos.");
                Console.WriteLine("  Registra primero un producto en el modulo de inventario.");
                Entrada.Pausa();
                return;
            }
            if (listProveedor.Count == 0)
            {
                Console.WriteLine("  No puedes crear una orden sin proveedores.");
                Console.WriteLine("  Registra primero un proveedor en el modulo de proveedores.");
                Entrada.Pausa();
                return;
            }

            string num = Entrada.LeerTexto("  Numero de orden: ");
            if (BuscarIndice(listOrden, num) != -1)
            {
                Console.WriteLine();
                Console.WriteLine("  Ya existe una orden con ese numero. No se agrego nada.");
                Entrada.Pausa();
                return;
            }

            Producto prodElegido = ElegirProducto(listProd);
            Provedor provElegido = ElegirProveedor(listProveedor);

            int cantidad = Entrada.LeerEntero("\n  Cantidad a pedir: ", 1);

            Orden nOrden = new Orden();
            nOrden.numOrden = num;
            nOrden.producto = prodElegido;
            nOrden.proveedor = provElegido;
            nOrden.cantidad = cantidad;
            nOrden.precioTotal = prodElegido.precio * cantidad;

            nOrden.pagado = false;
            nOrden.estado = EN_ESPERA;

            listOrden.Add(nOrden);

            Console.WriteLine();
            Console.WriteLine("  Orden creada:");
            MostrarDatos(nOrden);
            Entrada.Pausa();
        }
        private void BorrarOrden(List<Orden> listOrden)
        {
            Entrada.Titulo("BORRAR ORDEN");

            if (listOrden.Count == 0)
            {
                Console.WriteLine("  No hay ordenes registradas.");
                Entrada.Pausa();
                return;
            }

            MostrarOrdenes(listOrden);

            string num = Entrada.LeerTexto("\n  Numero de la orden a borrar: ");
            int i = BuscarIndice(listOrden, num);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  No se encontro ninguna orden con ese numero.");
                Entrada.Pausa();
                return;
            }

            Orden orden = listOrden[i];
            if (orden.pagado)
            {
                Console.WriteLine();
                Console.WriteLine("  Esta orden ya fue pagada (estado: " + orden.estado + ").");
                Console.WriteLine("  Solo se pueden borrar ordenes en estado \"" + EN_ESPERA + "\".");
                Entrada.Pausa();
                return;
            }

            Console.WriteLine();
            MostrarDatos(orden);

            if (Entrada.Confirmar("\n  Seguro que quieres borrarla?"))
            {
                listOrden.RemoveAt(i);
                Console.WriteLine("  Orden borrada.");
            }
            else
            {
                Console.WriteLine("  Operacion cancelada.");
            }
            Entrada.Pausa();
        }

        private void ModificarOrden(List<Producto> listProd, List<Provedor> listProveedor, List<Orden> listOrden)
        {
            Entrada.Titulo("MODIFICAR ORDEN");

            if (listOrden.Count == 0)
            {
                Console.WriteLine("  No hay ordenes registradas.");
                Entrada.Pausa();
                return;
            }

            MostrarOrdenes(listOrden);

            string num = Entrada.LeerTexto("\n  Numero de la orden a modificar: ");
            int i = BuscarIndice(listOrden, num);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  No se encontro ninguna orden con ese numero.");
                Entrada.Pausa();
                return;
            }

            Orden orden = listOrden[i];
            if (orden.pagado)
            {
                Console.WriteLine();
                Console.WriteLine("  Esta orden ya fue pagada (estado: " + orden.estado + ").");
                Console.WriteLine("  Solo se pueden modificar ordenes en estado \"" + EN_ESPERA + "\".");
                Entrada.Pausa();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("  Datos actuales:");
            MostrarDatos(orden);

            Producto prodElegido = ElegirProducto(listProd);
            Provedor provElegido = ElegirProveedor(listProveedor);
            int cantidad = Entrada.LeerEntero("\n  Cantidad nueva: ", 1);

            orden.producto = prodElegido;
            orden.proveedor = provElegido;
            orden.cantidad = cantidad;

            orden.precioTotal = prodElegido.precio * cantidad;

            Console.WriteLine();
            Console.WriteLine("  Orden modificada:");
            MostrarDatos(orden);
            Entrada.Pausa();
        }

        private void MostrarOrdenes(List<Orden> listOrden)
        {
            Entrada.Titulo("ORDENES REGISTRADAS");

            if (listOrden.Count == 0)
            {
                Console.WriteLine("  No hay ordenes registradas.");
                return;
            }

            Console.WriteLine("  No.  Orden      Producto             Cant.        Total  Estado");
            Console.WriteLine("  ------------------------------------------------------------------");

            for (int i = 0; i < listOrden.Count; i++)
            {
                Orden o = listOrden[i];
                string linea = "  ";
                linea += (i + 1).ToString().PadRight(5);
                linea += o.numOrden.PadRight(11);
                linea += o.producto.nombre.PadRight(21);
                linea += o.cantidad.ToString().PadLeft(5);
                linea += ("$" + o.precioTotal.ToString("F2")).PadLeft(13);
                linea += "  " + o.estado;
                Console.WriteLine(linea);
            }

            Console.WriteLine("  ------------------------------------------------------------------");
            Console.WriteLine("  Total de ordenes: " + listOrden.Count);
        }

        private void BuscarOrden(List<Orden> listOrden)
        {
            Entrada.Titulo("BUSCAR ORDEN");

            if (listOrden.Count == 0)
            {
                Console.WriteLine("  No hay ordenes registradas.");
                Entrada.Pausa();
                return;
            }

            string texto = Entrada.LeerTexto("  Numero de orden, producto o proveedor a buscar: ");
            int encontrados = 0;

            Console.WriteLine();
            for (int i = 0; i < listOrden.Count; i++)
            {
                Orden o = listOrden[i];
                if (o.numOrden.ToLower().Contains(texto.ToLower()) ||
                    o.producto.nombre.ToLower().Contains(texto.ToLower()) ||
                    o.proveedor.nom.ToLower().Contains(texto.ToLower()))
                {
                    MostrarDatos(o);
                    Console.WriteLine();
                    encontrados++;
                }
            }

            if (encontrados == 0)
            {
                Console.WriteLine("  No se encontro ninguna orden que coincida.");
            }
            else
            {
                Console.WriteLine("  Coincidencias encontradas: " + encontrados);
            }
            Entrada.Pausa();
        }

        private void PagarOrden(List<Orden> listOrden)
        {
            Entrada.Titulo("PAGAR ORDEN");

            if (listOrden.Count == 0)
            {
                Console.WriteLine("  No hay ordenes registradas.");
                Entrada.Pausa();
                return;
            }

            MostrarOrdenes(listOrden);

            string num = Entrada.LeerTexto("\n  Numero de la orden a pagar: ");
            int i = BuscarIndice(listOrden, num);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  No se encontro ninguna orden con ese numero.");
                Entrada.Pausa();
                return;
            }

            Orden orden = listOrden[i];
            if (orden.pagado)
            {
                Console.WriteLine();
                Console.WriteLine("  Esta orden ya estaba pagada (estado: " + orden.estado + ").");
                Console.WriteLine("  Una orden solo se puede pagar una vez.");
                Entrada.Pausa();
                return;
            }

            Console.WriteLine();
            MostrarDatos(orden);
            Console.WriteLine();
            Console.WriteLine("  Monto a pagar: $" + orden.precioTotal.ToString("F2"));

            if (Entrada.Confirmar("\n  Confirmas el pago?"))
            {

                orden.pagado = true;
                orden.estado = EN_TRANSITO;

                Console.WriteLine();
                Console.WriteLine("  Pago registrado. La orden paso a estado \"" + EN_TRANSITO + "\".");
            }
            else
            {
                Console.WriteLine("  Pago cancelado.");
            }
            Entrada.Pausa();
        }

        private void CompletarOrden(List<Producto> listProd, List<Orden> listOrden)
        {
            Entrada.Titulo("COMPLETAR ORDEN");

            if (listOrden.Count == 0)
            {
                Console.WriteLine("  No hay ordenes registradas.");
                Entrada.Pausa();
                return;
            }

            MostrarOrdenes(listOrden);

            string num = Entrada.LeerTexto("\n  Numero de la orden a completar: ");
            int i = BuscarIndice(listOrden, num);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  No se encontro ninguna orden con ese numero.");
                Entrada.Pausa();
                return;
            }

            Orden orden = listOrden[i];

            if (orden.estado == COMPLETADA)
            {
                Console.WriteLine();
                Console.WriteLine("  Esta orden ya estaba completada.");
                Entrada.Pausa();
                return;
            }
            if (!orden.pagado)
            {
                Console.WriteLine();
                Console.WriteLine("  Esta orden todavia no se ha pagado (estado: " + orden.estado + ").");
                Console.WriteLine("  Primero pagala con la opcion 6.");
                Entrada.Pausa();
                return;
            }

            if (BuscarIndiceProducto(listProd, orden.producto) == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  El producto \"" + orden.producto.nombre + "\" ya no existe en el inventario.");
                Console.WriteLine("  No se puede completar la orden.");
                Entrada.Pausa();
                return;
            }

            Console.WriteLine();
            MostrarDatos(orden);
            Console.WriteLine();
            Console.WriteLine("  Inventario actual de \"" + orden.producto.nombre + "\": " + orden.producto.inventario);

            if (Entrada.Confirmar("\n  Confirmas que llego la mercancia?"))
            {

                orden.producto.inventario = orden.producto.inventario + orden.cantidad;
                orden.estado = COMPLETADA;

                Console.WriteLine();
                Console.WriteLine("  Orden completada.");
                Console.WriteLine("  Inventario nuevo de \"" + orden.producto.nombre + "\": " + orden.producto.inventario);
            }
            else
            {
                Console.WriteLine("  Operacion cancelada.");
            }
            Entrada.Pausa();
        }


        private Producto ElegirProducto(List<Producto> listProd)
        {
            Console.WriteLine();
            Console.WriteLine("  Productos disponibles:");
            for (int i = 0; i < listProd.Count; i++)
            {
                Console.WriteLine("   " + (i + 1) + ". " + listProd[i].nombre +
                                  "  (inventario: " + listProd[i].inventario +
                                  ", precio: $" + listProd[i].precio.ToString("F2") + ")");
            }
            int op = Entrada.LeerEntero("  Elige el producto: ", 1, listProd.Count);

            return listProd[op - 1];
        }

        private Provedor ElegirProveedor(List<Provedor> listProveedor)
        {
            Console.WriteLine();
            Console.WriteLine("  Proveedores disponibles:");
            for (int i = 0; i < listProveedor.Count; i++)
            {
                Console.WriteLine("   " + (i + 1) + ". " + listProveedor[i].nom +
                                  "  (ID: " + listProveedor[i].id + ")");
            }
            int op = Entrada.LeerEntero("  Elige el proveedor: ", 1, listProveedor.Count);
            return listProveedor[op - 1];
        }

        private int BuscarIndice(List<Orden> listOrden, string num)
        {
            for (int i = 0; i < listOrden.Count; i++)
            {
                if (listOrden[i].numOrden.ToLower() == num.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        private int BuscarIndiceProducto(List<Producto> listProd, Producto buscado)
        {
            for (int i = 0; i < listProd.Count; i++)
            {
                if (listProd[i] == buscado)
                {
                    return i;
                }
            }
            return -1;
        }

        private void MostrarDatos(Orden o)
        {
            Console.WriteLine("  Orden:      " + o.numOrden);
            Console.WriteLine("  Producto:   " + o.producto.nombre + "  ($" + o.producto.precio.ToString("F2") + " c/u)");
            Console.WriteLine("  Proveedor:  " + o.proveedor.nom + "  (ID: " + o.proveedor.id + ", tel: " + o.proveedor.numero + ")");
            Console.WriteLine("  Cantidad:   " + o.cantidad);
            Console.WriteLine("  Total:      $" + o.precioTotal.ToString("F2"));
            if (o.pagado)
            {
                Console.WriteLine("  Pagado:     Si");
            }
            else
            {
                Console.WriteLine("  Pagado:     No");
            }
            Console.WriteLine("  Estado:     " + o.estado);
        }
    }
}