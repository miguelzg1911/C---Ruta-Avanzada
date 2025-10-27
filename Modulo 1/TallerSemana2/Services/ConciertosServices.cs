using System;
using System.Collections.Generic;
using System.Linq;
using TallerSemana2.Models;

namespace program.Services
{
    public class ConciertosServices
    {
        private List<Concerts> conciertos;

        public ConciertosServices(List<Concerts> conciertos)
        {
            this.conciertos = conciertos;
        }

        private void CreateConcierto()
        {
            Console.Write("ID concierto: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (conciertos.Any(c => c.idConcierto == id))
            {
                Console.WriteLine("Ese ID ya existe.");
                return;
            }

            Console.Write("Cantante: ");
            string cantante = Console.ReadLine();

            Console.Write("Lugar: ");
            string lugar = Console.ReadLine();

            Console.Write("Fecha (yyyy-mm-dd): ");
            DateOnly fecha = DateOnly.Parse(Console.ReadLine());

            Console.Write("ID cliente organizador: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            conciertos.Add(new Concerts
            {
                idConcierto = id,
                cantante = cantante,
                lugar = lugar,
                fecha = fecha,
                idCliente = idCliente
            });

            Console.WriteLine("Concierto registrado con éxito.");
        }

        private void ReadConciertos()
        {
            Console.WriteLine("\nLista de conciertos:");
            foreach (var c in conciertos)
            {
                Console.WriteLine($"ID: {c.idConcierto} | {c.cantante} en {c.lugar} | {c.fecha}");
            }
        }

        private void UpdateConcierto()
        {
            Console.Write("ID del concierto a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var concierto = conciertos.FirstOrDefault(c => c.idConcierto == id);
            if (concierto == null)
            {
                Console.WriteLine("No existe ese concierto.");
                return;
            }

            Console.Write("Nuevo cantante: ");
            concierto.cantante = Console.ReadLine();

            Console.Write("Nuevo lugar: ");
            concierto.lugar = Console.ReadLine();

            Console.Write("Nueva fecha (yyyy-mm-dd): ");
            concierto.fecha = DateOnly.Parse(Console.ReadLine());

            Console.WriteLine("Concierto actualizado.");
        }

        private void DeleteConcierto()
        {
            Console.Write("ID del concierto a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var concierto = conciertos.FirstOrDefault(c => c.idConcierto == id);
            if (concierto == null)
            {
                Console.WriteLine("No existe ese concierto.");
                return;
            }

            conciertos.Remove(concierto);
            Console.WriteLine("Concierto eliminado.");
        }

        public void MenuConciertos()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- Menú de Conciertos ---");
                Console.WriteLine("1. Crear concierto");
                Console.WriteLine("2. Listar conciertos");
                Console.WriteLine("3. Editar concierto");
                Console.WriteLine("4. Eliminar concierto");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1: CreateConcierto(); break;
                    case 2: ReadConciertos(); break;
                    case 3: UpdateConcierto(); break;
                    case 4: DeleteConcierto(); break;
                    case 5: Console.WriteLine("Volviendo..."); break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            } while (opcion != 5);
        }
    }
}
