namespace Client
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;

    public class Vm : INotifyPropertyChanged
    {
        public Vm()
        {
            //SendCommand= new RelayCommand(o=>ServerRef);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public RelayCommand SendCommand { get; private set; }

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
