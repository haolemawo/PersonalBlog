using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Post.EntityFrameworkCore.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<PostDbContext> dbContextOptions,
            string connectionString)
        {
            dbContextOptions.UseMySql(connectionString);
        }
    }
}
