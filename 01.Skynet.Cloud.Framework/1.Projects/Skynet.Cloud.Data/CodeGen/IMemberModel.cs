﻿using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data.Schema;

namespace UWay.Skynet.Cloud.Data.CodeGen
{
    public interface IMemberModel
    {
        string MemberName { get; }
        IColumnSchema Column { get; }
    }
}
