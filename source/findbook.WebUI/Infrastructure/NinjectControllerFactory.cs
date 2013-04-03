using System;
using System.Web.Mvc;
using Ninject;
using findbook.Domain.Abstract;
using findbook.Domain.Concrete;
using System.Web.Routing;

namespace findbook.WebUI.Infrastructure {
    public class NinjectControllerFactory : DefaultControllerFactory {
        private IKernel ninjectKernel;
        public NinjectControllerFactory() {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        Type controllerType) {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings() {
            // put additional bindings here
            ninjectKernel.Bind<IUsersRepository>().To<EFUsersRepository>();
            ninjectKernel.Bind<IPagesRepository>().To<EFPagesRepository>();
            ninjectKernel.Bind<IBooksRepository>().To<EFBooksRepository>();
            ninjectKernel.Bind<IBookCommentsRepository>().To<EFBookCommentsRepository>();
            ninjectKernel.Bind<ILeaveCommentsRepository>().To<EFLeaveCommentsRepository>();
        }
    }
}