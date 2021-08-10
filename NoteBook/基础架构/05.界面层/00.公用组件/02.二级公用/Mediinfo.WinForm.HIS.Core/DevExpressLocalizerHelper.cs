using DevExpress.XtraBars.Localization;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraLayout.Localization;
using DevExpress.XtraNavBar;
using DevExpress.XtraPrinting.Localization;
using DevExpress.XtraReports.Localization;
using DevExpress.XtraTreeList.Localization;
using DevExpress.XtraVerticalGrid.Localization;
using System.Diagnostics;
using System.Globalization;

namespace Mediinfo.WinForm.HIS.Core
{
    /// <summary>
    /// 汉化
    /// </summary>
    public class DevExpressLocalizerHelper
    {
        /// <summary>
        /// 汉化
        /// </summary>
        public static void SetSimpleChinese()
        {
            GridLocalizer.Active = new XtraGridLocalizerCHS();
            GridResLocalizer.Active = new XtraGridLocalizerCHS();
            LayoutLocalizer.Active = new XtraLayoutLocalizerCHS();
            LayoutResLocalizer.Active = new XtraLayoutLocalizerCHS();
            Localizer.Active = new XtraEditorLocalizerCHS();
            BarLocalizer.Active = new XtraBarsLocalizerCHS();
            BarResLocalizer.Active = new XtraBarsLocalizerCHS();
            TreeListLocalizer.Active = new XtraTreeListLocalizerCHS();
            TreeListResLocalizer.Active = new XtraTreeListLocalizerCHS();
            VGridLocalizer.Active = new XtraVerticalGridLocalizerCHS();
            VGridResLocalizer.Active = new XtraVerticalGridLocalizerCHS();
            NavBarLocalizer.Active = new NavBarLocalizerCHS();
            NavBarResLocalizer.Active = new NavBarLocalizerCHS();
            PreviewLocalizer.Active = new XtraPrintingCHS();
            ReportLocalizer.Active = new XtraReportsCHS();
        }
    }

    public class XtraPrintingCHS : PreviewLocalizer
    {
        public override string Language => "简体中文";

        //public XtraPrintingCHS()
        //{
        //    foreach (PreviewStringId previewStringId in Enum.GetValues(typeof(PreviewStringId)))
        //        AddString(previewStringId, "");
        //}

