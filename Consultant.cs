using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    class Consultant : Worker
    {
        /// <summary>
        /// Создание экземпляра Консультанта
        /// </summary>
        /// <param name="Path"></param>
        public Consultant(string Path) : base(Path)
        {
        }

        #region Методы
        /// <summary>
        /// Загрузка данных о клиентах из Справочника
        /// </summary>
        public override void Load()
        {
            ClearClients();
            using (StreamReader sr = new StreamReader(path))
            {
                titles = sr.ReadLine().Split(',');
                while (!sr.EndOfStream)
                {
                    string rangePassportHidden = string.Empty;
                    string numberPassportHidden = string.Empty;
                    string[] args = sr.ReadLine().Split('#');
                    foreach (var item in args[4])
                    {
                        rangePassportHidden += "*";
                    }
                    foreach (var item in args[5])
                    {
                        numberPassportHidden += "*";
                    }

                    Clients.Add(new Client
                    {
                        Surname = args[0],
                        Name = args[1],
                        Patronymic = args[2],
                        PhoneNumber = long.Parse(args[3]),
                        RangePassport = rangePassportHidden,
                        NumberPassport = numberPassportHidden,
                        DateAndTime = args[6],
                        WhatChanged = args[7],
                        WhoChanged = args[8]
                    });
                }
            }
        }

        /// <summary>
        /// Метод изменяет номер телефона по фамилии клиента
        /// </summary>
        /// <param name="SurnameToChangePhoneNumber"></param>
        /// <param name="PhoneNumberToChange"></param>
        public override void changeClientsList()
        {
            Console.Write("\nВведите фамилию клиента, чей телефон хотите изменить: ");
            string Surname = Convert.ToString(Console.ReadLine());

            Console.Write($"\nВведите новый номер телефона клиента {Surname}: ");
            long PhoneNumberToChange = long.Parse(Console.ReadLine());

            int i = 0;
            bool surnameNotFound = true;
            ClearClients();
            using (StreamReader sr = new StreamReader(path))
            {
                titles = sr.ReadLine().Split(',');
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    if (args[0] != Surname)
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
                            WhatChanged = args[7],
                            WhoChanged = args[8]
                        });
                    }
                    else
                    {
                        string nowDate = DateTime.Now.ToShortDateString();
                        string nowTime = DateTime.Now.ToShortTimeString();
                        string dateAndTime = $"{nowDate} {nowTime}";
                        Clients.Add(new Client
                        {
                            Surname = args[0],
                            Name = args[1],
                            Patronymic = args[2],
                            PhoneNumber = PhoneNumberToChange,
                            RangePassport = args[4],
                            NumberPassport = args[5],
                            DateAndTime = dateAndTime,
                            WhatChanged = "/н.тел.",
                            WhoChanged = $"{GetType().Name}"
                        });
                        surnameNotFound = false;
                    }
                    i++;
                }
                if (surnameNotFound)
                {
                    Console.WriteLine($"Клиента с фамилией {Surname} нет в Справочнике");
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
