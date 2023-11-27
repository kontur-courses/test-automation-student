using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiExample
{
    [SetUpFixture]
    public class SetUpContainer
    {
        //public static readonly IServiceProvider ContainerCache
        //    = new Container().BuildServiceProvider();

        [OneTimeTearDown]
        public async Task TearDown()
            => await (Container.GetRequiredService<ServiceProvider>() as ServiceProvider)!.DisposeAsync();
    }
}
