﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using UWay.Skynet.Cloud.Data.Common;
using UWay.Skynet.Cloud.Data.Dialect;
using UWay.Skynet.Cloud.Data.Dialect.ExpressionBuilder;
using UWay.Skynet.Cloud.Data.Dialect.SqlBuilder;
using UWay.Skynet.Cloud.Data.Driver;
using UWay.Skynet.Cloud.Data.LinqToSql;
using UWay.Skynet.Cloud.Data.Schema.Loader;
using UWay.Skynet.Cloud.Data.Schema.Script.Executor;
using UWay.Skynet.Cloud.Data.Schema.Script.Generator;
using UWay.Skynet.Cloud;
using UWay.Skynet.Cloud.Reflection;
using UWay.Skynet.Cloud.Mapping;

namespace UWay.Skynet.Cloud.Data
{

    partial class DbConfiguration
    {
        static readonly IDictionary<string, DbConfiguration> items;
        static readonly Dictionary<string, DbConfigurationInfo> Options;

        /// <summary>
        /// 
        /// </summary>
        public DbConfigurationInfo Option { get; set; }


        static DbConfiguration()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(s =>
            {
                var dlinqAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.GetName().Name == "System.Data.Linq");
                if (dlinqAsm != null)
                    ULinq.Init(dlinqAsm);
                var dataAnnotiationAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.GetName().Name == DataAnnotationMappingAdapter.StrAssemblyName);
                if (dataAnnotiationAsm != null)
                    DataAnnotationMappingAdapter.Init(dataAnnotiationAsm);
                var efDataAnnotiationAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(p => p.GetName().Name == EFDataAnnotiationAdapter.StrAssemblyName);
                if (efDataAnnotiationAsm != null)
                    EFDataAnnotiationAdapter.Init(efDataAnnotiationAsm);
                //初始化PrimitiveMapper
                Converter.IsPrimitiveType(Types.Boolean);
                //初始化Expressor
                var len = MethodRepository.Len;
                var mappings = UWay.Skynet.Cloud.Data.Common.MethodMapping.Mappings;

