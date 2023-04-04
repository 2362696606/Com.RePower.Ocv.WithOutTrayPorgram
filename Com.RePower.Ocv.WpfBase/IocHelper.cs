﻿using System.Diagnostics.CodeAnalysis;

namespace Com.RePower.WpfBase
{
    public sealed class IocHelper : IServiceProvider
    {
        /// <summary>
        /// Gets the default <see cref="Ioc"/> instance.
        /// </summary>
        public static IocHelper Default { get; } = new();

        /// <summary>
        /// The <see cref="IServiceProvider"/> instance to use, if initialized.
        /// </summary>
        private volatile IServiceProvider? _serviceProvider;

        /// <inheritdoc/>
        public object? GetService(Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(serviceType);

            // As per section I.12.6.6 of the official CLI ECMA-335 spec:
            // "[...] read and write access to properly aligned memory locations no larger than the native
            // word size is atomic when all the write accesses to a location are the same size. Atomic writes
            // shall alter no bits other than those written. Unless explicit layout control is used [...],
            // data elements no larger than the natural word size [...] shall be properly aligned.
            // Object references shall be treated as though they are stored in the native word size."
            // The field being accessed here is of native int size (reference type), and is only ever accessed
            // directly and atomically by a compare exchange instruction (see below), or here. We can therefore
            // assume this read is thread safe with respect to accesses to this property or to invocations to one
            // of the available configuration methods. So we can just read the field directly and make the necessary
            // check with our local copy, without the need of paying the locking overhead from this get accessor.
            IServiceProvider? provider = this._serviceProvider;

            if (provider is null)
            {
                ThrowInvalidOperationExceptionForMissingInitialization();
            }

            return provider!.GetService(serviceType);
        }

        /// <summary>
        /// Tries to resolve an instance of a specified service type.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <returns>An instance of the specified service, or <see langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the current <see cref="Ioc"/> instance has not been initialized.</exception>
        public T? GetService<T>()
            where T : class
        {
            IServiceProvider? provider = this._serviceProvider;

            if (provider is null)
            {
                ThrowInvalidOperationExceptionForMissingInitialization();
            }

            return (T?)provider!.GetService(typeof(T));
        }

        /// <summary>
        /// Resolves an instance of a specified service type.
        /// </summary>
        /// <typeparam name="T">The type of service to resolve.</typeparam>
        /// <returns>An instance of the specified service, or <see langword="null"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the current <see cref="Ioc"/> instance has not been initialized, or if the
        /// requested service type was not registered in the service provider currently in use.
        /// </exception>
        public T GetRequiredService<T>()
            where T : class
        {
            IServiceProvider? provider = this._serviceProvider;

            if (provider is null)
            {
                ThrowInvalidOperationExceptionForMissingInitialization();
            }

            T? service = (T?)provider!.GetService(typeof(T));

            if (service is null)
            {
                ThrowInvalidOperationExceptionForUnregisteredType();
            }

            return service!;
        }

        /// <summary>
        /// Initializes the shared <see cref="IServiceProvider"/> instance.
        /// </summary>
        /// <param name="serviceProvider">The input <see cref="IServiceProvider"/> instance to use.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="serviceProvider"/> is <see langword="null"/>.</exception>
        public void ConfigureServices(IServiceProvider serviceProvider)
        {
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IServiceProvider? oldServices = Interlocked.CompareExchange(ref this._serviceProvider, serviceProvider, null);

            if (oldServices is not null)
            {
                ThrowInvalidOperationExceptionForRepeatedConfiguration();
            }
        }
        public IServiceProvider GetServiceProvider()
        {
            IServiceProvider? provider = this._serviceProvider;

            if (provider is null)
            {
                ThrowInvalidOperationExceptionForMissingInitialization();
            }
            return provider;
        }


        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> when the <see cref="IServiceProvider"/> property is used before initialization.
        /// </summary>
        [DoesNotReturn]
        private static void ThrowInvalidOperationExceptionForMissingInitialization()
        {
            throw new InvalidOperationException("The service provider has not been configured yet.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> when the <see cref="IServiceProvider"/> property is missing a type registration.
        /// </summary>
        [DoesNotReturn]
        private static void ThrowInvalidOperationExceptionForUnregisteredType()
        {
            throw new InvalidOperationException("The requested service type was not registered.");
        }

        /// <summary>
        /// Throws an <see cref="InvalidOperationException"/> when a configuration is attempted more than once.
        /// </summary>
        [DoesNotReturn]
        private static void ThrowInvalidOperationExceptionForRepeatedConfiguration()
        {
            throw new InvalidOperationException("The default service provider has already been configured.");
        }
    }
}
