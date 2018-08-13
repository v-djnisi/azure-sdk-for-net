﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.Azure.Sdk.Build.Tasks.BaseTasks
{
    using System;
    using Microsoft.Build.Utilities;
    using Microsoft.Azure.Sdk.Build.Tasks.Utilities;
    using Microsoft.Build.Framework;
    using Microsoft.Azure.Build.Tasks;

    public abstract class NetSdkTask : Task, INetSdkTask
    {
        NetSdkTaskLogger _taskLogger;
        string _netSdkTaskName;
        public bool DebugTraceEnabled { get; set; }

        public string BuildScope { get; set; }

        public virtual string NetSdkTaskName { get { return "NetSdkTask"; } }

        /// <summary>
        /// Task instance of each derived task.
        /// This is required for the base services like TaskLogger to be provided from the base class.
        /// </summary>
        protected abstract INetSdkTask TaskInstance { get; }

        internal virtual NetSdkTaskLogger TaskLogger
        {
            get
            {
                if(_taskLogger == null)
                {
                    _taskLogger = new NetSdkTaskLogger(TaskInstance, DebugTraceEnabled);
                }

                return _taskLogger;
            }
        }

        public override bool Execute()
        {
            throw new NotImplementedException();
        }

        public virtual void InitExecute(bool debugTrace, bool debugMode)
        {
            DebugTraceEnabled = debugTrace;

            if(debugMode)
            {
                DebugTask dbgTsk = new DebugTask();
                dbgTsk.ExecWithInfo(this.NetSdkTaskName);
            }
        }

        #region Logging

        //public void LogDebug(string debugInfo)
        //{
        //    TaskLogger.LogDebugInfo(debugInfo);
        //}

        //public void LogDebug(string logFormat, params string[] info)
        //{
        //    TaskLogger.LogDebugInfo(string.Format(logFormat, info));
        //}

        #endregion
    }

    public interface INetSdkTask : ITask
    {
        string NetSdkTaskName { get; }
    }
}