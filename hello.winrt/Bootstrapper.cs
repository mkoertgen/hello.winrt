using System;
using System.Collections.Generic;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using hello.winrt.Models;
using hello.winrt.Views;

namespace hello.winrt
{
    public class Bootstrapper : BootstrapperBase
    {
        private IContainer _container;

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<MessageService>().As<IMessageService>().SingleInstance();

            builder.RegisterType<ShellViewModel>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            // TODO:
            //builder.RegisterModule<ScenarioModule>();

            _container = builder.Build();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return key == null ? _container.Resolve(service) : _container.ResolveNamed(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var type = typeof(IEnumerable<>).MakeGenericType(service);
            return _container.Resolve(type) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }
    }
}
