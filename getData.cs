﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    interface getData
    {
        void getInfo();

        void changeClientsPhoneNumberBySurname(string Surname, long PhoneNumber);
    }
}
