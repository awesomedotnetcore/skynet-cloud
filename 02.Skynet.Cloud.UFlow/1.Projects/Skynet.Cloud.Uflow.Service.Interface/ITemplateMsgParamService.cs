/************************************************************************************
* Copyright (c) 2019-07-11 12:02:26 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateMsgParam.cs
* 版本号：  V1.0.0.0
* 唯一标识：dc475e99-15bf-449a-b003-9f38c2ca6d13
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:26 
* 描述：流程业务 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:26 
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
   /// 流程业务服务接口类
   /// </summary>
   public interface ITemplateMsgParamService
   {
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      long Add(TemplateMsgParam  templateMsgParam);
      /// <summary>
      /// 添加流程业务{流程业务}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateMsgParam>  templateMsgParams);
      /// <summary>
      /// 更新流程业务{流程业务}对象(即:一条记录
      /// </summary>
      int Update(TemplateMsgParam  templateMsgParam);
      /// <summary>
      /// 删除流程业务{流程业务}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      TemplateMsgParam GetById(string id);
      /// <summary>
      /// 获取所有的流程业务{流程业务}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
