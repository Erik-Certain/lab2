using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Clothing
{
    public decimal Price { get; set; }
    public string Size { get; set; }

    protected Clothing(decimal price, string size)
    {
        Price = price;
        Size = size;
    }
}

public class Trousers : Clothing
{
    public string Material { get; set; }
  
    public Trousers(decimal price, string size, string material) : base(price, size)
    {
        Material = material;
    }
}

public class Shirt : Clothing
{
    public int  SleeveLength { get; set; }

    public Shirt(decimal price, string size, int sleeveLength) : base(price, size)
    {
        SleeveLength = sleeveLength;
    }
}

public class ClothesList : List<Clothing>{}

public static class ClothesListExtensions
{

    public static decimal TotalCostForSizeS(this IEnumerable<Clothing> list)
    {
        return list.Where(c => c.Size == "S").Sum(c => c.Price);
    }

    public static decimal AverageShirtPrice(this IEnumerable<Clothing> list)
    {
        var shirts = list.OfType<Shirt>().ToArray();
        return shirts.Any() ? shirts.Average(s => s.Price) : 0;
    }
}

class Program
{
    static void Main()
    {
      var clothesList = new ClothesList
      {
          new Trousers(100, "S", "Шёлк"),
          new Shirt(50, "S", 60),
          new Trousers(200, "M", "Шерсть"),
          new Shirt(150, "M", 70),
          new Trousers(300, "S", "Полиэстер")
      };

      var clothesSizeS = clothesList.Where(c => c.Size == "S").ToList();

      Console.WriteLine("Введите новые цены для товаров размера S:");

      foreach (var item in clothesSizeS)
      {
          Console.WriteLine($"Введите цену для {item.GetType().Name} (текущая цена {item.Price}):");
          while (true)
          {
              string input = Console.ReadLine();

              if (!decimal.TryParse(input, out decimal newPrice) || newPrice <= 0)
              {
                  Console.WriteLine("Ошибка: введите положительное число больше нуля.");
                  continue;
              }

              item.Price = newPrice;
              break;
          }
      }

      Console.WriteLine("Обновленный список одежды:");
      foreach (var item in clothesList)
      {
          Console.WriteLine($"Тип: {item.GetType().Name}, Размер: {item.Size}, Цена: {item.Price}");
      }

        Console.WriteLine($"Суммарная стоимость всех вещей размера S: {clothesList.TotalCostForSizeS()}");
        Console.WriteLine($"Средняя стоимость рубашек: {clothesList.AverageShirtPrice()}");
    }
}