namespace NServiceBus
{
    using System;

    /// <summary>
    /// Defines a bus to be used with NServiceBus.
    /// </summary>
    public interface IBus : ISendOnlyBus
    {
        /// <summary>
        /// Subscribes to receive published messages of the specified type.
        /// This method is only necessary if you turned off auto-subscribe.
        /// </summary>
        /// <param name="messageType">The type of message to subscribe to.</param>
        void Subscribe(Type messageType);

        /// <summary>
        /// Subscribes to receive published messages of type T.
        /// This method is only necessary if you turned off auto-subscribe.
        /// </summary>
        /// <typeparam name="T">The type of message to subscribe to.</typeparam>
        void Subscribe<T>();

        /// <summary>
        /// Unsubscribes from receiving published messages of the specified type.
        /// </summary>
        /// <param name="messageType">The type of message to subscribe to.</param>
        void Unsubscribe(Type messageType);

        /// <summary>
        /// Unsubscribes from receiving published messages of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of message to unsubscribe from.</typeparam>
        void Unsubscribe<T>();

        /// <summary>
        /// Sends the message to the endpoint which sent the message currently being handled on this thread.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="options">Options for this reply</param>
        void Reply(object message,ReplyOptions options);

        /// <summary>
        /// Instantiates a message of type T and performs a regular <see cref="Reply(object,ReplyOptions)"/>.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface</typeparam>
        /// <param name="messageConstructor">An action which initializes properties of the message</param>
        /// <param name="options">Options for this reply</param>
        void Reply<T>(Action<T> messageConstructor, ReplyOptions options);

        /// <summary>
        /// Returns a completion message with the specified error code to the sender
        /// of the message being handled. The type T can only be an enum or an integer.
        /// </summary>
        [ObsoleteEx(RemoveInVersion = "7.0", TreatAsErrorFromVersion = "6.0", Message = "Replaced by NServiceBus.Callbacks package")]
        void Return<T>(T errorEnum);

        /// <summary>
        /// Defers the processing of the message for the given delay. This feature is using the timeout manager so make sure that you enable timeouts
        /// </summary>
        [ObsoleteEx(RemoveInVersion = "7.0", TreatAsErrorFromVersion = "6.0", ReplacementTypeOrMember = "SendLocal(object message, SendLocalOptions options)")]
        // ReSharper disable UnusedParameter.Global
        ICallback Defer(TimeSpan delay, object message);
        // ReSharper restore UnusedParameter.Global

        /// <summary>
        /// Defers the processing of the message until the specified time. This feature is using the timeout manager so make sure that you enable timeouts
        /// </summary>
        [ObsoleteEx(RemoveInVersion = "7.0", TreatAsErrorFromVersion = "6.0", ReplacementTypeOrMember = "SendLocal(object message, SendLocalOptions options)")]
        // ReSharper disable UnusedParameter.Global
        ICallback Defer(DateTime processAt, object message);
        // ReSharper restore UnusedParameter.Global

        /// <summary>
        /// Moves the message being handled to the back of the list of available 
        /// messages so it can be handled later.
        /// </summary>
        void HandleCurrentMessageLater();

        /// <summary>
        /// Forwards the current message being handled to the destination maintaining
        /// all of its transport-level properties and headers.
        /// </summary>
        void ForwardCurrentMessageTo(string destination);

        /// <summary>
        /// Tells the bus to stop dispatching the current message to additional
        /// handlers.
        /// </summary>
        void DoNotContinueDispatchingCurrentMessageToHandlers();

        /// <summary>
        /// Gets the message context containing the Id, return address, and headers
        /// of the message currently being handled on this thread.
        /// </summary>
        IMessageContext CurrentMessageContext { get; }
    }
}
