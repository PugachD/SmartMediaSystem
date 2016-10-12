using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMediaSystems
{
    class DataBaseWork
    {

        /// <summary>
        /// Соединение с БД
        /// </summary>
        /// <returns>true - соединение успешно, false - нет</returns>
        public bool ConnectToDB() { return true; }

        /// <summary>
        /// Считывание с базы данных в список List<SourceData>
        /// </summary>
        /// <returns >true - извлечение успешно, false - нет</returns>
        public bool ReadFromDB() { return true; }

    }
}
