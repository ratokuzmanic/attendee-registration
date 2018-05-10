using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace DumpDays.AttendeeRegistration.Api
{
    public static class IoCConfig
    {
        public static IContainer RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            var executingAssembly = ExecutingAssembly;
            builder.RegisterApiControllers(executingAssembly);

            var allAssemblies = AllSolutionRelatedAssemblies;

            builder.RegisterAssemblyTypes(allAssemblies)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(allAssemblies)
                .Where(t => t.Name.EndsWith("Command"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(allAssemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            builder.RegisterAssemblyTypes(allAssemblies)
                .Where(t => t.Name.EndsWith("Context"))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerRequest();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            return container;
        }

        private static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();

        private static Assembly[] AllSolutionRelatedAssemblies =>
            Directory.EnumerateFiles
            (
                AppDomain.CurrentDomain.BaseDirectory,
                @"DumpDays.*.dll",
                SearchOption.AllDirectories
            )
            .Select(Assembly.LoadFrom)
            .ToArray();
    }
}
