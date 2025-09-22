using System;
using System.Collections.Generic;
using System.Linq;
using TallerSemana2.Models;

namespace program.Services
{
    public class TiquetesServices
    {
        private List<Tickets> tiquetes;
        private List<Customers> clientes;
        private List<Concerts> conciertos;

        public TiquetesServices(List<Tickets> tiquetes, List<Customers> clientes, List<Concerts> conciertos)
        {
            this.tiquetes = tiquetes;
            this.clientes = clientes;
            this.conciertos = conciertos;
        }

        private void CreateTiquete()
        {
            Console.Write("ID tiquete: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (tiquetes.Any(t => t.idTiquete == id))
            {
                Console.WriteLine("Ese ID ya existe.");
                return;
            }

            Console.Write("Precio: ");
            double precio = Convert.ToDouble(Console.ReadLine());

            Console.Write("ID cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            Console.Write("ID concierto: ");
            int idConcierto = Convert.ToInt32(Console.ReadLine());

            tiquetes.Add(new Tickets { idTiquete = id, precio = precio, idCliente = idCliente, idConcierto = idConcierto });

            Console.WriteLine("Compra registrada.");
        }

        private void ReadTiquetes()
        {
            Console.WriteLine("\nLista de tiquetes:");
            foreach (var t in tiquetes)
            {
                Console.WriteLine($"Tiquete {t.idTiquete} | Cliente {t.idCliente} | Concierto {t.idConcierto} | Precio {t.precio}");
            }
        }

        private void UpdateTiquete()
        {
            Console.Write("ID tiquete a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var tiquete = tiquetes.FirstOrDefault(t => t.idTiquete == id);
            if (tiquete == null)
            {
                Console.WriteLine("No existe ese tiquete.");
                return;
            }

            Console.Write("Nuevo precio: ");
            tiquete.precio = Convert.ToDouble(Console.ReadLine());

            Console.Write("Nuevo ID cliente: ");
            tiquete.idCliente = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nuevo ID concierto: ");
            tiquete.idConcierto = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Tiquete actualizado.");
        }

        private void DeleteTiquete()
        {
            Console.Write("ID tiquete a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var tiquete = tiquetes.FirstOrDefault(t => t.idTiquete == id);
            if (tiquete == null)
            {
                Console.WriteLine("No existe ese tiquete.");
                return;
            }

            tiquetes.Remove(tiquete);
            Console.WriteLine("Tiquete eliminado.");
        }

        private void HistorialCompras()
        {
            Console.Write("ID del cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            var cliente = clientes.FirstOrDefault(c => c.id == idCliente);
            if (cliente == null)
            {
                Console.WriteLine("Ese cliente no existe.");
                return;
            }

            var compras = from t in tiquetes
                          join c in conciertos on t.idConcierto equals c.idConcierto
                          where t.idCliente == idCliente
                          select new { t.idTiquete, t.precio, c.cantante, c.lugar, c.fecha };

            Console.WriteLine($"\nHistorial de {cliente.nombre}:");
            if (!compras.Any())
            {
                Console.WriteLine("No tiene compras.");
                return;
            }

            foreach (var compra in compras)
            {
                Console.WriteLine($"Tiquete {compra.idTiquete} | {compra.cantante} en {compra.lugar} el {compra.fecha} | ${compra.precio}");
            }
        }

        public void MenuTiquetes()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- Menú de Tiquetes ---");
                Console.WriteLine("1. Registrar compra");
                Console.WriteLine("2. Listar tiquetes");
                Console.WriteLine("3. Editar compra");
                Console.WriteLine("4. Eliminar compra");
                Console.WriteLine("5. Historial de compras");
                Console.WriteLine("6. Volver");
                Console.Write("Seleccione: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1: CreateTiquete(); break;
                    case 2: ReadTiquetes(); break;
                    case 3: UpdateTiquete(); break;
                    case 4: DeleteTiquete(); break;
                    case 5: HistorialCompras(); break;
                    case 6: Console.WriteLine("Volviendo..."); break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            } while (opcion != 6);
        }
    }
}
