namespace Client
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Annotations;
    using Contracts;
    using ServerRef;

    public class Vm : INotifyPropertyChanged
    {
        private int _messageCount;
        private Service1Client _server;
        public Vm()
        {

            _server = new ServerRef.Service1Client();
            SendCommand = new RelayCommand(o =>
            {
                _server.SendMessage(new DummyMessage { Message = Message, Time = DateTime.UtcNow });
                Message = null;
            });
            Poll();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Message { get; set; }
        public RelayCommand SendCommand { get; private set; }
        public int MessageCount
        {
            get { return _messageCount; }
            set
            {
                if (value == _messageCount)
                {
                    return;
                }
                _messageCount = value;
                OnPropertyChanged();
            }
        }

        private async void Poll()
        {
            while (true)
            {
                await Task.Delay(100);
                MessageCount = _server.GetMessageCount();
            }
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
