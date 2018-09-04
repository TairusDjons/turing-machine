using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Turing.IO;

namespace Turing.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TuringMachine>().As<ITuringMachine>().SingleInstance();
            builder.RegisterType<TuringCommandParser>().As<ITuringCommandParser>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();
            var container = builder.Build();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
    }
}
