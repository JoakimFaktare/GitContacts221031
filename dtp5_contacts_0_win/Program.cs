﻿using System;
using System.IO;

namespace dtp5_contacts_0
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
            
            public Person(bool ask = false) {
            if (ask)
                {
                    Console.Write("personal name: ");
                    persname = Console.ReadLine();
                    Console.Write("surname: ");
                    surname = Console.ReadLine();
                    Console.Write("phone: ");
                    phone = Console.ReadLine();
                    Console.Write("adress: ");
                    address = Console.ReadLine();
                    Console.Write("birthdate: ");
                    birthdate = Console.ReadLine();
                }
            }
            public Person(string[] attrs)
            {
                persname = attrs[0];
                surname = attrs[1];
                string[] phones = attrs[2].Split(';');
                phone = phones[0];
                string[] addresses = attrs[3].Split(';');
                address = addresses[0];
                string[] birthdates = attrs[4].Split(';');
                birthdate = birthdates[0];

            }

        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            PrintHelp();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                    Console.WriteLine("Bye bye!");
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.lis";
                        ReadContactListFromFile(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1];
                        ReadContactListFromFile(lastFileName);
                    }
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        SaveContacts(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1];
                        SaveContacts(lastFileName);
                    }
                }
                else if (commandLine[0] == "list")
                {
                    foreach (Person p in contactList)
                        if (p != null)
                    Console.WriteLine($"{p.persname} {p.surname}, {p.phone}, {p.address}, {p.birthdate}");
                }
                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        Person p = new Person(ask: true);
                        InsertInToContactlist(p);
                    }
                    else
                    {
                        Console.WriteLine("Not yet implemented: new /person/");
                    }
                }
                else if (commandLine[0] == "help")
                {
                    PrintHelp();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");
        }

        private static void SaveContacts(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate};");
                }
            }
        }

        private static void PrintHelp()  //Metdo för help funktion
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  delete       - emtpy the contact list");
            Console.WriteLine("  delete /persname/ /surname/ - delete a person");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        }

        private static void ReadContactListFromFile(string lastFileName) //Metod för att läsa contactlist.lis
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null) 
                {
                    Console.WriteLine(line);
                    string[] attrs = line.Split(';');
                    Person p = new Person(attrs);
                    //FIXME 
                    InsertInToContactlist(p);
                }
            }
        }

        private static void InsertInToContactlist(Person p)
        {
            for (int ix = 0; ix < contactList.Length; ix++) // eventuellt göra om till egen metod
            {
                if (contactList[ix] == null)
                {
                    contactList[ix] = p;
                    break;
                }
            }
        }
    }
}
