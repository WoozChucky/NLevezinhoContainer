using System;

namespace NLevezinho.Container.Core
{
    internal class Registration : IRegistration
    {
        public Type Type { get; }
        public Lifetime Lifetime { get; }
        public Type Implementation { get; }

        internal Registration(Type type, Lifetime lifetime)
        {
            Type = type;
            Implementation = null;
            Lifetime = lifetime;
        }

        internal Registration(Type type, Type implementation, Lifetime lifetime)
        {
            Type = type;
            Implementation = implementation;
            Lifetime = lifetime;
        }
    }
}
