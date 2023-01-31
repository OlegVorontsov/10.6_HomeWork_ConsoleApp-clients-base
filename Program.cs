using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = "Справочник.txt";
            Worker myWorker = new Worker(Path);

            Console.Write("Введите '0', если Вы - Консультант\n" +
                          "Введите '1', если Вы - Менеджер\n");
            var inputPost = Console.ReadLine();

            //Пользователь выбирает должность
            switch (inputPost)
            {
                case "0":
                    myWorker = new Consultant(Path);
                    break;
                case "1":
                    myWorker = new Manager(Path);
                    break;
                default:
                    Console.Write("Вы ввели недопустимый символ");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                    break;
            }
            myWorker.printPost();

            myWorker.putClients(Path);
            Console.ReadLine();

            myWorker.Load();
            myWorker.getInfo();
            myWorker.changeClientsList();
            myWorker.Load();
            myWorker.getInfo();
            Console.ReadLine();
        }
    }
}
