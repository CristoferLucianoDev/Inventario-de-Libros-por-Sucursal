namespace Inventario_de_Libros_por_Sucursal;

internal class Program
{
    static readonly BranchInventory inventory = new();

    static void Main(string[] args)
    {
        string option;

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

    static void ShowInventory()
    {
        Console.Clear();
        inventory.ShowInventory();
        Pause();
    }

    static void CheckAvailability()
    {
        Console.Clear();
        int book = SelectBook();
        int branch = SelectBranch();

        int quantity = inventory.GetQuantity(book, branch);
        Console.WriteLine($"\n{inventory.GetBookName(book)} en sucursal {inventory.GetBranchName(branch)}: {quantity} unidades.");
        Pause();
    }

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

    static void BookTotal()
    {
        Console.Clear();
        int book = SelectBook();

        int total = inventory.GetBookTotal(book);
        Console.WriteLine($"\nTotal de {inventory.GetBookName(book)} en todas las sucursales: {total} unidades.");
        Pause();
    }

    static void BranchTotal()
    {
        Console.Clear();
        int branch = SelectBranch();

        int total = inventory.GetBranchTotal(branch);
        Console.WriteLine($"\nInventario total de la sucursal {inventory.GetBranchName(branch)}: {total} unidades.");
        Pause();
    }

    static void LowInventory()
    {
        Console.Clear();
        inventory.ShowLowInventory();
        Pause();
    }

    static void BranchWithMostStock()
    {
        Console.Clear();
        int book = SelectBook();

        int branch = inventory.GetBranchWithMostStock(book);
        Console.WriteLine($"\nLa sucursal con mayor disponibilidad de {inventory.GetBookName(book)} es " +
                          $"{inventory.GetBranchName(branch)} con {inventory.GetQuantity(book, branch)} unidades.");
        Pause();
    }

    static int SelectBook()
    {
        Console.WriteLine("Libros:");
        for (int i = 0; i < inventory.BookCount; i++)
        {
            Console.WriteLine($"  {i + 1}. {inventory.GetBookName(i)}");
        }
        return ReadInt("Seleccione el libro: ", 1, inventory.BookCount) - 1;
    }

    static int SelectBranch()
    {
        Console.WriteLine("\nSucursales:");
        for (int j = 0; j < inventory.BranchCount; j++)
        {
            Console.WriteLine($"  {j + 1}. {inventory.GetBranchName(j)}");
        }
        return ReadInt("Seleccione la sucursal: ", 1, inventory.BranchCount) - 1;
    }

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

    static void Pause()
    {
        Console.Write("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }
}