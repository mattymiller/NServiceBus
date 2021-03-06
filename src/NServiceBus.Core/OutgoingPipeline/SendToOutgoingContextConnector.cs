namespace NServiceBus
{
    using System;
    using NServiceBus.OutgoingPipeline;
    using NServiceBus.Pipeline;
    using NServiceBus.Pipeline.Contexts;

    class SendToOutgoingContextConnector:StageConnector<OutgoingSendContext,OutgoingContext>
    {
        public override void Invoke(OutgoingSendContext context, Action<OutgoingContext> next)
        {
            next(new OutgoingContext(context));
        }
    }
}