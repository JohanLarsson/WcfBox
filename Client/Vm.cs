namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Annotations;
    using Contracts;
    using Localization;
    using ServerRef;

    public class Vm : INotifyPropertyChanged
    {
        private int _messageCount;
        private CultureInfo _selectedLanguage;
        public Vm()
        {
            SendCommand = new RelayCommand(async o =>
            {
                using (var client = new ServerRef.Service1Client())
                {
                    await client.SendMessageAsync(new DummyMessage { Message = Message, Time = DateTime.UtcNow });
                }
                Message = null;
            });
            Poll();
            SelectedLanguage = Languages.First();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Message { get; set; }
        public RelayCommand SendCommand { get; private set; }
        public CultureInfo SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (Equals(value, _selectedLanguage))
                {
                    return;
                }
                _selectedLanguage = value;
                TranslationManager.Instance.CurrentLanguage = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<CultureInfo> Languages { get { return TranslationManager.Instance.Languages; } } 
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
                using (var client = new Service1Client())
                {
                    MessageCount = client.GetMessageCount();
                }
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
