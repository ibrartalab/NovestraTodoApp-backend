using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Core.Options
{
    public class ConnectionStringOptions
    {
        public const string ConnectionString = "ConnectionStrings";

        public string DefaultConnection { get; set; } = string.Empty;
    }
}
