using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace myCoreMvc.Models
{
    public class Thing : IClonable
    {
        public Guid Id { get; set; }
    }
}
