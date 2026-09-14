using PuntoDeVenta_proyecto1.Objetos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeVenta_proyecto1
{
    internal class ModuloInventario
    {
        public void menuProducto(List<Producto> listProduct)
        {
            int op = 0;
            while (op != 6)
            {
                Entrada.Titulo("MODULO INVENTARIO");
                Console.WriteLine("  1. Agregar Producto");
                Console.WriteLine("  2. Borrar Producto");
                Console.WriteLine("  3. Modificar Producto");
                Console.WriteLine("  4. Mostrar Productos");
                Console.WriteLine("  5. Buscar Producto");
                Console.WriteLine("  6. Regresar al menu principal");
                Console.WriteLine("");
                op = Entrada.LeerEntero("  Opcion: ", 1, 6);

                switch (op)
                {
                    case 1:
                        AgregarProducto(listProduct);
                        break;
                    case 2:
                        BorrarProducto(listProduct);
                        break;
                    case 3:
                        ModificarProducto(listProduct);
                        break;
                    case 4:
                        MostrarProductos(listProduct);
                        Entrada.Pausa();
                        break;
                    case 5:
                        BuscarProducto(listProduct);
                        break;
                    case 6:
                        break;
                }
            }
        }

        private void AgregarProducto(List<Producto> listProduct)
        {
            Entrada.Titulo("AGREGAR PRODUCTO");

            string nombre = Entrada.LeerTexto("Nombre del producto: ");

            if (BuscarIndice(listProduct, nombre) != -1)
            {
                Console.WriteLine();
                Console.WriteLine("Ya existe un producto con ese nombre. No se agrego nada");
                Entrada.Pausa();
                return;
            }

            Producto nProd = new Producto();
            nProd.nombre = nombre;
            nProd.inventario = Entrada.LeerEntero("Cantidad en inventario: ", 0);
            nProd.precio = Entrada.LeerDecimal("Precio unitario: ", 0);

            listProduct.Add(nProd);

            Console.WriteLine();
            Console.WriteLine("Producto \"" + nProd.nombre + "\" agregado correctamente");
            Entrada.Pausa();
        }

        private void BorrarProducto(List<Producto> listProduct)
        {
            Entrada.Titulo("BORRAR PRODUCTO");

            if (listProduct.Count == 0)
            {
                Console.WriteLine("No hay productos registrados");
                Entrada.Pausa();
                return;
            }

            MostrarProductos(listProduct);

            string nombre = Entrada.LeerTexto("\n Nombre del producto a borrar: ");
            int i = BuscarIndice(listProduct, nombre);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("No se encontro ningun producto con ese nombre");
                Entrada.Pausa();
                return;
            }

            Console.WriteLine();
            MostrarDatos(listProduct[i]);

            if (Entrada.Confirmar("\nSeguro que quieres borrarlo?"))
            {
                listProduct.RemoveAt(i);
                Console.WriteLine("Producto borrado");
            }
            else
            {
                Console.WriteLine("Operacion cancelada");
            }
            Entrada.Pausa();
        }

        private void ModificarProducto(List<Producto> listProduct)
        {
            Entrada.Titulo("MODIFICAR PRODUCTO");

            if (listProduct.Count == 0)
            {
                Console.WriteLine("No hay productos registrados");
                Entrada.Pausa();
                return;
            }

            MostrarProductos(listProduct);

            string nombre = Entrada.LeerTexto("\nNombre del producto a modificar: ");
            int i = BuscarIndice(listProduct, nombre);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("No se encontro ningun producto con ese nombre");
                Entrada.Pausa();
                return;
            }

            Producto prod = listProduct[i];

            Console.WriteLine();
            Console.WriteLine("Datos actuales:");
            MostrarDatos(prod);
            Console.WriteLine();
            Console.WriteLine("Escribe los datos nuevos:");

            string nuevoNombre = Entrada.LeerTexto("Nombre nuevo: ");

            int repetido = BuscarIndice(listProduct, nuevoNombre);
            if (repetido != -1 && repetido != i)
            {
                Console.WriteLine();
                Console.WriteLine("Ya existe otro producto con ese nombre. No se modifico nada");
                Entrada.Pausa();
                return;
            }

            prod.nombre = nuevoNombre;
            prod.inventario = Entrada.LeerEntero("Inventario nuevo: ", 0);
            prod.precio = Entrada.LeerDecimal("Precio nuevo: ", 0);

            Console.WriteLine();
            Console.WriteLine("Producto modificado correctamente");
            Entrada.Pausa();
        }

        private void MostrarProductos(List<Producto> listProduct)
        {
            Entrada.Titulo("PRODUCTOS REGISTRADOS");

            if (listProduct.Count == 0)
            {
                Console.WriteLine("No hay productos registrados");
                return;
            }

            Console.WriteLine("  No.  Nombre                         Inventario       Precio");
            Console.WriteLine("  ----------------------------------------------------------");

            for (int i = 0; i < listProduct.Count; i++)
            {
                Producto p = listProduct[i];
                string linea = "  ";
                linea += (i + 1).ToString().PadRight(5);
                linea += p.nombre.PadRight(31);
                linea += p.inventario.ToString().PadLeft(10);
                linea += ("$" + p.precio.ToString("F2")).PadLeft(13);
                Console.WriteLine(linea);
            }

            Console.WriteLine("  ----------------------------------------------------------");
            Console.WriteLine("  Total de productos: " + listProduct.Count);
        }

        private void BuscarProducto(List<Producto> listProduct)
        {
            Entrada.Titulo("BUSCAR PRODUCTO");

            if (listProduct.Count == 0)
            {
                Console.WriteLine("No hay productos registrados.");
                Entrada.Pausa();
                return;
            }

            string texto = Entrada.LeerTexto("  Nombre (o parte del nombre) a buscar: ");
            int encontrados = 0;

            Console.WriteLine();
            for (int i = 0; i < listProduct.Count; i++)
            {

                if (listProduct[i].nombre.ToLower().Contains(texto.ToLower()))
                {
                    MostrarDatos(listProduct[i]);
                    Console.WriteLine();
                    encontrados++;
                }
            }

            if (encontrados == 0)
            {
                Console.WriteLine("  No se encontro ningun producto que coincida.");
            }
            else
            {
                Console.WriteLine("  Coincidencias encontradas: " + encontrados);
            }
            Entrada.Pausa();
        }

        private int BuscarIndice(List<Producto> listProduct, string nombre)
        {
            for (int i = 0; i < listProduct.Count; i++)
            {
                if (listProduct[i].nombre.ToLower() == nombre.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }
        private void MostrarDatos(Producto p)
        {
            Console.WriteLine("  Nombre:     " + p.nombre);
            Console.WriteLine("  Inventario: " + p.inventario);
            Console.WriteLine("  Precio:     $" + p.precio.ToString("F2"));
        }
    }
}