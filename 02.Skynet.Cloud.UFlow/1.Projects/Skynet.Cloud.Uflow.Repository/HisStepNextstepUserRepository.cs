/************************************************************************************
* Copyright (c) 2019-07-11 12:08:15 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepNextstepUser.cs
* 版本号：  V1.0.0.0
* 唯一标识：600637e1-fa53-43ee-ac92-8b7e9ef4d1f5
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:15 
* 描述：存放由用户指定的下一步处理人 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:15 
* 修改人： 谢韶光
* 版本号： V1.0.0.0
* 描述：
* 
* 
* 
* 
************************************************************************************/



namespace   UWay.Skynet.Cloud.Uflow.Repository
{
   using System;
   using UWay.Skynet.Cloud.Data;

   using  UWay.Skynet.Cloud.Uflow.Entity;
   using System.Linq;
   using System.Collections.Generic;

   /// <summary>
   /// 存放由用户指定的下一步处理人仓储类
   /// </summary>
   public class HisStepNextstepUserRepository:ObjectRepository
   {
      public HisStepNextstepUserRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public long Add(HisStepNextstepUser  hisStepNextstepUser)
      {
         return Add<HisStepNextstepUser>(hisStepNextstepUser);
      }
      /// <summary>
      /// 批量添加存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepNextstepUser>  hisStepNextstepUsers)
      {
         Batch<long, HisStepNextstepUser>(hisStepNextstepUsers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public int Update(HisStepNextstepUser  hisStepNextstepUser)
      {
         return Update<HisStepNextstepUser>(hisStepNextstepUser);
      }
      /// <summary>
      /// 删除存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepNextstepUser>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象(即:一条记录
      /// </summary>
      public HisStepNextstepUser GetById(string id)
      {
         return GetByID<HisStepNextstepUser>(id);
      }
      /// <summary>
      /// 获取所有的存放由用户指定的下一步处理人{存放由用户指定的下一步处理人}对象
      /// </summary>
      public IQueryable<HisStepNextstepUser> Query()
      {
         return CreateQuery<HisStepNextstepUser>();
      }
   }
}
