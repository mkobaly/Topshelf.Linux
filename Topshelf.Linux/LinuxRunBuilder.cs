using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf.Builders;

namespace Topshelf.Linux
{
	using System;
	using Hosts;
	using Logging;
	using Runtime;


	public class LinuxRunBuilder : RunBuilder
	{
		static readonly LogWriter _log = HostLogger.Get<LinuxRunBuilder>();

		public LinuxRunBuilder(HostEnvironment environment, HostSettings settings) : base(environment, settings)
		{
		}

		public override Host Build(ServiceBuilder serviceBuilder)
		{
			ServiceHandle serviceHandle = serviceBuilder.Build(Settings);
			return CreateHost(serviceHandle);
		}

		Host CreateHost(ServiceHandle serviceHandle)
		{
			if (Environment.IsRunningAsAService)
			{
				
				_log.Debug("Running as a service, creating service host.");
				return Environment.CreateServiceHost(Settings, serviceHandle);
			}

			_log.Debug("Running as a console application, creating the console host.");
			return new LinuxConsoleRunHost(Settings, Environment, serviceHandle);
		}
	}
}
