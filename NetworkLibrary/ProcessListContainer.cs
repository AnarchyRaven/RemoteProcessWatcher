//-----------------------------------------------------------------------
// <copyright file="ProcessListContainer.cs" company="FH Wiener Neustadt">
//     Copyright (c) Emre Rauhofer. All rights reserved.
// </copyright>
// <author>Emre Rauhofer</author>
// <summary>
// This is a network library.
// </summary>
//-----------------------------------------------------------------------
namespace NetworkLibrary
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ProcessListContainer"/> class.
    /// </summary>
    [Serializable]
    public class ProcessListContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessListContainer"/> class.
        /// </summary>
        public ProcessListContainer()
        {
            this.OldProcesses = new List<ProcessContainer>();
            this.NewProcesses = new List<ProcessContainer>();
        }

        /// <summary>
        /// Gets or sets a list of the old processes.
        /// </summary>
        /// <value> A list of processes. </value>
        public List<ProcessContainer> OldProcesses
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a list of the new processes.
        /// </summary>
        /// <value> A list of processes. </value>
        public List<ProcessContainer> NewProcesses
        {
            get;
            set;
        }
    }
}