        /// <summary>
        /// 打印预览汉化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override string GetLocalizedString(PreviewStringId id)
        {
            switch (id)
            {
                case PreviewStringId.Button_Help:
                    return "帮助";
                case PreviewStringId.TB_TTip_PageSetup:
                    return "页面设置";
                case PreviewStringId.MPForm_Lbl_Pages:
                    return "页";
                case PreviewStringId.SB_ZoomFactor:
                    return "放大系数";
                case PreviewStringId.TB_TTip_ZoomIn:
                    return "放大";
                case PreviewStringId.TB_TTip_FirstPage:
                    return "首页";
                case PreviewStringId.TB_TTip_PreviousPage:
                    return "上一页";
                case PreviewStringId.TB_TTip_NextPage:
                    return "下一页";
                case PreviewStringId.Msg_EmptyDocument:
                    return "该文档不包含任何页";
                case PreviewStringId.Msg_CreatingDocument:
                    return "建立文档...";
                case PreviewStringId.WaitForm_Caption:
                    return "请稍后";
                case PreviewStringId.TB_TTip_MultiplePages:
                    return "多页";
                case PreviewStringId.TB_TTip_Thumbnails:
                    return "缩略图";
                case PreviewStringId.Button_Apply:
                    return "应用";
                case PreviewStringId.RibbonPreview_Thumbnails_STipTitle:
                    return "缩略图";
                case PreviewStringId.RibbonPreview_Thumbnails_Caption:
                    return "缩略图";
                case PreviewStringId.TB_TTip_ZoomOut:
                    return "缩小";
                case PreviewStringId.TB_TTip_Zoom:
                    return "放缩";
                case PreviewStringId.TB_TTip_PrintDirect:
                    return "快速打印";
                case PreviewStringId.TB_TTip_Magnifier:
                    return "放大器";
                case PreviewStringId.TB_TTip_Print:
                    return "打印";
                case PreviewStringId.Button_Cancel:
                    return "取消";
                case PreviewStringId.TB_TTip_Close:
                    return "关闭预览";
                case PreviewStringId.TB_TTip_Backgr:
                    return "背景";
                case PreviewStringId.TB_TTip_EditPageHF:
                    return "页眉和页脚";
                case PreviewStringId.Button_Ok:
                    return "确定";
                case PreviewStringId.SB_PageNone:
                    return "无";
                case PreviewStringId.TB_TTip_LastPage:
                    return "尾页";
                case PreviewStringId.Msg_UnavailableNetPrinter:
                    return "网络打印机无法使用。";
                case PreviewStringId.Msg_NeedPrinter:
                    return "没有安装打印机。";
                case PreviewStringId.Msg_WrongPrinter:
                    return "打印机的名字是无效的。请检查打印机的设置。";
                case PreviewStringId.Msg_WrongPageSettings:
                    return "当前打印机不支持所选择页面大小。一定要继续吗？";
                case PreviewStringId.Msg_CustomDrawWarning:
                    return "警告!";
                case PreviewStringId.PreviewForm_Caption:
                    return "预览";
                case PreviewStringId.TB_TTip_Customize:
                    return "自定义";
                case PreviewStringId.Margin_Inch:
                    return "英寸";
                case PreviewStringId.Margin_Millimeter:
                    return "毫米";
                case PreviewStringId.Margin_TopMargin:
                    return "上边距";
                case PreviewStringId.Margin_BottomMargin:
                    return "下边距";
                case PreviewStringId.Margin_LeftMargin:
                    return "左边距";
                case PreviewStringId.Margin_RightMargin:
                    return "右边距";
                case PreviewStringId.ScrollingInfo_Page:
                    return "页";
                case PreviewStringId.Msg_PageMarginsWarning:
                    return "一个或多个页边距被设置到也可打印的页面范围之外，是否要继续？";
                case PreviewStringId.SB_PageInfo:
                    return "{0} / {1}";
                case PreviewStringId.TB_TTip_HandTool:
                    return "手形工具";
                case PreviewStringId.TB_TTip_Export:
                    return "导出文档...";
                case PreviewStringId.TB_TTip_Send:
                    return "通过EMail发送";
                case PreviewStringId.TB_TTip_Map:
                    return "文档结构图";
                case PreviewStringId.Msg_Search_Caption:
                    return "搜索";
                case PreviewStringId.TB_TTip_Search:
                    return "搜索";
                case PreviewStringId.RibbonPreview_Thumbnails_STipContent:
                    return "打开缩略图";
                case PreviewStringId.TB_TTip_Watermark:
                    return "水印";
                case PreviewStringId.MenuItem_File:
                    return "文件";
                case PreviewStringId.MenuItem_View:
                    return "视图";
                case PreviewStringId.MenuItem_Background:
                    return "背景...";
                case PreviewStringId.MenuItem_PageSetup:
                    return "页面调整";
                case PreviewStringId.MenuItem_Print:
                    return "打印...";
                case PreviewStringId.MenuItem_PrintDirect:
                    return "打印";
                case PreviewStringId.MenuItem_Export:
                    return "输出到";
                case PreviewStringId.MenuItem_Send:
                    return "以...格式发送";
                case PreviewStringId.MenuItem_Exit:
                    return "退出";
                case PreviewStringId.MenuItem_ViewToolbar:
                    return "工具条";
                case PreviewStringId.MenuItem_ViewStatusbar:
                    return "状态条";
                case PreviewStringId.MenuItem_ViewContinuous:
                    return "继续";
                case PreviewStringId.MenuItem_ViewFacing:
                    return "朝向";
                case PreviewStringId.MenuItem_BackgrColor:
                    return "颜色...";
                case PreviewStringId.MenuItem_Watermark:
                    return "水印...";
                case PreviewStringId.MenuItem_PdfDocument:
                    return "PDF 文件";
                case PreviewStringId.MenuItem_TxtDocument:
                    return " Text 文件";
                case PreviewStringId.MenuItem_CsvDocument:
                    return " CSV 文件";
                case PreviewStringId.MenuItem_MhtDocument:
                    return " MHT 文件";
                case PreviewStringId.MenuItem_XlsDocument:
                    return " XLS 文件";
                case PreviewStringId.MenuItem_RtfDocument:
                    return " RTF 文件";
                case PreviewStringId.MenuItem_HtmDocument:
                    return " HTML 文件";
                case PreviewStringId.MenuItem_GraphicDocument:
                    return " Image 文件";
                case PreviewStringId.MenuItem_ZoomPageWidth:
                    return " 页宽 ";
                case PreviewStringId.MenuItem_ZoomTextWidth:
                    return " 文本宽度 ";
                case PreviewStringId.MenuItem_ZoomWholePage:
                    return " 整页 ";
                case PreviewStringId.MenuItem_ZoomTwoPages:
                    return " 双页 ";
                case PreviewStringId.MenuItem_PageLayout:
                    return " 页面布局(&amp; P)";
                case PreviewStringId.SaveDlg_FilterPdf:
                    return "PDF文档";
                case PreviewStringId.SaveDlg_FilterTxt:
                    return "文本文档";
                case PreviewStringId.SaveDlg_FilterCsv:
                    return "CSV文档";
                case PreviewStringId.SaveDlg_FilterMht:
                    return "MHT文档";
                case PreviewStringId.SaveDlg_FilterXls:
                    return "Excel文档";
                case PreviewStringId.SaveDlg_FilterRtf:
                    return "多文本文档";
                case PreviewStringId.SaveDlg_FilterHtm:
                    return "HTML文档";
                case PreviewStringId.SaveDlg_FilterBmp:
                    return "BMP位图格式";
                case PreviewStringId.SaveDlg_FilterGif:
                    return "GIF图片格式";
                case PreviewStringId.SaveDlg_FilterJpeg:
                    return "JPEG图片格式";
                case PreviewStringId.SaveDlg_FilterPng:
                    return "PNG图片格式";
                case PreviewStringId.SaveDlg_FilterTiff:
                    return "TIFF图片格式";
                case PreviewStringId.SaveDlg_FilterEmf:
                    return "EMF图片格式";
                case PreviewStringId.SaveDlg_FilterWmf:
                    return "WMF图片格式";
                case PreviewStringId.SaveDlg_Title:
                    return "存储为";
                case PreviewStringId.EMail_From:
                    return "来自";
                case PreviewStringId.Msg_IncorrectPageRange:
                    return "这不是有效的页面范围";
                case PreviewStringId.WMForm_ImageZoom:
                    return "放缩";
                case PreviewStringId.WMForm_Direction_Horizontal:
                    return "水平";
                case PreviewStringId.WMForm_Direction_Vertical:
                    return "垂直";
                case PreviewStringId.WMForm_Direction_BackwardDiagonal:
                    return "后向倾斜";
                case PreviewStringId.WMForm_Direction_ForwardDiagonal:
                    return "前向倾斜";
                case PreviewStringId.WMForm_VertAlign_Bottom:
                    return "底部";
                case PreviewStringId.WMForm_VertAlign_Middle:
                    return "中间";
                case PreviewStringId.WMForm_VertAlign_Top:
                    return "顶部";
                case PreviewStringId.WMForm_HorzAlign_Left:
                    return "左";
                case PreviewStringId.WMForm_HorzAlign_Center:
                    return "中间";
                case PreviewStringId.WMForm_HorzAlign_Right:
                    return "右";
                case PreviewStringId.WMForm_PictureDlg_Title:
                    return "选择图片";
                case PreviewStringId.WMForm_ImageStretch:
                    return "拉伸";
                case PreviewStringId.WMForm_ImageClip:
                    return "裁剪";
                case PreviewStringId.WMForm_Watermark_Asap:
                    return "尽快";
                case PreviewStringId.WMForm_Watermark_Confidential:
                    return "机密";
                case PreviewStringId.WMForm_Watermark_Copy:
                    return "复制";
                case PreviewStringId.WMForm_Watermark_DoNotCopy:
                    return "不复制";
                case PreviewStringId.WMForm_Watermark_Draft:
                    return "草图";
                case PreviewStringId.WMForm_Watermark_Evaluation:
                    return "评价";
                case PreviewStringId.WMForm_Watermark_Original:
                    return "创新";
                case PreviewStringId.WMForm_Watermark_Personal:
                    return "个人";
                case PreviewStringId.WMForm_Watermark_Sample:
                    return "示例";
                case PreviewStringId.WMForm_Watermark_TopSecret:
                    return "最高机密";
                case PreviewStringId.WMForm_Watermark_Urgent:
                    return "紧迫";
                case PreviewStringId.Msg_FontInvalidNumber:
                    return "字体大小不能设置为0或者负数。";
                case PreviewStringId.Msg_NotSupportedFont:
                    return "尚不支持该字体";
                case PreviewStringId.PageInfo_PageNumber:
                    return "[页#]";
                case PreviewStringId.PageInfo_PageNumberOfTotal:
                    return "[页#，共#页]";
                case PreviewStringId.PageInfo_PageDate:
                    return "[已打印数据]";
                case PreviewStringId.PageInfo_PageTime:
                    return "[打印耗时]";
                case PreviewStringId.PageInfo_PageUserName:
                    return "[用户名]";
                case PreviewStringId.BarText_Toolbar:
                    return "工具条";
                case PreviewStringId.BarText_MainMenu:
                    return "主菜单";
                case PreviewStringId.BarText_StatusBar:
                    return "状态栏";
                case PreviewStringId.Msg_IncorrectZoomFactor:
                    return "数字大小必须界于{0}，{1}之间。";
                case PreviewStringId.Msg_InvalidMeasurement:
                    return "这不是一个有效的度量值。";
                case PreviewStringId.Msg_CannotAccessFile:
                    return "这个进程无法读取文件'{0}'，因为它正在被另一个进程使用。";
                case PreviewStringId.Msg_OpenFileQuestion:
                    return "你想打开该文件吗？";
                case PreviewStringId.ScalePopup_AdjustTo:
                    return "调整至：";
                case PreviewStringId.ScalePopup_FitTo:
                    return "适应";
                case PreviewStringId.ScalePopup_PagesWide:
                    return "页宽";
                case PreviewStringId.ScalePopup_NormalSize:
                    return "%正常大小";
                case PreviewStringId.TB_TTip_Scale:
                    return "旋转";
                case PreviewStringId.ExportOption_PdfPageRange:
                    return "页面范围：";
                case PreviewStringId.ExportOption_PdfNeverEmbeddedFonts:
                    return "不插入这些字体：";
                case PreviewStringId.ExportOption_PdfImageQuality:
                    return "图象质量：";
                case PreviewStringId.ExportOption_PdfImageQuality_Lowest:
                    return "最低";
                case PreviewStringId.ExportOption_PdfImageQuality_Low:
                    return "低";
                case PreviewStringId.ExportOption_PdfImageQuality_Medium:
                    return "中等";
                case PreviewStringId.ExportOption_PdfImageQuality_High:
                    return "高";
                case PreviewStringId.ExportOption_PdfImageQuality_Highest:
                    return "最高";
                case PreviewStringId.ExportOption_PdfDocumentAuthor:
                    return "作者：";
                case PreviewStringId.ExportOption_PdfDocumentApplication:
                    return "应用：";
                case PreviewStringId.ExportOption_PdfDocumentTitle:
                    return "标题：";
                case PreviewStringId.ExportOption_PdfDocumentSubject:
                    return "主题：";
                case PreviewStringId.ExportOption_PdfDocumentKeywords:
                    return "关键字：";
                case PreviewStringId.ExportOption_HtmlExportMode:
                    return "导出模式:";
                case PreviewStringId.ExportOption_HtmlExportMode_SingleFile:
                    return "单文件";
                case PreviewStringId.ExportOption_HtmlExportMode_SingleFilePageByPage:
                    return "逐页单文件";
                case PreviewStringId.ExportOption_HtmlExportMode_DifferentFiles:
                    return "不同的文件";
                case PreviewStringId.ExportOption_HtmlCharacterSet:
                    return "字符集:";
                case PreviewStringId.ExportOption_HtmlTitle:
                    return "标题:";
                case PreviewStringId.ExportOption_HtmlRemoveSecondarySymbols:
                    return "删除回车";
                case PreviewStringId.ExportOption_HtmlPageRange:
                    return "页范围:";
                case PreviewStringId.ExportOption_HtmlPageBorderWidth:
                    return "页边界宽度:";
                case PreviewStringId.ExportOption_HtmlPageBorderColor:
                    return "页边界颜色:";
                case PreviewStringId.ExportOption_TextSeparator:
                    return "文本分隔符:";
                case PreviewStringId.ExportOption_TextEncoding:
                    return "编码:";
                case PreviewStringId.ExportOption_XlsShowGridLines:
                    return "显示表格线";
                case PreviewStringId.ExportOption_ImageFormat:
                    return "图片格式:";
                case PreviewStringId.FolderBrowseDlg_ExportDirectory:
                    return "选择一个文件夹来保存导出的文档:";
                case PreviewStringId.ExportOptionsForm_CaptionPdf:
                    return "PDF 导出选项";
                case PreviewStringId.ExportOptionsForm_CaptionXls:
                    return " XLS 导出选项";
                case PreviewStringId.ExportOptionsForm_CaptionTxt:
                    return " 文本导出选项 ";
                case PreviewStringId.ExportOptionsForm_CaptionCsv:
                    return " CSV 导出选项";
                case PreviewStringId.ExportOptionsForm_CaptionImage:
                    return " 图片导出选项 ";
                case PreviewStringId.ExportOptionsForm_CaptionHtml:
                    return " HTML 导出选项";
                case PreviewStringId.ExportOptionsForm_CaptionMht:
                    return " MHT 导出选项";
                case PreviewStringId.ExportOptionsForm_CaptionRtf:
                    return " RTF 导出选项";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMargins_Description:
                    return " 上:{0}下:{1}";
                case PreviewStringId.RibbonPreview_GalleryItem_PaperSize_Description:
                    return "{0} x {1}";
                case PreviewStringId.RibbonPreview_PageGroup_Print:
                    return "打印";
                case PreviewStringId.RibbonPreview_PageGroup_PageSetup:
                    return "页面设置";
                case PreviewStringId.RibbonPreview_PageGroup_Navigation:
                    return "导航";
                case PreviewStringId.RibbonPreview_PageGroup_Zoom:
                    return "放缩";
                case PreviewStringId.RibbonPreview_PageGroup_Background:
                    return "页面背景";
                case PreviewStringId.RibbonPreview_PageGroup_Export:
                    return "导出";
                case PreviewStringId.RibbonPreview_DocumentMap_Caption:
                    return "书签";
                case PreviewStringId.RibbonPreview_Find_Caption:
                    return "查找";
                case PreviewStringId.RibbonPreview_Pointer_Caption:
                    return "指针";
                case PreviewStringId.RibbonPreview_HandTool_Caption:
                    return "手型工具";
                case PreviewStringId.RibbonPreview_Customize_Caption:
                    return "选项";
                case PreviewStringId.RibbonPreview_Print_Caption:
                    return "打印";
                case PreviewStringId.RibbonPreview_PrintDirect_Caption:
                    return "快速打印";
                case PreviewStringId.RibbonPreview_PageSetup_Caption:
                    return "自定义边距:";
                case PreviewStringId.RibbonPreview_EditPageHF_Caption:
                    return "页眉/页脚";
                case PreviewStringId.RibbonPreview_Magnifier_Caption:
                    return "放大器";
                case PreviewStringId.RibbonPreview_ZoomOut_Caption:
                    return "缩小";
                case PreviewStringId.RibbonPreview_ZoomIn_Caption:
                    return "放大";
                case PreviewStringId.RibbonPreview_ShowFirstPage_Caption:
                    return "首页";
                case PreviewStringId.RibbonPreview_ShowPrevPage_Caption:
                    return "上一页";
                case PreviewStringId.RibbonPreview_ShowNextPage_Caption:
                    return "下一页";
                case PreviewStringId.RibbonPreview_ShowLastPage_Caption:
                    return "最后一页";
                case PreviewStringId.RibbonPreview_MultiplePages_Caption:
                    return "多页";
                case PreviewStringId.RibbonPreview_FillBackground_Caption:
                    return "页面颜色";
                case PreviewStringId.RibbonPreview_Watermark_Caption:
                    return "水印";
                case PreviewStringId.RibbonPreview_ExportFile_Caption:
                    return "导出到";
                case PreviewStringId.RibbonPreview_SendFile_Caption:
                    return "EMail为";
                case PreviewStringId.RibbonPreview_ClosePreview_Caption:
                    return "关闭打印预览";
                case PreviewStringId.RibbonPreview_Scale_Caption:
                    return "旋转";
                case PreviewStringId.RibbonPreview_PageOrientation_Caption:
                    return "方向";
                case PreviewStringId.RibbonPreview_PaperSize_Caption:
                    return "大小";
                case PreviewStringId.RibbonPreview_PageMargins_Caption:
                    return "边距";
                case PreviewStringId.RibbonPreview_Zoom_Caption:
                    return "放缩";
                case PreviewStringId.RibbonPreview_DocumentMap_STipTitle:
                    return "文档结构图";
                case PreviewStringId.RibbonPreview_Find_STipTitle:
                    return "查找";
                case PreviewStringId.RibbonPreview_Pointer_STipTitle:
                    return "鼠标指针";
                case PreviewStringId.RibbonPreview_HandTool_STipTitle:
                    return "手型工具";
                case PreviewStringId.RibbonPreview_Customize_STipTitle:
                    return "选项";
                case PreviewStringId.RibbonPreview_Print_STipTitle:
                    return "打印(Ctrl + P)";
                case PreviewStringId.RibbonPreview_PrintDirect_STipTitle:
                    return "快速打印";
                case PreviewStringId.RibbonPreview_PageSetup_STipTitle:
                    return "页面设置";
                case PreviewStringId.RibbonPreview_EditPageHF_STipTitle:
                    return "页眉和页脚";
                case PreviewStringId.RibbonPreview_Magnifier_STipTitle:
                    return "放大器";
                case PreviewStringId.RibbonPreview_ZoomOut_STipTitle:
                    return "缩小";
                case PreviewStringId.RibbonPreview_ZoomIn_STipTitle:
                    return "放大";
                case PreviewStringId.RibbonPreview_ShowFirstPage_STipTitle:
                    return "第一页(Ctrl+Home)";
                case PreviewStringId.RibbonPreview_ShowPrevPage_STipTitle:
                    return "上一页(PageUp)";
                case PreviewStringId.RibbonPreview_ShowNextPage_STipTitle:
                    return "下一页(PageDown)";
                case PreviewStringId.RibbonPreview_ShowLastPage_STipTitle:
                    return "最后一页(Ctrl+End)";
                case PreviewStringId.RibbonPreview_MultiplePages_STipTitle:
                    return "多页查看";
                case PreviewStringId.RibbonPreview_FillBackground_STipTitle:
                    return "背景颜色";
                case PreviewStringId.RibbonPreview_Watermark_STipTitle:
                    return "水印";
                case PreviewStringId.RibbonPreview_ExportFile_STipTitle:
                    return "输出...";
                case PreviewStringId.RibbonPreview_SendFile_STipTitle:
                    return "在电子邮件中以...格式发送";
                case PreviewStringId.RibbonPreview_ClosePreview_STipTitle:
                    return "关闭打印预览";
                case PreviewStringId.RibbonPreview_Scale_STipTitle:
                    return "旋转";
                case PreviewStringId.RibbonPreview_PageOrientation_STipTitle:
                    return "页面方向";
                case PreviewStringId.RibbonPreview_PaperSize_STipTitle:
                    return "页面大小";
                case PreviewStringId.RibbonPreview_PageMargins_STipTitle:
                    return "页边距";
                case PreviewStringId.RibbonPreview_Zoom_STipTitle:
                    return "放缩";
                case PreviewStringId.RibbonPreview_PageGroup_PageSetup_STipTitle:
                    return "页面调整";
                case PreviewStringId.RibbonPreview_DocumentMap_STipContent:
                    return "打开文档结构图为你导航文档的结构。";
                case PreviewStringId.RibbonPreview_Find_STipContent:
                    return "显示查找对话框，查找文档中的文本。";
                case PreviewStringId.RibbonPreview_Pointer_STipContent:
                    return "显示鼠标指针。";
                case PreviewStringId.RibbonPreview_HandTool_STipContent:
                    return "调用抓取工具手动拖拽查看页面。";
                case PreviewStringId.RibbonPreview_Customize_STipContent:
                    return "打开可打印的组件编辑器对话框，并可以改变打印选项。";
                case PreviewStringId.RibbonPreview_Print_STipContent:
                    return "在打印前选择打印机，打印份数以及其他打印选项。";
                case PreviewStringId.RibbonPreview_PrintDirect_STipContent:
                    return "将文档不作任何修改直接送往默认打印机。";
                case PreviewStringId.RibbonPreview_PageSetup_STipContent:
                    return "显示页面调整对话框。";
                case PreviewStringId.RibbonPreview_EditPageHF_STipContent:
                    return "编辑该文档的页眉和页脚";
                case PreviewStringId.RibbonPreview_Magnifier_STipContent:
                    return "调用放大镜工具";
                case PreviewStringId.RibbonPreview_ZoomOut_STipContent:
                    return "缩小以便在一个减小的尺寸上看到页面的更多部分。";
                case PreviewStringId.RibbonPreview_ZoomIn_STipContent:
                    return "放大以便得到文档的近视图。";
                case PreviewStringId.RibbonPreview_ShowFirstPage_STipContent:
                    return "查看文档第一页。";
                case PreviewStringId.RibbonPreview_ShowPrevPage_STipContent:
                    return "查看文档上一页。";
                case PreviewStringId.RibbonPreview_ShowNextPage_STipContent:
                    return "查看文档下一页。";
                case PreviewStringId.RibbonPreview_ShowLastPage_STipContent:
                    return "查看文档最后一页。";
                case PreviewStringId.RibbonPreview_MultiplePages_STipContent:
                    return "选择页面布局以便在预览中排放文档页面。";
                case PreviewStringId.RibbonPreview_FillBackground_STipContent:
                    return "为文档页面背景选择颜色。";
                case PreviewStringId.RibbonPreview_Watermark_STipContent:
                    return "在页面的目录后插入文本或者图象的镜象。这通常用于指示一个文档被特殊处理过。";
                case PreviewStringId.RibbonPreview_ExportFile_STipContent:
                    return "将当前文档以一个可用的格式输出，并将其保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_SendFile_STipContent:
                    return "以一种可用格式输出当前文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_ClosePreview_STipContent:
                    return "关闭该文档的打印预览";
                case PreviewStringId.RibbonPreview_Scale_STipContent:
                    return "按实际大小的百分比伸展或收缩打印输出。";
                case PreviewStringId.RibbonPreview_PageOrientation_STipContent:
                    return "在纵向和横向布局之间转换页面。";
                case PreviewStringId.RibbonPreview_PaperSize_STipContent:
                    return "选择文档的页面大小。";
                case PreviewStringId.RibbonPreview_PageMargins_STipContent:
                    return "为整个文档选择页边距大小。点击定制页边距为文档应用指定的页边距大小。";
                case PreviewStringId.RibbonPreview_Zoom_STipContent:
                    return "改变文档预览的缩放等级。";
                case PreviewStringId.RibbonPreview_PageGroup_PageSetup_STipContent:
                    return "显示页面调整对话框。";
                case PreviewStringId.RibbonPreview_ExportPdf_Caption:
                    return "PDF 文件";
                case PreviewStringId.RibbonPreview_ExportHtm_Caption:
                    return " HTML 文件";
                case PreviewStringId.RibbonPreview_ExportTxt_Caption:
                    return " Text 文件";
                case PreviewStringId.RibbonPreview_ExportCsv_Caption:
                    return " CSV 文件";
                case PreviewStringId.RibbonPreview_ExportMht_Caption:
                    return " MHT 文件";
                case PreviewStringId.RibbonPreview_ExportXls_Caption:
                    return " XLS 文件";
                case PreviewStringId.RibbonPreview_ExportRtf_Caption:
                    return " RTF 文件";
                case PreviewStringId.RibbonPreview_ExportGraphic_Caption:
                    return " Image 文件";
                case PreviewStringId.RibbonPreview_SendPdf_Caption:
                    return " PDF 文件";
                case PreviewStringId.RibbonPreview_SendTxt_Caption:
                    return " Text 文件";
                case PreviewStringId.RibbonPreview_SendCsv_Caption:
                    return " CSV 文件";
                case PreviewStringId.RibbonPreview_SendMht_Caption:
                    return " MHT 文件";
                case PreviewStringId.RibbonPreview_SendXls_Caption:
                    return " XLS 文件";
                case PreviewStringId.RibbonPreview_SendRtf_Caption:
                    return " RTF 文件";
                case PreviewStringId.RibbonPreview_SendGraphic_Caption:
                    return " Image 文件";
                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationPortrait_Caption:
                    return " 纵向 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationLandscape_Caption:
                    return " 横向 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNormal_Caption:
                    return " 正常 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNarrow_Caption:
                    return " 窄 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsModerate_Caption:
                    return " 中等 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsWide_Caption:
                    return " 宽 ";
                case PreviewStringId.RibbonPreview_ExportPdf_Description:
                    return " Adobe便携式文档格式 ";
                case PreviewStringId.RibbonPreview_ExportHtm_Description:
                    return " Web页面 ";
                case PreviewStringId.RibbonPreview_ExportTxt_Description:
                    return " 纯文本 ";
                case PreviewStringId.RibbonPreview_ExportCsv_Description:
                    return " 逗号分隔值文本 ";
                case PreviewStringId.RibbonPreview_ExportMht_Description:
                    return " 单一文件的Web页 ";
                case PreviewStringId.RibbonPreview_ExportXls_Description:
                    return " Microsoft Excel工作薄";
                case PreviewStringId.RibbonPreview_ExportRtf_Description:
                    return " 多本文格式 ";
                case PreviewStringId.RibbonPreview_ExportGraphic_Description:
                    return " BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
                case PreviewStringId.RibbonPreview_SendPdf_Description:
                    return " Adobe便携式文档格式 ";
                case PreviewStringId.RibbonPreview_SendTxt_Description:
                    return " 纯文本 ";
                case PreviewStringId.RibbonPreview_SendCsv_Description:
                    return " 逗号分隔值文本 ";
                case PreviewStringId.RibbonPreview_SendMht_Description:
                    return " 单文件网页 ";
                case PreviewStringId.RibbonPreview_SendXls_Description:
                    return " Microsoft Excel工作薄";
                case PreviewStringId.RibbonPreview_SendRtf_Description:
                    return " 多文本格式 ";
                case PreviewStringId.RibbonPreview_SendGraphic_Description:
                    return " BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationPortrait_Description:
                    return "";
                case PreviewStringId.RibbonPreview_GalleryItem_PageOrientationLandscape_Description:
                    return "";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNormal_Description:
                    return " 正常 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsNarrow_Description:
                    return " 窄 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsModerate_Description:
                    return " 中等 ";
                case PreviewStringId.RibbonPreview_GalleryItem_PageMarginsWide_Description:
                    return " 宽 ";
                case PreviewStringId.Msg_OpenFileQuestionCaption:
                    return " 导出 ";
                case PreviewStringId.RibbonPreview_ExportPdf_STipTitle:
                    return " 以PDF格式输出 ";
                case PreviewStringId.RibbonPreview_ExportHtm_STipTitle:
                    return " 以HTML格式输出 ";
                case PreviewStringId.RibbonPreview_ExportTxt_STipTitle:
                    return " 以文本格式输出 ";
                case PreviewStringId.RibbonPreview_ExportCsv_STipTitle:
                    return " 以CSV格式输出 ";
                case PreviewStringId.RibbonPreview_ExportMht_STipTitle:
                    return " 以MHT格式输出 ";
                case PreviewStringId.RibbonPreview_ExportXls_STipTitle:
                    return " 以XLS格式输出 ";
                case PreviewStringId.RibbonPreview_ExportRtf_STipTitle:
                    return " 以RTF格式输出 ";
                case PreviewStringId.RibbonPreview_ExportGraphic_STipTitle:
                    return " 以图象格式输出 ";
                case PreviewStringId.RibbonPreview_SendPdf_STipTitle:
                    return " 在电子邮件中以PDF格式发送 ";
                case PreviewStringId.RibbonPreview_SendTxt_STipTitle:
                    return " 在电子邮件中以文本格式发送 ";
                case PreviewStringId.RibbonPreview_SendCsv_STipTitle:
                    return " 在电子邮件中以CSV格式发送 ";
                case PreviewStringId.RibbonPreview_SendMht_STipTitle:
                    return " 在电子邮件中以MHT格式发送 ";
                case PreviewStringId.RibbonPreview_SendXls_STipTitle:
                    return " 在电子邮件中以XLS格式发送 ";
                case PreviewStringId.RibbonPreview_SendRtf_STipTitle:
                    return " 在电子邮件中以RTF格式发送 ";
                case PreviewStringId.RibbonPreview_SendGraphic_STipTitle:
                    return " 在电子邮件中以图象格式发送 ";
                case PreviewStringId.RibbonPreview_ExportPdf_STipContent:
                    return " 将该文档以PDF格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportHtm_STipContent:
                    return " 将该文档以HTML格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportTxt_STipContent:
                    return " 将该文档以文本格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportCsv_STipContent:
                    return " 将该文档以CSV格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportMht_STipContent:
                    return " 将该文档以MHT格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportXls_STipContent:
                    return " 将该文档以XLS格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportRtf_STipContent:
                    return " 将该文档以RTF格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_ExportGraphic_STipContent:
                    return " 将该文档以图象格式输出并保存到磁盘文件上。";
                case PreviewStringId.RibbonPreview_SendPdf_STipContent:
                    return " 以PDF格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_SendTxt_STipContent:
                    return " 以文本格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_SendCsv_STipContent:
                    return " 以CSV格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_SendMht_STipContent:
                    return " 以MHT格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_SendXls_STipContent:
                    return " 以XLS格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_SendRtf_STipContent:
                    return " 以RTF格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_SendGraphic_STipContent:
                    return " 以图象格式输出文档，并且将其附到电子邮件中。";
                case PreviewStringId.RibbonPreview_PageText:
                    return " 打印预览 ";
                case PreviewStringId.RibbonPreview_ZoomExact_Caption:
                    return " 精确:";
                case PreviewStringId.Msg_CantFitBarcodeToControlBounds:
                    return " 对于条码来说控件的边界太小。";
                case PreviewStringId.Msg_InvalidBarcodeText:
                    return " 文本中有无效字符。";
                case PreviewStringId.Msg_InvalidBarcodeTextFormat:
                    return " 无效的文本格式 ";
                case PreviewStringId.ExportOption_TextSeparator_TabAlias:
                    return " TAB ";
                case PreviewStringId.ParametersRequest_Reset:
                    return " 复位 ";
                case PreviewStringId.ParametersRequest_Caption:
                    return " 参数 ";
                case PreviewStringId.ParametersRequest_Submit:
                    return " 提交 ";
                case PreviewStringId.Msg_InvPropName:
                    return " 无效的属性名称 ";
                case PreviewStringId.ExportOption_HtmlEmbedImagesInHTML:
                    return " 在HTML中嵌入图像 ";
                case PreviewStringId.ExportOption_ImageExportMode:
                    return " 导出模式:";
                case PreviewStringId.ExportOption_ImageExportMode_DifferentFiles:
                    return " 不同的文件 ";
                case PreviewStringId.ExportOption_ImageExportMode_SingleFile:
                    return " 单文件 ";
                case PreviewStringId.ExportOption_ImageExportMode_SingleFilePageByPage:
                    return " 逐页单文件 ";
                case PreviewStringId.ExportOption_ImagePageBorderColor:
                    return " 页边界颜色:";
                case PreviewStringId.ExportOption_ImagePageBorderWidth:
                    return " 页边界宽度:";
                case PreviewStringId.ExportOption_ImagePageRange:
                    return " 页范围:";
                case PreviewStringId.ExportOption_ImageResolution:
                    return " 分辨率(dpi):";
                case PreviewStringId.ExportOption_NativeFormatCompressed:
                    return " 压缩的 ";
                case PreviewStringId.ExportOption_PdfChangingPermissions_None:
                    return " 无 ";
                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions_DocumentOpenPassword:
                    return " 打开文档的密码 ";
                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions_None:
                    return " (无) ";
                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions_Permissions:
                    return " 权限 ";
                case PreviewStringId.ExportOption_PdfPrintingPermissions_None:
                    return " 无 ";
                case PreviewStringId.ExportOption_RtfExportMode:
                    return " 导出模式:";
                case PreviewStringId.ExportOption_RtfExportMode_SingleFile:
                    return " 单文件 ";
                case PreviewStringId.ExportOption_RtfExportMode_SingleFilePageByPage:
                    return " 逐页单文件 ";
                case PreviewStringId.ExportOption_RtfExportWatermarks:
                    return " 导出水印 ";
                case PreviewStringId.ExportOption_RtfPageRange:
                    return " 页范围:";
                case PreviewStringId.ExportOption_TextExportMode:
                    return " 文本导出模式:";
                case PreviewStringId.ExportOption_TextExportMode_Text:
                    return " 文本 ";
                case PreviewStringId.ExportOption_TextExportMode_Value:
                    return " 值 ";
                case PreviewStringId.ExportOption_TextQuoteStringsWithSeparators:
                    return " 引用字符串分隔符 ";
                case PreviewStringId.ExportOption_XlsExportHyperlinks:
                    return " 导出超链接 ";
                case PreviewStringId.ExportOption_XlsExportMode:
                    return " 导出模式:";
                case PreviewStringId.ExportOption_XlsExportMode_DifferentFiles:
                    return " 不同的文件 ";
                case PreviewStringId.ExportOption_XlsExportMode_SingleFile:
                    return " 单文件 ";
                case PreviewStringId.ExportOption_XlsPageRange:
                    return " 页范围:";
                case PreviewStringId.ExportOption_XlsSheetName:
                    return " 表名称:";
                case PreviewStringId.ExportOption_XlsxExportMode:
                    return " 导出模式:";
                case PreviewStringId.ExportOption_XlsxExportMode_DifferentFiles:
                    return " 不同的文件 ";
                case PreviewStringId.ExportOption_XlsxExportMode_SingleFile:
                    return " 单文件 ";
                case PreviewStringId.ExportOption_XlsxExportMode_SingleFilePageByPage:
                    return " 逐页单文件 ";
                case PreviewStringId.ExportOption_XlsxPageRange:
                    return " 页范围:";
                case PreviewStringId.ExportOption_XpsCompression:
                    return " 压缩:";
                case PreviewStringId.ExportOption_XpsCompression_Fast:
                    return " 快速 ";
                case PreviewStringId.ExportOption_XpsCompression_Maximum:
                    return " 最大 ";
                case PreviewStringId.ExportOption_XpsCompression_Normal:
                    return " 标准 ";
                case PreviewStringId.ExportOption_XpsCompression_NotCompressed:
                    return " 未压缩的 ";
                case PreviewStringId.ExportOption_XpsCompression_SuperFast:
                    return " 超快 ";
                case PreviewStringId.ExportOption_XpsDocumentCategory:
                    return " 分类:";
                case PreviewStringId.ExportOption_XpsDocumentCreator:
                    return " 作者:";
                case PreviewStringId.ExportOption_XpsDocumentDescription:
                    return " 描述:";
                case PreviewStringId.ExportOption_XpsDocumentKeywords:
                    return " 关键字:";
                case PreviewStringId.ExportOption_XpsDocumentSubject:
                    return " 主题:";
                case PreviewStringId.ExportOption_XpsDocumentTitle:
                    return " 标题:";
                case PreviewStringId.ExportOption_XpsDocumentVersion:
                    return " 版本:";
                case PreviewStringId.ExportOption_XpsPageRange:
                    return " 页范围:";
                case PreviewStringId.ExportOptionsForm_CaptionNativeOptions:
                    return " 原生格式选项 ";
                case PreviewStringId.ExportOptionsForm_CaptionXlsx:
                    return " XLSX 导出选项";
                case PreviewStringId.ExportOptionsForm_CaptionXps:
                    return " XPS 导出选项";
                case PreviewStringId.MenuItem_XlsxDocument:
                    return " XLSX 文件";
                case PreviewStringId.Msg_Caption:
                    return " 打印中 ";
                case PreviewStringId.NoneString:
                    return " (无) ";
                case PreviewStringId.OpenFileDialog_Filter:
                    return " 预览文档文件(*{0})|*{0}|所有文件(*.*)|*.*";
                case PreviewStringId.OpenFileDialog_Title:
                    return "打开";
                case PreviewStringId.RibbonPreview_ExportXlsx_Caption:
                    return "XLSX 文件";
                case PreviewStringId.RibbonPreview_ExportXps_Caption:
                    return " XPS 文件";
                case PreviewStringId.RibbonPreview_Open_Caption:
                    return " 打开 ";
                case PreviewStringId.RibbonPreview_PageGroup_Document:
                    return " 文档 ";
                case PreviewStringId.RibbonPreview_Parameters_Caption:
                    return " 参数 ";
                case PreviewStringId.RibbonPreview_Parameters_STipTitle:
                    return " 参数 ";
                case PreviewStringId.RibbonPreview_Save_Caption:
                    return " 保存 ";
                case PreviewStringId.RibbonPreview_SendXlsx_Caption:
                    return " XLSX 文件";
                case PreviewStringId.RibbonPreview_SendXps_Caption:
                    return " XPS 文件";
                case PreviewStringId.SB_PageOfPages:
                    return " 页 {0} / {1}";
                case PreviewStringId.SB_TTip_Stop:
                    return "停止";
                case PreviewStringId.TB_TTip_Open:
                    return "打开文档";
                case PreviewStringId.TB_TTip_Parameters:
                    return "参数";
                case PreviewStringId.TB_TTip_Save:
                    return "保存文档";
                case PreviewStringId.ExportOption_ConfirmPermissionsPasswordForm_Caption:
                    return "确认权限密码";
                case PreviewStringId.ExportOption_ConfirmPermissionsPasswordForm_Name:
                    return "权限密码:";
                case PreviewStringId.ExportOption_PdfChangingPermissions_CommentingFillingSigning:
                    return "注释、 填写表单域，并签署现有签名域";
                case PreviewStringId.ExportOption_PdfChangingPermissions_InsertingDeletingRotating:
                    return "插入、 删除和旋转页面";
                case PreviewStringId.SaveDlg_FilterXps:
                    return "XPS 文档";
                case PreviewStringId.WatermarkTypePicture:
                    return " (图片) ";
                case PreviewStringId.WatermarkTypeText:
                    return " (文本) ";
                case PreviewStringId.ExportOption_PdfPrintingPermissions_HighResolution:
                    return " 高分辨率 ";
                case PreviewStringId.ExportOption_PdfPrintingPermissions_LowResolution:
                    return " 低分辨率 （150 dpi）";
                case PreviewStringId.ExportOption_PdfSignature_EmptyCertificate:
                    return " 无 ";
                case PreviewStringId.ExportOption_PdfSignature_Issuer:
                    return " 发行人：";
                case PreviewStringId.ExportOption_PdfSignature_ValidRange:
                    return " 从有效： {0: d} {1:d} 到";
                case PreviewStringId.ExportOption_PdfSignatureOptions:
                    return "数字签名:";
                case PreviewStringId.ExportOption_PdfSignatureOptions_Certificate:
                    return "证书";
                case PreviewStringId.ExportOption_PdfSignatureOptions_ContactInfo:
                    return "联系信息";
                case PreviewStringId.ExportOption_PdfSignatureOptions_Location:
                    return "位置";
                case PreviewStringId.ExportOption_PdfSignatureOptions_None:
                    return "(无)";
                case PreviewStringId.ExportOption_PdfSignatureOptions_Reason:
                    return "原因";
                case PreviewStringId.ExportOption_XlsRawDataMode:
                    return "原始数据模式";
                case PreviewStringId.Msg_CannotLoadDocument:
                    return "指定的文件不能加载，因为它不包含有效的 XML 数据或超出允许的大小。";
                case PreviewStringId.Msg_ErrorTitle:
                    return "错误";
                case PreviewStringId.Msg_FileDoesNotContainValidXml:
                    return "指定的文件不包含有效的 XML 数据中的 PRNX 格式。停止加载。";
                case PreviewStringId.Msg_FileReadOnly:
                    return "文件{0}设置为只读，用不同的文件名再试。";
                case PreviewStringId.Msg_GoToNonExistentPage:
                    return "在此文档中有没有编号的页面 {0}。";
                case PreviewStringId.Msg_InvalidBarcodeData:
                    return "二进制数据不能超过 1033 字节。";
                case PreviewStringId.Msg_NoParameters:
                    return "不存在指定的参数： {0}.";
                case PreviewStringId.Msg_PathTooLong:
                    return "路径过长。尝试较短的名称。";
                case PreviewStringId.Msg_SeparatorCannotBeEmptyString:
                    return "分隔符不能为空字符串。";
                case PreviewStringId.Msg_WrongPrinting:
                    return "在打印文档时出错。";
                case PreviewStringId.RibbonPreview_ExportXlsx_Description:
                    return "Microsoft Excel 2007 工作簿";
                case PreviewStringId.RibbonPreview_Open_STipContent:
                    return "打开的文档。";
                case PreviewStringId.RibbonPreview_Open_STipTitle:
                    return "打开 （Ctrl + O）";
                case PreviewStringId.RibbonPreview_Save_STipContent:
                    return "保存该文档。";
                case PreviewStringId.RibbonPreview_Save_STipTitle:
                    return "保存(Ctrl + S)";
                case PreviewStringId.RibbonPreview_SendXlsx_Description:
                    return "Microsoft Excel 2007 工作簿";
                case PreviewStringId.RibbonPreview_PageGroup_Close:
                    return "关闭";
                case PreviewStringId.MenuItem_Copy:
                    return "复制";
                case PreviewStringId.ExportOption_ConfirmOpenPasswordForm_Caption:
                    return "确认文档打开口令";
                case PreviewStringId.ExportOption_ConfirmOpenPasswordForm_Name:
                    return "文档打开口令:";
                case PreviewStringId.ExportOption_ConfirmPermissionsPasswordForm_Note:
                    return "请确认权限密码。一定要记下该密码。你会需要它在将来更改这些设置。";
                case PreviewStringId.ExportOption_HtmlTableLayout:
                    return "表布局";
                case PreviewStringId.ExportOption_PdfChangingPermissions_FillingSigning:
                    return "在表单域中填写及签署现有签名域";
                case PreviewStringId.ExportOption_PdfConvertImagesToJpeg:
                    return "转换为 Jpeg 图像";
                case PreviewStringId.ExportOption_PdfPasswordSecurityOptions:
                    return "密码安全：";
                case PreviewStringId.ExportOption_PdfShowPrintDialogOnOpen:
                    return "显示打印对话框打开";
                case PreviewStringId.MenuItem_PrintSelection:
                    return "打印...";
                case PreviewStringId.Msg_FileAlreadyExists:
                    return "输出文件已经存在。单击确定以覆盖。";
                case PreviewStringId.Msg_FileDoesNotHavePrnxExtension:
                    return "指定的文件不会具有 PRNX 扩展名。是否仍要继续吗?";
                case PreviewStringId.Msg_InvalidatePath:
                    return "找不到指定的路径";
                case PreviewStringId.PageInfo_PageTotal:
                    return "[页 #]";
                case PreviewStringId.ParameterLookUpSettingsNoLookUp:
                    return "没有查找";
                case PreviewStringId.RibbonPreview_ExportXlsx_STipTitle:
                    return "导出到 XLSX";
                case PreviewStringId.RibbonPreview_ExportXps_Description:
                    return "XPS";
                case PreviewStringId.RibbonPreview_Parameters_STipContent:
                    return "打开参数窗格中，它允许您为报表参数输入值。";
                case PreviewStringId.RibbonPreview_SendXlsx_STipContent:
                    return "将文档导出到XLSX 并将其附加到电子邮件。";
                case PreviewStringId.RibbonPreview_SendXps_Description:
                    return "XPS";
                case PreviewStringId.SaveDlg_FilterXlsx:
                    return "XLSX 文件";
                case PreviewStringId.ExportOption_ConfirmationDoesNotMatchForm_Msg:
                    return "确认密码不匹配。请从头开始，然后再次输入该密码。";
                case PreviewStringId.SB_PageOfPagesHint:
                    return "文件中的页号。单击以打开转到页面对话框。";
                case PreviewStringId.ExportOption_ConfirmOpenPasswordForm_Note:
                    return "请确认文档打开口令。一定要记下该密码。它将需要打开的文档。";
                case PreviewStringId.ExportOption_PdfChangingPermissions_AnyExceptExtractingPages:
                    return "除了提取页面";
                case PreviewStringId.Msg_BigBitmapToCreate:
                    return "的输出文件是太大了。请尝试降低图像分辨率，或选择另一个出口模式。";
                case PreviewStringId.Msg_BigFileToCreate:
                    return "的输出文件是太大了。尽量减少它的页数，或将它拆分为几个文档.";
                case PreviewStringId.Msg_BigFileToCreateJPEG:
                    return "的输出文件是创建一个 JPEG 文件太大。请选择另一个图像格式或另一个出口模式。";
                case PreviewStringId.Msg_NoDifferentFilesInStream:
                    return "A 文档无法导出到一个流中的 DifferentFiles 模式。使用 SingleFile 或 SingleFilePageByPage 模式相反。";
                case PreviewStringId.Msg_XlsMoreThanMaxColumns:
                    return "创建的 XLS 文件是为 XLS 格式文件，太大，因为它包含超过 256 列。请尝试使用中兑换 XLSX 格式，相反。";
                case PreviewStringId.Msg_XlsMoreThanMaxRows:
                    return "创建的 XLS 文件是太大，XLS 格式，因为它包含超过 65536 行。请尝试使用中兑换 XLSX 格式，相反。";
                case PreviewStringId.Msg_XlsxMoreThanMaxColumns:
                    return "创建中兑换 XLSX 文件是太大，对于中兑换 XLSX 格式，因为它包含超过 16384 列。请尝试减少您的报表中的列的数量，并将报表导出到中兑换 XLSX 再次。";
                case PreviewStringId.Msg_XlsxMoreThanMaxRows:
                    return "创建中兑换 XLSX 文件是太大，对于中兑换 XLSX 格式，因为它包含超过 1048576 行。请尝试减少您的报表中的行的数量，并将报表导出到中兑换 XLSX 再次。";
                case PreviewStringId.NetworkPrinterFormat:
                    return "{0} {1} 上的";
                case PreviewStringId.RibbonPreview_ExportXlsx_STipContent:
                    return "出口中兑换 XLSX 文件并将其保存到磁盘上的文件。";
                case PreviewStringId.SaveDlg_FilterNativeFormat:
                    return "本机格式";
                case PreviewStringId.PrinterStatus_Error:
                    return "错误";
                case PreviewStringId.PrinterStatus_DoorOpen:
                    return "打印机大门是敞开的。";
                case PreviewStringId.ExportOption_XlsExportMode_SingleFilePageByPage:
                    return "逐页单文件";
                case PreviewStringId.TB_TTip_HighlightEditingFields:
                    return "突出显示编辑字段";
                case PreviewStringId.RibbonPreview_HighlightEditingFields_Caption:
                    return "编辑字段";
                case PreviewStringId.RibbonPreview_HighlightEditingFields_STipTitle:
                    return "突出显示编辑字段";
                case PreviewStringId.EditingFieldEditorCategories_Numeric:
                    return "数字";
                case PreviewStringId.ExportOption_PdfACompatibility:
                    return "PDF/A-2b";
                case PreviewStringId.ExportOption_PdfACompatibility_None:
                    return "无";
                case PreviewStringId.EditingFieldEditors_Integer:
                    return "整数";
                case PreviewStringId.EditingFieldEditors_Date:
                    return "日期";
            }
            return base.GetLocalizedString(id);
        }
    }

