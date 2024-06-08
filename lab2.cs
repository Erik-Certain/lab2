using System;
using System.Collections.Generic;
using System.Linq;


    public class Trousers
    {
        public string Material { get; set; }
        public int Cost { get; set; }
        public string Size { get; set; }

        public Trousers(string material, int cost, string size)
        {
            if (string.IsNullOrEmpty(material))
            {
                throw new ArgumentException("Материал не может быть пустым или равным null", nameof(material));
            }
            if (cost <= 0)
            {
                throw new ArgumentException("Стоимость должна быть положительным числом", nameof(cost));
            }
            if (string.IsNullOrEmpty(size))
            {
                throw new ArgumentException("Размер не может быть пустым или равным null", nameof(size));
            }

            Material = material;
            Cost = cost;
            Size = size;
        }
    }

    public class Shirt
    {
        public int SleeveLength { get; set; }
        public int Cost { get; set; }
        public string Size { get; set; }

        public Shirt(int sleeveLength, int cost, string size)
        {
            if (sleeveLength <= 0)
            {
                throw new ArgumentException("Длина рукава должна быть положительным числом", nameof(sleeveLength));
            }
            if (cost <= 0)
            {
                throw new ArgumentException("Стоимость должна быть положительным числом", nameof(cost));
            }
            if (string.IsNullOrEmpty(size))
            {
                throw new ArgumentException("Размер не может быть пустым или равным null", nameof(size));
            }

            SleeveLength = sleeveLength;
            Cost = cost;
            Size = size;
        }
    }

    public class ClothingList : List<object>
    {
        public int GetTotalCostOfSizeS()
        {
            return this.OfType<Trousers>()
                .Where(t => t.Size == "S")
                .Sum(t => t.Cost) +
                this.OfType<Shirt>()
                .Where(s => s.Size == "S")
                .Sum(s => s.Cost);
        }

        public double GetAverageShirtCost()
        {
            return this.OfType<Shirt>()
                .Average(s => s.Cost);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var clothingList = new ClothingList
            {
                new Trousers("Деним", 2000, "S"),
                new Trousers("Хлопок", 1500, "M"),
                new Shirt(60, 1000, "S"),
                new Shirt(65, 1200, "M"),
                new Trousers("Шерсть", 3000, "L")
            };
            Console.WriteLine("Суммарная стоимость всех вещей размера S: {0}", clothingList.GetTotalCostOfSizeS());
            Console.WriteLine("Средняя стоимость рубашки: {0}", clothingList.GetAverageShirtCost());
        }
    }