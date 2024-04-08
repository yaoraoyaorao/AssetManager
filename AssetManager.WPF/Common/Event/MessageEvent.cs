using Prism.Events;

namespace AssetManager.WPF.Common.Event
{
    public class MessageModel
    {
        public string Filter { get; set; }
        public string Message { get; set; }
    }

    public class MessageEvent : PubSubEvent<MessageModel>
    {

    }
}
