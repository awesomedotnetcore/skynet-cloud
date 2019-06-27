﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Data;

namespace UWay.Skynet.Cloud.Protocal
{
    public enum ProtocalType
    {
        DB = 0,
        Ftp = 1,
        Memory = 2,
        File = 3,
        Http = 4
    }

    [Table("ufa_connection_info")]
    public class ProtocolInfo
    {
        [Id("ID", SequenceName = "SEQ_UFA_CONNECTION_INFO_ID", IsDbGenerated = true)]
        public int ProtocolID { get; set; }

        [Column("Type")]
        public ProtocalType ProtocalType { get; set; }

        [Column("CONN_KEY")]
        public string ContainerName { get; set; }

        [Column("CONN_RELATE_ID")]
        public int CfgID { get; set; }

        [Column("IS_CONN_POOL")]
        public bool IsConnPool { get; set; }

        [Column("UPDATE_TIME")]
        public DateTime? UpdateDate { get; set; }

        [Column("DATABASE_NAME")]
        public string DataBaseName { get; set; }

        [Column("DESCRIPTION", DbType = DBType.NText)]
        public string Description { get; set; }

        [Ignore]

        public ProtocolCfgInfo Cfg { set; get; }

    }
}
