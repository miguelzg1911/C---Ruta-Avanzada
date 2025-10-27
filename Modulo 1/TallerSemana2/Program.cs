using System;
using program.Services;
using TallerSemana2.Models;

namespace program;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- Bienvenido a RiwiMusic --\n");

        // 🔹 Listas centrales
        var clientes = new List<Customers>
        {
            new Customers {id = 1, nombre = "Miguel", email = "miguelzg1911@gmail.com", documento = "1013341669", telefono = "3042991403"},
            new Customers {id = 2, nombre = "Simon", email = "simon@gmail.com", documento = "1222442340", telefono = "3002993030"},
            new Customers {id = 3, nombre = "Daniel", email = "daniel@gmail.com", documento = "7858568545", telefono = "1234567890"},
            new Customers {id = 4, nombre = "Tobias", email = "tobias@gmail.com", documento = "0022002200", telefono = "0987654321"},
            new Customers {id = 5, nombre = "Fulano", email = "fulano@gmail.com", documento = "1234567890", telefono = "3213451239"}
        };

        var conciertos = new List<Concerts>
        {
            new Concerts { idConcierto = 1, cantante = "Maluma", lugar = "Medellin", fecha = new DateOnly(2026, 12, 06), idCliente = 2 },
            new Concerts { idConcierto = 2, cantante = "Juanes", lugar = "Medellin", fecha = new DateOnly(2025, 10, 18), idCliente = 1 },
            new Concerts { idConcierto = 3, cantante = "KarolG", lugar = "Bogota", fecha = new DateOnly(2024, 06, 20), idCliente = 3 },
            new Concerts { idConcierto = 4, cantante = "LuisMiguel", lugar = "Cali", fecha = new DateOnly(2026, 02, 10), idCliente = 4 },
            new Concerts { idConcierto = 5, cantante = "Shakira", lugar = "Barranquilla", fecha = new DateOnly(2025, 11, 19), idCliente = 5 },
        };

        var tiquetes = new List<Tickets>
        {
            new Tickets { idTiquete = 1, precio = 100.00, idCliente = 1, idConcierto = 2},
            new Tickets { idTiquete = 2, precio = 200.00, idCliente = 2, idConcierto = 1},
            new Tickets { idTiquete = 3, precio = 50.00, idCliente = 3, idConcierto = 3},
            new Tickets { idTiquete = 4, precio = 75.00, idCliente = 4, idConcierto = 4},
            new Tickets { idTiquete = 5, precio = 250.00, idCliente = 5, idConcierto = 5},
        };

        // 🔹 Pasar listas a los servicios
        var servicioClientes = new ClientesServices(clientes);
        var servicioConciertos = new ConciertosServices(conciertos);
        var servicioTiquetes = new TiquetesServices(tiquetes, clientes, conciertos);

        int opcion;
        do
        {
            Console.WriteLine("\n--- Menú Principal ---");
            Console.WriteLine("1. Gestión de Clientes");
            Console.WriteLine("2. Gestión de Conciertos");
            Console.WriteLine("3. Gestión de Tiquetes");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Entrada inválida. Intente de nuevo.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    servicioClientes.MenuClientes();
                    break;
                case 2:
                    servicioConciertos.MenuConciertos();
                    break;
                case 3:
                    servicioTiquetes.MenuTiquetes();
                    break;
                case 4:
                    Console.WriteLine("Gracias por usar RiwiMusic. ¡Hasta pronto!");
                    break;
                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }

        } while (opcion != 4);
    }
}
