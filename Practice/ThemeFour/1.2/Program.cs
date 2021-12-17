using System;
using System.Collections.Generic;
using System.Linq;
using _2._1.ProgramModel;

namespace _2._1
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

        private static List<string> manufacturerNames = new List<string>()
            {
                "OOO Хороший день",
                "OOO Невероятный полдень",
                "OOO Потрясающий вечер",
                "OOO Изумительная ночь",
                "OOO Борящее утро",
            };

        private static List<string> addresses = new List<string>()
            {
                "Первая просека",
                "Вторая просека",
                "Третья просека",
                "Четвертая просека",
                "Пятая просека",
                "Первая рабочая",
                "Вторая рабочая",
                "Третья рабочая",
            };

        private static List<string> dealerNames = new List<string>()
            {
                "ИП Егор",
                "ИП Давид",
                "ИП Варвара",
            };

        static void Main()
        {
            List<Provider> providers = GetMockProviders();

            Console.Write("Введите строку поиска: ");
            string searchBy = Console.ReadLine().Trim().ToLower();

            var foundProviders = Search(providers, searchBy);

            PrintResult(foundProviders);
        }

        private static void PrintResult(List<Provider> providers)
        {
            if (providers.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Товары по запросу не найдены");
                return;
            }

            foreach(var provider in providers)
            {
                string providerToPrint = GetProviderToPrint(provider);
                Console.WriteLine(providerToPrint);
            }
        }

        private static string GetProviderToPrint(Provider provider)
        {
            string providerToPrint = String.Empty;
            bool isProviderDealer = provider is Dealer ? true : false;

            providerToPrint += "\nПоставщик:\n";
            providerToPrint += provider.ToString() + "\n";

            if (provider.Nomenclature.Count == 1)
                providerToPrint += "Товар: \n";
            else
                providerToPrint += "Товары: \n";

            foreach (var product in provider.Nomenclature)
            {
                if (isProviderDealer)
                {
                    product.Price = (provider as Dealer).GetPriceWithExtraCharge(product);
                }
                providerToPrint += product.ToString() + "\n";
                providerToPrint += new string('-', Console.WindowWidth / 3) + "\n";
            }
            providerToPrint += new string('-', Console.WindowWidth) + "\n";

            return providerToPrint;
        }

        private static List<Provider> Search(List<Provider> providers, string searchBy)
        {
            List<Provider> foundProviders = new List<Provider>();
            List<Product> foundProducts;

            for(int i = 0; i < providers.Count; ++i)
            {
                var provider = providers[i];
                var nomenclature = provider.Nomenclature;
                foundProducts = new List<Product>();

                foreach (var product in nomenclature)
                {
                    if (product.Name.ToLower().Contains(searchBy)
                        || product.VendorCode.ToLower().Contains(searchBy))
                    {
                        foundProducts.Add(new Product(product));
                    }
                }
                if(foundProducts.Count != 0)
                {
                    if (provider is Manufacturer)
                    {
                        var newManufacturer = new Manufacturer(provider.TIN, provider.Name, provider.Address, 
                            foundProducts);
                        foundProviders.Add(newManufacturer);
                    }
                    else
                    {
                        var dealer = provider as Dealer;
                        var newManufacturer = new Manufacturer(dealer.Manufacturer.TIN, dealer.Manufacturer.Name, dealer.Manufacturer.Address, foundProducts);
                        foundProviders.Add(new Dealer(provider.TIN, provider.Name, provider.Address,
                            newManufacturer, dealer.ExtraCharge));

                    }
                }
                
            }
            return foundProviders;
        }

        private static List<Provider> GetMockProviders()
        {
            List<Provider> providers = new List<Provider>();
            List<Manufacturer> manufacturers = GetMockManufacturers();
            List<Dealer> dealers = GetMockDealers(manufacturers);

            dealers.ForEach(x => providers.Add(x));
            manufacturers.ForEach(x => providers.Add(x));

            return providers;
        }

        private static List<Product> GetMockProducts()
        {
            Random rand = new Random();
            int amountOfProducts = rand.Next(10, 15);
            int startOfRange = 10000, endOfRange = 90000;
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


            List<Product> products = new List<Product>();
            for (int i = 0; i < amountOfProducts; i++)
            {
                products.Add(new Product(vendorCodes[i], names[i], rand.Next(100, 1000) / 100));
            }
            return products; 
        }
    
        private static List<Manufacturer> GetMockManufacturers()
        {
            int amountOfManufacturers = 5;
            List<string> tins = GenerateTINs(amountOfManufacturers);

            List<List<Product>> nomenclatures = new List<List<Product>>();
            for (int i = 0; i < amountOfManufacturers; ++i)
            {
                nomenclatures.Add(GetMockProducts());
            }

            List<Manufacturer> manufacturers = new List<Manufacturer>();
            for (int i = 0; i < amountOfManufacturers; ++i)
            {
                manufacturers.Add(new Manufacturer(tins[i], manufacturerNames[i], addresses[i], nomenclatures[i]));
            }

            return manufacturers;
        }
    
        private static List<Dealer> GetMockDealers(List<Manufacturer> manufacturers)
        {
            int amountOfDealers = 3;
            List<string> tins = GenerateTINs(amountOfDealers);
            List<Dealer> dealers = new List<Dealer>();
            List<double> extraCharges = new List<double>(amountOfDealers);
            Random rand = new Random();
            for (int i = 0; i < amountOfDealers; ++i)
            {
                extraCharges.Add(rand.NextDouble());
            }

            dealers.Add(new Dealer(tins[0], dealerNames[0], addresses[addresses.Count - 3], manufacturers[0], extraCharges[0]));
            dealers.Add(new Dealer(tins[1], dealerNames[1], addresses[addresses.Count - 2], manufacturers[3], extraCharges[1]));
            dealers.Add(new Dealer(tins[2], dealerNames[2], addresses[addresses.Count - 1], manufacturers[4], extraCharges[2]));

            return dealers;
        }
    
        private static List<string> GenerateTINs(int amount)
        {
            Random rand = new Random();
            int startOfRange = 10000, endOfRange = 90000;
            List<string> tins = new List<string>();
            for (int i = 0; i < amount; ++i)
            {
                tins.Add(rand.Next(startOfRange, endOfRange).ToString()
                    + rand.Next(startOfRange, endOfRange).ToString());
            }

            return tins;
        }
    }
}
