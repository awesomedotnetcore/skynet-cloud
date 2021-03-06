﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UWay.Skynet.Cloud.Nacos;
using System.Net.Http;

namespace Steeltoe.Configuration.NacosServerBase
{
    public static class ConfigNacosConfigurationBuilderExtensions
    {

        public static IConfigurationBuilder AddNacosConfigServer(this IConfigurationBuilder configurationBuilder, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {
            return configurationBuilder.AddNacosConfigServer(ConfigNacosClientSettings.DEFAULT_ENVIRONMENT, Assembly.GetEntryAssembly()?.GetName().Name, clientFactory, processor, logFactory);
        }

        public static IConfigurationBuilder AddNacosConfigServer(this IConfigurationBuilder configurationBuilder,  string environment, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {
            return configurationBuilder.AddNacosConfigServer(environment, Assembly.GetEntryAssembly()?.GetName().Name, clientFactory,processor, logFactory);
        }

        public static IConfigurationBuilder AddNacosConfigServer(this IConfigurationBuilder configurationBuilder, string environment, string applicationName, IHttpClientFactory clientFactory,  ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            var settings = new ConfigNacosClientSettings()
            {
                Name = applicationName ?? Assembly.GetEntryAssembly()?.GetName().Name,

                Environment = environment ?? ConfigNacosClientSettings.DEFAULT_ENVIRONMENT
            };

            return configurationBuilder.AddNacosConfigServer(settings, clientFactory, processor, logFactory);
        }

        public static IConfigurationBuilder AddNacosConfigServer(this IConfigurationBuilder configurationBuilder, ConfigNacosClientSettings defaultSettings, IHttpClientFactory clientFactory, ILocalConfigInfoProcessor processor, ILoggerFactory logFactory = null)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (defaultSettings == null)
            {
                throw new ArgumentNullException(nameof(defaultSettings));
            }

            if (!configurationBuilder.Sources.Any(c => c.GetType() == typeof(CloudFoundryConfigurationSource)))
            {
                configurationBuilder.Add(new CloudFoundryConfigurationSource());
            }

            configurationBuilder.Add(new ConfigNacosConfigurationSource(defaultSettings, configurationBuilder.Sources, clientFactory, processor, configurationBuilder.Properties,  logFactory));
            return configurationBuilder;
        }
    }
}
