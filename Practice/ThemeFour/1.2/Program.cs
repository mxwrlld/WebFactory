using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._2
{
    class Program
    {
        private static List<string> productNames = new List<string>()
        {
            "Колбаска",
            "Хлебушек",
            "Малинка",
            "Абрикосики",
            "Селёдочка",
            "Мяско",
            "Конфеточки",
            "Мармеладочки",
            "Звездочки - шоколадочки",
            "Ментос не мятный",
            "Чашечка для гречки",
            "Щетка против пыли",
            "Щетки для пыли",
            "Крабик не для еды",
            "Крабик для еды",
            "Морские продуктики",
            "Морепродуктики",
            "Водичка",
            "Минералочка",
            "Молочко",
            "Пылесосик",
            "Коврик",
            "Машинка постирочная"
        };

        static void Main()
        {
            List<Manufacturer> manufacturers = GetMockManufacturers();
            List<Dealer> dealers = GetMockDealers(manufacturers);
            List<Dealer> foundDealers;

            Console.Write("Введите строку поиска: ");
            string searchBy = Console.ReadLine().Trim().ToLower();

            List<Manufacturer> foundManufacturers = Search(manufacturers, dealers, searchBy, out foundDealers);
            PrintResult(foundManufacturers, foundDealers);
        }

        private static void PrintResult(List<Manufacturer> manufacturers, List<Dealer> dealers)
        {
            if (manufacturers.Count == 0 && dealers.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Товары по запросу не найдены");
                return;
            }
            // Для предприятий
            Console.WriteLine("Товары:");
            foreach (var manufacturer in manufacturers)
            {
                var nomenclature = manufacturer.Nomenclature;
                
                foreach (var product in nomenclature)
                {
                    Console.Write($"{product.Name} - ");
                    Console.WriteLine($"{product.Price:C2}");
                }
                Console.WriteLine("Поставщик:");
                Console.Write($"{manufacturer.Name} - ");
                Console.Write($"{manufacturer.TIN} - ");
                Console.Write($"{manufacturer.Address}");
                Console.WriteLine();
                Console.WriteLine(new string('-', Console.WindowWidth / 3));
            }
            Console.WriteLine(new string('-', Console.WindowWidth));

            Console.WriteLine("Товары:");
            // Для дилеров
            foreach (var dealer in dealers)
            {
                var manufacturer = dealer.Manufacturer;
                var nomenclature = manufacturer.Nomenclature;
                foreach (var product in nomenclature)
                {
                    Console.Write($"{product.Name} - ");
                    Console.WriteLine($"{dealer.GetPriceWithExtraCharge(product):C2}");
                }
                Console.WriteLine("Поставщик:");
                Console.Write($"{dealer.Name} - ");
                Console.Write($"{dealer.TIN} - ");
                Console.Write($"{dealer.Address}");
                Console.Write($" ({manufacturer.Name})");
                Console.WriteLine();
                Console.WriteLine(new string('-', Console.WindowWidth / 2));
            }

            Console.WriteLine(new string('-', Console.WindowWidth));

        }

        private static List<Manufacturer> Search(List<Manufacturer> manufacturers, List<Dealer> dealers, string searchBy
            , out List<Dealer> foundDealers)
        {
            List<Manufacturer> foundManufacturers = new List<Manufacturer>();
            List<Product> foundProducts;
            for (int i = 0; i < manufacturers.Count; i++)
            {
                var nomenclature = manufacturers[i].Nomenclature;
                foundProducts = new List<Product>();
                for (int j = 0; j < nomenclature.Count; j++)
                {
                    if(nomenclature[j].Name.ToLower().Contains(searchBy)
                        || nomenclature[j].VendorCode.ToLower().Contains(searchBy))
                    {
                        foundProducts.Add(nomenclature[j]);
                    }
                }
                if(foundProducts.Count != 0)
                {
                    foundManufacturers.Add(new Manufacturer(manufacturers[i].TIN, manufacturers[i].Name,
                        manufacturers[i].Address, foundProducts));
                }
            }

            foundDealers = new List<Dealer>();
            for (int i = 0; i < foundManufacturers.Count; i++)
            {
                for (int j = 0; j < dealers.Count; j++)
                {
                    if (dealers[j].Manufacturer.Equals(foundManufacturers[i]))
                    {
                        dealers[j].Manufacturer.Nomenclature = foundManufacturers[i].Nomenclature;
                        foundDealers.Add(new Dealer(dealers[j].TIN, dealers[j].Name,
                            dealers[j].Address, dealers[j].Manufacturer, dealers[j].ExtraCharge));
                    }
                }
            }

            return foundManufacturers;
        }
        private static List<Product> GetMockProducts()
        {
            Random rand = new Random();
            int startOfRange = 10000;
            int endOfRange = 90000;
            int amountOfProducts = rand.Next(10, 15);
            List<string> vendorCodes = new List<string>();
            for (int i = 0; i < amountOfProducts; ++i)
            {
                vendorCodes.Add(rand.Next(startOfRange, endOfRange).ToString()
                    + rand.Next(startOfRange, endOfRange).ToString()
                    + rand.Next(startOfRange, endOfRange).ToString());
            }

            List<string> names = new List<string>();
            int newNameIndex;
            for(int i = 0; i < amountOfProducts; ++i)
            {
                newNameIndex = rand.Next(0, productNames.Count - 1);
                if(names.Any(x => x == productNames[newNameIndex]))
                {
                    --i;
                    continue;
                }
                else
                    names.Add(productNames[newNameIndex]);
            }


            List<Product> products = new List<Product>(amountOfProducts);
            for (int i = 0; i < amountOfProducts; i++)
            {
                products.Add(new Product(vendorCodes[i], names[i], rand.Next(100, 1000) / 100));
            }
            return products; 
        }
    
        private static List<Manufacturer> GetMockManufacturers()
        {
            int amountOfManufacturers = 5;
            Random rand = new Random();
            int startOfRange = 10000;
            int endOfRange = 90000;
            List<string> tins = new List<string>();
            for (int i = 0; i < amountOfManufacturers; ++i)
            {
                tins.Add(rand.Next(startOfRange, endOfRange).ToString()
                    + rand.Next(startOfRange, endOfRange).ToString());
            }

            List<string> names = new List<string>()
            {
                "OOO Хороший день",
                "OOO Невероятный полдень",
                "OOO Потрясающий вечер",
                "OOO Изумительная ночь",
                "OOO Борящее утро",
            };

            List<string> addresses = new List<string>()
            {
                "Первая просека",
                "Вторая просека",
                "Третья просека",
                "Четвертая просека",
                "Пятая просека"
            };

            List<List<Product>> nomenclatures = new List<List<Product>>(amountOfManufacturers);
            for (int i = 0; i < amountOfManufacturers; ++i)
            {
                nomenclatures.Add(GetMockProducts());
            }

            List<Manufacturer> manufacturers = new List<Manufacturer>(amountOfManufacturers);
            for (int i = 0; i < amountOfManufacturers; ++i)
            {
                manufacturers.Add(new Manufacturer(tins[i], names[i], addresses[i], nomenclatures[i]));
            }

            return manufacturers;
        }
    
        private static List<Dealer> GetMockDealers(List<Manufacturer> manufacturers)
        {
            int amountOfDealers = 3;
            List<string> tins = GenerateTINs(amountOfDealers);
            List<Dealer> dealers = new List<Dealer>(amountOfDealers);
            List<string> names = new List<string>()
            {
                "ИП Егор",
                "ИП Давид",
                "ИП Варвара",
            };

            List<string> addresses = new List<string>()
            {
                "Первая рабочая",
                "Вторая рабочая",
                "Третья рабочая",
            };

            List<double> extraCharges = new List<double>(amountOfDealers);
            Random rand = new Random();
            for (int i = 0; i < amountOfDealers; ++i)
            {
                extraCharges.Add(rand.NextDouble());
            }

            dealers.Add(new Dealer(tins[0], names[0], addresses[0], manufacturers[0], extraCharges[0]));
            dealers.Add(new Dealer(tins[1], names[1], addresses[1], manufacturers[3], extraCharges[1]));
            dealers.Add(new Dealer(tins[2], names[2], addresses[2], manufacturers[4], extraCharges[2]));

            return dealers;
        }
    
        private static List<string> GenerateTINs(int amount)
        {
            Random rand = new Random();
            int startOfRange = 10000;
            int endOfRange = 90000;
            List<string> tins = new List<string>(amount);
            for (int i = 0; i < amount; ++i)
            {
                tins.Add(rand.Next(startOfRange, endOfRange).ToString()
                    + rand.Next(startOfRange, endOfRange).ToString());
            }

            return tins;
        }
    }



    class Provider
    {
        public string TIN { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Provider(string tIN, string name, string address)
        {
            TIN = tIN;
            Name = name;
            Address = address;
        }
    }

    class Manufacturer : Provider
    {
        public List<Product> Nomenclature { get; set; }

        public Manufacturer(string tin, string name, string address, List<Product> nomenclature): base(tin, name, address)
        {
            Nomenclature = nomenclature;
        }

        public override bool Equals(object obj)
        {
            return obj is Manufacturer manufacturer &&
                   TIN == manufacturer.TIN &&
                   Name == manufacturer.Name &&
                   Address == manufacturer.Address;
        }
    }

    class Product
    {
        private string vendorCode;
        public string VendorCode
        {
            get => vendorCode;
            set
            {
                if (value.Length >= 10 && value.Length <= 15)
                    vendorCode = value;
                else
                    throw new ArgumentOutOfRangeException("Артикул - набор из 10-15 цифр");
            }
        }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string vendorCode, string name, decimal price)
        {
            VendorCode = vendorCode;
            Name = name;
            Price = price;
        }
    }

    class Dealer : Provider
    {
        public Manufacturer Manufacturer { get; set; }
        public double ExtraCharge { get; set; }

        public Dealer(string tin, string name, string address, Manufacturer manufacturer, double extraCharge): base(tin, name, address)
        {
            Manufacturer = manufacturer;
            ExtraCharge = extraCharge;
        }

        public decimal GetPriceWithExtraCharge(Product product)
        {
            decimal price = product.Price;
            return price + (price * (decimal)ExtraCharge);
        }
    }
}
