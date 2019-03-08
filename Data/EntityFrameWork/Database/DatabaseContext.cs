using Data.Context;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;


namespace Data.Database
{
    public class DatabaseContext : BaseContext
    {
        public DatabaseContext() : base("name=DatabaseContext")
        {

        }

        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer<DatabaseContext>(null);
        }

        public DatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var typesToRegister = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes()
                            .Where(type => !string.IsNullOrEmpty(type.Namespace))
                            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))).ToList();

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
