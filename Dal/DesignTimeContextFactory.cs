using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CroixRouge.Dal
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<bdCroixRougeContext>
    {
        private const string CONNECTION_STRING_CONFIG_KEY = "DesignTimeConnectionString";
        readonly string connectionString;
        public DesignTimeContextFactory()
        {
            var helper = new ConfigurationHelper(CONNECTION_STRING_CONFIG_KEY);
            connectionString = helper.GetConnectionString();
        }
        public bdCroixRougeContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<bdCroixRougeContext> builder = new DbContextOptionsBuilder<bdCroixRougeContext>();
            builder.UseSqlServer(connectionString);
            return new bdCroixRougeContext(builder.Options);
        }
    }
}