using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    class Manager : Worker
    {
        /// <summary>
        /// Создание экземпляра Менеджера
        /// </summary>
        /// <param name="Path"></param>
        public Manager(string Path) : base(Path)
        {
        }

        #region Методы

        /// <summary>
        /// Получение строки о клиенте через консоль
        /// </summary>
        /// <returns></returns>
        public string getInfoFromConsole()
        {
            string line = string.Empty;

            Console.Write("\nВведите фамилию клиента: ");
            string Surname = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите имя клиента: ");
            string Name = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите отчество клиента: ");
            string Patronymic = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите номер телефона клиента: ");
            long phoneNumber = long.Parse(Console.ReadLine());

            Console.Write("\nВведите серию паспорта клиента: ");
            string rangePassport = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите номер паспорта клиента: ");
            string numberPassport = Convert.ToString(Console.ReadLine());

            string nowDate = DateTime.Now.ToShortDateString();
            string nowTime = DateTime.Now.ToShortTimeString();
            string dateAndTime = $"{nowDate} {nowTime}";

            line = $"{Surname}#" +
                   $"{Name}#" +
                   $"{Patronymic}#" +
                   $"{phoneNumber}#" +
                   $"{rangePassport}#" +
                   $"{numberPassport}#" +
                   $"{dateAndTime}#" +
                   "ничего#" +
                   $"{GetType().Name}";

            return line;
        }

        /// <summary>
        /// Добавление данных о клиентах в конец Справочника
        /// </summary>
        /// <param name="Path"></param>
        public override void putClients(string Path)
        {
            using (StreamWriter put = new StreamWriter(Path, true, Encoding.UTF8))
            {
                char key = 'д';
                do
                {
                    put.WriteLine(getInfoFromConsole());

                    Console.Write("Продожить (н/д): ");
                    key = Console.ReadKey(true).KeyChar;
                } while (char.ToLower(key) == 'д');
            }
        }

        /// <summary>
        /// Метод изменяет данные о клиенте по фамилии
        /// </summary>
        public override void changeClientsList()
        {
            Console.Write("\nВведите фамилию клиента, чьи данные хотите изменить: ");
            string SurnameOld = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите новую фамилию клиента: ");
            string SurnameNew = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите новое имя клиента: ");
            string NameNew = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите новое отчество клиента: ");
            string PatronymicNew = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите новый номер телефона клиента: ");
            long phoneNumberNew = long.Parse(Console.ReadLine());

            Console.Write("\nВведите новую серию паспорта клиента: ");
            string rangePassportNew = Convert.ToString(Console.ReadLine());

            Console.Write("\nВведите новый номер паспорта клиента: ");
            string numberPassportNew = Convert.ToString(Console.ReadLine());

            int i = 0;
            bool surnameNotFound = true;
            ClearClients();
            using (StreamReader sr = new StreamReader(path))
            {
                titles = sr.ReadLine().Split(',');
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    if (args[0] != SurnameOld)
                    {
                        Clients.Add(new Client
                        {
                            Surname = args[0],
                            Name = args[1],
                            Patronymic = args[2],
                            PhoneNumber = long.Parse(args[3]),
                            RangePassport = args[4],
                            NumberPassport = args[5],
                            DateAndTime = args[6],
                            WhatChanged = "ничего",
                            WhoChanged = args[8]
                        });
                    }
                    else
                    {
                        bool somthingChanged = true;
                        string whatChanged = string.Empty;
                        if (SurnameOld != SurnameNew)
                        {
                            whatChanged += "/фам.";
                            somthingChanged = false;
                        }
                        if (args[1] != NameNew)
                        {
                            whatChanged += "/им.";
                            somthingChanged = false;
                        }
                        if (args[2] != PatronymicNew)
                        {
                            whatChanged += "/отч.";
                            somthingChanged = false;
                        }
                        if (long.Parse(args[3]) != phoneNumberNew)
                        {
                            whatChanged += "/н.тел.";
                            somthingChanged = false;
                        }
                        if (args[4] != rangePassportNew)
                        {
                            whatChanged += "/сер.пасп.";
                            somthingChanged = false;
                        }
                        if (args[5] != numberPassportNew)
                        {
                            whatChanged += "/н.пасп.";
                            somthingChanged = false;
                        }
                        if (somthingChanged)
                        {
                            whatChanged = "ничего";
                        }
                        string nowDate = DateTime.Now.ToShortDateString();
                        string nowTime = DateTime.Now.ToShortTimeString();
                        string dateAndTime = $"{nowDate} {nowTime}";
                        Clients.Add(new Client
                        {
                            Surname = SurnameNew,
                            Name = NameNew,
                            Patronymic = PatronymicNew,
                            PhoneNumber = phoneNumberNew,
                            RangePassport = rangePassportNew,
                            NumberPassport = numberPassportNew,
                            DateAndTime = dateAndTime,
                            WhatChanged = whatChanged,
                            WhoChanged = $"{GetType().Name}"
                        });
                        surnameNotFound = false;
                    }
                    i++;
                }
                if (surnameNotFound)
                {
                    Console.WriteLine($"Клиента с фамилией {SurnameOld} нет в Справочнике");
                }
            }
            File.Delete(path);
            File.Create(path).Close();
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string lineTitles = string.Empty;
                lineTitles = "Фамилия, Имя, Отчество, Телефон, Серия, Номер паспорта, Дата и время записи, Что изменено, Кто изменил";
                sw.WriteLine(lineTitles);
                for (int j = 0; j < i; j++)
                {
                    string lineClient = string.Empty;
                    lineClient = $"{Clients[j].Surname}#" +
                                 $"{Clients[j].Name}#" +
                                 $"{Clients[j].Patronymic}#" +
                                 $"{Clients[j].PhoneNumber}#" +
                                 $"{Clients[j].RangePassport}#" +
                                 $"{Clients[j].NumberPassport}#" +
                                 $"{Clients[j].DateAndTime}#" +
                                 $"{Clients[j].WhatChanged}#" +
                                 $"{Clients[j].WhoChanged}";
                    sw.WriteLine(lineClient);
                }
            }
        }

        #endregion
    }
}
