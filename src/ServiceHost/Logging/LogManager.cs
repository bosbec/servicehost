// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogManager.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the LogManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Bosbec.ServiceHost.Logging.Console;

    /// <summary>
    /// Defines the LogManager type.
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// The changed handlers.
        /// </summary>
        private static readonly List<Action<ILoggerFactory>> ChangedHandlers = new List<Action<ILoggerFactory>>();

        /// <summary>
        /// The default logger factory.
        /// </summary>
        private static readonly ILoggerFactory Default = new ConsoleLoggerFactory(true);

        /// <summary>
        /// The current logger factory.
        /// </summary>
        private static ILoggerFactory _current = Default;

        /// <summary>
        /// Raised when the logger factory is changed.
        /// </summary>
        public static event Action<ILoggerFactory> Changed
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                ChangedHandlers.Add(value);

                value(Current);
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                ChangedHandlers.Remove(value);
            }
        }

        /// <summary>
        /// Gets or sets the current logger factory.
        /// </summary>
        public static ILoggerFactory Current
        {
            get
            {
                return _current;
            }

            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException(@"Cannot set current ILoggerFactory to null!

If you want to disable logging completely, you can set Current to an instance of NullLoggerFactory.

Alternatively, if you're using the configuration API, you can disable logging like so:

    ServiceHost.Create(myAdapter)
        .Logging(l => l.None())
        .(...)

");
                }

                if (value == _current)
                {
                    return;
                }

                _current = value;

                ChangedHandlers.ToList().ForEach(h => h(value));
            }
        }
    }
}