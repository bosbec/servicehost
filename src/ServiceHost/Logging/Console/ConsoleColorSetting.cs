// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleColorSetting.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the ColorSetting type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging.Console
{
    using System;

    /// <summary>
    /// Defines the ColorSetting type.
    /// </summary>
    public class ConsoleColorSetting
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleColorSetting"/> class.
        /// </summary>
        /// <param name="foregroundColor">
        /// The foreground color.
        /// </param>
        private ConsoleColorSetting(ConsoleColor foregroundColor)
        {
            ForegroundColor = foregroundColor;
        }

        /// <summary>
        /// Gets the foreground color.
        /// </summary>
        public ConsoleColor ForegroundColor { get; private set; }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        public ConsoleColor? BackgroundColor { get; private set; }

        /// <summary>
        /// Sets the foreground color to the specified color.
        /// </summary>
        /// <param name="foregroundColor">
        /// The foreground color.
        /// </param>
        /// <returns>
        /// The color setting.
        /// </returns>
        public static ConsoleColorSetting Foreground(ConsoleColor foregroundColor)
        {
            var colorSettings = new ConsoleColorSetting(foregroundColor);

            return colorSettings;
        }

        /// <summary>
        /// Sets the background color to the specified color
        /// </summary>
        /// <param name="backgroundColor">
        /// The background color.
        /// </param>
        /// <returns>
        /// The color setting.
        /// </returns>
        public ConsoleColorSetting Background(ConsoleColor backgroundColor)
        {
            BackgroundColor = backgroundColor;

            return this;
        }

        /// <summary>
        /// Sets the current console colors to those specified in this color setting,
        /// restoring them to the previous colors when disposing
        /// </summary>
        /// <returns>
        /// The console color context.
        /// </returns>
        public IDisposable Enter()
        {
            return new ConsoleColorContext(this);
        }

        /// <summary>
        /// Defines the ColorSetting type.
        /// </summary>
        private class ConsoleColorContext : IDisposable
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ConsoleColorContext"/> class.
            /// </summary>
            /// <param name="colorSetting">
            /// The color setting.
            /// </param>
            public ConsoleColorContext(ConsoleColorSetting colorSetting)
            {
                if (colorSetting.BackgroundColor.HasValue)
                {
                    Console.BackgroundColor = colorSetting.BackgroundColor.Value;
                }

                Console.ForegroundColor = colorSetting.ForegroundColor;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Console.ResetColor();
            }
        }
    }
}