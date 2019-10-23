using System;
using System.Diagnostics;

namespace NLevezinho.Container.Core
{
    
    public interface IRegistration
    {
        Type Type { get; }
        
        Lifetime Lifetime { get; }

        Type? Implementation { get; }
    }
}
