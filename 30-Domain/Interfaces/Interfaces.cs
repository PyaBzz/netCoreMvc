﻿using Baz.Core;
using System;

namespace myCoreMvc.Domain
{
    public interface ISavable : IClonable
    {
        Guid? Id { get; set; }
    }
}
