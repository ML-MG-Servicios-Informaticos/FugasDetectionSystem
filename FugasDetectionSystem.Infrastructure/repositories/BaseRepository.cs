using FugasDetectionSystem.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Infrastructure.repositories
{
    public class BaseRepository
    {
        internal readonly string _connectionString;

        public BaseRepository(IDatabaseSettings databaseSettings)
        {
            _connectionString = databaseSettings.ConnectionString;
        }
    }
}
