using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    class Worker
    {
        protected static List<Client> Clients;

        protected static string path;
        protected static int index;
        protected static string[] titles;

        #region Коструктор
        /// <summary>
        /// Создание экземпляра Работника
        /// </summary>
        /// <param name="Path"></param>
        public Worker(string Path)
        {
            path = Path;
            index = 0;
            titles = new string[6];
            Clients = new List<Client>();
        }
        #endregion

        #region Методы
        /// <summary>
        /// Вывод на экран названия должности
        /// </summary>
        public void printPost()
        {
            Console.WriteLine($"Выбранная должность: {GetType().Name}");
        }

        public void ClearClients()
        {
            Clients.Clear();
        }

        /// <summary>
        /// Загрузка данных о клиентах из Справочника
        /// </summary>
        public virtual void Load()
        {
            ClearClients();
            using (StreamReader sr = new StreamReader(path))
            {
                titles = sr.ReadLine().Split(',');

                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    Clients.Add(new Client(args[0], args[1], args[2], long.Parse(args[3]), args[4], args[5]));
                }
            }
        }

        /// <summary>
        /// Вывод данных из коллекции клиентов в консоль
        /// </summary>
        public void getInfo()
        {
            Console.WriteLine($"{titles[0],15}{titles[1],15}{titles[2],15}{titles[3],15}{titles[4],15}{titles[5],20}");

            foreach (var item in Clients)
            {
                Console.WriteLine(item.Print());
            }
        }

        public virtual void changeClientsList()
        {
        }

        public virtual void putClients(string Path)
        {
            ClearClients();
            Console.WriteLine($"У {GetType().Name} нет реализации вызванного метода");
        }
        #endregion
    }
}
