using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using TuringMachine.IDE.Services;
using TuringMachine.IDE.ViewModels;
using TuringMachine.IDE.WPF.Services;
using TuringMachine.Format;

namespace TuringMachine.IDE.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected static void ConfigureServices()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<TuringFormat>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<OpenFileDialogService>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SaveFileDialogService>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ErrorDialogService>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MainViewModel>()
                .AsSelf()
                .InstancePerLifetimeScope();

            var container = builder.Build();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureServices();
        }
    }
}
