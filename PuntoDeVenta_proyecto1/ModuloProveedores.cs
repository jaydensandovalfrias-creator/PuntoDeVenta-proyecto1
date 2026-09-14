using PuntoDeVenta_proyecto1.Objetos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PuntoDeVenta_proyecto1
{
    class ModuloProveedores
    {
        public void menuProveedores(List<Provedor> listProv)
        {
            int op = 0;
            while (op != 6)
            {
                Entrada.Titulo("MODULO PROVEEDORES");
                Console.WriteLine("  1. Agregar Proveedor");
                Console.WriteLine("  2. Borrar Proveedor");
                Console.WriteLine("  3. Modificar Proveedor");
                Console.WriteLine("  4. Mostrar Proveedores");
                Console.WriteLine("  5. Buscar Proveedor");
                Console.WriteLine("  6. Regresar al menu principal");
                Console.WriteLine("--------------------------------------------------");
                op = Entrada.LeerEntero("  Opcion: ", 1, 6);

                switch (op)
                {
                    case 1:
                        AgregarProveedor(listProv);
                        break;
                    case 2:
                        BorrarProveedor(listProv);
                        break;
                    case 3:
                        ModificarProveedor(listProv);
                        break;
                    case 4:
                        MostrarProveedores(listProv);
                        Entrada.Pausa();
                        break;
                    case 5:
                        BuscarProveedor(listProv);
                        break;
                    case 6:
                        break;
                }
            }
        }

        private void AgregarProveedor(List<Provedor> listProv)
        {
            Entrada.Titulo("AGREGAR PROVEEDOR");

            string id = Entrada.LeerTexto("  ID del proveedor: ");

            if (BuscarIndice(listProv, id) != -1)
            {
                Console.WriteLine();
                Console.WriteLine("  Ya existe un proveedor con ese ID. No se agrego nada.");
                Entrada.Pausa();
                return;
            }

            Provedor nProv = new Provedor();
            nProv.id = id;
            nProv.nom = Entrada.LeerTexto("  Nombre del proveedor: ");
            nProv.numero = Entrada.LeerTexto("  Numero de telefono: ");

            listProv.Add(nProv);

            Console.WriteLine();
            Console.WriteLine("  Proveedor \"" + nProv.nom + "\" agregado correctamente.");
            Entrada.Pausa();
        }

        private void BorrarProveedor(List<Provedor> listProv)
        {
            Entrada.Titulo("BORRAR PROVEEDOR");

            if (listProv.Count == 0)
            {
                Console.WriteLine("  No hay proveedores registrados.");
                Entrada.Pausa();
                return;
            }

            MostrarProveedores(listProv);

            string id = Entrada.LeerTexto("\n  ID del proveedor a borrar: ");
            int i = BuscarIndice(listProv, id);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  No se encontro ningun proveedor con ese ID.");
                Entrada.Pausa();
                return;
            }

            Console.WriteLine();
            MostrarDatos(listProv[i]);

            if (Entrada.Confirmar("\n  Seguro que quieres borrarlo?"))
            {
                listProv.RemoveAt(i);
                Console.WriteLine("  Proveedor borrado.");
            }
            else
            {
                Console.WriteLine("  Operacion cancelada.");
            }
            Entrada.Pausa();
        }

        private void ModificarProveedor(List<Provedor> listProv)
        {
            Entrada.Titulo("MODIFICAR PROVEEDOR");

            if (listProv.Count == 0)
            {
                Console.WriteLine("  No hay proveedores registrados.");
                Entrada.Pausa();
                return;
            }

            MostrarProveedores(listProv);

            string id = Entrada.LeerTexto("\n  ID del proveedor a modificar: ");
            int i = BuscarIndice(listProv, id);

            if (i == -1)
            {
                Console.WriteLine();
                Console.WriteLine("  No se encontro ningun proveedor con ese ID.");
                Entrada.Pausa();
                return;
            }

            Provedor prov = listProv[i];

            Console.WriteLine();
            Console.WriteLine("  Datos actuales:");
            MostrarDatos(prov);
            Console.WriteLine();
            Console.WriteLine("  Escribe los datos nuevos:");

            string nuevoId = Entrada.LeerTexto("  ID nuevo: ");

            int repetido = BuscarIndice(listProv, nuevoId);
            if (repetido != -1 && repetido != i)
            {
                Console.WriteLine();
                Console.WriteLine("  Ya existe otro proveedor con ese ID. No se modifico nada.");
                Entrada.Pausa();
                return;
            }

            prov.id = nuevoId;
            prov.nom = Entrada.LeerTexto("  Nombre nuevo: ");
            prov.numero = Entrada.LeerTexto("  Numero nuevo: ");

            Console.WriteLine();
            Console.WriteLine("  Proveedor modificado correctamente.");
            Entrada.Pausa();
        }


        private void MostrarProveedores(List<Provedor> listProv)
        {
            Entrada.Titulo("PROVEEDORES REGISTRADOS");

            if (listProv.Count == 0)
            {
                Console.WriteLine("  No hay proveedores registrados.");
                return;
            }

            Console.WriteLine("  No.  ID              Nombre                    Telefono");
            Console.WriteLine("  ----------------------------------------------------------");

            for (int i = 0; i < listProv.Count; i++)
            {
                Provedor p = listProv[i];
                string linea = "  ";
                linea += (i + 1).ToString().PadRight(5);
                linea += p.id.PadRight(16);
                linea += p.nom.PadRight(26);
                linea += p.numero;
                Console.WriteLine(linea);
            }

            Console.WriteLine("  ----------------------------------------------------------");
            Console.WriteLine("  Total de proveedores: " + listProv.Count);
        }

        private void BuscarProveedor(List<Provedor> listProv)
        {
            Entrada.Titulo("BUSCAR PROVEEDOR");

            if (listProv.Count == 0)
            {
                Console.WriteLine("  No hay proveedores registrados.");
                Entrada.Pausa();
                return;
            }

            string texto = Entrada.LeerTexto("  ID o nombre a buscar: ");
            int encontrados = 0;

            Console.WriteLine();
            for (int i = 0; i < listProv.Count; i++)
            {
                Provedor p = listProv[i];
                if (p.id.ToLower().Contains(texto.ToLower()) ||
                    p.nom.ToLower().Contains(texto.ToLower()))
                {
                    MostrarDatos(p);
                    Console.WriteLine();
                    encontrados++;
                }
            }

            if (encontrados == 0)
            {
                Console.WriteLine("  No se encontro ningun proveedor que coincida.");
            }
            else
            {
                Console.WriteLine("  Coincidencias encontradas: " + encontrados);
            }
            Entrada.Pausa();
        }

        private int BuscarIndice(List<Provedor> listProv, string id)
        {
            for (int i = 0; i < listProv.Count; i++)
            {
                if (listProv[i].id.ToLower() == id.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        private void MostrarDatos(Provedor p)
        {
            Console.WriteLine("  ID:       " + p.id);
            Console.WriteLine("  Nombre:   " + p.nom);
            Console.WriteLine("  Telefono: " + p.numero);
        }
    }
}
