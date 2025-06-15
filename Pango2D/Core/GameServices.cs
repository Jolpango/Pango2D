using System;
using System.Collections.Generic;

namespace Pango2D.Core
{
    /// <summary>
    /// Provides a centralized service registry for managing and retrieving service instances by type.
    /// </summary>
    /// <remarks>The <see cref="GameServices"/> class allows for the registration, retrieval, and
    /// unregistration of services by their type. It is designed to act as a lightweight service container, enabling
    /// dependency injection and decoupling of components in an application.  Services are registered by their type and
    /// can be retrieved later using the same type. Attempting to retrieve a service that has not been registered will
    /// result in an exception. Services must be reference types.</remarks>
    public class GameServices
    {
        private readonly Dictionary<Type, object> services = new();

        /// <summary>
        /// Retrieves a registered service of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve.</typeparam>
        /// <returns>The registered service of type <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception">Thrown if no service of type <typeparamref name="T"/> is registered.</exception>
        public T Get<T>()
        {
            if(services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            
            throw new Exception($"No service of type {typeof(T)} registered");
        }

        /// <summary>
        /// Attempts to retrieve a service of the specified type from the collection.
        /// </summary>
        /// <remarks>This method checks if a service of the specified type exists in the collection and
        /// returns it if available.  If the service is not found, the method returns the default value for the
        /// specified type.</remarks>
        /// <typeparam name="T">The type of the service to retrieve.</typeparam>
        /// <returns>The service instance of type <typeparamref name="T"/> if found; otherwise, the default value for type
        /// <typeparamref name="T"/>.</returns>
        public T TryGet<T>()
        {
            if (services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            return default;
        }

        /// <summary>
        /// Registers a service instance of the specified type.
        /// </summary>
        /// <remarks>This method adds the specified service instance to the internal service collection. 
        /// Subsequent calls to retrieve the service by type will return the registered instance.</remarks>
        /// <typeparam name="T">The type of the service to register. Must be a reference type.</typeparam>
        /// <param name="service">The instance of the service to register. Cannot be <see langword="null"/>.</param>
        public void Register<T>(T service) where T : class
        {
            services.Add(typeof(T), service);
        }

        /// <summary>
        /// Unregisters a service of the specified type.
        /// </summary>
        /// <remarks>Removes the service of the specified type from the internal service registry.  If the
        /// service type is not registered, the method performs no action.</remarks>
        /// <typeparam name="T">The type of the service to unregister.</typeparam>
        /// <param name="service">The instance of the service to unregister. This parameter is not used in the operation but is required to
        /// specify the type.</param>
        public void Unregister<T>(T service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service), "Service cannot be null");
            if (!services.ContainsKey(typeof(T)))
            {
                throw new Exception($"No service of type {typeof(T)} registered");
            }
            services.Remove(typeof(T));
        }
    }
}
