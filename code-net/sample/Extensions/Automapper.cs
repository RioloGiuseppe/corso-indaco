using Microsoft.Extensions.DependencyInjection;
using sample.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Extensions
{
    public static class AutomapperEXT
    {
        public static void UseAutomapper(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(o => o.GetName().Name.StartsWith("sample"))
                .ToArray();
            services.AddAutoMapper(assemblies);
        }
    }
}
