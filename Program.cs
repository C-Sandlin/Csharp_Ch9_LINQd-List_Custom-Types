using System;
using System.Collections.Generic;
using System.Linq;
using LINQd_list;

namespace LINQd_List_custom_types
{
    public class Program
    {
        public static void Main()
        {

            //REFERENCE EXAMPLE
            // persons[0] = new Person { PersonID = 1, car = "Ferrari" };
            // persons[1] = new Person { PersonID = 1, car = "BMW" };
            // persons[2] = new Person { PersonID = 2, car = "Audi" };
            // Is there a way I can group by personID and get the list of all the cars he has?

            // var results = from p in persons
            //               group p.car by p.PersonId into g
            //               select new { PersonId = g.Key, Cars = g.ToList() };


            List<Customer> customers = new List<Customer>() {
                new Customer(){ Name="Bob Lesman", Balance=80345.66, Bank="FTB"},
                new Customer(){ Name="Joe Landy", Balance=9284756.21, Bank="WF"},
                new Customer(){ Name="Meg Ford", Balance=487233.01, Bank="BOA"},
                new Customer(){ Name="Peg Vale", Balance=7001449.92, Bank="BOA"},
                new Customer(){ Name="Mike Johnson", Balance=790872.12, Bank="WF"},
                new Customer(){ Name="Les Paul", Balance=8374892.54, Bank="WF"},
                new Customer(){ Name="Sid Crosby", Balance=957436.39, Bank="FTB"},
                new Customer(){ Name="Sarah Ng", Balance=56562389.85, Bank="FTB"},
                new Customer(){ Name="Tina Fey", Balance=1000000.00, Bank="CITI"},
                new Customer(){ Name="Sid Brown", Balance=49582.68, Bank="CITI"}
            };

            /*
                Given the same customer set, display how many millionaires per bank.
                Ref: https://stackoverflow.com/questions/7325278/group-by-in-linq

                Example Output:
                WF 2
                BOA 1
                FTB 1
                CITI 1
            */

            var millionaires = from customer in customers
                               group customer.Balance > 999999.00 by customer.Bank into newGroup
                               select new { Bank = newGroup.Key, Balance = newGroup.ToList(), numMillionaires = newGroup.Where(passesBankAcctTest => passesBankAcctTest == true).Count() };

            foreach (var taco in millionaires)
            {
                Console.WriteLine($"The Bank {taco.Bank} has {taco.numMillionaires} millionaires.");
            };

            // ORRRRR

            var millionaires2 = from customer in customers
                                where customer.Balance >= 1000000.00
                                select customer;
            var banksGroup = from millionaire2 in millionaires2
                             group millionaire2 by millionaire2.Bank into newGroup
                             select new { Bank = newGroup.Key, Customers = newGroup.ToList() };
            banksGroup.ToList().ForEach(bank =>
            {
                Console.WriteLine($"{bank.Bank} - {bank.Customers.Count}");
            });
        }
    }
}
