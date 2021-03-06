/************************************************************************************
* Copyright (c) 2019-07-11 12:03:13 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.InstanceStepMsg.cs
* 版本号：  V1.0.0.0
* 唯一标识：fe23f7eb-af96-4744-9782-a316f8862f20
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:03:13 
* 描述：步骤消息处理 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:03:13 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Service.Interface
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;
   /// <summary>
   /// 步骤消息处理服务接口类
   /// </summary>
   public interface IInstanceStepMsgService
   {
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      long Add(InstanceStepMsg  instanceStepMsg);
      /// <summary>
      /// 添加步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      void Add(IList<InstanceStepMsg>  instanceStepMsgs);
      /// <summary>
      /// 更新步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      int Update(InstanceStepMsg  instanceStepMsg);
      /// <summary>
      /// 删除步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      InstanceStepMsg GetById(string id);
      /// <summary>
      /// 获取所有的步骤消息处理{步骤消息处理}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
