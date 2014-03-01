namespace Client
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Annotations;

    public class Vm : INotifyPropertyChanged
    {
        private int _messageCount;
        public Vm()
        {
            Poll();
            //SendCommand= new RelayCommand(o=>ServerRef);
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
                await Task.Delay(500);

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
