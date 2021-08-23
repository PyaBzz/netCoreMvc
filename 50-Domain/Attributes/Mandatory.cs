using System;
using System.ComponentModel.DataAnnotations;
using Baz.Core;

namespace myCoreMvc.Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Mandatory : Attribute
    {
    }
}
