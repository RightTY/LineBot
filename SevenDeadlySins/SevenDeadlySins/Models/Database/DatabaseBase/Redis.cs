using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins.Models.Database.DatabaseBase
{
    /// <summary>
    /// Redis
    /// </summary>
    public class Redis
    {
        /// <summary>
        /// Connection
        /// </summary>
        public string Connection { get; set; }
        /// <summary>
        /// InstanceName
        /// </summary>
        public string InstanceName { get; set; }
    }
}
