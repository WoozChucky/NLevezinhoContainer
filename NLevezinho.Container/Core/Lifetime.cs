namespace NLevezinho.Container.Core
{
    /// <summary>
    /// IRegistration object''s lifetime.
    /// </summary>
    public enum Lifetime
    {
        /// <summary>
        /// Unique.
        /// </summary>
        Singleton,

        /// <summary>
        /// New instance for each request.
        /// </summary>
        Scoped,

        /// <summary>
        /// New instance every time.
        /// </summary>
        Transient
    }
}
