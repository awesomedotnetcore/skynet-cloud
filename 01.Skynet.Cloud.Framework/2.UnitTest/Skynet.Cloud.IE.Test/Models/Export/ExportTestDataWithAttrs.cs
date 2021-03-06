﻿// ======================================================================
// 
//           filename : ExportTestDataWithAttrs.cs
//           description :
// 
//           created by magic.s.g.xie at  2019-11-05 20:02
//           
//           
//           
//           
// 
// ======================================================================

using UWay.Skynet.Cloud.IE.Core;
using UWay.Skynet.Cloud.IE.Excel;

namespace UWay.Skynet.Cloud.IE.Tests.Models.Export
{
    [ExcelExporter(Name = "测试", TableStyle = "Light10")]
    public class ExportTestDataWithAttrs
    {
        [ExporterHeader(DisplayName = "加粗文本", IsBold = true)]
        public string Text { get; set; }

        [ExporterHeader(DisplayName = "普通文本")] public string Text2 { get; set; }

        [ExporterHeader(DisplayName = "忽略", IsIgnore = true)]
        public string Text3 { get; set; }

        [ExporterHeader(DisplayName = "数值", Format = "#,##0")]
        public decimal Number { get; set; }

        [ExporterHeader(DisplayName = "名称", IsAutoFit = true)]
        public string Name { get; set; }
    }
}