                mre.Set();
            }
           );

            Options = new Dictionary<string, DbConfigurationInfo>(StringComparer.Ordinal);
            items = new Dictionary<string, DbConfiguration>(StringComparer.Ordinal);


            Options["MySql.Data.MySqlClient"] = new DbConfigurationInfo
            {
                Driver = new MySqlDriver(),
                Dialect = new MySqlDialect(),
                FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.MySQL.MySqlFunctionRegistry(),
                DbExpressionBuilder = new MySqlExpressionBuilder(),
                SqlBuilder = (dialect, funcRegistry) => new MySqlBuilder { Dialect = dialect, FuncRegistry = funcRegistry },

                ScriptGenerator = () => new MySQLScriptGenerator(),
                ScriptExecutor = () => new MySQLScriptExecutor(),
                SchemaLoader = () => new MySqlSchemaLoader(),

            };

            Options["Oracle.ManagedDataAccess.Client"] = new DbConfigurationInfo
            {
                Driver = new OracleODPDriver(),
                Dialect = new OracleDialect(),
                FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.Oracle.OracleFunctionRegistry(),
                DbExpressionBuilder = new OracleExpressionBuilder(),
                SqlBuilder = (dialect, funcRegistry) => new OracleSqlBuilder { Dialect = dialect, FuncRegistry = funcRegistry },

                ScriptGenerator = () => new OracleScriptGenerator(),
                ScriptExecutor = () => new OracleScriptExecutor(),
                SchemaLoader = () => new OracleSchemaLoader(),
            };

            Options[DbProviderNames.SqlServer] = new DbConfigurationInfo
            {
                Driver = new SqlServer2005Driver(),
                Dialect = new MsSql2005Dialect(),
                FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.MsSql.MsSql2005FunctionRegistry(),
                DbExpressionBuilder = new MsSql2005ExpressionBuilder(),
                SqlBuilder = (dialect, funcRegistry) => new SqlServer2005SqlBuilder { Dialect = dialect, FuncRegistry = funcRegistry },

                ScriptGenerator = () => new SqlServerScriptGenerator(),
                ScriptExecutor = () => new SqlServerScriptExecutor(),
                SchemaLoader = () => new SqlServerSchemaLoader(),
            };

            //Options["System.Data.SQLite"] = new DbConfigurationInfo
            //{
            //    Driver = new SQLiteDriver(),
            //    Dialect = new SQLiteDialect(),
            //    FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.SQLite.SQLiteFunctionManager(),
            //    DbExpressionBuilder = new SQLiteExpressionBuilder(),
            //    SqlBuilder = (dialect, funcRegistry) => new SQLiteSqlBuilder { Dialect = dialect, FuncRegistry = funcRegistry },

            //    ScriptGenerator = () => new SQLiteScriptGenerator(),
            //    ScriptExecutor = () => new SQLiteScriptExecutor(),
            //    SchemaLoader = () => new SQLiteSchemaLoader(),

            //};

            //Options["System.Data.SqlServerCe.3.5"] = new DbConfigurationInfo
            //{
            //    Driver = new SqlCeDriver(),
            //    Dialect = new SqlCe35Dialect(),
            //    FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe.SqlCeFunctionRegistry(),
            //    DbExpressionBuilder = new SqlCe35ExpressionBuilder(),
            //    SqlBuilder = (dialect, funcRegistry) => new SqlCeBuilder { Dialect = dialect, FuncRegistry = funcRegistry },

            //    ScriptGenerator = () => new SqlCeScriptGenerator(),
            //    ScriptExecutor = () => new SqlCeScriptExecutor(),
            //    SchemaLoader = () => new SqlCeSchemaLoader(),

            //};

            //Options["System.Data.SqlServerCe.4.0"] = new DbConfigurationInfo
            //{
            //    Driver = new SqlCeDriver(),
            //    Dialect = new SqlCe35Dialect(),
            //    FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.SqlCe.SqlCeFunctionRegistry(),
            //    DbExpressionBuilder = new SqlCe35ExpressionBuilder(),
            //    SqlBuilder = (dialect, funcRegistry) => new SqlCeBuilder { Dialect = dialect, FuncRegistry = funcRegistry },

            //    ScriptGenerator = () => new SqlCeScriptGenerator(),
            //    ScriptExecutor = () => new SqlCeScriptExecutor(),
            //    SchemaLoader = () => new SqlCeSchemaLoader(),

            //};

            //providerNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            //foreach (var item in DbProviderFactories.GetFactoryClasses().Rows.Cast<DataRow>().Select(p => p["InvariantName"] as string))
            //    providerNames.Add(item);

            mre.WaitOne();
            mre.Close();
        }


        internal static DbConfiguration Get(DbContextOption dbContextOption)
        {
            Guard.NotNull(dbContextOption, "dbContextOption");
            DbConfiguration cfg;
            items.TryGetValue(dbContextOption.Container, out cfg);
            if (cfg == null)
            {
                //自动配置
                cfg = DbConfiguration.Configure(dbContextOption);

            }
            //if(cfg.sqlLogger == null)
            //{
            //    cfg.sqlLogger = dbContextOption.Logger;
            //}
            return cfg;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public static void Configure(IEnumerable<DbContextOption> dbContextOptions)
        {
            dbContextOptions.ForEach(item =>
            {
                Configure(item);
            });

        }

        /// <summary>
        /// 通过connectionStringName对象创建DbConfiguration对象（可以用于配置文件中有多个数据库连接字符串配置）
        /// </summary>
        /// <param name="dbContextOption"></param>
        /// <returns></returns>
        public static DbConfiguration Configure(DbContextOption dbContextOption)
        {
            Guard.NotNullOrEmpty(dbContextOption.Container, "connectionStringName");
            DbConfiguration cfg;
            if (items.TryGetValue(dbContextOption.Container, out cfg))
            {
                if (cfg.sqlLogger == null)
                    cfg.SetSqlLogger(() => dbContextOption.LogggerFactory.CreateLogger(dbContextOption.Container));
                return cfg;
            }
              

            //var item = UCloudConfiguration.Current.GetConnectionString(connectionStringName);

            if (string.IsNullOrEmpty(dbContextOption.Provider))
                throw new ConfigurationErrorsException("connectionString.ProviderName");
            var connectionString = dbContextOption.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                throw new ConfigurationErrorsException("ConnectionString");
            DbProviderFactory factory = null;
            try
            {
                factory = DbProviderFactoriesEx.GetFactory(dbContextOption.Provider);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(dbContextOption.Provider + " Provider name invalid for" + dbContextOption.Container, ex);
            }



            cfg = new DbConfiguration(dbContextOption.Provider, dbContextOption.Container, connectionString, dbContextOption.Default, factory);
            if (!string.IsNullOrEmpty(dbContextOption.MappingFile))
            {
                cfg.AddFile(dbContextOption.MappingFile);
            } else if (!string.IsNullOrEmpty(dbContextOption.ModuleAssemblyName))
            {
                var assembly = Assembly.Load(dbContextOption.ModuleAssemblyName);
                var types = assembly?.GetTypes();
                var list = types?.Where(t =>
                    t.IsClass && !t.IsGenericType && !t.IsAbstract &&
                    t.GetAttribute<TableAttribute>() != null).ToList();
                if (list != null && list.Any())
                {
                    list.ForEach(t =>
                    {
                        cfg.AddClass(t);
                    });
                }
            }

            cfg.SetSqlLogger(() => dbContextOption.LogggerFactory.CreateLogger(dbContextOption.Container));

            lock (items)
                items[cfg.Name] = cfg;

            AutoMatchDialect(cfg, connectionString, dbContextOption.Provider, factory); if (dbContextOption.ConnectionString != null)
                cfg.MakeDefault();

            return cfg;
        }

        ///// <summary>
        ///// 构造配置函数
        ///// </summary>
        ///// <param name="connectionStringName"></param>
        ///// <param name="connectionString"></param>
        ///// <param name="providerName"></param>
        ///// <param name="netType"></param>
        ///// <returns></returns>
        //public static DbConfiguration Configure(string connectionStringName, string connectionString, string providerName, NetType netType = NetType.LTE)
        //{
        //    Guard.NotNullOrEmpty(connectionStringName, "connectionStringName");
        //    DbConfiguration cfg;
        //    if (items.TryGetValue(connectionStringName, out cfg))
        //        return cfg;
        //    if (providerName.IsNullOrEmpty())
        //        throw new ConfigurationErrorsException(connectionStringName + " Provider name not exists or invalid for" + connectionStringName);

        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ConfigurationErrorsException("ConnectionString");


        //    DbProviderFactory factory = null;
        //    try
        //    {
        //        factory = DbProviderFactoriesEx.GetFactory(providerName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConfigurationErrorsException(providerName + " Provider name invalid for" + connectionStringName, ex);
        //    }



        //    cfg = new DbConfiguration(providerName, connectionStringName, connectionString, factory);
        //    cfg.AddMappingFile(netType);
        //    lock (items)
        //        items[cfg.Name] = cfg;

        //    AutoMatchDialect(cfg, connectionString, providerName, factory);

        //    return cfg;
        //}

        ///// <summary>
        ///// 构造配置函数
        ///// </summary>
        ///// <param name="connectionStringName"></param>
        ///// <param name="connectionString"></param>
        ///// <param name="providerName"></param>
        ///// <param name="netType"></param>
        ///// <returns></returns>
        //public static DbConfiguration Configure(string connectionStringName, string connectionString, string providerName, NetType netType, Data.DataBaseType databaseType)
        //{
        //    Guard.NotNullOrEmpty(connectionStringName, "connectionStringName");
        //    DbConfiguration cfg;
        //    if (items.TryGetValue(connectionStringName, out cfg))
        //        return cfg;
        //    if (providerName.IsNullOrEmpty())
        //        throw new ConfigurationErrorsException(connectionStringName + " Provider name not exists or invalid for" + connectionStringName);

        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ConfigurationErrorsException("ConnectionString");


        //    DbProviderFactory factory = null;
        //    try
        //    {
        //        factory = DbProviderFactoriesEx.GetFactory(providerName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConfigurationErrorsException(providerName + " Provider name invalid for" + connectionStringName, ex);
        //    }



        //    cfg = new DbConfiguration(providerName, connectionStringName, connectionString, factory);
        //    cfg.AddMappingFile(netType, databaseType);
        //    lock (items)
        //        items[cfg.Name] = cfg;

        //    AutoMatchDialect(cfg, connectionString, providerName, factory);

        //    return cfg;
        //}

        //public static DbConfiguration Configure(string connectionStringName, string connectionString, string providerName)
        //{
        //    Guard.NotNullOrEmpty(connectionStringName, "connectionStringName");
        //    DbConfiguration cfg;
        //    if (items.TryGetValue(connectionStringName, out cfg))
        //        return cfg;
        //    if (providerName.IsNullOrEmpty())
        //        throw new ConfigurationErrorsException(connectionStringName + " Provider name not exists or invalid for" + connectionStringName);

        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ConfigurationErrorsException("ConnectionString");


        //    DbProviderFactory factory = null;
        //    try
        //    {
        //        factory = DbProviderFactoriesEx.GetFactory(providerName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ConfigurationErrorsException(providerName + " Provider name invalid for" + connectionStringName, ex);
        //    }



        //    cfg = new DbConfiguration(providerName, connectionStringName, connectionString, factory);
        //    cfg.AddMappingFile(connectionStringName);
        //    lock (items)
        //        items[cfg.Name] = cfg;

        //    AutoMatchDialect(cfg, connectionString, providerName, factory);

        //    return cfg;
        //}

        private static void AutoMatchDialect(DbConfiguration cfg, string connectionString, string providerName, DbProviderFactory factory)
        {
            if (Options.ContainsKey(providerName))
            {
                cfg.Option = Options[providerName];
            }

            PopulateSqlServer2000(cfg, factory);
        }



        /// <summary>
        ///  通过connectionString和providerName创建DbConfiguration对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="providerName"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration Configure(string connectionString, string providerName, bool isDefault)
        {
            Guard.NotNullOrEmpty(connectionString, "connectionString");
            Guard.NotNullOrEmpty(providerName, "providerName");


            DbProviderFactory factory = null;
            try
            {
                factory = DbProviderFactoriesEx.GetFactory(providerName);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(providerName + " Provider name invalid", ex);
            }

            var cfg = new DbConfiguration(providerName, Guid.NewGuid().ToString(), connectionString, isDefault, factory);


            lock (items)
                items[cfg.Name] = cfg;

            AutoMatchDialect(cfg, connectionString, providerName, factory);
            return cfg;
        }

        /// <summary>
        /// 配置Access
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureAccess(string databaseFile, bool isDefault)
        {
            return Configure(BuildAccessConnectionString(databaseFile), DbProviderNames.Oledb, isDefault);
        }

        /// <summary>
        /// 配置SqlCe35
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSqlCe35(string databaseFile, bool isDefault)
        {
            return Configure(BuildSqlCeConnectionString(databaseFile), DbProviderNames.SqlCe35, isDefault);
        }

        /// <summary>
        /// 配置SqlCe4
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSqlCe4(string databaseFile, bool isDefault)
        {
            return Configure(BuildSqlCeConnectionString(databaseFile), DbProviderNames.SqlCe40, isDefault);
        }

        /// <summary>
        /// 配置SQLExpress
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSQLExpress(string databaseFile, bool isDefault)
        {
            return Configure(BuildSQLExpressConnectionString(databaseFile), DbProviderNames.SqlServer, isDefault);
        }

        /// <summary>
        /// 配置SQLite
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSQLite(string databaseFile, bool isDefault)
        {
            return Configure(BuildSQLiteConnectionString(databaseFile), DbProviderNames.SQLite, isDefault);
        }

        /// <summary>
        /// 配置SQLite
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="password"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSQLite(string databaseFile, string password, bool isDefault)
        {
            return DbConfiguration.Configure(BuildSQLiteConnectionString(databaseFile, password), DbProviderNames.SQLite, isDefault);
        }

        /// <summary>
        /// 配置SQLite
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="failIfMissing"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSQLite(string databaseFile, bool failIfMissing, bool isDefault)
        {
            return DbConfiguration.Configure(BuildSQLiteConnectionString(databaseFile, failIfMissing), DbProviderNames.SQLite, isDefault);
        }

        /// <summary>
        /// 配置SQLite
        /// </summary>
        /// <param name="databaseFile"></param>
        /// <param name="password"></param>
        /// <param name="failIfMissing"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSQLite(string databaseFile, string password, bool failIfMissing, bool isDefault)
        {
            return DbConfiguration.Configure(BuildSQLiteConnectionString(databaseFile, password, failIfMissing), DbProviderNames.SQLite, isDefault);
        }

        /// <summary>
        /// 配置MySQL
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureMySQL(string connectionString, bool isDefault)
        {
            return DbConfiguration.Configure(connectionString, DbProviderNames.MySQL, isDefault);
        }

        /// <summary>
        /// 配置SqlServer
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureSqlServer(string connectionString, bool isDefault)
        {
            return DbConfiguration.Configure(connectionString, DbProviderNames.SqlServer, isDefault);
        }

        /// <summary>
        /// 配置Oracle
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureOracle(string connectionString, bool isDefault)
        {
            return DbConfiguration.Configure(connectionString, DbProviderNames.Oracle, isDefault);
        }

        /// <summary>
        ///  配置Oracle ODP
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration ConfigureOracleODP(string connectionString, bool isDefault)
        {
            return DbConfiguration.Configure(connectionString, DbProviderNames.Oracle_ODP, isDefault);
        }

        private DbConnection connection;

        /// <summary>
        /// 通过DbConnection对象创建DbConfiguration对象
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="isDefault"></param>
        /// <param name="entityAssmbly"></param>
        /// <param name="mappFile"></param>
        /// <returns></returns>
        public static DbConfiguration Configure(DbConnection conn, string entityAssmbly, string mappFile, bool isDefault)
        {
            return Configure(Guid.NewGuid().ToString(), conn, entityAssmbly, mappFile, isDefault);
        }



        /// <summary>
        /// 通过DbConnection对象创建DbConfiguration对象
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration Configure(DbConnection conn, bool isDefault)
        {
            return Configure(Guid.NewGuid().ToString(), conn, null, null, isDefault);
        }

        /// <summary>
        /// 通过DbConnection对象创建DbConfiguration对象
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="entityAssembly"></param>
        /// <param name="containerName"></param>
        /// <param name="mappingFile"></param>
        /// <param name="isDefault"></param>
        /// <returns></returns>
        public static DbConfiguration Configure(string containerName, DbConnection conn, string entityAssembly, string mappingFile, bool isDefault)
        {
            Guard.NotNull(conn, "conn");
            var providerType = conn.GetType()
                .Assembly
                .GetTypes()
                .Where(t => typeof(System.Data.Common.DbProviderFactory).IsAssignableFrom(t)
                && t.Namespace == conn.GetType().Namespace)
                .FirstOrDefault()
                ;
            if (providerType == null)
                throw new NotSupportedException("not found 'DbProviderFactory'");
            var factory = providerType.GetField("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as DbProviderFactory;
            Guard.NotNull(factory, "factory");

            var providerName = providerType.Namespace;
            var cfg = new DbConfiguration(providerName, containerName, conn.ConnectionString, isDefault,factory);
            if (!Options.ContainsKey(providerName))
            {
                var dbtype = conn.GetType().Name;
                if (dbtype.StartsWith("MySql")) providerName = DbProviderNames.MySQL;
                else if (dbtype.StartsWith("SqlCe")) providerName = DbProviderNames.SqlCe35;
                else if (dbtype.StartsWith("Oledb")) providerName = DbProviderNames.Oledb;
                else if (dbtype.StartsWith("Oracle")) providerName = DbProviderNames.Oracle;
                else if (dbtype.StartsWith("SQLite")) providerName = DbProviderNames.SQLite;
                else if (dbtype.StartsWith("System.Data.SqlClient.")) providerName = DbProviderNames.SqlServer;
            }
            if (!string.IsNullOrEmpty(mappingFile))
            {
                cfg.AddFile(mappingFile);
            }
            else if (!string.IsNullOrEmpty(entityAssembly))
            {
                AddMappingClass(Assembly.Load(entityAssembly), cfg);

            } else
            {
                AddMappingClass(Assembly.GetExecutingAssembly(), cfg);
            }

            PopulateSqlServer2000(conn, factory, cfg);
            cfg.connection = conn;
            return cfg;
        }

        private static void AddMappingClass(Assembly assembly, DbConfiguration cfg) {
            var types = assembly?.GetTypes();
        var list = types?.Where(t =>
            t.IsClass && !t.IsGenericType && !t.IsAbstract &&
            t.GetAttribute<TableAttribute>() != null).ToList();
                if (list != null && list.Any())
                {
                    list.ForEach(t =>
                    {
                        cfg.AddClass(t);
                    });
                }
        }

        private static void PopulateSqlServer2000(DbConfiguration cfg, DbProviderFactory factory)
        {
            if (factory is System.Data.SqlClient.SqlClientFactory)
            {
                var connectionStringBuilder = factory.CreateConnectionStringBuilder();
                connectionStringBuilder.ConnectionString = cfg.ConnectionString;
                connectionStringBuilder["Database"] = "master";
                using (var conn = factory.CreateConnection())
                {
                    conn.ConnectionString = connectionStringBuilder.ConnectionString;
                    conn.Open();
                    var serverVersion = conn.ServerVersion;
                    var version = int.Parse(serverVersion.Substring(0, 2));
                    if (version < 9)
                    {
                        InitMsSql2000(cfg);
                    }
                }

            }
        }

        private static void InitMsSql2000(DbConfiguration cfg)
        {
            cfg.Option.Driver = new SqlServer2000Driver();
            cfg.Option.Dialect = new MsSql2000Dialect();
            cfg.Option.ScriptExecutor = () => new SqlServer2000ScriptExecutor();
            cfg.Option.FuncRegistry = new UWay.Skynet.Cloud.Data.Dialect.Function.MsSql.MsSqlFunctionRegistry();
            cfg.Option.DbExpressionBuilder = new MsSql2000ExpressionBuilder();
        }

        private static void PopulateSqlServer2000(DbConnection conn, DbProviderFactory factory, DbConfiguration cfg)
        {
            if (factory is System.Data.SqlClient.SqlClientFactory)
            {
                var connectionStringBuilder = factory.CreateConnectionStringBuilder();
                connectionStringBuilder.ConnectionString = cfg.ConnectionString;
                connectionStringBuilder["Database"] = "master";

                var state = conn.State;
                if (state != ConnectionState.Open)
                    conn.Open();

                var serverVersion = conn.ServerVersion;
                if (state != ConnectionState.Open)
                    conn.Close();

                var version = int.Parse(serverVersion.Substring(0, 2));
                if (version < 9)
                    InitMsSql2000(cfg);

            }
        }

    }
}
