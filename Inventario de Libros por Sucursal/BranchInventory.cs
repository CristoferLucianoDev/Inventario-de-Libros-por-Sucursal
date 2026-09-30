using System.Linq;

namespace Inventario_de_Libros_por_Sucursal;

/// <summary>
/// Representa el inventario de libros distribuido entre varias sucursales,
/// usando una matriz 2D donde las filas son libros y las columnas son sucursales.
/// </summary>
public class BranchInventory
{
    private readonly string[] _books = { "Dune", "SpiderMan", "Rayuela", "Ficciones", "Clean Code" };
    private readonly string[] _branches = { "Central", "Norte", "Sur" };

    // Matriz de cantidades: fila = libro (índice en _books), columna = sucursal (índice en _branches).
    private readonly int[,] _inventory =
    {
        {  5,       2,     8 },
        {  0,       4,     3 },
        { 10,       6,     1 },
        {  3,       7,     9 },
        {  2,       5,     4 }
    };

    /// <summary>Cantidad total de libros distintos registrados.</summary>
    public int BookCount => _inventory.GetLength(0);

    /// <summary>Cantidad total de sucursales registradas.</summary>
    public int BranchCount => _inventory.GetLength(1);

    /// <summary>Obtiene el nombre del libro dado su índice.</summary>
    public string GetBookName(int book) => _books[book];

    /// <summary>Obtiene el nombre de la sucursal dado su índice.</summary>
    public string GetBranchName(int branch) => _branches[branch];

    /// <summary>
    /// Imprime en consola una tabla con todos los libros, sucursales y cantidades disponibles.
    /// </summary>
    public void ShowInventory()
    {
        // Encabezado: nombre de columna "Libro" seguido del nombre de cada sucursal.
        Console.WriteLine($"{"Libro",-15}" + string.Join("", _branches.Select(b => $"{b,10}")));

        for (int i = 0; i < BookCount; i++)
        {
            Console.Write($"{_books[i],-15}");
            for (int j = 0; j < BranchCount; j++)
            {
                Console.Write($"{_inventory[i, j], 10}");
            }
            Console.WriteLine();
        }
    }

    /// <summary>Obtiene la cantidad disponible de un libro en una sucursal específica.</summary>
    public int GetQuantity(int book, int branch)
    {
        return _inventory[book, branch];
    }

    /// <summary>Actualiza la cantidad disponible de un libro en una sucursal específica.</summary>
    public void UpdateQuantity(int book, int branch, int quantity)
    {
        _inventory[book, branch] = quantity;
    }

    /// <summary>Suma la cantidad de un libro en todas las sucursales.</summary>
    public int GetBookTotal(int book)
    {
        int total = 0;

        for (int j = 0; j < BranchCount; j++)
        {
            total += _inventory[book, j];
        }
        return total;
    }

    /// <summary>Suma la cantidad de todos los libros en una sucursal.</summary>
    public int GetBranchTotal(int branch)
    {
        int total = 0;

        for(int i = 0; i < BookCount; i++)
        {
            total += _inventory[i, branch];
        }
        return total;
    }

    /// <summary>
    /// Muestra los libros cuya cantidad en alguna sucursal es baja (3 unidades o menos).
    /// Si ninguno cumple la condición, informa que el inventario es suficiente.
    /// </summary>
    public void ShowLowInventory()
    {
        bool found = false;

        for (int i = 0; i < BookCount; i++)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                if (_inventory[i, j] <= 3)
                {
                    Console.WriteLine($"Libro: {_books[i]}, Sucursal: {_branches[j]}, Cantidad: {_inventory[i, j]}");
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("\nHay suficiente inventario en todas las sucursales.");
        }
    }

    /// <summary>
    /// Determina el índice de la sucursal con mayor cantidad disponible de un libro dado.
    /// </summary>
    public int GetBranchWithMostStock(int book)
    {
        // Recorre las sucursales comparando cantidades y se queda con el índice del máximo.
        int MaxIndex = 0;
        for (int j = 1; j < BranchCount; j++)
        {
            if ( _inventory[book, j] > _inventory[book, MaxIndex])
            {
                MaxIndex = j;
            }
        }
        return MaxIndex;
    }
}
