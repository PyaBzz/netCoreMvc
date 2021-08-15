using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace myCoreMvc.DbMigrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var paramCount = args.Length;
            if (paramCount < 2)
            {
                throw new ArgumentException("Exactly two parameters are required");
            }
            DbMigrator.Go(args);
        }
    }
}
