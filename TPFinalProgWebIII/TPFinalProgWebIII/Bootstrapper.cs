using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.RepositoryImp;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.ServiceImp;
using Unity;
using Unity.AspNet.Mvc;

namespace TPFinalProgWebIII
{
    public static class Bootstrapper

    {

        public static IUnityContainer Initialise()

        {

            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new
            UnityDependencyResolver(container));

            return container;

        }

        private static IUnityContainer BuildUnityContainer()

        {

            var container = new UnityContainer();

            RegisterTypes(container);

            return container;

        }

        public static void RegisterTypes(IUnityContainer container) {

            container.RegisterType(typeof(IGeneralService<>), typeof(GeneralServiceImp<>));
            container.RegisterType(typeof(IGeneralRepository<>), typeof(GeneralRepositoryImp<>));
            container.RegisterType<IUsuarioService, UsuarioServiceImp>();
            container.RegisterType<IUsuarioRepository, UsuarioRepositoryImp>();
        }

    }
}