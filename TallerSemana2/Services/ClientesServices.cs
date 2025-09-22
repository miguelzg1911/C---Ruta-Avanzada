using System;
using System.Collections.Generic;
using System.Linq;
using TallerSemana2.Models;

namespace program.Services
{
    public class ClientesServices
    {
        private List<Customers> clientes;

        public ClientesServices(List<Customers> clientes)
        {
            this.clientes = clientes;
        }

        private void CreateCliente()
        {
            Console.Write("Ingresa su id: ");
            int nuevoId = Convert.ToInt32(Console.ReadLine());

            if (clientes.Any(c => c.id == nuevoId))
            {
                Console.WriteLine("El id del cliente ya existe");
                return;
            }

            Console.Write("Ingresa el nombre del cliente: ");
            string nuevoNombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nuevoNombre))
            {
                Console.WriteLine("El nombre no puede estar vacío");
                return;
            }

            Console.Write("Ingresa el email: ");
            string nuevoEmail = Console.ReadLine();
            if (!nuevoEmail.Contains("@"))
            {
                Console.WriteLine("El email no es válido");
                return;
            }

            Console.Write("Ingresa el documento: ");
            string nuevoDocumento = Console.ReadLine();

            Console.Write("Ingresa el teléfono: ");
            string nuevoTelefono = Console.ReadLine();

            clientes.Add(new Customers
            {
                id = nuevoId,
                nombre = nuevoNombre,
                email = nuevoEmail,
                documento = nuevoDocumento,
                telefono = nuevoTelefono
            });

            Console.WriteLine("Cliente registrado con éxito.");
        }

        private void ReadClientes()
        {
            Console.WriteLine("\nLista de clientes:");
            foreach (var c in clientes)
            {
                Console.WriteLine($"Cliente: {c.id} | {c.nombre} | {c.email} | {c.documento} | {c.telefono}");
            }
        }

        private void UpdateCliente()
        {
            Console.Write("Ingresa el id del cliente a editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var clienteExistente = clientes.FirstOrDefault(c => c.id == id);
            if (clienteExistente == null)
            {
                Console.WriteLine("El id del cliente no existe");
                return;
            }

            Console.Write("Nuevo nombre: ");
            string nuevoNombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nuevoNombre))
            {
                Console.WriteLine("El nombre no puede estar vacío");
                return;
            }

            Console.Write("Nuevo email: ");
            string nuevoEmail = Console.ReadLine();
            if (!nuevoEmail.Contains("@"))
            {
                Console.WriteLine("El email no es válido");
                return;
            }

            Console.Write("Nuevo documento: ");
            string nuevoDocumento = Console.ReadLine();

            Console.Write("Nuevo teléfono: ");
            string nuevoTelefono = Console.ReadLine();

            clienteExistente.nombre = nuevoNombre;
            clienteExistente.email = nuevoEmail;
            clienteExistente.documento = nuevoDocumento;
            clienteExistente.telefono = nuevoTelefono;

            Console.WriteLine("Cliente actualizado con éxito.");
        }

        private void DeleteCliente()
        {
            Console.Write("Ingresa el id del cliente a eliminar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            var clienteExistente = clientes.FirstOrDefault(c => c.id == id);
            if (clienteExistente == null)
            {
                Console.WriteLine("El cliente no existe");
                return;
            }

            clientes.Remove(clienteExistente);
            Console.WriteLine("Cliente eliminado con éxito.");
        }

        public void MenuClientes()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- Menú de Clientes ---");
                Console.WriteLine("1. Crear cliente");
                Console.WriteLine("2. Listar clientes");
                Console.WriteLine("3. Editar cliente");
                Console.WriteLine("4. Eliminar cliente");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1: CreateCliente(); break;
                    case 2: ReadClientes(); break;
                    case 3: UpdateCliente(); break;
                    case 4: DeleteCliente(); break;
                    case 5: Console.WriteLine("Volviendo..."); break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            } while (opcion != 5);
        }
    }
}
