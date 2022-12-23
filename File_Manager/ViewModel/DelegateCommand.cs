using System;
using System.Windows.Input;

namespace File_Manager.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canEx;
        private ICommand goHome;

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canEx = null)
        {
            this.execute = execute;
            this.canEx = canEx;
        }

        public DelegateCommand(ICommand goHome)
        {
            this.goHome = goHome;
        }

        public bool CanExecute(object parameter)
        {
            return canEx == null || canEx.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}