using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FugasDetectionSystem.Domain.Interfaces
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }
    }
}
