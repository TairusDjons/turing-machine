using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Turing.IO;
using Turing.ViewModels;
using Turing.IService;
using Turing.WPF.Services;

namespace Turing.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TuringMachineFactory>().As<ITuringMachineFactory>().SingleInstance();
            builder.RegisterType<TuringCommandParser>().As<ITuringCommandParser>().SingleInstance();
            builder.RegisterType<OpenFileDialogService>().As<IOpenFileDialogService>().SingleInstance();
            builder.RegisterType<MainViewModel>().SingleInstance();
            var container = builder.Build();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
            base.OnStartup(e);
        }
    }
}
