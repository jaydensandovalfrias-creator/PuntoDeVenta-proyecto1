using System;

namespace PuntoDeVenta_proyecto1
{
    static class Entrada
    {
        public static string LeerTexto(string mensaje)
        {
            string valor = "";
            while (valor == "")
            {
                Console.Write(mensaje);
                string? linea = Console.ReadLine();
                if (linea != null)
                {
                    valor = linea.Trim();
                }
                if (valor == "")
                {
                    Console.WriteLine("El dato no puede quedar vacio.");
                }
            }
            return valor;
        }

        public static int LeerEntero(string mensaje, int minimo)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? linea = Console.ReadLine();
                int valor;
                if (int.TryParse(linea, out valor) && valor >= minimo)
                {
                    return valor;
                }
                Console.WriteLine("Dato invalido. Escribe un numero entero mayor o igual a " + minimo + ".");
            }
        }

        public static int LeerEntero(string mensaje, int minimo, int maximo)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? linea = Console.ReadLine();
                int valor;
                if (int.TryParse(linea, out valor) && valor >= minimo && valor <= maximo)
                {
                    return valor;
                }
                Console.WriteLine("Dato invalido. Escribe un numero entre " + minimo + " y " + maximo + ".");
            }
        }

        public static double LeerDecimal(string mensaje, double minimo)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? linea = Console.ReadLine();
                double valor;
                if (double.TryParse(linea, out valor) && valor >= minimo)
                {
                    return valor;
                }
                Console.WriteLine("Dato invalido. Escribe un numero mayor o igual a " + minimo + ".");
            }
        }
        public static bool Confirmar(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje + " (s/n): ");
                string? linea = Console.ReadLine();
                string resp = "";
                if (linea != null)
                {
                    resp = linea.Trim().ToLower();
                }
                if (resp == "s" || resp == "si") return true;
                if (resp == "n" || resp == "no") return false;
                Console.WriteLine("  > Contesta con s o n.");
            }
        }

        public static void Pausa()
        {
            Console.WriteLine();
            Console.Write("Presiona ENTER para continuar...");
            Console.ReadLine();
        }


        public static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.WriteLine("  " + texto);
        }
    }
}