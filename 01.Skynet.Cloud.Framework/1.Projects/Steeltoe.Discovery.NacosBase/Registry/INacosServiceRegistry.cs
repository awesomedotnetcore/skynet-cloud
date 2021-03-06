﻿using System;
using System.Collections.Generic;
using System.Text;
using Steeltoe.Common.Discovery;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Nacos;

namespace Steeltoe.Discovery.Nacos.Registry
{
    public interface INacosServiceRegistry : IServiceRegistry<INacosRegistration>
    {
        ///// <summary>
        ///// Register the provided registration in Nacos
        ///// </summary>
        ///// <param name="registration">the registration to register</param>
        ///// <returns>the task</returns>
        //Task RegisterAsync(RegisterInstanceRequest registration);

        ///// <summary>
        ///// Deregister the provided registration in Nacos
        ///// </summary>
        ///// <param name="registration">the registration to register</param>
        ///// <returns>the task</returns>
        //Task DeregisterAsync(RemoveInstanceRequest registration);

        ///// <summary>
        ///// Set the status of the registration in Nacos
        ///// </summary>
        ///// <param name="registration">the registration to register</param>
        ///// <param name="status">the status value to set</param>
        ///// <returns>the task</returns>
        //Task SetStatusAsync(ModifyInstanceHealthStatusRequest registration, string status);

        /////// <summary>
        /////// Get the status of the registration in Nacos
        /////// </summary>
        /////// <param name="registration">the registration to register</param>
        /////// <returns>the status value</returns>
        ////Task<object> GetStatusAsync(INacosRegistration registration);
    }
}
