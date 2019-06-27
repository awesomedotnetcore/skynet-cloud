﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UWay.Skynet.Cloud.Linq.Impl.Grouping.Aggregates;

namespace UWay.Skynet.Cloud.DataSource
{
    public class DataSourceResult
    {
        public IEnumerable Data { get; set; }
        public long Total { get; set; }
        public IEnumerable<AggregateResult> AggregateResults { get; set; }
        public object Errors { get; set; }
    }

    public class DataSourceResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public long Total { get; set; }
        public IEnumerable<AggregateResult> AggregateResults { get; set; }
        public object Errors { get; set; }
    }


}
