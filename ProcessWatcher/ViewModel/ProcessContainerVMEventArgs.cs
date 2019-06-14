//-----------------------------------------------------------------------
// <copyright file="ProcessContainerVMEventArgs.cs" company="FH Wiener Neustadt">
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
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="ProcessContainerVMEventArgs"/> class.
    /// </summary>
    public class ProcessContainerVMEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessContainerVMEventArgs"/> class.
        /// </summary>
        /// <param name="e"> The list of process container. </param>
        public ProcessContainerVMEventArgs(List<ProcessContainerVM> e)
        {
            this.Current = e;
        }

        /// <summary>
        /// Gets the list of process container.
        /// </summary>
        /// <value> A list of process container. </value>
        public List<ProcessContainerVM> Current
        {
            get;
            private set;
        }
    }
}
