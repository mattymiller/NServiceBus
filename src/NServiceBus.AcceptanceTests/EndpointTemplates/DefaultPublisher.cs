namespace NServiceBus.AcceptanceTests.EndpointTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using NServiceBus.AcceptanceTesting;
    using NServiceBus.AcceptanceTesting.Support;
    using NServiceBus.Config.ConfigurationSource;
    using NServiceBus.Pipeline;
    using NServiceBus.Pipeline.Contexts;

    public class DefaultPublisher : IEndpointSetupTemplate
    {
        public BusConfiguration GetConfiguration(RunDescriptor runDescriptor, EndpointConfiguration endpointConfiguration, IConfigurationSource configSource, Action<BusConfiguration> configurationBuilderCustomization)
        {
            return new DefaultServer(new List<Type> { typeof(SubscriptionTracer), typeof(SubscriptionTracer.Registration) }).GetConfiguration(runDescriptor, endpointConfiguration, configSource, b =>
            {
                b.Pipeline.Register<SubscriptionTracer.Registration>();
                configurationBuilderCustomization(b);
            });
        }

        class SubscriptionTracer : Behavior<OutgoingContext>
        {
            public ScenarioContext Context { get; set; }

            public override async Task Invoke(OutgoingContext context, Func<Task> next)
            {
                await next().ConfigureAwait(false);

                List<string> subscribers;

                if (context.TryGet("SubscribersForEvent", out  subscribers))
                {
                    Context.AddTrace(string.Format("Subscribers for {0} : {1}", context.MessageType.Name, string.Join(";", subscribers)));
                }

                bool nosubscribers;

                if (context.TryGet("NoSubscribersFoundForMessage", out nosubscribers) && nosubscribers)
                {
                    Context.AddTrace(string.Format("No Subscribers found for message {0}", context.MessageType.Name));
                }
            }

            public class Registration : RegisterStep
            {
                public Registration()
                    : base("SubscriptionTracer", typeof(SubscriptionTracer), "Traces the list of found subscribers")
                {
                }
            }
        }
    }
}