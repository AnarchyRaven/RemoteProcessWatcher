//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a remote agent.
// </summary>
//-----------------------------------------------------------------------
namespace RemoteAgent
{
    /// <summary>
    /// The <see cref="Program"/> class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry of the app.
        /// </summary>
        /// <param name="args"> The command line arguments. </param>
        private static void Main(string[] args)
        {
            App app = new App(args, new Renderer());
            app.Start();
        }
    }
}
