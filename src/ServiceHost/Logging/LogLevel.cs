// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogLevel.cs" company="Bosbec AB">
//   Copyright © Bosbec AB 2014
// </copyright>
// <summary>
//   Defines the LogLevel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Bosbec.ServiceHost.Logging
{
    /// <summary>
    /// Defines the LogLevel type.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Log statement of very low importance which is most likely only relevant in extreme debugging scenarios.
        /// </summary>
        Debug = 0,

        /// <summary>
        /// Log statement of low importance to unwatched running systems which however can be very relevant when testing and debugging.
        /// </summary>
        Info = 1,

        /// <summary>
        /// Log statement of fairly high importance - always contains relevant information on somewhing that may be a sign that something is wrong.
        /// </summary>
        Warn = 2,

        /// <summary>
        /// Log statement of the highest importance - always contains relevant information on something that has gone wrong.
        /// </summary>
        Error = 3,
    }
}