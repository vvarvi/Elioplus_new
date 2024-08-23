using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Configuration;

namespace WdS.ElioPlus
{
    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private readonly object _lockObject = new object();
        private bool _started;

        private BackgroundJobServer _backgroundJobServer;

        private HangfireBootstrapper()
        {
        }

        public void Start()
        {
            lock (_lockObject)
            {
                if (_started) return;
                _started = true;

                HostingEnvironment.RegisterObject(this);

                //GlobalConfiguration.Configuration
                //    .UseSqlServerStorage("ConnectionString");
                //    .UseMongoStorage("mongodb://eliousr:QcR2#tPp@10.215.0.1", "ElioHangfire");

                //var migrationOptions = new MongoMigrationOptions
                //{
                //    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                //    BackupStrategy = new CollectionMongoBackupStrategy()
                //};

                //string conStringMongoDB = (ConfigurationManager.ConnectionStrings["ConnectionStringHangfire"] != null && ConfigurationManager.ConnectionStrings["ConnectionStringHangfire"].ToString() != "") ? ConfigurationManager.ConnectionStrings["ConnectionStringHangfire"].ToString() : "";
                //string mongoDBName = (ConfigurationManager.AppSettings["mongoDBNameHangfire"] != null && ConfigurationManager.AppSettings["mongoDBNameHangfire"].ToString() != "") ? ConfigurationManager.AppSettings["mongoDBNameHangfire"].ToString() : "";

                //if (conStringMongoDB != "" && mongoDBName != "")
                //{
                //    GlobalConfiguration.Configuration
                //    //.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                //    //.UseSimpleAssemblyNameTypeSerializer()
                //    //.UseRecommendedSerializerSettings()
                //    .UseMongoStorage(conStringMongoDB, mongoDBName, new MongoStorageOptions { MigrationOptions = migrationOptions });
                //}

                //_backgroundJobServer = new BackgroundJobServer();
            }
        }

        public void Stop()
        {
            lock (_lockObject)
            {
                if (_backgroundJobServer != null)
                {
                    _backgroundJobServer.Dispose();
                }

                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}