    public class XtraReportsCHS : ReportLocalizer
    {
        public override string Language => "简体中文";

        //public XtraReportsCHS()
        //{
        //    foreach (ReportStringId reportStringId in Enum.GetValues(typeof(ReportStringId)))
        //        AddString(reportStringId, "");
        //}

        /// <summary>
        /// 报表汉化
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override string GetLocalizedString(ReportStringId id)
        {
            switch (id)
            {
                case ReportStringId.RibbonXRDesign_AlignToGrid_STipTitle:
                    return " 网格对齐 ";
                case ReportStringId.RibbonXRDesign_SaveFile_Description:
                    return " 保存当前报表 ";
                case ReportStringId.RibbonXRDesign_SizeToControlWidth_STipTitle:
                    return " 置为相同宽度 ";
                case ReportStringId.CatData:
                    return " 数据 ";
                case ReportStringId.Cmd_RtfLoad:
                    return " 载入文件...";
                case ReportStringId.CatBehavior:
                    return " 行为 ";
                case ReportStringId.RibbonXRDesign_FontName_STipContent:
                    return " 改变字体样式.";
                case ReportStringId.UD_Capt_JustifyJustify:
                    return " 两边对齐 ";
                case ReportStringId.RibbonXRDesign_ForeColor_STipTitle:
                    return " 前景色 ";
                case ReportStringId.UD_Hint_Paste:
                    return " 将剪贴板上的控件粘贴到选定的位置 ";
                case ReportStringId.UD_TTip_VertSpaceConcatenate:
                    return " 删除垂直间距 ";
                case ReportStringId.UD_Capt_Close:
                    return " 关闭(&amp; C)";
                case ReportStringId.UD_Hint_Close:
                    return " 关闭(&amp; C)";
                case ReportStringId.UD_Msg_ReportChanged:
                    return " 报表已经发生修改。想要保存修改后的内容吗？";
                case ReportStringId.UD_TTip_FormatFontName:
                    return " 字体名称 ";
                case ReportStringId.UD_TTip_FormatFontSize:
                    return " 字体大小 ";
                case ReportStringId.STag_Name_ColumnSpacing:
                    return " 列间距 ";
                case ReportStringId.UD_Capt_Paste:
                    return " 粘贴 ";
                case ReportStringId.Verb_EditBindings:
                    return " 编辑带区 ";
                case ReportStringId.UD_Capt_MakeSameSizeBoth:
                    return " 宽度和高度 ";
                case ReportStringId.UD_Capt_SaveAll:
                    return " 保存全部(&amp; A)";
                case ReportStringId.RibbonXRDesign_Cut_STipContent:
                    return " 剪切在报表中所选定的控件，并将其放到剪贴板中..";
                case ReportStringId.UD_Hint_ForegroundColor:
                    return " 设置控件的前景色 ";
                case ReportStringId.UD_Hint_MakeSameSizeBoth:
                    return " 使选择的控件尺寸相等 ";
                case ReportStringId.UD_Title_FieldList_AddNewDataSourceText:
                    return " 增加新数据源 ";
                case ReportStringId.RibbonXRDesign_HtmlHome_Caption:
                    return " 主页 ";
                case ReportStringId.RibbonXRDesign_Copy_STipContent:
                    return " 复制在报表中所选定的控件，并将其放到剪贴板中.";
                case ReportStringId.RibbonXRDesign_CenterVertically_STipTitle:
                    return " 垂直居中 ";
                case ReportStringId.RepTabCtl_ReportStatus:
                    return "{0}{{纸张类型: {1} } }";
                case ReportStringId.RibbonXRDesign_HtmlRefresh_STipTitle:
                    return " 刷新 ";
                case ReportStringId.UD_Capt_ZoomOut:
                    return " 缩小 ";
                case ReportStringId.STag_Name_ColumnMode:
                    return " 多列模式 ";
                case ReportStringId.RibbonXRDesign_BackColor_STipContent:
                    return " 改变文字的背景色.";
                case ReportStringId.UD_Hint_SaveFileAs:
                    return " 用一个新名称来保存报表 ";
                case ReportStringId.RibbonXRDesign_VertSpaceMakeEqual_Caption:
                    return " 垂直间距相等 ";
                case ReportStringId.MultiColumnDesignMsg1:
                    return " 重复列的间距 ";
                case ReportStringId.MultiColumnDesignMsg2:
                    return " 放置于此的控件不能正确打印 ";
                case ReportStringId.RibbonXRDesign_SaveFileAs_Description:
                    return " 重新命名保存报表 ";
                case ReportStringId.UD_TTip_EditCopy:
                    return " 复制 ";
                case ReportStringId.CatNavigation:
                    return " 导航 ";
                case ReportStringId.Msg_CreateSomeInstance:
                    return " 在一个窗体上不能创建两个类实例 ";
                case ReportStringId.PivotGridForm_ItemSettings_Caption:
                    return " 打印设置 ";
                case ReportStringId.UD_Group_CenterInForm:
                    return " 居中 ";
                case ReportStringId.Msg_NoBookmarksWereFoundInReportForXrToc:
                    return " 没有在报表中找到书签 ";
                case ReportStringId.RibbonXRDesign_SizeToControl_Caption:
                    return " 置为相同大小 ";
                case ReportStringId.RibbonXRDesign_SaveAll_STipContent:
                    return " 保存所有修改的报表 ";
                case ReportStringId.PivotGridFrame_Appearances_DescriptionText:
                    return " 选择一个或多个外观对象来自定义可见元素对应打印外观.";
                case ReportStringId.RibbonXRDesign_JustifyCenter_STipTitle:
                    return " 文本居中 ";
                case ReportStringId.Msg_WrongReportClassName:
                    return " 在逆序列化过程中发生错误－可能由于错误的报表类名 ";
                case ReportStringId.Cmd_TableConvertToLabels:
                    return " 转换为标签 ";
                case ReportStringId.RibbonXRDesign_ZoomIn_STipTitle:
                    return " 放大 ";
                case ReportStringId.UD_TTip_FormatItalic:
                    return " 斜体 ";
                case ReportStringId.RibbonXRDesign_Scripts_STipContent:
                    return " 显示或隐藏脚本编辑器 ";
                case ReportStringId.RibbonXRDesign_Copy_STipTitle:
                    return " 复制 ";
                case ReportStringId.RibbonXRDesign_VertSpaceConcatenate_STipTitle:
                    return " 删除垂直间距 ";
                case ReportStringId.BindingMapperForm_InvalidBindingWarning:
                    return " 无效绑定 ";
                case ReportStringId.PivotGridForm_ItemLayout_Description:
                    return " 用户自定义当前XRPivotGrid的布局并预览数据.";
                case ReportStringId.UD_Capt_MakeSameSizeSizeToGrid:
                    return " 大小对齐到网络 ";
                case ReportStringId.UD_Hint_AlignRights:
                    return " 将选择的控件右对齐 ";
                case ReportStringId.Cmd_Delete:
                    return " 删除(&amp; D)";
                case ReportStringId.Cmd_Detail:
                    return " 详细内容 ";
                case ReportStringId.UD_Capt_OpenFile:
                    return " 打开文件...";
                case ReportStringId.RibbonXRDesign_CenterVertically_Caption:
                    return " 垂直居中 ";
                case ReportStringId.Msg_FillDataError:
                    return " 在组装数据源的时候发生错误。程序抛出以下异常:";
                case ReportStringId.Msg_FileCorrupted:
                    return " 不能载入报表。该文件可能被损坏或者报表的部件已经丢失。";
                case ReportStringId.RibbonXRDesign_OpenFile_STipContent:
                    return " 打开报表.";
                case ReportStringId.STag_Name_Height:
                    return " 高度 ";
                case ReportStringId.PivotGridForm_GroupMain_Description:
                    return " 重要设置(字段, 布局).";
                case ReportStringId.RibbonXRDesign_VertSpaceDecrease_STipContent:
                    return " 减少所选控件的垂直间距.";
                case ReportStringId.RibbonXRDesign_StatusBar_HtmlDone:
                    return " 已完成 ";
                case ReportStringId.UD_Capt_MdiTileVertical:
                    return " 纵向平铺 ";
                case ReportStringId.UD_Hint_MakeSameSizeWidth:
                    return " 使选择的控件宽度相等 ";
                case ReportStringId.RibbonXRDesign_ZoomIn_Caption:
                    return " 放大 ";
                case ReportStringId.NewParameterEditorForm_DataSource:
                    return " 数据源：";
                case ReportStringId.UD_TTip_FormatBold:
                    return " 加粗 ";
                case ReportStringId.UD_XtraReportsToolboxCategoryName:
                    return " 标准控件 ";
                case ReportStringId.UD_Capt_MainMenuName:
                    return " 主菜单 ";
                case ReportStringId.PivotGridForm_ItemFields_Description:
                    return " 管理字段.";
                case ReportStringId.UD_Hint_TabbedInterface:
                    return " 在标签和WindowMDI布局模式之间转化 ";
                case ReportStringId.RibbonXRDesign_Copy_Caption:
                    return " 复制 ";
                case ReportStringId.RibbonXRDesign_VertSpaceIncrease_STipTitle:
                    return " 增加垂直间距 ";
                case ReportStringId.Msg_IncorrectPadding:
                    return " 输入值必须等于或大于0.";
                case ReportStringId.UD_Hint_Undo:
                    return " 撤销上一次操作 ";
                case ReportStringId.UD_Hint_Redo:
                    return " 重做上一次操作 ";
                case ReportStringId.UD_Hint_Zoom:
                    return " 选择 / 输入缩放率 ";
                case ReportStringId.UD_Hint_Exit:
                    return " 关闭设计器 ";
                case ReportStringId.UD_Hint_Copy:
                    return " 将控件拷贝到剪贴板 ";
                case ReportStringId.UD_TTip_HorizSpaceMakeEqual:
                    return " 水平间距相等 ";
                case ReportStringId.SR_Side_Margins:
                    return " 边距 ";
                case ReportStringId.RibbonXRDesign_ForeColor_STipContent:
                    return " 改变文字前景色.";
                case ReportStringId.UD_Capt_ToolbarName:
                    return " 主工具栏 ";
                case ReportStringId.UD_Title_ReportExplorer:
                    return " 报表资源管理器 ";
                case ReportStringId.RibbonXRDesign_AlignRight_Caption:
                    return " 右对齐 ";
                case ReportStringId.RibbonXRDesign_AlignToGrid_STipContent:
                    return " 将选定控件的位置对其到网格 ";
                case ReportStringId.UD_TTip_AlignLeft:
                    return " 左对齐 ";
                case ReportStringId.RibbonXRDesign_NewReportWizard_Caption:
                    return " 使用向导新建立报表...";
                case ReportStringId.Verb_RTFClear:
                    return " 清除 ";
                case ReportStringId.RibbonXRDesign_HtmlBackward_STipTitle:
                    return " 返回 ";
                case ReportStringId.RibbonXRDesign_JustifyJustify_STipContent:
                    return " 同时将文字左右两端同时对其，并根据需要增加字符间距.";
                case ReportStringId.RibbonXRDesign_HtmlForward_Caption:
                    return " 下页 ";
                case ReportStringId.RibbonXRDesign_BringToFront_Caption:
                    return " 置于顶部 ";
                case ReportStringId.RibbonXRDesign_SaveAll_Caption:
                    return " 保存全部 ";
                case ReportStringId.UD_Hint_FontUnderline:
                    return " 字体加下划线 ";
                case ReportStringId.RibbonXRDesign_VertSpaceMakeEqual_STipContent:
                    return " 将所选的控件设置为相同的垂直间距.";
                case ReportStringId.RibbonXRDesign_HtmlPageText:
                    return " HTML视图 ";
                case ReportStringId.UD_Title_FieldList_ProjectObjectsText:
                    return " 项目对象 ";
                case ReportStringId.Msg_InvalidLeaderSymbolForXrTocLevel:
                    return " 无效的符号 ";
                case ReportStringId.UD_Hint_AlignToGrid:
                    return " 将选择的控件与网格对齐 ";
                case ReportStringId.RibbonXRDesign_FontUnderline_STipContent:
                    return " 给所选文字加上下划线 ";
                case ReportStringId.UD_TTip_Undo:
                    return " 撤销 ";
                case ReportStringId.UD_TTip_Redo:
                    return " 重做 ";
                case ReportStringId.Cmd_TableInsertColumnToRight:
                    return " 在右面插入一列 ";
                case ReportStringId.Verb_RunDesigner:
                    return " 运行设计器...";
                case ReportStringId.UD_Capt_AlignLefts:
                    return " 左对齐 ";
                case ReportStringId.UD_Group_VerticalSpacing:
                    return " 垂直间距 ";
                case ReportStringId.CatOptions:
                    return " 选项 ";
                case ReportStringId.RepTabCtl_Preview:
                    return " 预览 ";
                case ReportStringId.RibbonXRDesign_ForeColor_Caption:
                    return " 前景色 ";
                case ReportStringId.RibbonXRDesign_PageGroup_Scripts:
                    return " 脚本 ";
                case ReportStringId.UD_Capt_FormattingToolbarName:
                    return " 格式化工具栏 ";
                case ReportStringId.STag_Name_Bands:
                    return " 带区 ";
                case ReportStringId.UD_Hint_Cut:
                    return " 删除该控件并将它拷贝到剪贴板 ";
                case ReportStringId.UD_Capt_JustifyCenter:
                    return " 居中对齐 ";
                case ReportStringId.Cmd_TableInsertColumnToLeft:
                    return " 在左面插入一列 ";
                case ReportStringId.ScriptEditor_Validate:
                    return " 验证 ";
                case ReportStringId.NewParameterEditorForm_DataAdapter:
                    return " Dataadapter:";
                case ReportStringId.Msg_ErrorTitle:
                    return " 错误 ";
                case ReportStringId.Cmd_TableDeleteColumn:
                    return " 删除列 ";
                case ReportStringId.UD_Capt_NewReport:
                    return " 新建(Ctrl + N) ";
                case ReportStringId.ScriptEditor_ErrorDescription:
                    return " 描述 ";
                case ReportStringId.ScriptEditor_ClickValidate:
                    return " 点击'验证'检查脚本。";
                case ReportStringId.SSForm_Msg_FileFilter:
                    return " 报表样式单文件(*.repss) | *.repss | 所有的文件(*.*) | *.*";
                case ReportStringId.Cmd_RtfClear:
                    return " 清除 ";
                case ReportStringId.UD_Hint_JustifyLeft:
                    return " 左对齐控件中的文本 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceMakeEqual_Caption:
                    return " 水平间距相等 ";
                case ReportStringId.RibbonXRDesign_Zoom_STipContent:
                    return " 改变文档设计器的缩放比例.";
                case ReportStringId.Msg_IncorrectArgument:
                    return " 错误的参数值 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceDecrease_Caption:
                    return " 减少水平间距 ";
                case ReportStringId.RibbonXRDesign_Windows_STipTitle:
                    return " 显示 / 隐藏窗口 ";
                case ReportStringId.RibbonXRDesign_AlignHorizontalCenters_STipContent:
                    return " 沿水平方向中间对齐所选控件 ";
                case ReportStringId.Cmd_AlignToGrid:
                    return " 对齐表格 ";
                case ReportStringId.RepTabCtl_HtmlView:
                    return " HTML视图 ";
                case ReportStringId.RibbonXRDesign_HtmlHome_STipTitle:
                    return " 主页 ";
                case ReportStringId.BindingMapperForm_ShowOnlyInvalidBindings:
                    return " 只显示无效绑定 ";
                case ReportStringId.SR_Vertical_Pitch:
                    return " 垂直倾斜 ";
                case ReportStringId.Cmd_TableDeleteRow:
                    return " 删除行 ";
                case ReportStringId.RibbonXRDesign_FontItalic_STipContent:
                    return " 使所选文字为斜体.";
                case ReportStringId.Verb_FormatString:
                    return " 格式化字符串...";
                case ReportStringId.ScriptEditor_ErrorColumn:
                    return " 列 ";
                case ReportStringId.UD_Capt_OrderSendToBack:
                    return " 置于底层 ";
                case ReportStringId.XRSubreport_ReportSourceInfo:
                    return " 报表资源:{0}";
                case ReportStringId.UD_Capt_SelectAll:
                    return " 选择所有 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceDecrease_STipTitle:
                    return " 减少水平间距 ";
                case ReportStringId.FRSForm_Msg_MoreThanOneRule:
                    return " 你选择了一个以上的格式规则 ";
                case ReportStringId.RibbonXRDesign_HtmlFind_STipTitle:
                    return " 查找 ";
                case ReportStringId.UD_Hint_ViewDockPanels:
                    return " 隐藏 / 显示{0} 窗口 ";
                case ReportStringId.Msg_ScriptExecutionError:
                    return "在程序{0} 中的脚本执行时发生错误: {1}Procedure{0}已经执行,它不会被再调用。";
                case ReportStringId.UD_Hint_FontItalic:
                    return " 使字体变斜 ";
                case ReportStringId.Msg_GroupSortWarning:
                    return " 您所要删除的组不为空。你想连同其控制一起删除吗？";
                case ReportStringId.Msg_InvalidReportSource:
                    return " 不能作为当前报表的子报表 ";
                case ReportStringId.UD_Capt_ZoomFactor:
                    return " 缩放比例:{0}%";
                case ReportStringId.RibbonXRDesign_ZoomExact_Caption:
                    return " 精确缩放:";
                case ReportStringId.Msg_ScriptError:
                    return " 脚本中的错误如下:{0}";
                case ReportStringId.CatStructure:
                    return " 结构 ";
                case ReportStringId.Msg_WarningControlsAreOverlapped:
                    return " 输出通知:以下空间出现重叠并且可能导致输出到HTML、XLS、和RTF不正确 -{0}.";
                case ReportStringId.UD_Capt_AlignMiddles:
                    return " 水平居中对齐 ";
                case ReportStringId.NewParameterEditorForm_DataMember:
                    return " Datamember:";
                case ReportStringId.PivotGridForm_ItemAppearances_Caption:
                    return " 外观 ";
                case ReportStringId.RibbonXRDesign_StatusBar_HtmlProcessing:
                    return " 处理中...";
                case ReportStringId.RibbonXRDesign_FontUnderline_Caption:
                    return " 下划线 ";
                case ReportStringId.RibbonXRDesign_JustifyJustify_Caption:
                    return " 两端对齐 ";
                case ReportStringId.UD_Title_GroupAndSort:
                    return " 分组和排序 ";
                case ReportStringId.Cmd_GroupFooter:
                    return " 分组尾 ";
                case ReportStringId.DesignerStatus_Height:
                    return " 高度 ";
                case ReportStringId.RibbonXRDesign_NewReport_Caption:
                    return " 新建报表 ";
                case ReportStringId.PivotGridForm_ItemSettings_Description:
                    return " 调整当前XRPivotGrid的打印设置.";
                case ReportStringId.RibbonXRDesign_AlignBottom_STipContent:
                    return " 底部对齐所选控件 ";
                case ReportStringId.UD_TTip_FormatAlignLeft:
                    return " 左对齐 ";
                case ReportStringId.UD_Hint_AlignTops:
                    return " 将选择的控件顶端对齐 ";
                case ReportStringId.RibbonXRDesign_SaveFile_Caption:
                    return " 保存 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceMakeEqual_STipTitle:
                    return " 水平间距等距 ";
                case ReportStringId.DesignerStatus_Size:
                    return " 大小 ";
                case ReportStringId.UD_Hint_OrderSendToBack:
                    return " 使选择的控件置于底层 ";
                case ReportStringId.Verb_EditText:
                    return " 编辑内容 ";
                case ReportStringId.Cmd_BringToFront:
                    return " 置于顶层 ";
                case ReportStringId.UD_Capt_MakeSameSizeHeight:
                    return " 高度 ";
                case ReportStringId.Msg_GroupSortNoDataSource:
                    return " 需先为报表增加一个数据源，才可增加一个新组或排序规则。";
                case ReportStringId.UD_Hint_NewWizardReport:
                    return " 使用导航功能，创建一个新报表 ";
                case ReportStringId.UD_Hint_MdiTileVertical:
                    return " 从左边向右边安排打开的所有文档 ";
                case ReportStringId.SSForm_Msg_StyleNamePreviewPostfix:
                    return " 样式 ";
                case ReportStringId.UD_Capt_OrderBringToFront:
                    return " 置于顶层 ";
                case ReportStringId.SSForm_Msg_NoStyleSelected:
                    return " 没有选中任何样式 ";
                case ReportStringId.RibbonXRDesign_AlignBottom_Caption:
                    return " 底部对齐 ";
                case ReportStringId.UD_Capt_MdiCascade:
                    return " 级联(&amp; C)";
                case ReportStringId.UD_Capt_JustifyLeft:
                    return " 左对齐 ";
                case ReportStringId.RibbonXRDesign_NewReportWizard_STipContent:
                    return " 启动报表向导帮助你创建简单的、自定义报表.";
                case ReportStringId.RibbonXRDesign_NewReport_STipContent:
                    return " 创建一个新的空白报表,你可以插入字段和控件并设计报表.";
                case ReportStringId.Cmd_TableInsertCell:
                    return " 插入单元格 ";
                case ReportStringId.UD_Capt_AlignCenters:
                    return " 垂直居中对齐 ";
                case ReportStringId.Msg_DontSupportMulticolumn:
                    return " 详细报表不支持多列。";
                case ReportStringId.UD_Hint_JustifyJustify:
                    return " 控件内容两边对齐 ";
                case ReportStringId.RibbonXRDesign_Zoom_STipTitle:
                    return " 缩放 ";
                case ReportStringId.UD_TTip_VertSpaceIncrease:
                    return " 增加垂直间距 ";
                case ReportStringId.UD_Capt_AlignTops:
                    return " 顶端对齐 ";
                case ReportStringId.RibbonXRDesign_AlignRight_STipContent:
                    return " 右对齐所选控件 ";
                case ReportStringId.UD_PropertyGrid_NotSetText:
                    return " (未设置) ";
                case ReportStringId.CatElements:
                    return " 元素 ";
                case ReportStringId.UD_Msg_MdiReportChanged:
                    return " '{0}'已发生了改变。是否要保存这些变化？";
                case ReportStringId.UD_Capt_ForegroundColor:
                    return " 前景色 ";
                case ReportStringId.RibbonXRDesign_VertSpaceIncrease_STipContent:
                    return " 增加所选控件的垂直间距.";
                case ReportStringId.UD_Capt_SaveFileAs:
                    return " 另存为...";
                case ReportStringId.UD_Capt_SpacingIncrease:
                    return " 增加间距 ";
                case ReportStringId.CatPrinting:
                    return " 打印 ";
                case ReportStringId.UD_Hint_OpenFile:
                    return " 打开报表 ";
                case ReportStringId.RibbonXRDesign_SaveFileAs_STipTitle:
                    return " 报表另存为 ";
                case ReportStringId.STag_Name_FormatString:
                    return " 格式化字符串 ";
                case ReportStringId.RibbonXRDesign_Undo_Caption:
                    return " 撤消 ";
                case ReportStringId.Cmd_BandMoveDown:
                    return " 上移 ";
                case ReportStringId.Msg_PlacingXrTocIntoIncorrectContainer:
                    return " XRTableOfContents控件只能放到页眉页脚 ";
                case ReportStringId.ScriptEditor_ScriptsAreValid:
                    return " 所有脚本都合法。";
                case ReportStringId.UD_TTip_SizeToControlHeight:
                    return " 高度相同 ";
                case ReportStringId.Cmd_GroupHeader:
                    return " 分组头 ";
                case ReportStringId.UD_Hint_SpacingIncrease:
                    return " 增加选择的控件间距 ";
                case ReportStringId.SR_Width:
                    return " 宽度 ";
                case ReportStringId.Msg_WarningRemoveCalculatedFields:
                    return " 该操作将从所有数据表中删除所有计算字段。您是否继续 ?";
                case ReportStringId.RibbonXRDesign_Cut_STipTitle:
                    return " 剪切 ";
                case ReportStringId.RibbonXRDesign_Close_STipContent:
                    return " 关闭当前的报表 ";
                case ReportStringId.RibbonXRDesign_Undo_STipContent:
                    return " 撤消最后一步操作.";
                case ReportStringId.UD_Hint_ViewTabs: return " 切换到{0} 标签 ";
                case ReportStringId.UD_Hint_ViewBars:
                    return " 隐藏 / 显示{0}";
                case ReportStringId.RepTabCtl_Scripts:
                    return " 脚本 ";
                case ReportStringId.ScriptEditor_NewString:
                    return " 新建 ";
                case ReportStringId.RibbonXRDesign_CenterHorizontally_STipTitle:
                    return " 水平居中 ";
                case ReportStringId.RibbonXRDesign_Paste_STipContent:
                    return " 粘贴剪贴板中的内容.";
                case ReportStringId.DesignerStatus_Location:
                    return " 位置 ";
                case ReportStringId.PivotGridFrame_Layouts_DescriptionText:
                    return " 修改XRPivotGrid的布局(排序设置, 字段排列)并单击应用按钮来应用对当前XRPivotGrid的修改.你也可以保存布局到XML文件中(这使得其可以在设计时和运行时下加载并应用到其他视图).";
                case ReportStringId.RibbonXRDesign_Close_Caption:
                    return " 关闭 ";
                case ReportStringId.GroupSort_AddSort:
                    return " 增加排序 ";
                case ReportStringId.UD_Capt_ZoomToolbarName:
                    return " 缩放工具 ";
                case ReportStringId.STag_Name_PreviewRowCount:
                    return " 预览行数 ";
                case ReportStringId.RibbonXRDesign_Zoom_Caption:
                    return " 缩放 ";
                case ReportStringId.UD_Hint_MdiCascade:
                    return " 将打开的多有文档设置为级联，以便于他们可以相互重叠 ";
                case ReportStringId.UD_Capt_Cut:
                    return " 剪切 ";
                case ReportStringId.RibbonXRDesign_SendToBack_STipContent:
                    return " 将所选控件置于底部.";
                case ReportStringId.UD_TTip_VertSpaceDecrease:
                    return " 减少垂直间距 ";
                case ReportStringId.UD_Hint_BackGroundColor:
                    return " 设置控件的背景色 ";
                case ReportStringId.UD_Capt_BackGroundColor:
                    return " 背景色 ";
                case ReportStringId.Verb_Bind:
                    return " 绑定 ";
                case ReportStringId.Verb_Save:
                    return " 存储...";
                case ReportStringId.RibbonXRDesign_FontItalic_STipTitle:
                    return " 斜体 ";
                case ReportStringId.Verb_Delete:
                    return " 删除...";
                case ReportStringId.RibbonXRDesign_VertSpaceDecrease_STipTitle:
                    return " 减少垂直间距 ";
                case ReportStringId.RibbonXRDesign_Undo_STipTitle:
                    return " 撤消 ";
                case ReportStringId.UD_Group_MakeSameSize:
                    return " 尺寸相等 ";
                case ReportStringId.RibbonXRDesign_AlignHorizontalCenters_Caption:
                    return " 水平居中 ";
                case ReportStringId.RibbonXRDesign_VertSpaceDecrease_Caption:
                    return " 减少垂直间距 ";
                case ReportStringId.Cmd_SendToBack:
                    return " 置于底层 ";
                case ReportStringId.UD_TTip_FormatUnderline:
                    return " 下划线 ";
                case ReportStringId.Verb_Export:
                    return " 保存 / 导出...";
                case ReportStringId.SR_Top_Margin:
                    return " 上边距 ";
                case ReportStringId.UD_Hint_AlignBottoms:
                    return " 将选择的控件底端对齐 ";
                case ReportStringId.Cmd_Properties:
                    return " 属性(&amp; R)";
                case ReportStringId.UD_TTip_HorizSpaceIncrease:
                    return " 增加水平间距 ";
                case ReportStringId.UD_TTip_FormatBackColor:
                    return " 背景色 ";
                case ReportStringId.BCForm_Lbl_Property:
                    return " 属性 ";
                case ReportStringId.UD_TTip_ItemDescription:
                    return " 拖放对象来创建一个绑定控件 ";
                case ReportStringId.UD_Capt_CenterInFormHorizontally:
                    return " 水平方向 ";
                case ReportStringId.UD_Hint_CenterInFormHorizontally:
                    return " 在一个带区内使选择的控件水平居中 ";
                case ReportStringId.UD_TTip_FileSave:
                    return " 保存文件 ";
                case ReportStringId.UD_TTip_FileOpen:
                    return " 打开文件 ";
                case ReportStringId.RibbonXRDesign_PageText:
                    return " 报表设计器 ";
                case ReportStringId.UD_Hint_SaveAll:
                    return " 保存所有报表 ";
                case ReportStringId.RibbonXRDesign_Paste_STipTitle:
                    return " 粘贴 ";
                case ReportStringId.NewParameterEditorForm_ValueMember:
                    return " 值成员 ";
                case ReportStringId.RibbonXRDesign_VertSpaceMakeEqual_STipTitle:
                    return " 垂直等距 ";
                case ReportStringId.UD_Capt_AlignBottoms:
                    return " 底端对齐 ";
                case ReportStringId.RibbonXRDesign_BringToFront_STipContent:
                    return " 将所选控件置于顶部.";
                case ReportStringId.UD_Hint_MakeSameSizeSizeToGrid:
                    return " 调整选择的控件使网格对齐 ";
                case ReportStringId.RibbonXRDesign_FontBold_STipTitle:
                    return " 粗体 ";
                case ReportStringId.UD_Hint_ZoomOut:
                    return " 缩小设计界面 ";
                case ReportStringId.Verb_Import:
                    return " 导入...";
                case ReportStringId.Verb_Insert:
                    return " 插入...";
                case ReportStringId.RibbonXRDesign_AlignLeft_Caption:
                    return " 左对齐 ";
                case ReportStringId.UD_Capt_TabbedInterface:
                    return " 选项卡页面 ";
                case ReportStringId.RibbonXRDesign_Scripts_Caption:
                    return " 脚本 ";
                case ReportStringId.RepTabCtl_Designer:
                    return " 设计视图 ";
                case ReportStringId.Cmd_InsertBand:
                    return " 插入带区 ";
                case ReportStringId.Msg_FileContentCorrupted:
                    return " 不能载入报表的布局设计。该文件可能已经损坏或者包含错误信息。";
                case ReportStringId.RibbonXRDesign_VertSpaceIncrease_Caption:
                    return " 增加垂直间距 ";
                case ReportStringId.RibbonXRDesign_SaveFileAs_STipContent:
                    return " 将报表保存为另外一个文件名.";
                case ReportStringId.UD_XtraReportsPointerItemCaption:
                    return " 指针 ";
                case ReportStringId.UD_TTip_FormatAlignRight:
                    return " 右对齐 ";
                case ReportStringId.UD_Hint_MdiTileHorizontal:
                    return " 从顶向部安排打开的所有文档 ";
                case ReportStringId.Cmd_DetailReport:
                    return " 详细报表 ";
                case ReportStringId.UD_TTip_HorizSpaceDecrease:
                    return " 减少水平间距 ";
                case ReportStringId.UD_Hint_CenterInFormVertically:
                    return " 在一个带区内使选择的控件垂直居中 ";
                case ReportStringId.RibbonXRDesign_HtmlBackward_Caption:
                    return " 返回 ";
                case ReportStringId.RibbonXRDesign_JustifyRight_STipContent:
                    return " 右对齐文本.";
                case ReportStringId.RibbonXRDesign_AlignLeft_STipTitle:
                    return " 左对齐 ";
                case ReportStringId.RibbonXRDesign_AlignVerticalCenters_STipTitle:
                    return " 居中对齐 ";
                case ReportStringId.RibbonXRDesign_NewReportWizard_Description:
                    return " 启动一个向导创建新的报表 ";
                case ReportStringId.Msg_CreateReportInstance:
                    return " 当前编辑的报表与你尝试打开的报表类型不同。&lt; br / &gt; 你想打开已经选中的报表吗？";
                case ReportStringId.UD_TTip_SendToBack:
                    return " 置于底层 ";
                case ReportStringId.RibbonXRDesign_Close_STipTitle:
                    return " 关闭 ";
                case ReportStringId.RibbonXRDesign_AlignLeft_STipContent:
                    return " 左对齐所选控件 ";
                case ReportStringId.UD_TTip_AlignToGrid:
                    return " 对齐到网格 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceConcatenate_Caption:
                    return " 删除水平间距 ";
                case ReportStringId.RibbonXRDesign_SaveFile_STipContent:
                    return " 保存报表.";
                case ReportStringId.PivotGridForm_GroupMain_Caption:
                    return " 主要的 ";
                case ReportStringId.Msg_LargeText:
                    return " 文本内容太大。";
                case ReportStringId.UD_Hint_AlignCenters:
                    return " 将选择的控件垂直居中对齐 ";
                case ReportStringId.RibbonXRDesign_HtmlFind_STipContent:
                    return " 在该页上查找文本.";
                case ReportStringId.RibbonXRDesign_Redo_Caption:
                    return " 重做 ";
                case ReportStringId.RibbonXRDesign_NewReport_Description:
                    return " 创建新的空报表 ";
                case ReportStringId.SR_Number_Across:
                    return " 数字交叉 ";
                case ReportStringId.RibbonXRDesign_AlignRight_STipTitle:
                    return " 右对齐 ";
                case ReportStringId.RibbonXRDesign_Windows_STipContent:
                    return " 显示 / 隐藏工具栏,报表浏览器,字段列表和属性窗口.";
                case ReportStringId.RibbonXRDesign_JustifyLeft_Caption:
                    return " 左对齐文本 ";
                case ReportStringId.UD_TTip_AlignHorizontalCenters:
                    return " 中间对齐 ";
                case ReportStringId.RibbonXRDesign_Cut_Caption:
                    return " 剪切 ";
                case ReportStringId.RibbonXRDesign_BackColor_Caption:
                    return " 背景颜色 ";
                case ReportStringId.RibbonXRDesign_FontSize_STipTitle:
                    return " 字体大小 ";
                case ReportStringId.Cmd_TopMargin:
                    return " 上边距 ";
                case ReportStringId.RibbonXRDesign_ZoomIn_STipContent:
                    return " 放大查看报表的局部视图.";
                case ReportStringId.UD_TTip_CenterVertically:
                    return " 垂直居中 ";
                case ReportStringId.RibbonXRDesign_AlignVerticalCenters_STipContent:
                    return " 沿垂直方向居中对齐所选控件 ";
                case ReportStringId.SR_Horizontal_Pitch:
                    return " 水平倾斜 ";
                case ReportStringId.UD_Hint_NewReport:
                    return " 创建一个新的空报表 ";
                case ReportStringId.RibbonXRDesign_AlignTop_Caption:
                    return " 顶部对齐 ";
                case ReportStringId.RibbonXRDesign_SizeToControlHeight_Caption:
                    return " 置为相同高度 ";
                case ReportStringId.PivotGridForm_GroupPrinting_Description:
                    return " 当前XRPivotGrid打印选项处理.";
                case ReportStringId.RibbonXRDesign_Exit_STipContent:
                    return " 关闭报表设计器 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceConcatenate_STipTitle:
                    return " 删除水平间距 ";
                case ReportStringId.UD_TTip_DataMemberDescription:
                    return " 数据成员:{0}";
                case ReportStringId.RibbonXRDesign_JustifyRight_STipTitle:
                    return " 右对齐文本 ";
                case ReportStringId.UD_Capt_SpacingRemove:
                    return " 删除间距 ";
                case ReportStringId.RibbonXRDesign_FontBold_Caption:
                    return " 粗体 ";
                case ReportStringId.Msg_WarningUnsavedReports:
                    return " 打印警告：保存一下报表并预览最近改动的子报表 -{0}";
                case ReportStringId.UD_TTip_AlignTop:
                    return " 顶端对齐 ";
                case ReportStringId.UD_Hint_SpacingDecrease:
                    return " 减少选择的控件间距 ";
                case ReportStringId.Cmd_BottomMargin:
                    return " 下边距 ";
                case ReportStringId.CatLayout:
                    return " 布局 ";
                case ReportStringId.UD_Hint_AlignLefts:
                    return " 将选择的控件左对齐 ";
                case ReportStringId.Msg_ShapeRotationToolTip:
                    return " 使用Ctrl和鼠标左键来旋转成型 ";
                case ReportStringId.RibbonXRDesign_Redo_STipContent:
                    return " 重做最后一步操作.";
                case ReportStringId.UD_Capt_NewWizardReport:
                    return " 报表导航...";
                case ReportStringId.RibbonXRDesign_HtmlBackward_STipContent:
                    return " 返回上页.";
                case ReportStringId.RibbonXRDesign_Windows_Caption:
                    return " 窗口 ";
                case ReportStringId.Msg_Caption:
                    return " XtraReports ";
                case ReportStringId.GroupSort_AddGroup:
                    return " 增加组 ";
                case ReportStringId.UD_Group_Window:
                    return " 窗口 ";
                case ReportStringId.RibbonXRDesign_CenterHorizontally_STipContent:
                    return " 水平居中一个带区内所选控件.";
                case ReportStringId.UD_Group_View:
                    return " 视图 ";
                case ReportStringId.UD_Group_Edit:
                    return " 编辑 ";
                case ReportStringId.UD_Group_File:
                    return " 文件 ";
                case ReportStringId.UD_Group_Font:
                    return " 字体 ";
                case ReportStringId.RibbonXRDesign_JustifyLeft_STipTitle:
                    return " 左对齐文本 ";
                case ReportStringId.Cmd_BandMoveUp:
                    return " 下移动 ";
                case ReportStringId.Cmd_TableInsertRowAbove:
                    return " 在上面插入一行 ";
                case ReportStringId.Cmd_TableInsertRowBelow:
                    return " 在下面插入一行 ";
                case ReportStringId.UD_Group_HorizontalSpacing:
                    return " 水平间距 ";
                case ReportStringId.BandDsg_QuantityPerPage:
                    return " 每页一个页眉 ";
                case ReportStringId.CatUserDesigner:
                    return " 用户设计器 ";
                case ReportStringId.Msg_InvalidCondition:
                    return " 该条件必须是布尔！";
                case ReportStringId.RibbonXRDesign_AlignTop_STipTitle:
                    return " 顶部对齐 ";
                case ReportStringId.UD_Group_Justify:
                    return " 文本对齐 ";
                case ReportStringId.RibbonXRDesign_PageGroup_SizeAndLayout:
                    return " 布局 ";
                case ReportStringId.PivotGridFrame_Layouts_SelectorCaption2:
                    return " 显示字段选择器 ";
                case ReportStringId.PivotGridFrame_Layouts_SelectorCaption1:
                    return " 隐藏字段选择器 ";
                case ReportStringId.SSForm_Msg_SelectedStylesText:
                    return " 选中的样式...";
                case ReportStringId.Cmd_PageHeader:
                    return " 页眉 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceDecrease_STipContent:
                    return " 减少所选控件之间的水平间距.";
                case ReportStringId.CatParameters:
                    return " 参数 ";
                case ReportStringId.STag_Name_Checked:
                    return " 选中 ";
                case ReportStringId.Cmd_ReportFooter:
                    return " 报表尾 ";
                case ReportStringId.UD_Title_FieldList_NonePickerNodeText:
                    return " 无 ";
                case ReportStringId.RibbonXRDesign_Paste_Caption:
                    return " 粘贴 ";
                case ReportStringId.RibbonXRDesign_CenterHorizontally_Caption:
                    return " 水平居中 ";
                case ReportStringId.BCForm_Lbl_Binding:
                    return " 绑定 ";
                case ReportStringId.Msg_CyclicBookmarks:
                    return " 报表中有循环标签 ";
                case ReportStringId.STag_Name_ColumnCount:
                    return " 列数 ";
                case ReportStringId.Cmd_InsertUnboundDetailReport:
                    return " 未绑定的 ";
                case ReportStringId.STag_Name_ColumnWidth:
                    return " 列宽 ";
                case ReportStringId.UD_Title_FieldList:
                    return " 字段列表 ";
                case ReportStringId.UD_TTip_TableDescription:
                    return " 拖放对象来创建一个表 ";
                case ReportStringId.RibbonXRDesign_Scripts_STipTitle:
                    return " 显示 / 隐藏脚本 ";
                case ReportStringId.UD_Hint_ZoomIn:
                    return " 放大设计界面 ";
                case ReportStringId.CatDesign:
                    return " 设计 ";
                case ReportStringId.UD_TTip_AlignRight:
                    return " 右对齐 ";
                case ReportStringId.XRSubreport_ReportSourceUrlInfo:
                    return " 报表资源URL：{0}";
                case ReportStringId.UD_TTip_SizeToControlWidth:
                    return " 宽度相等 ";
                case ReportStringId.PivotGridForm_ItemAppearances_Description:
                    return " 调整当前XRPivotGrid的打印外观.";
                case ReportStringId.Cmd_TableDelete:
                    return " 删除表 ";
                case ReportStringId.Verb_ReportWizard:
                    return " 报表导航...";
                case ReportStringId.UD_TTip_FormatForeColor:
                    return " 前景色 ";
                case ReportStringId.UD_ReportDesigner:
                    return " 报表设计器 ";
                case ReportStringId.Verb_EditGroupFields:
                    return " 编辑字段组...";
                case ReportStringId.RibbonXRDesign_JustifyCenter_STipContent:
                    return " 文本居中.";
                case ReportStringId.UD_Hint_JustifyCenter:
                    return " 控件内容居中对齐 ";
                case ReportStringId.Cmd_Paste:
                    return " 粘贴(&amp; P)";
                case ReportStringId.RibbonXRDesign_AlignTop_STipContent:
                    return " 顶部对齐所选控件 ";
                case ReportStringId.UD_Hint_Delete:
                    return " 删除控件 ";
                case ReportStringId.UD_TTip_CenterHorizontally:
                    return " 水平居中 ";
                case ReportStringId.Msg_WarningFontNameCantBeEmpty:
                    return " 字体名称不可为空。";
                case ReportStringId.RibbonXRDesign_VertSpaceConcatenate_Caption:
                    return " 删除垂直间距 ";
                case ReportStringId.PivotGridForm_ItemFields_Caption:
                    return " 字段 ";
                case ReportStringId.Cmd_PageFooter:
                    return " 页脚 ";
                case ReportStringId.RibbonXRDesign_FontUnderline_STipTitle:
                    return " 下划线 ";
                case ReportStringId.Msg_FileNotFound:
                    return " 文件找不到。";
                case ReportStringId.RibbonXRDesign_OpenFile_Caption:
                    return " 打开...";
                case ReportStringId.UD_TTip_AlignBottom:
                    return " 底端对齐 ";
                case ReportStringId.UD_Capt_FontUnderline:
                    return " 下划线 ";
                case ReportStringId.RibbonXRDesign_PageGroup_HtmlNavigation:
                    return " 导航 ";
                case ReportStringId.SSForm_Msg_MoreThanOneStyle:
                    return " 你已经选中了两种及两种以上的样式 ";
                case ReportStringId.PivotGridForm_GroupPrinting_Caption:
                    return " 打印 ";
                case ReportStringId.RibbonXRDesign_FontSize_STipContent:
                    return " 改变字体大小.";
                case ReportStringId.Msg_ScriptingPermissionErrorMessage:
                    return " 您没有权限来执行报表中的这段脚本.具体细节:{0}";
                case ReportStringId.RibbonXRDesign_SaveFileAs_Caption:
                    return " 另存为...";
                case ReportStringId.GroupSort_MoveDown:
                    return " 向下 ";
                case ReportStringId.STag_Name_DataBinding:
                    return " 数据绑定 ";
                case ReportStringId.UD_Hint_JustifyRight:
                    return " 控件内容右对齐 ";
                case ReportStringId.Verb_AddFieldToArea:
                    return " 在区域中增加字段 ";
                case ReportStringId.STag_Capt_Tasks:
                    return " 任务 ";
                case ReportStringId.Cmd_ReportHeader:
                    return " 报表头 ";
                case ReportStringId.RibbonXRDesign_AlignHorizontalCenters_STipTitle:
                    return " 中间对齐 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceIncrease_STipTitle:
                    return " 增加水平间距 ";
                case ReportStringId.RibbonXRDesign_HtmlRefresh_STipContent:
                    return " 刷新当前页.";
                case ReportStringId.Msg_SerializationErrorTitle:
                    return " 错误 ";
                case ReportStringId.CatAppearance:
                    return " 外观 ";
                case ReportStringId.UD_Hint_MakeSameSizeHeight:
                    return " 使选择的控件高度相等 ";
                case ReportStringId.Msg_ScriptCodeIsNotCorrect:
                    return " 您输入的代码不正确 ";
                case ReportStringId.STag_Capt_Format:
                    return "{0}{1}";
                case ReportStringId.Verb_LoadReportTemplate:
                    return " 加载报表模版 ";
                case ReportStringId.RibbonXRDesign_FontName_STipTitle:
                    return " 字体 ";
                case ReportStringId.Dlg_SaveFile_Title:
                    return " 保存'{0}' ";
                case ReportStringId.UD_Capt_LayoutToolbarName:
                    return " 布局设计工具栏 ";
                case ReportStringId.Cmd_Copy:
                    return " 复制(&amp; Y)";
                case ReportStringId.RibbonXRDesign_SizeToGrid_STipTitle:
                    return " 尺寸对齐到网格 ";
                case ReportStringId.RibbonXRDesign_HtmlForward_STipContent:
                    return " 移到下一页.";
                case ReportStringId.RibbonXRDesign_PageGroup_Report:
                    return " 报表 ";
                case ReportStringId.PivotGridFrame_Fields_ColumnsText:
                    return " XRPivotGrid字段 ";
                case ReportStringId.UD_TTip_EditCut:
                    return " 剪切 ";
                case ReportStringId.UD_Capt_Delete:
                    return " 删除 ";
                case ReportStringId.RibbonXRDesign_PageGroup_View:
                    return "视图";
                case ReportStringId.RibbonXRDesign_PageGroup_Zoom:
                    return "缩放";
                case ReportStringId.RibbonXRDesign_PageGroup_Font:
                    return "字体";
                case ReportStringId.RibbonXRDesign_PageGroup_Edit:
                    return "编辑";
                case ReportStringId.UD_Capt_JustifyRight:
                    return "右对齐";
                case ReportStringId.Verb_About:
                    return "关于...";
                case ReportStringId.Msg_InvalidExpression:
                    return "指定的表达式包含非法字符(行{0},列{1}).";
                case ReportStringId.Cmd_TableDeleteCell:
                    return "删除单元格";
                case ReportStringId.RibbonXRDesign_AlignBottom_STipTitle:
                    return "底部对齐";
                case ReportStringId.UD_Capt_AlignToGrid:
                    return " 对齐网格 ";
                case ReportStringId.RibbonXRDesign_SizeToGrid_Caption:
                    return " 尺寸对齐到网格 ";
                case ReportStringId.Cmd_ViewCode:
                    return " 查看代码 ";
                case ReportStringId.Verb_EditBands:
                    return " 编辑带区...";
                case ReportStringId.UD_TTip_HorizSpaceConcatenate:
                    return " 删除水平间距 ";
                case ReportStringId.BandDsg_QuantityPerReport:
                    return "每个报表一个报表头 ";
                case ReportStringId.Msg_ReportImporting:
                    return "正在载如报表布局.请等待...";
                case ReportStringId.RibbonXRDesign_Redo_STipTitle:
                    return "重做";
                case ReportStringId.Wizard_PageChooseFields_Msg:
                    return " 若要继续，你必须为报表选择字段。";
                case ReportStringId.SR_Number_Down:
                    return " 数字下降 ";
                case ReportStringId.NewParameterEditorForm_DisplayMember:
                    return " 显示成员 ";
                case ReportStringId.RibbonXRDesign_BackColor_STipTitle:
                    return " 背景色 ";
                case ReportStringId.UD_Hint_AlignMiddles:
                    return " 将选择的控件水平居中对齐 ";
                case ReportStringId.RibbonXRDesign_SaveFile_STipTitle:
                    return " 保存报表 ";
                case ReportStringId.RibbonXRDesign_FontItalic_Caption:
                    return " 斜体 ";
                case ReportStringId.RibbonXRDesign_CenterVertically_STipContent:
                    return " 垂直居中一个带区内所选控件.";
                case ReportStringId.RibbonXRDesign_HorizSpaceMakeEqual_STipContent:
                    return " 将所选的控件设置为相同的水平间距.";
                case ReportStringId.UD_TTip_SizeToControl:
                    return " 尺寸大小相等 ";
                case ReportStringId.UD_Group_Format:
                    return " 格式化 ";
                case ReportStringId.RibbonXRDesign_PageGroup_Alignment:
                    return " 对齐方式 ";
                case ReportStringId.RibbonXRDesign_OpenFile_STipTitle:
                    return " 打开报表 ";
                case ReportStringId.UD_Capt_CenterInFormVertically:
                    return " 垂直方向 ";
                case ReportStringId.RibbonXRDesign_FontBold_STipContent:
                    return " 使所选文字为粗体.";
                case ReportStringId.RibbonXRDesign_SizeToControl_STipTitle:
                    return " 置为相同大小 ";
                case ReportStringId.UD_TTip_VertSpaceMakeEqual:
                    return " 垂直间距相等 ";
                case ReportStringId.UD_Capt_ZoomIn:
                    return " 放大 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceIncrease_Caption:
                    return " 增加水平间距 ";
                case ReportStringId.UD_Hint_SaveFile:
                    return " 保存报表 ";
                case ReportStringId.PanelDesignMsg:
                    return " 请在这里放置控件，使他们集中在一起 ";
                case ReportStringId.RibbonXRDesign_HtmlForward_STipTitle:
                    return " 下页 ";
                case ReportStringId.Cmd_TableInsert:
                    return " 插入 ";
                case ReportStringId.UD_Capt_AlignRights:
                    return " 右对齐 ";
                case ReportStringId.ScriptEditor_ErrorLine:
                    return " 行 ";
                case ReportStringId.UD_TTip_SizeToGrid:
                    return " 均匀排列 ";
                case ReportStringId.Cmd_InsertDetailReport:
                    return " 插入详细的报表 ";
                case ReportStringId.UD_Capt_StatusBarName:
                    return " 状态条 ";
                case ReportStringId.RibbonXRDesign_ZoomOut_Caption:
                    return " 缩小 ";
                case ReportStringId.UD_TTip_AlignVerticalCenters:
                    return " 中间对齐 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceIncrease_STipContent:
                    return " 增加所选控件之间的水平间距.";
                case ReportStringId.RibbonXRDesign_SizeToControlWidth_STipContent:
                    return " 将所选控件置为相同宽度.";
                case ReportStringId.XRSubreport_NameInfo:
                    return " 名称:{0}";
                case ReportStringId.RibbonXRDesign_HtmlRefresh_Caption:
                    return " 刷新 ";
                case ReportStringId.UD_TTip_FormatJustify:
                    return " 两边对齐 ";
                case ReportStringId.GroupSort_Delete:
                    return " 删除 ";
                case ReportStringId.PivotGridFrame_Fields_DescriptionText2:
                    return " 选择并拖放字段到PivotGrid字段面板来创建PivotGrid字段.";
                case ReportStringId.PivotGridFrame_Fields_DescriptionText1:
                    return " 你可以添加和删除XRPivotGrid字段并修改其设置.";
                case ReportStringId.UD_Capt_Copy:
                    return " 复制 ";
                case ReportStringId.UD_Capt_Zoom:
                    return " 缩放 ";
                case ReportStringId.UD_Capt_Exit:
                    return " 退出 ";
                case ReportStringId.UD_Capt_Redo:
                    return " 重做 ";
                case ReportStringId.UD_Capt_Undo:
                    return " 撤销 ";
                case ReportStringId.RibbonXRDesign_HtmlFind_Caption:
                    return " 查找 ";
                case ReportStringId.UD_Hint_OrderBringToFront:
                    return " 使选择的控件置于顶层 ";
                case ReportStringId.Msg_WarningControlsAreOutOfMargin:
                    return " 打印通知:以下控件超出右页边距,这将导致额外的页面被打印 -{0}.";
                case ReportStringId.Verb_RTFLoad:
                    return " 载入文件...";
                case ReportStringId.UD_Hint_SelectAll:
                    return " 选择文档里面的所有控件 ";
                case ReportStringId.RibbonXRDesign_JustifyCenter_Caption:
                    return " 文本居中 ";
                case ReportStringId.RibbonXRDesign_VertSpaceConcatenate_STipContent:
                    return " 删除所选控件的垂直间距.";
                case ReportStringId.RibbonXRDesign_SizeToControlHeight_STipTitle:
                    return " 置为相同高度 ";
                case ReportStringId.RibbonXRDesign_SizeToControlHeight_STipContent:
                    return " 将所选控件置为相同高度.";
                case ReportStringId.RibbonXRDesign_AlignVerticalCenters_Caption:
                    return " 居中对齐 ";
                case ReportStringId.RibbonXRDesign_HtmlHome_STipContent:
                    return " 显示主页.";
                case ReportStringId.PivotGridForm_ItemLayout_Caption:
                    return " 布局 ";
                case ReportStringId.ScriptEditor_ScriptHasBeenChanged:
                    return " 错误与实际脚本无关,脚本已经在上传验证后改变，查看实际错误，请点击验证按钮 ";
                case ReportStringId.Cmd_Cut:
                    return " 剪切(&amp; T)";
                case ReportStringId.UD_Group_Align:
                    return " 对齐 ";
                case ReportStringId.UD_Group_Order:
                    return " 顺序 ";
                case ReportStringId.RibbonXRDesign_ZoomOut_STipContent:
                    return " 缩小尺寸以查看报表中的更多内容 ";
                case ReportStringId.UD_Capt_SpacingMakeEqual:
                    return " 间距相等 ";
                case ReportStringId.RibbonXRDesign_HorizSpaceConcatenate_STipContent:
                    return " 删除所选控件之间的水平间距.";
                case ReportStringId.RibbonXRDesign_Exit_STipTitle:
                    return " 退出 ";
                case ReportStringId.UD_Title_ErrorList:
                    return " 脚本错误 ";
                case ReportStringId.UD_Capt_FontItalic:
                    return " 斜线 ";
                case ReportStringId.CatPageSettings:
                    return " 页面设置 ";
                case ReportStringId.RibbonXRDesign_SizeToGrid_STipContent:
                    return " 将所选的控件的尺寸对齐到网格.";
                case ReportStringId.RibbonXRDesign_JustifyRight_Caption:
                    return " 右对齐文本 ";
                case ReportStringId.RibbonXRDesign_SendToBack_STipTitle:
                    return " 置于底部 ";
                case ReportStringId.RibbonXRDesign_NewReportWizard_STipTitle:
                    return " 使用向导建立新报表 ";
                case ReportStringId.UD_Group_DockPanelsList:
                    return " 窗口 ";
                case ReportStringId.Msg_InvalidMethodCall:
                    return " 对象的当前状态使得方法调用失败。";
                case ReportStringId.UD_FormCaption:
                    return " 报表设计者 ";
                case ReportStringId.XRSubreport_NullReportSourceInfo:
                    return " 空 ";
                case ReportStringId.Verb_SummaryWizard:
                    return " 汇总...";
                case ReportStringId.RibbonXRDesign_BringToFront_STipTitle:
                    return " 置于顶部 ";
                case ReportStringId.Msg_WarningRemoveParameters:
                    return " 该操作将删除所有参数.您是否继续 ?";
                case ReportStringId.UD_Capt_MdiTileHorizontal:
                    return " 横向平铺 ";
                case ReportStringId.UD_TTip_EditPaste:
                    return " 粘贴 ";
                case ReportStringId.UD_Hint_FontBold:
                    return " 使字体变粗 ";
                case ReportStringId.RibbonXRDesign_SendToBack_Caption:
                    return " 置于底部 ";
                case ReportStringId.RibbonXRDesign_SizeToControlWidth_Caption:
                    return " 置为相同宽度 ";
                case ReportStringId.UD_Group_TabButtonsList:
                    return " 标签按钮 ";
                case ReportStringId.UD_Capt_MakeSameSizeWidth:
                    return " 宽度 ";
                case ReportStringId.RibbonXRDesign_JustifyJustify_STipTitle:
                    return " 两端对齐 ";
                case ReportStringId.RibbonXRDesign_SizeToControl_STipContent:
                    return " 将所选控件置为相同大小.";
                case ReportStringId.UD_TTip_FormatCenter:
                    return " 居中 ";
                case ReportStringId.FRSForm_Msg_NoRuleSelected:
                    return " 尚未选中任何格式规则 ";
                case ReportStringId.UD_Capt_FontBold:
                    return " 加粗 ";
                case ReportStringId.GroupSort_MoveUp:
                    return " 向上 ";
                case ReportStringId.RibbonXRDesign_JustifyLeft_STipContent:
                    return " 左对齐文本.";
                case ReportStringId.UD_Capt_SpacingDecrease:
                    return " 减少间距 ";
                case ReportStringId.SR_Height:
                    return " 高度 ";
                case ReportStringId.RibbonXRDesign_Exit_Caption:
                    return " 退出 ";
                case ReportStringId.RibbonXRDesign_NewReport_STipTitle:
                    return " 新建空白报表 ";
                case ReportStringId.UD_Title_ToolBox:
                    return " 工具箱 ";
                case ReportStringId.UD_Capt_SaveFile:
                    return " 保存 ";
                case ReportStringId.RibbonXRDesign_ZoomOut_STipTitle:
                    return " 缩小 ";
                case ReportStringId.UD_Title_PropertyGrid:
                    return " 属性 ";
                case ReportStringId.SSForm_Msg_InvalidFileFormat:
                    return " 无效的文件格式 ";
                case ReportStringId.UD_SaveFileDialog_DialogFilter:
                    return " 报表文件(*{0})| *{1}| AllFiles(*.*) | *.*";
                case ReportStringId.RibbonXRDesign_SaveAll_STipTitle:
                    return " 保存全部报表 ";
                case ReportStringId.Msg_NotEnoughMemoryToPaint:
                    return " 没有足够的内存来绘制。缩放级别将重置。";
                case ReportStringId.RibbonXRDesign_AlignToGrid_Caption:
                    return " 对齐到网格 ";
                case ReportStringId.UD_Group_ToolbarsList:
                    return " 工具栏 ";
                case ReportStringId.UD_TTip_BringToFront:
                    return " 置于顶层 ";
                case ReportStringId.STag_Name_FieldArea:
                    return " 区域添加一个新域 ";
                case ReportStringId.Msg_IncorrectBandType:
                    return " 错误的带区类型 ";
                case ReportStringId.UD_Hint_SpacingRemove:
                    return " 删除选择的控件间距 ";
                case ReportStringId.STag_Name_ColumnLayout:
                    return " 多列布局 ";
                case ReportStringId.UD_Hint_SpacingMakeEqual:
                    return " 使选择的控件间距相等 ";
                case ReportStringId.Cmd_Commands:
                    return " 命令 ";
                case ReportStringId.RibbonXRDesign_ToolboxControlsPage:
                    return " 工具箱 ";
                case ReportStringId.UD_Title_ReportExplorer_Components:
                    return " 组件 ";
                case ReportStringId.UD_Title_ReportExplorer_NullControl:
                    return " 无 ";
                case ReportStringId.Msg_InvalidXrTocInstance:
                    return " 可以将添加到报表中没有更多的 XRTableOfContents 实例。";
                case ReportStringId.Msg_InvalidExpressionEx:
                    return " 指定的表达式是无效的。";
                case ReportStringId.Msg_NoCustomFunction:
                    return " 自定义函数 '{0}' 找不到。";
                case ReportStringId.Cmd_DeleteFormattingRule:
                    return " 删除 ";
                case ReportStringId.Cmd_DeleteStyle:
                    return " 删除 ";
                case ReportStringId.UD_Title_ReportExplorer_Styles:
                    return " 样式 ";
                case ReportStringId.RepTabCtl_NoReportStatus:
                    return " 没有 ";
                case ReportStringId.Cmd_CloneStyle:
                    return " 克隆风格 ";
                case ReportStringId.Cmd_CloneFormattingRule:
                    return " 克隆格式设置规则 ";
                case ReportStringId.ExpressionEditor_Description_FieldsPrefix:
                    return " 字段类型: ";
                case ReportStringId.ExpressionEditor_ItemInfo_Variables_CurrentRowIndex_Description:
                    return " 返回在数据源中当前数据行的索引值。注意该索引值是从0开始的。";
                case ReportStringId.ExpressionEditor_ConditionCaption:
                    return " 条件编辑器 ";
                case ReportStringId.ExpressionEditor_ItemInfo_Fields:
                    return " 字段 ";
                case ReportStringId.ExpressionEditor_ItemInfo_Variables_RowCount_Description:
                    return " 返回在数据源中的数据行的总数。";
                case ReportStringId.ExpressionEditor_ItemInfo_Variables:
                    return " 变量 ";
                case ReportStringId.ExpressionEditor_ExpressionCaption:
                    return " 表达式编辑器 ";
            }
            return base.GetLocalizedString(id);
        }
    }

