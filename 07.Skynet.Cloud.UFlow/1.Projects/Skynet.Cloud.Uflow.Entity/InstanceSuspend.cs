/************************************************************************************
* Copyright (c) 2019-07-11 10:53:30 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Entity
* 文件名：  UWay.Skynet.Cloud.Uflow.Entity.InstanceSuspend.cs
* 版本号：  V1.0.0.0
* 唯一标识：7e821210-792c-4633-afc0-b0867ef2b801
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 10:53:30 
* 描述： 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 10:53:30 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Entity
{
   using System;
   using UWay.Skynet.Cloud.Data;

   [Table("UF_INSTANCE_SUSPEND")]
   public class InstanceSuspend
   {
      /// <summary>
      /// 唯一ID
      /// <summary>
      [Id("FID", IsDbGenerated =true, SequenceName ="seq_FID")]
      public string Fid{ set; get;}
      /// <summary>
      /// 关联T_UA_GROUP。
      /// <summary>
      [Column("INSTANCE_FLOW_ID",DbType = DBType.NVarChar)]
      public string InstanceFlowId{ set; get;}
      /// <summary>
      /// 0流程挂起,1步骤挂起
      /// <summary>
      [Column("TYPE",DbType = DBType.Int32)]
      public int Type{ set; get;}
      /// <summary>
      /// 关联T_WF_Flow的ID。
      /// <summary>
      [Column("INSTANCE_STEP_ID",DbType = DBType.NVarChar)]
      public string InstanceStepId{ set; get;}
      /// <summary>
      /// 启动日期
      /// <summary>
      [Column("BEGIN_DATE",DbType = DBType.DateTime)]
      public DateTime BeginDate{ set; get;}
      /// <summary>
      /// 结束日期
      /// <summary>
      [Column("END_DATE",DbType = DBType.DateTime)]
      public DateTime? EndDate{ set; get;}
      /// <summary>
      /// 以小时为单位
      /// <summary>
      [Column("USED_HOURS",DbType = DBType.Int32)]
      public int UsedHours{ set; get;}
      /// <summary>
      /// 创建者
      /// <summary>
      [Column("CREATOR",DbType = DBType.NVarChar)]
      public string Creator{ set; get;}
      /// <summary>
      /// 创建日期
      /// <summary>
      [Column("CREATE_DATE",DbType = DBType.DateTime)]
      public DateTime CreateDate{ set; get;}
      /// <summary>
      /// 编辑者
      /// <summary>
      [Column("EDITOR",DbType = DBType.NVarChar)]
      public string Editor{ set; get;}
      /// <summary>
      /// 编辑日期
      /// <summary>
      [Column("EDIT_DATE",DbType = DBType.DateTime)]
      public DateTime EditDate{ set; get;}
   }
}
