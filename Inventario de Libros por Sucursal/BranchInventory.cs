using System.Linq;

namespace Inventario_de_Libros_por_Sucursal;

public class BranchInventory
{
    private readonly string[] _books = { "Dune", "SpiderMan", "Rayuela", "Ficciones", "Clean Code" };
    private readonly string[] _branches = { "Central", "Norte", "Sur" };

    private readonly int[,] _inventory =
    {
        {  5,       2,     8 },   
        {  0,       4,     3 },  
        { 10,       6,     1 },   
        {  3,       7,     9 },   
        {  2,       5,     4 }    
    };

    public int BookCount => _inventory.GetLength(0);
    public int BranchCount => _inventory.GetLength(1);

    public string GetBookName(int book) => _books[book];
    public string GetBranchName(int branch) => _branches[branch];

    public void ShowInventory()
    {
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

    public int GetQuantity(int book, int branch)
    {
        return _inventory[book, branch];
    }

    public void UpdateQuantity(int book, int branch, int quantity)
    {
        _inventory[book, branch] = quantity;
    }

    public int GetBookTotal(int book)
    {
        int total = 0;

        for (int j = 0; j < BranchCount; j++)
        {
            total += _inventory[book, j];
        }
        return total;
    }

    public int GetBranchTotal(int branch)
    {
        int total = 0;

        for(int i = 0; i < BookCount; i++)
        {
            total += _inventory[i, branch];
        }
        return total;
    }

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

    public int GetBranchWithMostStock(int book)
    {
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