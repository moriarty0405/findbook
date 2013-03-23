﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using findbook.Domain.Abstract;
using findbook.Domain.Concrete;
using findbook.WebUI.Infrastructure.Abstract;
using findbook.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure {
        public class NinjectDependencyResolver : IDependencyResolver {
                private IKernel kernel;
                public NinjectDependencyResolver() {
                        kernel = new StandardKernel();
                        AddBindings();
                }
                public object GetService(Type serviceType) {
                        return kernel.TryGet(serviceType);
                }
                public IEnumerable<object> GetServices(Type serviceType) {
                        return kernel.GetAll(serviceType);
                }
                public IBindingToSyntax<T> Bind<T>() {
                        return kernel.Bind<T>();
                }
                public IKernel Kernel {
                        get { return kernel; }
                }

                private void AddBindings() {
                        // put additional bindings here
                        Bind<IUsersRepository>().To<EFUsersRepository>();

                        Bind<IAuthProvider>().To<FormsAuthProvider>();
                }
        }
}