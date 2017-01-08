using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace LP.Common.DB
{
    public class MongoConnect : IMongoConnect
    {
        private static IMongoClient _client = null;
        private static IMongoDatabase _defaultDB = null;

        public void SetConnectCfg(string connectString, string defaultDBName)
        {
            _client = new MongoClient($"mongodb://{ connectString }");
            _defaultDB = _client.GetDatabase(defaultDBName);
        }

        public MongoConnect(string connectString, string dbName)
        {
            SetConnectCfg(connectString, dbName);
        }

        public IMongoDatabase GetDatabase(string name)
        {
            return _client.GetDatabase(name);
        }

        public IMongoDatabase GetDatabase()
        {
            return _defaultDB;
        }

        public void Disconnect()
        {
        }

        public string PrintCfg()
        {
            return $"DB:{_defaultDB.DatabaseNamespace.DatabaseName}; IP:{_client.Cluster.Settings.EndPoints.First()}";
        }
    }

    public interface IMongoConnect
    {
        void SetConnectCfg(string connectString, string defaultDBName);
        IMongoDatabase GetDatabase();
        IMongoDatabase GetDatabase(string name);
        string PrintCfg();
    }

    public interface IEntity
    {
    }
}
