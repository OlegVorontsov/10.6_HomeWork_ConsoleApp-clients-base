using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    class Consultant : Worker, GetData
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
                        NumberPassport = numberPassportHidden
                    });
                }
            }
        }

        /// <summary>
        /// Метод изменяет номер телефона по фамилии клиента
        /// </summary>
        /// <param name="SurnameToChangePhoneNumber"></param>
        /// <param name="PhoneNumberToChange"></param>
        public override void changeClientsPhoneNumberBySurname(string SurnameToChangePhoneNumber, long PhoneNumberToChange)
        {
            int i = 0;
            bool surnameNotFound = true;
            ClearClients();
            using (StreamReader sr = new StreamReader(path))
            {
                titles = sr.ReadLine().Split(',');
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    if (args[0] != SurnameToChangePhoneNumber)
                    {
                        Clients.Add(new Client
                        {
                            Surname = args[0],
                            Name = args[1],
                            Patronymic = args[2],
                            PhoneNumber = long.Parse(args[3]),
                            RangePassport = args[4],
                            NumberPassport = args[5]
                        });
                    }
                    else
                    {
                        Clients.Add(new Client
                        {
                            Surname = args[0],
                            Name = args[1],
                            Patronymic = args[2],
                            PhoneNumber = PhoneNumberToChange,
                            RangePassport = args[4],
                            NumberPassport = args[5]
                        });
                        surnameNotFound = false;
                    }
                    i++;
                }
                if (surnameNotFound)
                {
                    Console.WriteLine($"Клиента с фамилией {SurnameToChangePhoneNumber} нет в Справочнике");
                }
            }
            File.Delete(path);
            File.Create(path).Close();
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                string lineTitles = string.Empty;
                lineTitles = "Фамилия, Имя, Отчество, Телефон, Серия, Номер паспорта";
                sw.WriteLine(lineTitles);
                for (int j = 0; j < i; j++)
                {
                    string lineClient = string.Empty;
                    lineClient = $"{Clients[j].Surname}#" +
                                 $"{Clients[j].Name}#" +
                                 $"{Clients[j].Patronymic}#" +
                                 $"{Clients[j].PhoneNumber}#" +
                                 $"{Clients[j].RangePassport}#" +
                                 $"{Clients[j].NumberPassport}";
                    sw.WriteLine(lineClient);
                }
            }
        }
        #endregion
    }
}
