using PolRegio.Domain.Services.EventBus;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace PolRegio.Domain.Models.Events
{
    public class UmbracoContentSavedEvent : IEvent
    {
        public IContentService Sender { get; set; }
        public SaveEventArgs<IContent> Args { get; set; }

        public UmbracoContentSavedEvent(IContentService sender, SaveEventArgs<IContent> args)
        {
            Sender = sender;
            Args = args;
        }
    }
}