using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10._6_HomeWork_ConsoleApp_clients_base
{
    public struct Client
    {
        #region Поля
        /// <summary>
        /// Фамилия
        /// </summary>
        private string surname;

        /// <summary>
        /// Имя
        /// </summary>
        private string name;

        /// <summary>
        /// Отчество
        /// </summary>
        private string patronymic;

        /// <summary>
        /// Номер телефона
        /// </summary>
        private long phoneNumber;

        /// <summary>
        /// Серия паспорта
        /// </summary>
        private string rangePassport;

        /// <summary>
        /// Номер паспорта
        /// </summary>
        private string numberPassport;
        #endregion

        #region Свойства
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname
        {
            get { return this.surname; }
            set { this.surname = value; }
        }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic
        {
            get { return this.patronymic; }
            set { this.patronymic = value; }
        }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public long PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        /// <summary>
        /// Серия паспорта
        /// </summary>
        public string RangePassport
        {
            get { return this.rangePassport; }
            set { this.rangePassport = value; }
        }

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string NumberPassport
        {
            get { return this.numberPassport; }
            set { this.numberPassport = value; }
        }
        #endregion

        #region Конструктор
        public Client(string Surname, string Name, string Patronymic, long PhoneNumber, string RangePassport, string NumberPassport)
        {
            this.surname = Surname;
            this.name = Name;
            this.patronymic = Patronymic;
            this.phoneNumber = PhoneNumber;
            this.rangePassport = RangePassport;
            this.numberPassport = NumberPassport;
        }
        #endregion

        #region Методы
        public string Print()
        {
            return $"{this.surname,15}{this.name,15}{this.patronymic,15}{this.phoneNumber,17}{this.rangePassport,13}{this.numberPassport,15}";
        }
        #endregion
    }
}
