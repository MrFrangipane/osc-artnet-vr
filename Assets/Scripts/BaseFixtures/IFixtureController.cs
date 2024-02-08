using System;
using System.Collections.Generic;

namespace Scripts
{
    public interface IFixtureController
    {
        List<ushort> Channels { get; }
        Int16 Address { get;  }
    }
}
