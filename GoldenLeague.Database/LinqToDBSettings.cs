using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Database
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class LinqToDBSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";
        public string ConnString { get; }

        public LinqToDBSettings(string connString)
        {
            ConnString = connString;
        }

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "SqlServer",
                        ProviderName = "SqlServer",
                        ConnectionString = ConnString
                    };
            }
        }
    }
}
