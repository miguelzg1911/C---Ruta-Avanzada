using System;
using System.ComponentModel.Design;

namespace ProyectoTienda;

public class Tienda
{
    static double totalCompra = 0;
    static List<string> historialCompras = new List<string>();

    public static void productosIniciales()
    {
        string[] producto = new string[4] { "gomitas", "lokiños", "chicles", "papitas" };
        double[] precio = new double[4] { 2000, 300, 500, 3000 };
        int[] cantidad = new int[4] { 10, 30, 35, 10 };

        mostrarProductos(producto, precio, cantidad);

        bool compro = comprarProductos(producto, precio, cantidad);

        if (compro)
        {
            seguirComprando(producto, precio, cantidad);
        }
    }

    public static void mostrarProductos(string[] producto, double[] precio, int[] cantidad)
    {
        Console.WriteLine("---BIENVENIDO A LA TIENDA DE ANA---\n");

        Console.WriteLine("Productos disponibles: ");
        for (int i = 0; i < producto.Length; i++)
        {
            Console.WriteLine(
                $"{i + 1}. | Producto: {producto[i]}, | Precio: {precio[i]}, | Cantidad: {cantidad[i]}\n");
        }
    }

    public static bool comprarProductos(string[] producto, double[] precio, int[] cantidad)
    {
        Console.Write("¿¿Deseas comprar algun producto?? (s/n): ");
        string comprar = Console.ReadLine();

        if (comprar == "s")
        {
            mostrarProductos(producto, precio, cantidad);
            int i = -1;

            while (true)
            {
                Console.Write("Ingresa el indice del producto que deseas comprar: ");
                int comprarProducto = Convert.ToInt32(Console.ReadLine());

                i = comprarProducto - 1;

                if (comprarProducto >= 1 && comprarProducto <= producto.Length)
                {
                    Console.WriteLine(
                        $"El producto | {producto[i]} | precio:{precio[i]} | cantidad: {cantidad[i]} | si existe \n");
                    break;
                }
                else
                {
                    Console.WriteLine("¡¡Este producto no existe!!");
                }
            }

            Console.Write("¿Cuantas unidades quieres comprar?: \n");
            int cantidadUnidades = Convert.ToInt32(Console.ReadLine());

            if (cantidadUnidades <= cantidad[i])
            {
                cantidad[i] -= cantidadUnidades;

                totalCompra += cantidadUnidades * precio[i];

                double subTotal = cantidadUnidades * precio[i];

                historialCompras.Add($"{cantidadUnidades} {producto[i]} = {cantidadUnidades * precio[i]}");
                Console.WriteLine("¡¡Compra realizada con exito.!!");
                Console.WriteLine($"Has comprado {cantidadUnidades} {producto[i]}");
                Console.WriteLine($"Subtotal: {subTotal}");
                Console.WriteLine($"Total acumulado: {totalCompra}");
                Console.WriteLine($"Stock restante de {producto[i]}: {cantidad[i]} unidades\n");
                
                return true;
            }
            else
            {
                Console.WriteLine("¡¡No hay suficiente cantidad disponible!!");
                return true;
            }
        }
        else
        {
            Console.WriteLine("¡¡GRACIAS POR VISITAR LA TIENDA, VUELVE PRONTO!!");
            return false;
        }
    }
        

    public static void seguirComprando(string[] producto, double[] precio, int[] cantidad)
    {
        while (true)
        {
            Console.Write("¿Deseas seguir comprando? (s/n): ");
            string respuesta = Console.ReadLine().ToLower();
            if (respuesta == "s")
            {
                mostrarProductos(producto, precio, cantidad);
                comprarProductos(producto, precio, cantidad);
            }
            else if (respuesta == "n")
            {
                generarTicket(totalCompra);
                break;
            }
            else
            {
                Console.WriteLine("¡¡Ingresa un dato valido!!");    
            }
        }
    }

    
    public static double descuentos(double totalCompra)
    {
        double descuento = 0;

        if (totalCompra > 20000)
        {
            descuento = totalCompra * 0.20;
            Console.WriteLine("Se aplicó un descuento del 20%.");
        }
        else if (totalCompra > 10000)
        {
            descuento = totalCompra * 0.10;
            Console.WriteLine("Se aplicó un descuento del 10%.");
        }
        else
        {
            Console.WriteLine("No aplica descuento.");
        }

        double totalFinal = totalCompra - descuento;
        Console.WriteLine($"Descuento aplicado: {descuento}");

        return totalFinal;
    }

    public static void generarTicket(double totalCompra)
    {
        Console.WriteLine("\n--- TICKET DE COMPRA ---");
        
        Console.WriteLine("Productos comprados:");
        foreach (string item in historialCompras)
        {
            Console.WriteLine(item);
        }
        
        double totalConDescuento = descuentos(totalCompra);

        Console.WriteLine($"\nTOTAL A PAGAR: {totalConDescuento}");

        // Mensaje final
        Console.WriteLine("\n¡¡GRACIAS POR COMPRAR EN LA TIENDA DE ANA!!");
    }
}