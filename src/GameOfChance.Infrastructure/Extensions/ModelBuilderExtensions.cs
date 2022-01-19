using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameOfChance.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder modelBuilder, Assembly assembly = null, string configNamespace = "")
        {
            MethodInfo methodInfo = (from m in typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy)
                                     where m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase)
                                     where m.GetParameters().FirstOrDefault()!.ParameterType.Name == "IEntityTypeConfiguration`1"
                                     select m).FirstOrDefault();

            IEnumerable<Type> enumerable = from c in (assembly ?? Assembly.GetExecutingAssembly()).GetTypes()
                                           where c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters
                                           select c;
            if (!string.IsNullOrEmpty(configNamespace))
            {
                enumerable = enumerable.Where((Type c) => c.Namespace == configNamespace);
            }

            foreach (Type item in enumerable)
            {
                Type[] interfaces = item.GetInterfaces();
                foreach (Type type in interfaces)
                {
                    if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        methodInfo.MakeGenericMethod(type.GenericTypeArguments[0]).Invoke(modelBuilder, new object[1]
                        {
                            Activator.CreateInstance(item)
                        });
                        Console.WriteLine("applied model " + item.Name);
                        break;
                    }
                }
            }
        }
    }
}
