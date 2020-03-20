using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity.Extension;
using Unity;
using ActualFileStorage.BLL.Services.Interfaces;
using ActualFileStorage.BLL.Services;

namespace ActualFileStorage.PL.UnityExtensions
{
    public class ServicesExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            //Container.RegisterType<>
            Container.RegisterType<IAuthService, AuthService>();
            Container.RegisterType<IAdminService, AdminService>();
            Container.RegisterType<IProfileService, ProfileService>();
            Container.RegisterType<IRegistrationService, RegistrationService>();
        }
    }
}