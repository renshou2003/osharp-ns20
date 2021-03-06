﻿using System;

using Microsoft.Extensions.DependencyInjection;

using OSharp.Core;
using OSharp.Core.Builders;
using OSharp.Core.Packs;

using Shouldly;

using Xunit;


namespace OSharp.Dependency.Tests
{
    public class ServiceLocatorTests
    {
        [Fact]
        public void Instance_Test()
        {
            ServiceLocator locator1 = ServiceLocator.Instance;
            ServiceLocator locator2 = ServiceLocator.Instance;
            locator2.ShouldBe(locator1);
        }

        [Fact]
        public void TrySetApplicationServiceProvider_Test()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<ServiceLocatorTests, ServiceLocatorTests>();
            services.AddLogging();
            services.AddOSharp(b => b.AddCorePack());

            IServiceProvider provider = services.BuildServiceProvider();
            OSharpPackManager packManager = provider.GetService<OSharpPackManager>();
            packManager.UsePacks(provider);

            ServiceLocator.Instance.GetService<ServiceLocatorTests>().ShouldNotBeNull();
        }
    }
}
