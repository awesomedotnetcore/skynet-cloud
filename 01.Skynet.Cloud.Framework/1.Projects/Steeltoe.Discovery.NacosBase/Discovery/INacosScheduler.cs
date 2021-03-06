﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Steeltoe.Discovery.Nacos.Discovery
{
    /// <summary>
    /// Scheduler used for scheduling heartbeats to the Consul server
    /// </summary>
    public interface INacosScheduler : IDisposable
    {
        /// <summary>
        /// Adds an instances id to be checked
        /// </summary>
        /// <param name="instanceId">the instance id</param>
        void Add(string instanceId);

        /// <summary>
        /// Remove an instance id from checking
        /// </summary>
        /// <param name="instanceId">the instance id</param>
        void Remove(string instanceId);
    }
}
