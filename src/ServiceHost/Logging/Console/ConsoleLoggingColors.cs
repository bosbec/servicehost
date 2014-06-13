// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleLoggingColors.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ConsoleLoggingColors type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging.Console
{
    using System;

    /// <summary>
    /// Defines the ConsoleLoggingColors type.
    /// </summary>
    public class ConsoleLoggingColors
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLoggingColors"/> class.
        /// </summary>
        public ConsoleLoggingColors()
        {
            Debug = ConsoleColorSetting.Foreground(ConsoleColor.Gray);
            Info = ConsoleColorSetting.Foreground(ConsoleColor.Green);
            Warn = ConsoleColorSetting.Foreground(ConsoleColor.Yellow);
            Error = ConsoleColorSetting.Foreground(ConsoleColor.Red);
        }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public ConsoleColorSetting Error { get; set; }

        /// <summary>
        /// Gets or sets the warn.
        /// </summary>
        public ConsoleColorSetting Warn { get; set; }

        /// <summary>
        /// Gets or sets the info.
        /// </summary>
        public ConsoleColorSetting Info { get; set; }

        /// <summary>
        /// Gets or sets the debug.
        /// </summary>
        public ConsoleColorSetting Debug { get; set; }
    }
}