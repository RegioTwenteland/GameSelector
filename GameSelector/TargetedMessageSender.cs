namespace GameSelector
{
    internal class TargetedMessageSender
    {
        private ControllerId _sender;
        private ControllerId _recipient;

        private MessageSender _messageSender;

        public TargetedMessageSender(ControllerId recipient, MessageSender messageSender)
        {
            _recipient = recipient;
            _messageSender = messageSender;
        }

        public void Bind(ControllerId sender)
        {
            _messageSender.Bind(sender);
        }

        public void Send(string key, object value = null)
        {
            _messageSender.Send(_recipient, key, value);
        }
    }
}
