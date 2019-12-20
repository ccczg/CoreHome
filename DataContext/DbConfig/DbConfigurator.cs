﻿using DataContext.ModelDbContext;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace DataContext.DbConfig
{
    public class DbConfigurator
    {
        private const string dbConnectionString = "server=localhost;user id=root;password=lxy15937905153;database=articles";
        private const string redisConnectionString = "localhost";

        private static ArticleDbContext dbContext = null;
        private static ConnectionMultiplexer cacheContext = null;

        private DbConfigurator() { }

        ~DbConfigurator()
        {
            dbContext.Dispose();
            cacheContext.Dispose();
        }

        public static ArticleDbContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    DbContextOptionsBuilder<ArticleDbContext> optionBuilder = new DbContextOptionsBuilder<ArticleDbContext>();
                    optionBuilder.UseMySQL(dbConnectionString);
                    dbContext = new ArticleDbContext(optionBuilder.Options);
                    dbContext.Database.EnsureCreatedAsync();
                }
                return dbContext;
            }
        }

        public static ConnectionMultiplexer CacheContext
        {
            get
            {
                if (cacheContext == null)
                {
                    cacheContext = ConnectionMultiplexer.Connect(redisConnectionString);
                }
                return cacheContext;
            }
        }

    }
}
