using myCoreMvc.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myCoreMvc.App
{
    public enum TransactionResult { Added, Updated, Deleted, NotFound, Failed }
}
