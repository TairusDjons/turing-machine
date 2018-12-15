using CommonServiceLocator;

namespace TuringMachine.IDE.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}
