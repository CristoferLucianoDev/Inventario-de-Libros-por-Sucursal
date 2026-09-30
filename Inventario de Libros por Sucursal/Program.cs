namespace Inventario_de_Libros_por_Sucursal;

/// <summary>
/// Punto de entrada de la aplicación: muestra un menú de consola que permite
/// consultar y actualizar el inventario de libros por sucursal.
/// </summary>
internal class Program
{
    static readonly BranchInventory inventory = new();

    static void Main(string[] args)
    {
        string option;

        // Bucle principal del menú: se repite hasta que el usuario elija "0" (Salir).
        do
        {
            Console.Clear();
            Console.WriteLine("====== INVENTARIO POR SUCURSAL ======\n");
            Console.WriteLine("1. Mostrar inventario completo");
            Console.WriteLine("2. Consultar disponibilidad");
            Console.WriteLine("3. Actualizar disponibilidad");
            Console.WriteLine("4. Total disponible de un libro");
            Console.WriteLine("5. Total de inventario por sucursal");
            Console.WriteLine("6. Mostrar libros con bajo inventario");
            Console.WriteLine("7. Sucursal con mayor disponibilidad");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");
            option = Console.ReadLine() ?? string.Empty;

            switch (option)
            {
                case "1": ShowInventory(); break;
                case "2": CheckAvailability(); break;
                case "3": UpdateAvailability(); break;
                case "4": BookTotal(); break;
                case "5": BranchTotal(); break;
                case "6": LowInventory(); break;
                case "7": BranchWithMostStock(); break;
                case "0": Console.WriteLine("\nSaliendo del sistema..."); break;
                default:
                    Console.WriteLine("\nOpción no válida.");
                    Pause();
                    break;
            }
        } while (option != "0");
    }

    /// <summary>Opción 1: muestra la tabla completa de inventario.</summary>
    static void ShowInventory()
    {
        Console.Clear();
        inventory.ShowInventory();
        Pause();
    }

    /// <summary>Opción 2: pide un libro y una sucursal, y muestra la cantidad disponible.</summary>
    static void CheckAvailability()
    {
        Console.Clear();
        int book = SelectBook();
        int branch = SelectBranch();

        int quantity = inventory.GetQuantity(book, branch);
        Console.WriteLine($"\n{inventory.GetBookName(book)} en sucursal {inventory.GetBranchName(branch)}: {quantity} unidades.");
        Pause();
    }

    /// <summary>Opción 3: pide un libro, una sucursal y una nueva cantidad, y actualiza el inventario.</summary>
    static void UpdateAvailability()
    {
        Console.Clear();
        int book = SelectBook();
        int branch = SelectBranch();

        Console.WriteLine($"\nCantidad actual: {inventory.GetQuantity(book, branch)}");
        int quantity = ReadInt("Nueva cantidad: ", 0, int.MaxValue);

        inventory.UpdateQuantity(book, branch, quantity);
        Console.WriteLine("\nDisponibilidad actualizada correctamente.");
        Pause();
    }

    /// <summary>Opción 4: pide un libro y muestra su total disponible en todas las sucursales.</summary>
    static void BookTotal()
    {
        Console.Clear();
        int book = SelectBook();

        int total = inventory.GetBookTotal(book);
        Console.WriteLine($"\nTotal de {inventory.GetBookName(book)} en todas las sucursales: {total} unidades.");
        Pause();
    }

    /// <summary>Opción 5: pide una sucursal y muestra el total de inventario que tiene.</summary>
    static void BranchTotal()
    {
        Console.Clear();
        int branch = SelectBranch();

        int total = inventory.GetBranchTotal(branch);
        Console.WriteLine($"\nInventario total de la sucursal {inventory.GetBranchName(branch)}: {total} unidades.");
        Pause();
    }

    /// <summary>Opción 6: muestra los libros con inventario bajo en alguna sucursal.</summary>
    static void LowInventory()
    {
        Console.Clear();
        inventory.ShowLowInventory();
        Pause();
    }

    /// <summary>Opción 7: pide un libro y muestra la sucursal con mayor disponibilidad de ese libro.</summary>
    static void BranchWithMostStock()
    {
        Console.Clear();
        int book = SelectBook();

        int branch = inventory.GetBranchWithMostStock(book);
        Console.WriteLine($"\nLa sucursal con mayor disponibilidad de {inventory.GetBookName(book)} es " +
                          $"{inventory.GetBranchName(branch)} con {inventory.GetQuantity(book, branch)} unidades.");
        Pause();
    }

    /// <summary>Lista los libros disponibles y devuelve el índice (base 0) elegido por el usuario.</summary>
    static int SelectBook()
    {
        Console.WriteLine("Libros:");
        for (int i = 0; i < inventory.BookCount; i++)
        {
            Console.WriteLine($"  {i + 1}. {inventory.GetBookName(i)}");
        }
        return ReadInt("Seleccione el libro: ", 1, inventory.BookCount) - 1;
    }

    /// <summary>Lista las sucursales disponibles y devuelve el índice (base 0) elegido por el usuario.</summary>
    static int SelectBranch()
    {
        Console.WriteLine("\nSucursales:");
        for (int j = 0; j < inventory.BranchCount; j++)
        {
            Console.WriteLine($"  {j + 1}. {inventory.GetBranchName(j)}");
        }
        return ReadInt("Seleccione la sucursal: ", 1, inventory.BranchCount) - 1;
    }

    /// <summary>
    /// Lee un entero de consola validando que esté en el rango [min, max],
    /// repitiendo la pregunta hasta recibir un valor válido.
    /// </summary>
    static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int number) && number >= min && number <= max)
            {
                return number;
            }

            Console.WriteLine(max == int.MaxValue
                ? $"Debe ingresar un número mayor o igual a {min}."
                : $"Debe ingresar un número entre {min} y {max}.");
        }
    }

    /// <summary>Pausa la ejecución hasta que el usuario presione una tecla.</summary>
    static void Pause()
    {
        Console.Write("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}
