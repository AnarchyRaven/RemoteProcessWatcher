//-----------------------------------------------------------------------
// <copyright file="IRenderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace RemoteAgent
{
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="IRenderer"/> interface.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// This method prints a normal message to the console.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="message"> The message to be printed. </param>
        void PrintMessage(object sender, StringEventArgs message);

        /// <summary>
        /// This method prints a error message to the console.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="message"> The message to be printed. </param>
        void PrintErrorMessage(object sender, StringEventArgs message);
    }
}