    /// <summary>
    /// 网格汉化
    /// </summary>
    public class XtraGridLocalizerCHS : GridLocalizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public XtraGridLocalizerCHS()
        //{
        //    foreach (GridStringId gridStringId in Enum.GetValues(typeof(GridStringId)))
        //        AddString(gridStringId, "");
        //}

        public override string GetLocalizedString(GridStringId id)
        {
            switch (id)
            {
                case GridStringId.FileIsNotFoundError:
                    return "文件{0}找不到";
                case GridStringId.ColumnViewExceptionMessage:
                    return " 要修正当前值吗?";
                case GridStringId.CustomizationCaption:
                    return "自定义";
                case GridStringId.CustomizationColumns:
                    return "列";
                case GridStringId.CustomizationBands:
                    return "带宽";
                case GridStringId.PopupFilterAll:
                    return "(全部)";
                case GridStringId.PopupFilterCustom:
                    return "(自定义)";
                case GridStringId.PopupFilterBlanks:
                    return "(空白)";
                case GridStringId.PopupFilterNonBlanks:
                    return "(无空白)";
                case GridStringId.CustomFilterDialogFormCaption:
                    return "用户自定义自动过滤器";
                case GridStringId.CustomFilterDialogCaption:
                    return "显示符合下列条件的行:";
                case GridStringId.CustomFilterDialogRadioAnd:
                    return "于(&A)";
                case GridStringId.CustomFilterDialogRadioOr:
                    return "或(&O)";
                case GridStringId.CustomFilterDialogOkButton:
                    return "确定(&O)";
                case GridStringId.CustomFilterDialogClearFilter:
                    return "清除过滤器(&L)";
                case GridStringId.CustomFilterDialogCancelButton:
                    return "取消(&C)";
                case GridStringId.CustomFilterDialog2FieldCheck:
                    return "字段";
                case GridStringId.MenuFooterSum:
                    return "和";
                case GridStringId.MenuFooterMin:
                    return "最小值";
                case GridStringId.MenuFooterMax:
                    return "最大值";
                case GridStringId.MenuFooterCount:
                    return "计数";
                case GridStringId.MenuFooterAverage:
                    return "平均值";
                case GridStringId.MenuFooterNone:
                    return "无";
                case GridStringId.MenuFooterAddSummaryItem:
                    return "添加新的汇总";
                case GridStringId.MenuFooterClearSummaryItems:
                    return "清除汇总";
                case GridStringId.MenuFooterSumFormat:
                    return "和={0:#.##}";
                case GridStringId.MenuFooterMinFormat:
                    return "最小值={0}";
                case GridStringId.MenuFooterMaxFormat:
                    return "最大值={0}";
                case GridStringId.MenuFooterCountFormat:
                    return "{0}";
                case GridStringId.MenuFooterCountGroupFormat:
                    return "计数={0}";
                case GridStringId.MenuFooterAverageFormat:
                    return "平均={0:#.##}";
                case GridStringId.MenuFooterCustomFormat:
                    return "统计值={0}";
                case GridStringId.MenuColumnSortAscending:
                    return "升序排列";
                case GridStringId.MenuColumnSortDescending:
                    return "降序排列";
                case GridStringId.MenuColumnClearSorting:
                    return "清除排序设置";
                case GridStringId.MenuColumnGroup:
                    return "按此列分组";
                case GridStringId.MenuColumnRemoveColumn:
                    return "移除列";
                case GridStringId.MenuColumnFindFilterShow:
                    return "显示发现面板";
                case GridStringId.MenuColumnAutoFilterRowShow:
                    return "显示自动过滤行";
                case GridStringId.MenuColumnFilterMode:
                    return "过滤模式";
                case GridStringId.MenuColumnFilterModeValue:
                    return "值";
                case GridStringId.MenuColumnFilterModeDisplayText:
                    return "显示文本";
                case GridStringId.MenuGroupPanelShow:
                    return "显示分组依据框";
                case GridStringId.FilterPanelCustomizeButton:
                    return "自定义";
                case GridStringId.MenuColumnUnGroup:
                    return "不分组";
                case GridStringId.MenuColumnColumnCustomization:
                    return "列选择";
                case GridStringId.MenuColumnBestFit:
                    return "最佳匹配";
                case GridStringId.MenuColumnFilter:
                    return "允许筛选数据";
                case GridStringId.MenuColumnFilterEditor:
                    return "设定数据筛选条件";
                case GridStringId.MenuColumnClearFilter:
                    return "清除过滤器";
                case GridStringId.MenuColumnBestFitAllColumns:
                    return "最佳匹配(所有列)";
                case GridStringId.MenuGroupPanelFullExpand:
                    return "全部展开";
                case GridStringId.MenuGroupPanelFullCollapse:
                    return "全部收合";
                case GridStringId.MenuGroupPanelClearGrouping:
                    return "清除分组";
                case GridStringId.PrintDesignerBandedView:
                    return "打印设置 (Banded View)";
                case GridStringId.PrintDesignerGridView:
                    return "打印设置(网格视图)";
                case GridStringId.PrintDesignerCardView:
                    return "打印设置(卡视图)";
                case GridStringId.PrintDesignerBandHeader:
                    return "起始带宽";
                case GridStringId.PrintDesignerDescription:
                    return "为当前视图设置不同的打印选项";
                case GridStringId.MenuColumnGroupBox:
                    return "分组依据框";
                case GridStringId.CardViewNewCard:
                    return "新建卡";
                case GridStringId.CardViewQuickCustomizationButton:
                    return "自定义";
                case GridStringId.CardViewQuickCustomizationButtonFilter:
                    return "过滤器　";
                case GridStringId.CardViewQuickCustomizationButtonSort:
                    return "排序方式:";
                case GridStringId.GridGroupPanelText:
                    return "拖动列标题至此,根据该列分组";
                case GridStringId.GridNewRowText:
                    return "在此处添加一行";
                case GridStringId.FilterBuilderOkButton:
                    return "确定(&O)";
                case GridStringId.FilterBuilderCancelButton:
                    return "取消(&C)";
                case GridStringId.FilterBuilderApplyButton:
                    return "应用(&A)";
                case GridStringId.FilterBuilderCaption:
                    return "数据筛选条件设定：";
                case GridStringId.GridOutlookIntervals:
                    return "更早;上个月;三周之前;两周之前;上周;;;;;;;;昨天;今天;明天;;;;;;;;下周;两周后;三周后;下个月;一个月之后;";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }

    public class XtraEditorLocalizerCHS : DevExpress.XtraEditors.Controls.Localizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public XtraEditorLocalizerCHS()
        //{
        //    foreach (StringId stringId in Enum.GetValues(typeof(StringId)))
        //        AddString(stringId, "");
        //}

        public override string GetLocalizedString(StringId id)
        {
            switch (id)
            {
                case StringId.PictureEditOpenFileFilter:
                    return ";*.ico;*.位图文件 (*.bmp)|*.bmp|GIF文件 (*.gif)|*.gif|JPEG文件 (*.jpg;*.jpeg)|*.jpg;*.jpeg|Icon 文件 (*.ico)|*.ico|所有图像文件 |*.bmp;*.gif;*.jpg;*.jpeg;*.ico;*.png;*.tif|所有文件 |*.*";
                case StringId.NavigatorNextButtonHint:
                    return "下一个";
                case StringId.ImagePopupPicture:
                    return "(图像)";
                case StringId.TabHeaderButtonNext:
                    return "向右滚动";
                case StringId.TabHeaderButtonPrev:
                    return "向左滚动";
                case StringId.XtraMessageBoxOkButtonText:
                    return "确定(&O)";
                case StringId.Cancel:
                    return "取消(&C)";
                case StringId.DateEditToday:
                    return "今天";
                case StringId.DateEditClear:
                    return "清除";
                case StringId.PictureEditMenuCut:
                    return "剪切";
                case StringId.NavigatorEditButtonHint:
                    return "编辑";
                case StringId.TextEditMenuCut:
                    return "剪切(&t)";
                case StringId.ImagePopupEmpty:
                    return "(空)";
                case StringId.NavigatorNextPageButtonHint:
                    return "下一页";
                case StringId.NavigatorTextStringFormat:
                    return "记录 {0} of {1}";
                case StringId.CaptionError:
                    return "错误";
                case StringId.XtraMessageBoxNoButtonText:
                    return "否(&N)";
                case StringId.PictureEditOpenFileTitle:
                    return "打开";
                case StringId.PictureEditOpenFileError:
                    return "错误的图像格式";
                case StringId.XtraMessageBoxIgnoreButtonText:
                    return "忽略(&I)";
                case StringId.NavigatorRemoveButtonHint:
                    return "删除";
                case StringId.TabHeaderButtonClose:
                    return "关闭";
                case StringId.CheckUnchecked:
                    return "非校验";
                case StringId.PictureEditSaveFileFilter:
                    return "位图文件 (*.bmp)|*.bmp|GIF文件 (*.gif)|*.gif|JPEG 文件 (*.jpg)|*.jpg";
                case StringId.TextEditMenuSelectAll:
                    return "全选(&A)";
                case StringId.PictureEditSaveFileTitle:
                    return "另存为";
                case StringId.DataEmpty:
                    return "没有图像数据";
                case StringId.XtraMessageBoxAbortButtonText:
                    return "中断(&A)";
                case StringId.CheckIndeterminate:
                    return "不确定";
                case StringId.NavigatorLastButtonHint:
                    return "最后一个";
                case StringId.TextEditMenuCopy:
                    return "复制(&C)";
                case StringId.TextEditMenuUndo:
                    return "撤销(&U)";
                case StringId.CalcError:
                    return "计算错误";
                case StringId.CalcButtonBack:
                    return "后退";
                case StringId.CalcButtonSqrt:
                    return "平方根";
                case StringId.LookUpColumnDefaultName:
                    return "名称";
                case StringId.NavigatorEndEditButtonHint:
                    return "结束编辑";
                case StringId.NotValidArrayLength:
                    return "无效的数组长度。";
                case StringId.ColorTabWeb:
                    return "网页";
                case StringId.PictureEditMenuSave:
                    return "保存";
                case StringId.PictureEditMenuCopy:
                    return "复制";
                case StringId.PictureEditMenuLoad:
                    return "调用";
                case StringId.NavigatorFirstButtonHint:
                    return "第一个";
                case StringId.MaskBoxValidateError:
                    return @"输入值不完整,是否修正?

    是 - 返回编辑器,修正该值.
    否 -保留该值.
    取消 - 重设为原来的值.";
                case StringId.UnknownPictureFormat:
                    return "未知的图形格式";
                case StringId.NavigatorPreviousPageButtonHint:
                    return "前一页";
                case StringId.XtraMessageBoxRetryButtonText:
                    return "重试(&R)";
                case StringId.LookUpEditValueIsNull:
                    return "[编辑值为空]";
                case StringId.CalcButtonC:
                    return "C";
                case StringId.XtraMessageBoxCancelButtonText:
                    return "取消(&C)l";
                case StringId.LookUpInvalidEditValueType:
                    return "无效的 LookUpEdit 编辑值类型。";
                case StringId.NavigatorAppendButtonHint:
                    return "追加";
                case StringId.CalcButtonMx:
                    return "M+";
                case StringId.CalcButtonMC:
                    return "MC";
                case StringId.CalcButtonMS:
                    return "MS";
                case StringId.CalcButtonMR:
                    return "MR";
                case StringId.CalcButtonCE:
                    return "CE";
                case StringId.NavigatorCancelEditButtonHint:
                    return "取消编辑";
                case StringId.PictureEditOpenFileErrorCaption:
                    return "打开错误";
                case StringId.OK:
                    return "确定(&O)";
                case StringId.CheckChecked:
                    return "校验";
                case StringId.TextEditMenuPaste:
                    return "粘贴(&P)";
                case StringId.TextEditMenuDelete:
                    return "删除(&D)";
                case StringId.ColorTabSystem:
                    return "系统";
                case StringId.PictureEditMenuPaste:
                    return "粘贴";
                case StringId.XtraMessageBoxYesButtonText:
                    return "是(&Y)";
                case StringId.InvalidValueText:
                    return "无效值";
                case StringId.PictureEditMenuDelete:
                    return "删除";
                case StringId.NavigatorPreviousButtonHint:
                    return "前一个";
                case StringId.ColorTabCustom:
                    return "自定义";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }

    public class XtraBarsLocalizerCHS : BarLocalizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public XtraBarsLocalizerCHS()
        //{
        //    foreach (BarString barString in Enum.GetValues(typeof(BarString)))
        //        AddString(barString, "");
        //}

        public override string GetLocalizedString(BarString id)
        {
            switch (id)
            {
                case BarString.AddOrRemove:
                    return "添加或删除按钮(&A)";
                case BarString.ResetBar:
                    return "确定要对 '{0}' 工具栏所做的改动进行重置吗？";
                case BarString.ResetBarCaption:
                    return "自定义";
                case BarString.ResetButton:
                    return "重设工具栏(&R)";
                case BarString.CustomizeButton:
                    return "自定义...(&C)";
                case BarString.ToolBarMenu:
                    return "重新设定(&R)$刪除(&D)$!重新命名(&N)$!默认格式(&L)$全文字模式(&T)$文字菜单(&O)$图片及文字(&A)$!启用组(&G)$可见的(&V)$最近使用的(&M)";
                case BarString.NewToolbarName:
                    return "工具";
                case BarString.NewMenuName:
                    return "主菜单";
                case BarString.NewStatusBarName:
                    return "状态栏";
                case BarString.NewToolbarCustomNameFormat:
                    return "自定义{0}";
                case BarString.NewToolbarCaption:
                    return "新建工具栏";
                case BarString.RenameToolbarCaption:
                    return "重命名工具栏";
                case BarString.CustomizeWindowCaption:
                    return "自定义";
                case BarString.MenuAnimationSystem:
                    return "(系统默认值)";
                case BarString.MenuAnimationNone:
                    return "无";
                case BarString.MenuAnimationSlide:
                    return "片";
                case BarString.MenuAnimationFade:
                    return "减弱";
                case BarString.MenuAnimationUnfold:
                    return "展开";
                case BarString.MenuAnimationRandom:
                    return "随机";
                case BarString.PopupMenuEditor:
                    return "弹出菜单编辑器";
                case BarString.ToolbarNameCaption:
                    return "工具栏名称(&T)";
                case BarString.RibbonToolbarBelow:
                    return "将快速访问工具栏显示在功能区下方(&S)";
                case BarString.RibbonToolbarAbove:
                    return "将快速访问工具栏显示在功能区上方(&S)";
                case BarString.RibbonToolbarRemove:
                    return "移除快速访问工具栏(&R)";
                case BarString.RibbonToolbarAdd:
                    return "添加快速访问工具栏(&A)";
                case BarString.RibbonToolbarMinimizeRibbon:
                    return "最小化功能区(&N)";
                case BarString.RibbonGalleryFilter:
                    return "所有组";
                case BarString.RibbonGalleryFilterNone:
                    return "无";
                case BarString.BarUnassignedItems:
                    return "(未设定项)";
                case BarString.BarAllItems:
                    return "(所有项)";
                case BarString.RibbonUnassignedPages:
                    return "(未设定页)";
                case BarString.RibbonAllPages:
                    return "(所有页)";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }

    public class XtraTreeListLocalizerCHS : TreeListLocalizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public XtraTreeListLocalizerCHS()
        //{
        //    foreach (TreeListStringId treeListStringId in Enum.GetValues(typeof(TreeListStringId)))
        //        AddString(treeListStringId, "");
        //}

        public override string GetLocalizedString(TreeListStringId id)
        {
            switch (id)
            {
                case TreeListStringId.MenuColumnBestFit:
                    return "最佳匹配";
                case TreeListStringId.PrintDesignerHeader:
                    return "打印设置";
                case TreeListStringId.ColumnCustomizationText:
                    return "自定义";
                case TreeListStringId.MenuFooterMin:
                    return "最小值";
                case TreeListStringId.MenuFooterMax:
                    return "最大值";
                case TreeListStringId.MenuFooterSum:
                    return "和";
                case TreeListStringId.MenuFooterAllNodes:
                    return "所有节点";
                case TreeListStringId.MenuFooterCount:
                    return "计数";
                case TreeListStringId.MenuColumnSortAscending:
                    return "升序排列";
                case TreeListStringId.MenuFooterNone:
                    return "无";
                case TreeListStringId.MenuColumnSortDescending:
                    return "降序排列";
                case TreeListStringId.PrintDesignerDescription:
                    return "为当前的树状列表设置不同的打印选项";
                case TreeListStringId.MenuColumnBestFitAllColumns:
                    return "最佳匹配 (所有列)";
                case TreeListStringId.MenuFooterAverageFormat:
                    return "平均值={0:#.##}";
                case TreeListStringId.ColumnNamePrefix:
                    return "列";
                case TreeListStringId.MenuFooterMinFormat:
                    return "最小值={0}";
                case TreeListStringId.MenuFooterCountFormat:
                    return "{0}";
                case TreeListStringId.MenuColumnColumnCustomization:
                    return "列选择";
                case TreeListStringId.MenuFooterMaxFormat:
                    return "最大值={0}";
                case TreeListStringId.MenuFooterSumFormat:
                    return "和={0:#.##}";
                case TreeListStringId.MultiSelectMethodNotSupported:
                    return "OptionsBehavior.MultiSelect未激活时，指定方法不能工作.";
                case TreeListStringId.InvalidNodeExceptionText:
                    return " 要修正当前值吗?";
                case TreeListStringId.MenuFooterAverage:
                    return "平均值";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }

    public class XtraVerticalGridLocalizerCHS : VGridLocalizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public XtraVerticalGridLocalizerCHS()
        //{
        //    foreach (VGridStringId vgridStringId in Enum.GetValues(typeof(VGridStringId)))
        //        AddString(vgridStringId, "");
        //}

        public override string GetLocalizedString(VGridStringId id)
        {
            switch (id)
            {
                case VGridStringId.RowCustomizationText:
                    return "定制";
                case VGridStringId.RowCustomizationNewCategoryFormText:
                    return "新增数据类别";
                case VGridStringId.RowCustomizationNewCategoryFormLabelText:
                    return "标题：";
                case VGridStringId.RowCustomizationNewCategoryText:
                    return "新增";
                case VGridStringId.RowCustomizationDeleteCategoryText:
                    return "删除";
                case VGridStringId.InvalidRecordExceptionText:
                    return "是否要修改不正确的数据值？";
                case VGridStringId.RowCustomizationTabPageCategoriesText:
                    return "分类数据";
                case VGridStringId.RowCustomizationTabPageRowsText:
                    return "数据列";
                case VGridStringId.StyleCreatorName:
                    return "风格定制器";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }

    public class XtraLayoutLocalizerCHS : LayoutLocalizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public XtraLayoutLocalizerCHS()
        //{
        //    foreach (LayoutStringId layoutStringId in Enum.GetValues(typeof(LayoutStringId)))
        //        AddString(layoutStringId, "");
        //}

        public override string GetLocalizedString(LayoutStringId id)
        {
            switch (id)
            {
                case LayoutStringId.DefaultItemText:
                    return "项目";
                case LayoutStringId.DefaultActionText:
                    return "默认动作";
                case LayoutStringId.DefaultEmptyText:
                    return "无";
                case LayoutStringId.LayoutItemDescription:
                    return "版面设计控制器的项目元素";
                case LayoutStringId.LayoutGroupDescription:
                    return "版面设计控制器的群组元素";
                case LayoutStringId.TabbedGroupDescription:
                    return "版面控制器的群组标签页元素";
                case LayoutStringId.LayoutControlDescription:
                    return "版面控制";
                case LayoutStringId.CustomizationFormTitle:
                    return "定制";
                case LayoutStringId.TreeViewPageTitle:
                    return "版面设计树状视图";
                case LayoutStringId.HiddenItemsPageTitle:
                    return "隐藏项目";
                case LayoutStringId.RenameSelected:
                    return "重命名";
                case LayoutStringId.HideItemMenutext:
                    return "隐藏项目";
                case LayoutStringId.LockItemSizeMenuText:
                    return "锁定项目大小";
                case LayoutStringId.UnLockItemSizeMenuText:
                    return "解除项目大小锁定";
                case LayoutStringId.GroupItemsMenuText:
                    return "群组";
                case LayoutStringId.UnGroupItemsMenuText:
                    return "解除群组设定";
                case LayoutStringId.CreateTabbedGroupMenuText:
                    return "创建群组标签页";
                case LayoutStringId.AddTabMenuText:
                    return "增加标签页";
                case LayoutStringId.UnGroupTabbedGroupMenuText:
                    return "解除群组标签页设定";
                case LayoutStringId.TreeViewRootNodeName:
                    return "最上层";
                case LayoutStringId.ShowCustomizationFormMenuText:
                    return "定制版面";
                case LayoutStringId.HideCustomizationFormMenuText:
                    return "隐藏定制表格";
                case LayoutStringId.EmptySpaceItemDefaultText:
                    return "空白区域项目";
                case LayoutStringId.SplitterItemDefaultText:
                    return "分隔器版面設計控制器的群組標籤頁項目";
                case LayoutStringId.ControlGroupDefaultText:
                    return "群组";
                case LayoutStringId.EmptyRootGroupText:
                    return "在这里放置控件";
                case LayoutStringId.EmptyTabbedGroupText:
                    return "将群组拖放到群组标签页区域";
                case LayoutStringId.ResetLayoutMenuText:
                    return "重设版面";
                case LayoutStringId.RenameMenuText:
                    return "重命名";
                case LayoutStringId.TextPositionMenuText:
                    return "文本位置";
                case LayoutStringId.TextPositionLeftMenuText:
                    return "左边";
                case LayoutStringId.TextPositionRightMenuText:
                    return "右边";
                case LayoutStringId.TextPositionTopMenuText:
                    return "上方";
                case LayoutStringId.TextPositionBottomMenuText:
                    return "下方";
                case LayoutStringId.ShowTextMenuItem:
                    return "显示文本";
                case LayoutStringId.HideTextMenuItem:
                    return "隐藏文本";
                case LayoutStringId.LockSizeMenuItem:
                    return "锁定大小";
                case LayoutStringId.LockWidthMenuItem:
                    return "锁定宽度";
                case LayoutStringId.CreateEmptySpaceItem:
                    return "创建空白区域项目";
                case LayoutStringId.LockHeightMenuItem:
                    return "锁定高度";
                case LayoutStringId.LockMenuGroup:
                    return "强制限定大小";
                case LayoutStringId.FreeSizingMenuItem:
                    return "允许改变大小";
                case LayoutStringId.ResetConstraintsToDefaultsMenuItem:
                    return "重设为默认值";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }

    public class NavBarLocalizerCHS : NavBarLocalizer
    {
        public override string Language => CultureInfo.CurrentUICulture.Name;

        //public NavBarLocalizerCHS()
        //{
        //    foreach (NavBarStringId navBarStringId in Enum.GetValues(typeof(NavBarStringId)))
        //        AddString(navBarStringId, "");
        //}

        public override string GetLocalizedString(NavBarStringId id)
        {
            switch (id)
            {
                case NavBarStringId.NavPaneMenuAddRemoveButtons:
                    return "添加或删除按钮（&A）";
                case NavBarStringId.NavPaneMenuShowMoreButtons:
                    return "显示更多按钮（&M）";
                case NavBarStringId.NavPaneChevronHint:
                    return "配置按钮";
                case NavBarStringId.NavPaneMenuShowFewerButtons:
                    return "显示少量按钮（&F）";
            }
            Debug.WriteLine(id.ToString() + "的默认值(" + this.GetType().ToString() + ")=" + base.GetLocalizedString(id));
            return base.GetLocalizedString(id);
        }
    }
}