//-----------------------------------------------------------------------
// <copyright file="Renderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a remote agent.
// </summary>
//-----------------------------------------------------------------------
namespace RemoteAgent
{
    using System;
    using NetworkLibrary;
    
    /// <summary>
    /// The <see cref="Renderer"/> class.
    /// </summary>
    public class Renderer : IRenderer
    {
        /// <summary>
        /// This method prints a error message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="message"> The message. </param>
        public void PrintErrorMessage(object sender, StringEventArgs message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(message.Message);
        }

        /// <summary>
        /// This method prints a message.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="message"> The message. </param>
        public void PrintMessage(object sender, StringEventArgs message)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(message.Message);
        }
    }
}