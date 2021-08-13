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
            if (paramCount == 2)
            {
                DbMigrator.Go(args[0], args[1]);
            }
            else
            {
                throw new ArgumentException("Exactly two parameters are required");
            }
        }
    }
}
