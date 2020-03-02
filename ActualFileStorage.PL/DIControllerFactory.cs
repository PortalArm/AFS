using ActualFileStorage.BLL.Links;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace ActualFileStorage.PL
{

    public class DIControllerFactory : DefaultControllerFactory
    {

        private IUnityContainer _container;
        //public void CompositionRootRoutine(IUnityContainer c)
        //{
        //    _container = c;
        //}

        public DIControllerFactory(IUnityContainer c) => _container = c;
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //File.AppendAllLines(Properties.Resources.logfile,new[] { $"Creating {controllerType} controller" });
            if (controllerType == null)
                return null;
            // request context?
            return _container.Resolve(controllerType) as Controller;

            //return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}