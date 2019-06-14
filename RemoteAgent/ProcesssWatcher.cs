//-----------------------------------------------------------------------
// <copyright file="ProcesssWatcher.cs" company="FH Wiener Neustadt">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using NetworkLibrary;

    /// <summary>
    /// The <see cref="ProcesssWatcher"/> class.
    /// </summary>
    public class ProcesssWatcher
    {
        /// <summary>
        /// The thread that watches the processes.
        /// </summary>
        private Thread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcesssWatcher"/> class.
        /// </summary>
        public ProcesssWatcher()
        {
            this.OldProcessList = new List<ProcessContainer>();
            this.NewProcessList = new List<ProcessContainer>();
            this.IsRunning = false;
        }

        /// <summary>
        /// The event when the process has been changed.
        /// </summary>
        public event EventHandler<ProcessListEventArgs> OnProcessChanged;

        /// <summary>
        /// Gets or sets a value indicating whether if the watcher is still running.
        /// </summary>
        /// <value> Is true if the watcher is still running. </value>
        public bool IsRunning
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the old process list.
        /// </summary>
        /// <value> A normal list. </value>
        public List<ProcessContainer> OldProcessList
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the new process list.
        /// </summary>
        /// <value> A normal list. </value>
        public List<ProcessContainer> NewProcessList
        {
            get;
            private set;
        }

        /// <summary>
        /// This method gets a container that contains all current processes.
        /// </summary>
        /// <returns> It returns a <see cref="ProcessListContainer"/>. </returns>
        public ProcessListContainer InitializeNewProcesses()
        {
            ProcessListContainer listContainer = new ProcessListContainer();

            listContainer.NewProcesses = this.OldProcessList;
            listContainer.OldProcesses = this.OldProcessList;

            return listContainer;
        }

        /// <summary>
        /// This method starts the watcher.
        /// </summary>
        public void Start()
        {
            if (this.thread != null && this.thread.IsAlive)
            {
                throw new ArgumentException("Error process is already running.");
            }

            this.GetAllCurrentProcesses();
            this.IsRunning = true;
            this.thread = new Thread(this.Worker);
            this.thread.Start();
        }

        /// <summary>
        /// This method stops the watcher.
        /// </summary>
        public void Stop()
        {
            if (this.thread == null || !this.thread.IsAlive)
            {
                throw new ArgumentException("Error process is already dead.");
            }

            this.IsRunning = false;
            this.thread.Join();
        }

        /// <summary>
        /// This method fires when the process have been changed.
        /// </summary>
        /// <param name="e"> The changed processes. </param>
        protected virtual void FireOnProcessChanged(ProcessListEventArgs e)
        {
            if (this.OnProcessChanged != null)
            {
                this.OnProcessChanged(this, e);
            }
        }

        /// <summary>
        /// This method gets all current processes.
        /// </summary>
        private void GetAllCurrentProcesses()
        {
            foreach (var item in Process.GetProcesses())
            {
                this.OldProcessList.Add(new ProcessContainer(item));
            }

            ProcessListContainer init = new ProcessListContainer();
            init.NewProcesses = this.OldProcessList;
            this.FireOnProcessChanged(new ProcessListEventArgs(init));
        }

        /// <summary>
        /// This is the main thread of the watcher.
        /// </summary>
        private void Worker()
        {
            while (this.IsRunning)
            {
                Thread.Sleep(5000);

                foreach (var item in Process.GetProcesses())
                {
                    this.NewProcessList.Add(new ProcessContainer(item));
                }

                var container = this.CompareNewProcessesWithCurrentProcesses();

                this.OldProcessList.Clear();

                foreach (var item in this.NewProcessList)
                {
                    this.OldProcessList.Add(item);
                }

                this.NewProcessList.Clear();

                this.FireOnProcessChanged(new ProcessListEventArgs(container));
            }
        }

        /// <summary>
        /// This method compares the new processes with the old processes.
        /// </summary>
        /// <returns> It returns a container with the changed processes. </returns>
        private ProcessListContainer CompareNewProcessesWithCurrentProcesses()
        {
            ProcessListContainer processListContainer = new ProcessListContainer();

            foreach (var item in this.NewProcessList)
            {
                if (this.OldProcessList.Where(x => x.Id == item.Id).Count() == 0)
                {
                    // New Process
                    processListContainer.NewProcesses.Add(item);
                    continue;
                }

                for (int i = 0; i < this.OldProcessList.Count; i++)
                {
                    if (item.Id == this.OldProcessList[i].Id)
                    {
                        // Changed Process
                        if (this.OldProcessList[i].IsProcessDifferent(item))
                        {
                            processListContainer.OldProcesses.Add(this.OldProcessList[i]);
                            processListContainer.NewProcesses.Add(item);
                        }

                        break;
                    }
                }
            }

            foreach (var item in this.OldProcessList)
            {
                if (this.NewProcessList.Where(x => x.Id == item.Id).Count() == 0)
                {
                    // Process killed
                    processListContainer.OldProcesses.Add(item);
                    continue;
                }
            }

            return processListContainer;
        }
    }
}