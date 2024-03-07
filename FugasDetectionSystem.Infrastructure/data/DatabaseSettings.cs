using FugasDetectionSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Infrastructure.data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; } = string.Empty;


        public DatabaseSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }   
    }
}
