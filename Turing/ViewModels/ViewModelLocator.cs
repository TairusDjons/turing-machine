using CommonServiceLocator;

namespace Turing.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}
