using System;
using Caliburn.Micro;
using hello.winrt.Models;

namespace hello.winrt.Views
{
    public class ShellViewModel : Screen
    {
        private readonly IMessageService _messageService;

        public ShellViewModel(IEventAggregator events, IMessageService messageService)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));
            if (messageService == null) throw new ArgumentNullException(nameof(messageService));

            events.SubscribeOnPublishedThread(this);
            _messageService = messageService;

            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            DisplayName = "hello.winrt";
        }

        public void Handle(MessageEvent message)
        {
            message.Result = _messageService.Show(
                message.Message, message.Caption, message.Button, message.Icon);
        }
    }
}
