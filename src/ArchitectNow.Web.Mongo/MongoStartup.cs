﻿using System;
using ArchitectNow.Mongo;
using ArchitectNow.Web.Mongo.Configuration;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ArchitectNow.Web.Mongo
{
	public abstract class MongoStartup : StartupBase
	{
		protected MongoStartup(IHostingEnvironment env, ILoggerFactory loggerFactory) : base(env, loggerFactory)
		{
		}

		protected override IServiceProvider ConfigureServicesInternal(IServiceCollection services, Action<IServiceCollection> beforeCreateContainerAction = null)
		{
			if (beforeCreateContainerAction == null)
			{
				beforeCreateContainerAction = collection => { };
			}

			beforeCreateContainerAction += collection =>
			{
				if (Features.UseHangfire)
				{
					services.ConfigureHangfire(GetHangfireConnectionString, GetHangfireDatabaseName(), ConfigureHangfire);
				}
			};

			return base.ConfigureServicesInternal(services, beforeCreateContainerAction);
		}
		
		protected abstract string GetHangfireDatabaseName();

		protected override void ConfigureAutofac(ContainerBuilder containerBuilder)
		{
			base.ConfigureAutofac(containerBuilder);
			containerBuilder.RegisterModule<MongoModule>();
		}
    }
}