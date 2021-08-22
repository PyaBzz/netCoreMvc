﻿using Baz.Core;
using myCoreMvc.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace myCoreMvc.Domain
{
    public class User : Thing
    {
        [Persist]
        public string Name { get; set; }
        [Persist]
        public DateTime DateOfBirth { get; set; }
        [Persist]
        public string Role { get; set; }
        [Persist]
        public byte[] Salt { get; set; }
        [Persist]
        public string Hash { get; set; }
    }
}
