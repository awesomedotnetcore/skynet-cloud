/************************************************************************************
* Copyright (c) 2019-07-11 12:02:21 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateEvent.cs
* 版本号：  V1.0.0.0
* 唯一标识：e8241e9b-f03d-4854-ae42-691542789372
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:21 
* 描述：事件 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:21 
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
   /// 事件服务接口类
   /// </summary>
   public interface ITemplateEventService
   {
      /// <summary>
      /// 添加事件{事件}对象(即:一条记录
      /// </summary>
      long Add(TemplateEvent  templateEvent);
      /// <summary>
      /// 添加事件{事件}对象(即:一条记录
      /// </summary>
      void Add(IList<TemplateEvent>  templateEvents);
      /// <summary>
      /// 更新事件{事件}对象(即:一条记录
      /// </summary>
      int Update(TemplateEvent  templateEvent);
      /// <summary>
      /// 删除事件{事件}对象(即:一条记录
      /// </summary>
      int Delete(string[] idArrays );
      /// <summary>
      /// 获取指定的事件{事件}对象(即:一条记录
      /// </summary>
      TemplateEvent GetById(string id);
      /// <summary>
      /// 获取所有的事件{事件}对象(即:一条记录
      /// </summary>
      DataSourceResult Page(DataSourceRequest request);
   }
}
