namespace NServiceBus.Routing
{
    using System;
    using NServiceBus.Pipeline;

    /// <summary>
    /// Provides context for subscription requests
    /// </summary>
    public class SubscribeContext:BehaviorContext
    {
        /// <summary>
        /// Initializes the context with the given message type and parent context
        /// </summary>
        public SubscribeContext(BehaviorContext parentContext, Type messageType) : base(parentContext)
        {
        }
    }
}