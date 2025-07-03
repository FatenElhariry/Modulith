using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.Application.DI
{
    public static class Helper
    {
        public static IEnumerable<Assembly> GetApplicationAssemply()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.StartsWith("EShop")).ToList();
            // check missing assemblies and add them if necessary
            var pathLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var file in Directory.GetFiles(pathLocation, "EShop.*.dll"))
            {
                if (!assembly.Any(a => a.Location.Equals(file, StringComparison.OrdinalIgnoreCase)))
                {
                    assembly.Add(Assembly.LoadFrom(file));
                }
            }
            return assembly;
        }

    }
}
