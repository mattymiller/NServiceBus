namespace NServiceBus.Routing
{
    using System;
    using NServiceBus.Pipeline;

    /// <summary>
    /// Provides context for unsubscribe requests
    /// </summary>
    public class UnsubscribeContext : BehaviorContext
    {
        /// <summary>
        /// Initializes the context with the given message type and parent context
        /// </summary>
        public UnsubscribeContext(BehaviorContext parentContext, Type messageType)
            : base(parentContext)
        {
        }
    }
}