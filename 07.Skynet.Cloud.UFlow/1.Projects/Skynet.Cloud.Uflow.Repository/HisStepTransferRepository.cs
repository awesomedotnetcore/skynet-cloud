/************************************************************************************
* Copyright (c) 2019-07-11 12:08:18 优网科技 All Rights Reserved.
* CLR版本： .Standard 2.x
* 公司名称：优网科技
* 命名空间：UWay.Skynet.Cloud.Uflow.Repository
* 文件名：  UWay.Skynet.Cloud.Uflow.Repository.HisStepTransfer.cs
* 版本号：  V1.0.0.0
* 唯一标识：501522ed-4bf0-4284-aae3-617fc4ad9de2
* 创建人：  magic.s.g.xie
* 电子邮箱：xiesg@uway.cn
* 创建时间：2019-07-11 12:08:18 
* 描述：记录下引擎对每个转发控制转发的结果 
* 
* 
* =====================================================================
* 修改标记 
* 修改时间：2019-07-11 12:08:18 
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
   /// 记录下引擎对每个转发控制转发的结果仓储类
   /// </summary>
   public class HisStepTransferRepository:ObjectRepository
   {
      public HisStepTransferRepository(IDbContext uow):base(uow)
      {
      }
      /// <summary>
      /// 添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public long Add(HisStepTransfer  hisStepTransfer)
      {
         return Add<HisStepTransfer>(hisStepTransfer);
      }
      /// <summary>
      /// 批量添加记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public void Add(IList<HisStepTransfer>  hisStepTransfers)
      {
         Batch<long, HisStepTransfer>(hisStepTransfers, (u, v) => u.Insert(v));
      }
      /// <summary>
      /// 更新记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Update(HisStepTransfer  hisStepTransfer)
      {
         return Update<HisStepTransfer>(hisStepTransfer);
      }
      /// <summary>
      /// 删除记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public int Delete(string[] idArrays )
      {
         return Delete<HisStepTransfer>(p => idArrays.Contains(p.Fid)); 
      }
      /// <summary>
      /// 获取指定的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象(即:一条记录
      /// </summary>
      public HisStepTransfer GetById(string id)
      {
         return GetByID<HisStepTransfer>(id);
      }
      /// <summary>
      /// 获取所有的记录下引擎对每个转发控制转发的结果{记录下引擎对每个转发控制转发的结果}对象
      /// </summary>
      public IQueryable<HisStepTransfer> Query()
      {
         return CreateQuery<HisStepTransfer>();
      }
   }
}
