﻿//-----------------------------------------------------------------------
// <copyright file="AgentVmEventArgs.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a dashboard.
// </summary>
//-----------------------------------------------------------------------
namespace ProcessWatcher.ViewModel
{
    using System;

    /// <summary>
    /// The <see cref="AgentVmEventArgs"/> class.
    /// </summary>
    public class AgentVmEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgentVmEventArgs"/> class.
        /// </summary>
        /// <param name="agentVm"> The agent. </param>
        public AgentVmEventArgs(AgentVm agentVm)
        {
            this.AgentVm = agentVm;
        }

        /// <summary>
        /// Gets the agent.
        /// </summary>
        /// <value> A <see cref="AgentVm"/>. </value>
        public AgentVm AgentVm
        {
            get;
            private set;
        }
    }
}