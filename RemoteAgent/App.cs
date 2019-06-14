//-----------------------------------------------------------------------
// <copyright file="App.cs" company="FH Wiener Neustadt">
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
    /// The <see cref="App"/> class.
    /// </summary>
    public class App
    {
        /// <summary>
        /// The process watcher.
        /// </summary>
        private ProcesssWatcher watcher;

        /// <summary>
        /// The host that accepts clients.
        /// </summary>
        private Host host;

        /// <summary>
        /// The machine name.
        /// </summary>
        private string hostname;

        /// <summary>
        /// The console renderer.
        /// </summary>
        private IRenderer renderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="args"> The command line arguments. </param>
        /// <param name="renderer"> The renderer. </param>
        public App(string[] args, IRenderer renderer)
        {
            this.renderer = renderer;
            this.OnMessagePrint += renderer.PrintMessage;
            this.OnErrorMessagePrint += renderer.PrintErrorMessage;

            try
            {
                this.host = new Host(CommandLineReader.GetParameter(args));
            }
            catch (Exception e)
            {
                this.FireOnErrorMessagePrint(new StringEventArgs(e.ToString()));
                Environment.Exit(1);
            }

            this.watcher = new ProcesssWatcher();
            this.hostname = Environment.MachineName;
            this.host.OnClientConnected += this.ClientConnected;
            this.watcher.OnProcessChanged += this.OnProcessChanged;
        }

        /// <summary>
        /// The event for a error message to be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnErrorMessagePrint;

        /// <summary>
        /// The event for a message to be printed.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessagePrint;

        /// <summary>
        /// This method starts the app.
        /// </summary>
        public void Start()
        {
            try
            {
                this.host.Start();
            }
            catch (Exception e)
            {
                this.FireOnErrorMessagePrint(new StringEventArgs(e.Message));
            }
        }

        /// <summary>
        /// This method stops the app.
        /// </summary>
        public void Stop()
        {
            try
            {
                this.watcher.Stop();
                this.host.Stop();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                this.FireOnErrorMessagePrint(new StringEventArgs(e.Message));
            }
        }

        /// <summary>
        /// This method sends the process list to the clients.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The process list. </param>
        public void OnProcessChanged(object sender, ProcessListEventArgs e)
        {
            if (e.List.NewProcesses.Count == 0 && e.List.OldProcesses.Count == 0)
            {
                try
                {
                    var message = NetworkSerealizer.SerealizePing();
                    this.host.SendToClients(message);
                }
                catch (Exception ex)
                {
                    this.FireOnErrorMessagePrint(new StringEventArgs(ex.Message));
                }
            }
            else
            {
                try
                {
                    var message = NetworkSerealizer.Serealize(e.List, this.hostname);

                    this.host.SendToClients(message);
                }
                catch (Exception ex)
                {
                    this.FireOnErrorMessagePrint(new StringEventArgs(ex.Message));
                }
            }
        }

        /// <summary>
        /// This method fires when a client has been connected. And starts either the process watcher or sends the current processes to the new client.
        /// </summary>
        /// <param name="sender"> The object sender. </param>
        /// <param name="e"> The event args. </param>
        public void ClientConnected(object sender, EventArgs e)
        {
            if (this.watcher.IsRunning != true)
            {
                try
                {
                    this.watcher.Start();
                }
                catch (Exception ex)
                {
                    this.FireOnErrorMessagePrint(new StringEventArgs(ex.Message));
                }
            }
            else
            {
                ProcessListContainer listContainer = this.watcher.InitializeNewProcesses();
                this.OnProcessChanged(null, new ProcessListEventArgs(listContainer));
            }
        }

        /// <summary>
        /// This method fires when a message should be printed.
        /// </summary>
        /// <param name="e"> The message. </param>
        protected virtual void FireMessagePrint(StringEventArgs e)
        {
            if (this.OnMessagePrint != null)
            {
                this.OnMessagePrint(this, e);
            }
        }

        /// <summary>
        /// This method fires when a error message should be printed.
        /// </summary>
        /// <param name="e"> The message. </param>
        protected virtual void FireOnErrorMessagePrint(StringEventArgs e)
        {
            if (this.OnErrorMessagePrint != null)
            {
                this.OnErrorMessagePrint(this, e);
            }
        }
    }
}