namespace AgeRanger.Api
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using AgeRanger.Data.Contracts;
  using AgeRanger.Data.Helpers;
  using AgeRanger.Data.Models;
  using AgeRanger.Data.RuleResolvers;
  using Microsoft.Practices.Unity;

  /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer unityContainer)
        {
          unityContainer.RegisterType<RepositoryFactories, RepositoryFactories>();
          unityContainer.RegisterType<IRepositoryProvider, RepositoryProvider>();
          unityContainer.RegisterType<IAgeRangerUow, AgeRangerUow>();
          unityContainer.RegisterType<IDictionary<Type, Func<DbContext, object>>, Dictionary<Type, Func<DbContext, object>>>
               (new InjectionConstructor());
          unityContainer.RegisterType<IRuleResolver<Person>, AgeGroupResolver<Person>>();

        }
    }
}
