//-----------------------------------------------------------------------
// <copyright file="CommandLineReader.cs" company="FH Wiener Neustadt">
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
    using System.Net;
    using System.Net.NetworkInformation;

    /// <summary>
    /// The <see cref="CommandLineReader"/> class.
    /// </summary>
    public static class CommandLineReader
    {
        /// <summary>
        /// This method checks if the given parameters are correct.
        /// </summary>
        /// <param name="arguments"> The given parameters. </param>
        /// <returns> It returns either the default port number or the user given port. </returns>
        public static int GetParameter(string[] arguments)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            int port;

            if (arguments.Length > 1)
            {
                throw new ArgumentException("Error the programm only takes one parameter.");
            }
            else if (arguments.Length == 1)
            {
                string temp = arguments[0];

                if (temp.Length < 6 || temp.Substring(0, 6) != "/port:")
                {
                    throw new ArgumentException("Error the parameter has to begin with /port:.");
                }

                if (!int.TryParse(temp.Substring(6, temp.Length - 6), out port))
                {
                    throw new ArgumentException("Error the parameter has to contain a valid port number.");
                }

                if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
                {
                    throw new ArgumentException("Error the parameter has to contain a valid port number.");
                }
            }
            else
            {
                port = 80;
            }

            return port;
        }
    }
}