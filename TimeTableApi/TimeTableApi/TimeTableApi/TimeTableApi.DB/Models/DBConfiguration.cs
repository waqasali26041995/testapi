using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableApi.DB.Models
{
    public class DBConfiguration : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
