/************************************************************************************
* Copyright (c) 2019-07-11 12:02:43 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Service.Interface
* 文件名：  UWay.Skynet.Cloud.Uflow.Service.Interface.TemplateAuthority.cs
* 版本号：  V1.0.0.0
* 唯一标识：ed25f7fd-8ec3-4bee-95d9-8a83fd5a8ef4
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:02:43 
* 描述：流程权限表 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:02:43 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Service
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using UWay.Skynet.Cloud.Request;
   using UWay.Skynet.Cloud.Uflow.Entity;
   using System.Collections.Generic;

   using System.Linq;
   using UWay.Skynet.Cloud.Uflow.Service.Interface;
   using UWay.Skynet.Cloud.Uflow.Repository;
   using UWay.Skynet.Cloud.Linq;
   using UWay.Skynet.Cloud;

   /// <summary>
   /// 流程权限表服务实现类
   /// </summary>
   public class TemplateAuthorityService: ITemplateAuthorityService
   {
      /// <summary>
      /// 添加流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public long Add(TemplateAuthority  templateAuthority)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateAuthorityRepository(dbContext).Add(templateAuthority);
         }
      }
      /// <summary>
      /// 添加流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public void Add(IList<TemplateAuthority>  templateAuthoritys)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            new TemplateAuthorityRepository(dbContext).Add(templateAuthoritys);
         }
      }
      /// <summary>
      /// 更新流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public int Update(TemplateAuthority  templateAuthority)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateAuthorityRepository(dbContext).Update(templateAuthority);
         }
      }
      /// <summary>
      /// 删除流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateAuthorityRepository(dbContext).Delete(idArrays);
         }
      }
      /// <summary>
      /// 获取指定的流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public TemplateAuthority GetById(string id)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateAuthorityRepository(dbContext).GetById(id);
         }
      }
      /// <summary>
      /// 获取所有的流程权限表{流程权限表}对象(即:一条记录
      /// </summary>
      public DataSourceResult Page(DataSourceRequest request)
      {
         using(var dbContext = UnitOfWork.Get(Unity.ContainerName))
         {
            return new TemplateAuthorityRepository(dbContext).Query().ToDataSourceResult(request);
         }
      }
   }
}
