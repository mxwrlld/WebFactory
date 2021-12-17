using _1._1.DAL;
using _1._1.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._1
{
    class Program
    {
        static void Main()
        {
            using (var context = new PersonalCustomerCardsContext())
            {
                var users = GetMockData();

                foreach (var user in users)
                {
                    context.UserProfiles.Add(user);
                }

                context.SaveChanges();
            }

            using (var context = new PersonalCustomerCardsContext())
            {
                var users = context.UserProfiles
                    .Include(user => user.PersonalCard)
                    .ToList();


                foreach (var user in users)
                {
                    Console.WriteLine(user.ToString());
                }
            }
        }

        private static IList<UserProfile> GetMockData()
        {
            var users = GetMockUser();
            var cards = GetMockCards();
            var purchases = GetMockPurchases();

            users[0].PersonalCard = cards[1];
            cards[1].Id = users[0].Id;

            users[1].PersonalCard = cards[2];
            cards[2].Id = users[1].Id;

            users[2].PersonalCard = cards[0];
            cards[0].Id = users[2].Id;

            cards[0].Purchases.Add(purchases[0]);
            cards[0].Purchases.Add(purchases[1]);
            cards[0].Purchases.Add(purchases[2]);
            cards[0].Purchases.Add(purchases[3]);


            cards[1].Purchases.Add(purchases[4]);
            cards[1].Purchases.Add(purchases[5]);

            cards[2].Purchases.Add(purchases[6]);

            return users;
        }

        private static IList<Purchase> GetMockPurchases()
        {
            IList<Purchase> purchases = new List<Purchase>();
            int amountOfPurchases = 7;

            Random rand = new Random(Seed: 5);
            for (int i = 0; i < amountOfPurchases; ++i)
            {
                purchases.Add(new Purchase()
                {
                    Sum = (decimal)(rand.NextDouble() * 10)
                }) ;
            }
            return purchases;
        }

        private static IList<PersonalCard> GetMockCards()
        {
            IList<PersonalCard> personalCards = new List<PersonalCard>();
            int amountOfCards = 3;

            Random rand = new Random(Seed: 5);
            for(int i = 0; i < amountOfCards; ++i)
            {
                personalCards.Add(new PersonalCard()
                {
                    Discount = (float)(rand.NextDouble())
                });
            }

            return personalCards;
        }

        private static IList<UserProfile> GetMockUser()
        {
            IList<UserProfile> userProfiles = new List<UserProfile>();
            List<string> names = new List<string>()
            {
                "Константинов Павел",
                "Булгакова Дарья",
                "Беляков Ян"
            };
            List<string> emails = new List<string>()
            {
                "constPavel@gmail.com",
                "bulgDarya@gmail.com",
                "belyakovYan@gmail.com"
            };

            for(int i = 0; i < names.Count; ++i)
            {
                userProfiles.Add(new UserProfile()
                {
                    Email = emails[i],
                    FirstName = names[i].Split()[1],
                    LastName = names[i].Split()[0],
                    Birthdate = new DateTime(2000, i + 1, 10 + i)
                });
            }

            return userProfiles;
        }
    }
}
