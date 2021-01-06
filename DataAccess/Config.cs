using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess
{
    public static class Config
    {
        public static String GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path);

            var root = configurationBuilder.Build();
            String ConnectionString = root.GetConnectionString("SIF");

            return ConnectionString;
        }
    }
}
