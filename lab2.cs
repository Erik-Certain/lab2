using System;
using System.Collections.Generic;
using System.Linq;

// Базовый класс одежды
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

// Класс брюк
public class Trousers : Clothing
{
    public string Material { get; set; }
  
    public Trousers(decimal price, string size, string material) : base(price, size)
    {
        Material = material;
    }
}

// Класс рубашки
public class Shirt : Clothing
{
    public int  SleeveLength { get; set; }

    public Shirt(decimal price, string size, int sleeveLength) : base(price, size)
    {
        SleeveLength = sleeveLength;
    }
}

// Класс списка одежды
public class ClothesList : List<Clothing>{}

// Отдельный статический класс для методов расширения
public static class ClothesListExtensions
{
    // Метод расширения для определения суммарной стоимости всех вещей размера S
    public static decimal TotalCostForSizeS(this IEnumerable<Clothing> list)
    {
        return list.Where(c => c.Size == "S").Sum(c => c.Price);
    }

    // Метод расширения для определения средней стоимости рубашек
    public static decimal AverageShirtPrice(this IEnumerable<Clothing> list)
    {
        var shirts = list.OfType<Shirt>().ToArray();
        return shirts.Any() ? shirts.Average(s => s.Price) : 0;
    }
}

// Проверка работы классов

class Program
{
    static void Main()
    {
        // Создаем изначальный список одежды с предустановленными значениями.
        var clothesList = new ClothesList
        {
            new Trousers(100, "S", "Шёлк"),
            new Shirt(50, "S", 60),
            new Trousers(200, "M", "Шерсть"),
            new Shirt(150, "M", 70),
            new Trousers(300, "S", "Полиэстер")
        };

        // Выбираем из списка только те элементы, которые имеют размер S.
        var clothesSizeS = clothesList.Where(c => c.Size == "S").ToList();

        Console.WriteLine("Введите новые цены для товаров размера S (введите 'end' для завершения ввода):");

        for (int i = 0; i < clothesSizeS.Count;)
        {
            Console.WriteLine($"Введите цену для {clothesSizeS[i].GetType().Name} (текущая цена {clothesSizeS[i].Price}):");
            string input = Console.ReadLine();

            if(input == "end")
            {
                break;
            }

            if (!decimal.TryParse(input, out decimal newPrice) || newPrice <= 0)
            {
                Console.WriteLine("Ошибка: введите положительное число больше нуля.");
                continue; // пропускаем итерацию и оставляем цикл активным для повторного ввода
            }

            clothesSizeS[i].Price = newPrice; // Обновляем цену текущего элемента
            i++; // Переходим к следующему элементу размера S
        }

        // Выводим обновленный список одежды
        Console.WriteLine("Обновленный список одежды:");
        foreach (var item in clothesList)
        {
            Console.WriteLine($"Тип: {item.GetType().Name}, Размер: {item.Size}, Цена: {item.Price}");
        }

        Console.WriteLine($"Суммарная стоимость всех вещей размера S: {clothesList.TotalCostForSizeS()}");
        Console.WriteLine($"Средняя стоимость рубашек: {clothesList.AverageShirtPrice()}");
    }
}