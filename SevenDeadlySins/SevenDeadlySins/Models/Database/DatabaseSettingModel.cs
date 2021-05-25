using SevenDeadlySins.Models.Database.DatabaseBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Database
{
    public class DatabaseSettingModel
    {
        /// <summary>
        /// MSSQL
        /// </summary>
        public MsSql MSSQL { get; set; }
        /// <summary>
        /// MySQL
        /// </summary>
        public MySql MySQL { get; set; }
    }
}
