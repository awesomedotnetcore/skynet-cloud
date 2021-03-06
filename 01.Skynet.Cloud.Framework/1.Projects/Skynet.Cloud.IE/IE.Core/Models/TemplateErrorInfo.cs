﻿// ======================================================================
// 
//           filename : TemplateErrorInfo.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-09-18 9:43
// 
// ======================================================================

namespace UWay.Skynet.Cloud.IE.Core.Models
{
    /// <summary>
    ///     模板错误信息
    /// </summary>
    public class TemplateErrorInfo
    {
        /// <summary>
        ///     错误等级
        /// </summary>
        public ErrorLevels ErrorLevel { get; set; }

        /// <summary>
        ///     Excel列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///     要求的列名
        /// </summary>
        public string RequireColumnName { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }
    }
}