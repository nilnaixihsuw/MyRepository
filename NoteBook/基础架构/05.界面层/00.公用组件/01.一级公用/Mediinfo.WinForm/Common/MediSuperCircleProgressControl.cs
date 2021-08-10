using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Design;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    /// <summary>
    /// 多功能加载控件(包含百分数和描述文字)
    /// </summary>
    [ToolboxItem(true)]
    public class CircleProgressControl : BaseStyleControl, ITransparentBackgroundManager, ISupportLookAndFeel
    {
        private const string defCaption = "请稍后";
        private const string defDescription = "医院目录正在上传中 ...";

        private DevExpress.Utils.Animation.DotWaitingIndicatorProperties dotWaitingIndicatorProperties;
        private DevExpress.Utils.Animation.LineWaitingIndicatorProperties lineWaitingIndicatorProperties;
        private DevExpress.Utils.Animation.RingWaitingIndicatorProperties ringWaitingIndicatorProperties;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CircleProgressControl()
        {
            this.appearanceCaptionCore = CreateAppearanceCaption();
            this.appearanceDescriptionCore = CreateAppearanceDescription();
            DoubleBuffered = true;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            waitingAnimatorTypeCore = DevExpress.Utils.Animation.WaitingAnimatorType.Default;
            InitProperties();
            LookAndFeel.StyleChanged += LookAndFeel_StyleChanged;
            AnimationHelper = new LoadingAnimationHelper(this);
            AppearanceCaption.Changed += OnAppearanceCaptionChanged;
            AppearanceDescription.Changed += OnAppearanceDescriptionChanged;
            Appearance.Changed += OnAppearanceChanged;
        }

        protected void InitProperties()
        {
            dotWaitingIndicatorProperties = new DevExpress.Utils.Animation.DotWaitingIndicatorProperties();
            dotWaitingIndicatorProperties.AppearanceCaption.Assign(AppearanceCaption);
            dotWaitingIndicatorProperties.AppearanceDescription.Assign(AppearanceDescription);
            dotWaitingIndicatorProperties.Appearance.Assign(Appearance);
            dotWaitingIndicatorProperties.AllowBackground = false;
            dotWaitingIndicatorProperties.Caption = defCaption;
            dotWaitingIndicatorProperties.Description = defDescription;
            lineWaitingIndicatorProperties = new DevExpress.Utils.Animation.LineWaitingIndicatorProperties();
            lineWaitingIndicatorProperties.EnsureParentProperties(dotWaitingIndicatorProperties);
            lineWaitingIndicatorProperties.Changed += OnWaitingIndicatorPropertiesChanged;
            ringWaitingIndicatorProperties = new DevExpress.Utils.Animation.RingWaitingIndicatorProperties();
            ringWaitingIndicatorProperties.EnsureParentProperties(dotWaitingIndicatorProperties);
            ringWaitingIndicatorProperties.Changed += OnWaitingIndicatorPropertiesChanged;
        }

        private void OnAppearanceChanged(object sender, EventArgs e)
        {
            dotWaitingIndicatorProperties.Appearance.AssignInternal(Appearance);
            AnimationHelper.SetAnimator();
        }

        private void OnAppearanceDescriptionChanged(object sender, EventArgs e)
        {
            if (ViewInfo != null)
                ViewInfo.UpdateStyle();
            dotWaitingIndicatorProperties.AppearanceDescription.Assign(AppearanceDescription);
        }

        private void OnAppearanceCaptionChanged(object sender, EventArgs e)
        {
            if (ViewInfo != null)
                ViewInfo.UpdateStyle();
            dotWaitingIndicatorProperties.AppearanceCaption.Assign(AppearanceCaption);
        }

        private void OnWaitingIndicatorPropertiesChanged(object sender, EventArgs e)
        {
            var args = e as PropertyChangedEventArgs;
            bool updateAnimator = false;
            if (args != null)
                updateAnimator = args.PropertyName == "AnimationElementCount" || args.PropertyName == "FrameCount" ||
                    args.PropertyName == "FrameInterval" || args.PropertyName == "AnimationSpeed" || args.PropertyName == "AnimationElementImage" || args.PropertyName == "AnimationAcceleration";
            if (updateAnimator)
                AnimationHelper.SetAnimator();
            Invalidate();
            Update();
        }

        private AppearanceDefault defaultAppearanceCaptionCore;

        internal AppearanceDefault DefaultAppearanceCaption
        {
            get
            {
                if (defaultAppearanceCaptionCore == null)
                    defaultAppearanceCaptionCore = new AppearanceDefault(Color.Empty, Color.Empty, CaptionDefaultFont);
                return defaultAppearanceCaptionCore;
            }
        }

        private AppearanceDefault defaultAppearanceDescriptionCore;

        internal AppearanceDefault DefaultAppearanceDescription
        {
            get
            {
                if (defaultAppearanceDescriptionCore == null)
                    defaultAppearanceDescriptionCore = new AppearanceDefault(Color.Empty, Color.Empty, DescriptionDefaultFont);
                return defaultAppearanceDescriptionCore;
            }
        }

        protected virtual AppearanceObject CreateAppearanceCaption()
        {
            return CreateAppearanceCore();
        }

        protected virtual AppearanceObject CreateAppearanceDescription()
        {
            return CreateAppearanceCore();
        }

        private AppearanceObject CreateAppearanceCore()
        {
            AppearanceObject res = base.CreateAppearance();
            return res;
        }

        internal LoadingAnimationHelper AnimationHelper { get; set; }

        protected override BaseControlPainter CreatePainter()
        {
            return new CircleProgressControlPainter();
        }

        protected override BaseStyleControlViewInfo CreateViewInfo()
        {
            return new CircleProgressControlViewInfo(this);
        }

        protected override Size DefaultSize { get { return new Size(246, 66); } }
        private Font captionDefaultFontCore;

        protected virtual Font CaptionDefaultFont
        {
            get
            {
                if (captionDefaultFontCore == null)
                    captionDefaultFontCore = new Font("Microsoft Sans Serif", 12F);
                return captionDefaultFontCore;
            }
        }

        private Font descriptionDefaultFontCore;

        protected virtual Font DescriptionDefaultFont
        {
            get
            {
                if (descriptionDefaultFontCore == null)
                    descriptionDefaultFontCore = new Font("Microsoft Sans Serif", 8.25F);
                return descriptionDefaultFontCore;
            }
        }

        private DevExpress.Utils.Animation.WaitingAnimatorType waitingAnimatorTypeCore;

        [DefaultValue(DevExpress.Utils.Animation.WaitingAnimatorType.Default), Category("Behavior")]
        public DevExpress.Utils.Animation.WaitingAnimatorType WaitAnimationType
        {
            get { return waitingAnimatorTypeCore; }
            set
            {
                waitingAnimatorTypeCore = value;
                AnimationHelper.SetAnimator();
                Invalidate();
            }
        }

        internal DevExpress.Utils.Animation.IWaitingIndicatorProperties WaitingIndicatorProperties
        {
            get
            {
                if (WaitAnimationType == DevExpress.Utils.Animation.WaitingAnimatorType.Line)
                    return lineWaitingIndicatorProperties;
                if (WaitAnimationType == DevExpress.Utils.Animation.WaitingAnimatorType.Ring)
                    return ringWaitingIndicatorProperties;
                return null;
            }
        }

        internal DevExpress.Utils.Animation.DotWaitingIndicatorProperties GetDotWaitingindicatorProperties()
        {
            return dotWaitingIndicatorProperties;
        }

        [DefaultValue((float)7.0), Category("Behavior")]
        public float AnimationAcceleration
        {
            get { return dotWaitingIndicatorProperties.AnimationAcceleration; }
            set { dotWaitingIndicatorProperties.AnimationAcceleration = value; }
        }

        [DefaultValue((float)5.5), Category("Behavior")]
        public float AnimationSpeed
        {
            get { return dotWaitingIndicatorProperties.AnimationSpeed; }
            set { dotWaitingIndicatorProperties.AnimationSpeed = value; }
        }

        [DefaultValue(38000), Category("Behavior")]
        public int FrameCount
        {
            get { return dotWaitingIndicatorProperties.FrameCount; }
            set { dotWaitingIndicatorProperties.FrameCount = value; }
        }

        [DefaultValue(1000), Category("Behavior")]
        public int FrameInterval
        {
            get { return dotWaitingIndicatorProperties.FrameInterval; }
            set { dotWaitingIndicatorProperties.FrameInterval = value; }
        }

        [DefaultValue(40), Category("Behavior")]
        public int RingAnimationDiameter
        {
            get { return ringWaitingIndicatorProperties.RingAnimationDiameter; }
            set { ringWaitingIndicatorProperties.RingAnimationDiameter = value; }
        }

        [DefaultValue(null), Category("Behavior")]
        public Image AnimationElementImage
        {
            get { return dotWaitingIndicatorProperties.AnimationElementImage; }
            set { dotWaitingIndicatorProperties.AnimationElementImage = value; }
        }

        [DefaultValue(10), Category("Behavior")]
        public int LineAnimationElementHeight
        {
            get { return lineWaitingIndicatorProperties.LineAnimationElementHeight; }
            set { lineWaitingIndicatorProperties.LineAnimationElementHeight = value; }
        }

        [DefaultValue(DevExpress.Utils.Animation.LineAnimationElementType.Circle), Category("Behavior")]
        public DevExpress.Utils.Animation.LineAnimationElementType LineAnimationElementType
        {
            get { return lineWaitingIndicatorProperties.LineAnimationElementType; }
            set { lineWaitingIndicatorProperties.LineAnimationElementType = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        [Category("Display Options"), DefaultValue(defCaption), Localizable(true), SmartTagProperty("Caption", "", SmartTagActionType.RefreshAfterExecute)]
        public string Caption
        {
            get { return dotWaitingIndicatorProperties.Caption; }
            set
            {
                dotWaitingIndicatorProperties.Caption = value;
                CheckBestFit();
            }
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            CheckBestFit();
        }

        [Category("Display Options"), DefaultValue(defDescription), Localizable(true), SmartTagProperty("Description", "", SmartTagActionType.RefreshAfterExecute)]
        public string Description
        {
            get { return dotWaitingIndicatorProperties.Description; }
            set
            {
                dotWaitingIndicatorProperties.Description = value;
                CheckBestFit();
            }
        }

        private string percentProcess = "";

        /// <summary>
        /// 百分数进度
        /// </summary>
        [Category("Display Options"), DefaultValue(""), SmartTagProperty("Show Percent", "", SmartTagActionType.RefreshAfterExecute)]
        public string PercentProcess
        {
            get { return percentProcess; }
            set
            {
                percentProcess = value;
                Invalidate();
            }
        }

        private bool isShowPercentage = true;

        /// <summary>
        /// 是否百分数
        /// </summary>
        [Category("Display Options"), DefaultValue(true), SmartTagProperty("Is Show Percent ?", "", SmartTagActionType.RefreshAfterExecute)]
        public bool IsShowPercentage
        {
            get { return isShowPercentage; }
            set
            {
                isShowPercentage = value;
                Invalidate();
            }
        }
        /// <summary>
        /// 是否显示标题
        /// </summary>
        [Category("Display Options"), DefaultValue(true), SmartTagProperty("Show Caption", "", SmartTagActionType.RefreshAfterExecute)]
        public bool ShowCaption
        {
            get { return dotWaitingIndicatorProperties.ShowCaption; }
            set
            {
                if (ShowCaption != value)
                {
                    dotWaitingIndicatorProperties.ShowCaption = value;
                    Invalidate();
                }
            }
        }
        /// <summary>
        /// 是否显示描述信息
        /// </summary>
        [Category("Display Options"), DefaultValue(true), SmartTagProperty("Show Description", "", SmartTagActionType.RefreshAfterExecute)]
        public bool ShowDescription
        {
            get { return dotWaitingIndicatorProperties.ShowDescription; }
            set
            {
                if (ShowDescription != value)
                {
                    dotWaitingIndicatorProperties.ShowDescription = value;
                    Invalidate();
                }
            }
        }

        [Category("Display Options"), DefaultValue(8),
        Obsolete("The TextHorzOffset property is now obsolete. Use the AnimationToTextDistance property instead."),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TextHorzOffset
        {
            get { return dotWaitingIndicatorProperties.ImageToTextDistance; }
            set
            {
                if (value < 0) throw new ArgumentException("TextHorzOffset");
                dotWaitingIndicatorProperties.ImageToTextDistance = value;
            }
        }

        [Category("Behavior"), DefaultValue(5)]
        public int AnimationElementCount
        {
            get { return dotWaitingIndicatorProperties.AnimationElementCount; }
            set { dotWaitingIndicatorProperties.AnimationElementCount = value; }
        }

        [Category("Display Options"), DefaultValue(8)]
        public int AnimationToTextDistance
        {
            get { return dotWaitingIndicatorProperties.ImageToTextDistance; }
            set { dotWaitingIndicatorProperties.ImageToTextDistance = value; }
        }

        [Category("Display Options"), DefaultValue(0)]
        public int ImageHorzOffset
        {
            get { return dotWaitingIndicatorProperties.ImageOffset; }
            set
            {
                if (value < 0) throw new ArgumentException("ImageOffset");
                if (dotWaitingIndicatorProperties.ImageOffset != value)
                {
                    dotWaitingIndicatorProperties.ImageOffset = value;
                    Invalidate();
                }
            }
        }

        [Category("Display Options"), DefaultValue(0)]
        public int CaptionToDescriptionDistance
        {
            get { return dotWaitingIndicatorProperties.CaptionToDescriptionDistance; }
            set
            {
                if (value < 0) throw new ArgumentException("CaptionToDescriptionDistance");
                dotWaitingIndicatorProperties.CaptionToDescriptionDistance = value;
            }
        }

        private bool autoHeight;

        [DefaultValue(false), Category("Layout")]
        public bool AutoHeight
        {
            get { return this.autoHeight || AutoSize; }
            set
            {
                if (AutoHeight == value) return;
                this.autoHeight = value;
                CheckBestFit();
            }
        }

        private bool autoWidth;

        [DefaultValue(false), Category("Layout")]
        public bool AutoWidth
        {
            get { return this.autoWidth || AutoSize; }
            set
            {
                if (AutoWidth == value) return;
                this.autoWidth = value;
                CheckBestFit();
            }
        }

        protected internal void DoBestFit()
        {
            CheckBestFit();
        }

        protected virtual void CheckBestFit()
        {
            Size = new Size(GetWidth(), GetHeight());
            OnPropertiesChanged();
        }

        protected virtual int GetWidth()
        {
            if (AutoWidth)
                return GetBestSize().Width;
            return ViewInfo.OriginalSize.Width;
        }

        protected virtual int GetHeight()
        {
            if (AutoHeight)
                return GetBestSize().Height;
            return ViewInfo.OriginalSize.Height;
        }

        protected virtual Size GetBestSize()
        {
            if (AnimationHelper.CustomIndicatorInfo != null)
            {
                Size result = AnimationHelper.CustomIndicatorInfo.GetBestSize();
                result.Width += Padding.Horizontal + 4;
                result.Height += Padding.Vertical + 4;
                return result;
            }
            else
                return ViewInfo.CalcPreferredSize();
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (AutoSize)
            {
                return GetBestSize();
            }
            return base.GetPreferredSize(proposedSize);
        }

        private new CircleProgressControlViewInfo ViewInfo
        { get { return base.ViewInfo as CircleProgressControlViewInfo; } }

        [Browsable(false)]
        public Size ImageSize
        {
            get
            {
                if (AnimationHelper.Image != null)
                    return AnimationHelper.Image.Size;
                return Size.Empty;
            }
        }

        private AppearanceObject appearanceCaptionCore = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DXCategory(CategoryName.Appearance)]
        public AppearanceObject AppearanceCaption { get { return appearanceCaptionCore; } set { appearanceCaptionCore = value; } }

        private AppearanceObject appearanceDescriptionCore = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DXCategory(CategoryName.Appearance)]
        public AppearanceObject AppearanceDescription { get { return appearanceDescriptionCore; } set { appearanceDescriptionCore = value; } }

        [DefaultValue(BorderStyles.Default)]
        public override BorderStyles BorderStyle
        {
            get
            {
                if (BaseBorderStyle == BorderStyles.Default)
                {
                    if (StyleController != null && StyleController.BorderStyle != BorderStyles.Default)
                        return StyleController.BorderStyle;
                }
                return BaseBorderStyle;
            }
            set { base.BorderStyle = value; }
        }

        [Category("Behavior"), DefaultValue(ContentAlignment.MiddleLeft)]
        public ContentAlignment ContentAlignment
        {
            get { return dotWaitingIndicatorProperties.ContentAlignment; }
            set { dotWaitingIndicatorProperties.ContentAlignment = value; }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            ViewInfo.UpdateOriginalSize(Size);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (specified.HasFlag(BoundsSpecified.Size))
            {
                if (AutoWidth)
                    width = GetWidth();
                if (AutoHeight)
                    height = GetHeight();
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected virtual void AdjustSize()
        {
            if (this.AutoSize)
                this.PerformLayout();
        }

        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            if (ViewInfo != null)
                ViewInfo.UpdateStyle();
            AnimationHelper.Reset();
            AnimationHelper.SetAnimator();
            AdjustSize();
            Invalidate();
        }

        #region ITransparentBackgroundManager

        Color ITransparentBackgroundManager.GetForeColor(object childObject)
        {
            return GetForeColorCore();
        }

        Color ITransparentBackgroundManager.GetForeColor(Control childControl)
        {
            return GetForeColorCore();
        }

        private Color GetForeColorCore()
        {
            return LookAndFeelHelper.GetTransparentForeColor(LookAndFeel, this);
        }

        #endregion ITransparentBackgroundManager

        protected override void Dispose(bool disposing)
        {
            LookAndFeel.StyleChanged -= LookAndFeel_StyleChanged;
            if (disposing)
            {
                if (AnimationHelper != null)
                    AnimationHelper.Dispose();
                AnimationHelper = null;
                if (lineWaitingIndicatorProperties != null)
                    lineWaitingIndicatorProperties.Changed -= OnWaitingIndicatorPropertiesChanged;
                if (ringWaitingIndicatorProperties != null)
                    ringWaitingIndicatorProperties.Changed -= OnWaitingIndicatorPropertiesChanged;
                if (Appearance != null)
                    Appearance.Changed -= OnAppearanceChanged;
                if (AppearanceCaption != null)
                {
                    AppearanceCaption.Changed -= OnAppearanceCaptionChanged;
                    DestroyAppearance(AppearanceCaption);
                }
                AppearanceCaption = null;
                if (AppearanceDescription != null)
                {
                    AppearanceDescription.Changed -= OnAppearanceDescriptionChanged;
                    DestroyAppearance(AppearanceDescription);
                }
                AppearanceDescription = null;
                if (captionDefaultFontCore != null)
                    captionDefaultFontCore.Dispose();
                captionDefaultFontCore = null;
                if (descriptionDefaultFontCore != null)
                    descriptionDefaultFontCore.Dispose();
                descriptionDefaultFontCore = null;
            }
            base.Dispose(disposing);
        }
    }

    internal class LoadingAnimationHelper : IDisposable
    {
        public LoadingAnimationHelper(CircleProgressControl progressPanel)
        {
            ProgressPanel = progressPanel;
        }

        ~LoadingAnimationHelper()
        {
            Dispose(false);
        }

        private DevExpress.Utils.Animation.BaseLoadingAnimator animatorCore = null;
        private DevExpress.Utils.Animation.BaseWaitingIndicatorInfo customIndicatorInfo;

        public DevExpress.Utils.Animation.BaseLoadingAnimator Animator
        {
            get
            {
                if (this.animatorCore == null)
                {
                    this.animatorCore = new LoadingAnimator(ProgressPanel, Image);
                }
                return this.animatorCore;
            }
        }

        public DevExpress.Utils.Animation.BaseWaitingIndicatorInfo CustomIndicatorInfo
        {
            get { return customIndicatorInfo; }
        }

        internal void SetAnimator()
        {
            if (animatorCore != null)
                animatorCore.StopAnimation();
            if (animatorCore is DevExpress.Utils.Animation.DotAnimator)
                (animatorCore as DevExpress.Utils.Animation.DotAnimator).Invalidate -= OnInvalidate;
            if (ProgressPanel.WaitAnimationType == DevExpress.Utils.Animation.WaitingAnimatorType.Line)
            {
                customIndicatorInfo = new DevExpress.Utils.Animation.LineAnimatorInfo(ProgressPanel.WaitingIndicatorProperties, new DevExpress.Utils.Animation.AsyncSkinInfo(ProgressPanel.LookAndFeel));
                this.animatorCore = customIndicatorInfo.WaitingAnimator as DevExpress.Utils.Animation.BaseLoadingAnimator;
                (animatorCore as DevExpress.Utils.Animation.LineAnimator).Invalidate += OnInvalidate;
                animatorCore.StartAnimation();
                return;
            }
            if (ProgressPanel.WaitAnimationType == DevExpress.Utils.Animation.WaitingAnimatorType.Ring)
            {
                customIndicatorInfo = new DevExpress.Utils.Animation.RingAnimatorInfo(ProgressPanel.WaitingIndicatorProperties, new DevExpress.Utils.Animation.AsyncSkinInfo(ProgressPanel.LookAndFeel));
                this.animatorCore = customIndicatorInfo.WaitingAnimator as DevExpress.Utils.Animation.BaseLoadingAnimator;
                (animatorCore as DevExpress.Utils.Animation.DotAnimator).Invalidate += OnInvalidate;
                animatorCore.StartAnimation();
                return;
            }
            this.animatorCore = new LoadingAnimator(ProgressPanel, Image);
            customIndicatorInfo = null;
            animatorCore.StartAnimation();
        }

        private void OnInvalidate(object sender, EventArgs e)
        {
            ProgressPanel.Invalidate();
        }

        private Image imageCore;

        public Image Image
        {
            get
            {
                if (imageCore == null)
                    imageCore = LoadingImages.GetImage(ProgressPanel.LookAndFeel, true);
                return imageCore;
            }
        }

        public CircleProgressControl ProgressPanel { get; set; }

        public void Reset()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (animatorCore != null)
                {
                    animatorCore.StopAnimation();
                    animatorCore.Dispose();
                }
            }
            LoadingImages.DisposeImage(ref imageCore);
            animatorCore = null;
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }

    internal class CircleProgressControlPainter : BaseControlPainter
    {
        public CircleProgressControlPainter()
        {
            this.timerAnimation.Tick += TimerAnimation_Tick;
        }

        private void IncreaseValue()
        {
            if (_value + 1 <= _numberOfCircles)
                _value++;
            else
                _value = 1;
        }

        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            IncreaseValue();
            //Invalidate();
        }

        #region Private Fields

        private int _value = 1;

        private int _interval = 200;

        private Color _circleColor = Color.FromArgb(0, 176, 236);

        private bool _autoStart;

        private bool _stopped = true;

        private float _circleSize = 1.0F;

        private int _numberOfCircles = 10;

        private int _numberOfVisibleCircles = 10;

        private RotationType _rotation = RotationType.Clockwise;

        private float _percentage;

        private bool _showPercentage = true;

        private bool _showText = true;

        private TextDisplayModes _textDisplay = TextDisplayModes.None;
        private System.Windows.Forms.Timer timerAnimation = new Timer();

        #endregion Private Fields

        private Rectangle ImageBounds = Rectangle.Empty;
        private Rectangle CaptionBounds = Rectangle.Empty;
        private Rectangle DescriptionBounds = Rectangle.Empty;

        protected override void DrawContent(ControlGraphicsInfoArgs info)
        {
            base.DrawContent(info);
            CircleProgressControlViewInfo viewInfo = (CircleProgressControlViewInfo)info.ViewInfo;
            if (viewInfo.OwnerControl.AnimationHelper.Animator is DevExpress.Utils.Animation.DotAnimator)
            {
                CircleProgressControl panel = viewInfo.OwnerControl;
                ApplyForeColor(viewInfo, panel);
                DrawCustomAnimation(info);
                return;
            }
            ImageBounds = viewInfo.ImageBounds;
            CaptionBounds = viewInfo.GetCaptionBounds(info);
            DescriptionBounds = viewInfo.GetDescriptionBounds(info);
            UpdateBounds(viewInfo);
            DrawAnimation(info);
            DrawCaption(info);
            DrawDescription(info);
        }

        /// <summary>
        /// 画圆点
        /// </summary>
        private void DrawTorusByDot()
        {
        }

        /// <summary>
        /// 画圆环
        /// </summary>
        private void DrawTorus()
        {
        }

        protected virtual void UpdateBounds(CircleProgressControlViewInfo viewInfo)
        {
            CircleProgressControl panel = viewInfo.OwnerControl;
            ContentAlignment contentAlignment = panel.ContentAlignment;
            int leftCornerX = ImageBounds.X;
            int leftCornerY = Math.Min(ImageBounds.Y, Math.Min(CaptionBounds.Y, DescriptionBounds.Y));
            int totalWidth = Math.Max(ImageBounds.Right, Math.Max(CaptionBounds.Right, DescriptionBounds.Right)) - leftCornerX;
            int totalHeight = Math.Max(ImageBounds.Bottom, Math.Max(CaptionBounds.Bottom, DescriptionBounds.Bottom)) - leftCornerY;
            Rectangle clientRect = viewInfo.ClientRectCore;
            clientRect.X += viewInfo.OwnerControl.ImageHorzOffset;
            clientRect.Width -= viewInfo.OwnerControl.ImageHorzOffset;
            Point newLocation = PlacementHelper.Arrange(new Size(totalWidth, totalHeight), clientRect, contentAlignment, RoundMode.Down).Location;
            int offsetX = newLocation.X - leftCornerX;
            int offsetY = newLocation.Y - leftCornerY;
            CaptionBounds.Offset(offsetX, offsetY);
            DescriptionBounds.Offset(offsetX, offsetY);
            ImageBounds.Offset(offsetX, offsetY);
        }

        protected virtual void ApplyForeColor(CircleProgressControlViewInfo viewInfo, CircleProgressControl panel)
        {
            if (panel == null || viewInfo == null || panel.GetDotWaitingindicatorProperties() == null) return;
            DevExpress.Utils.Animation.DotWaitingIndicatorProperties dotWaitingIndicatorProperties = panel.GetDotWaitingindicatorProperties();
            dotWaitingIndicatorProperties.Appearance.ForeColor = panel.Appearance.ForeColor == Color.Empty ? viewInfo.GetForecolor() : panel.Appearance.ForeColor;
            dotWaitingIndicatorProperties.AppearanceCaption.ForeColor = panel.AppearanceCaption.ForeColor == Color.Empty ? viewInfo.GetForecolor() : panel.AppearanceCaption.ForeColor;
            dotWaitingIndicatorProperties.AppearanceDescription.ForeColor = panel.AppearanceDescription.ForeColor == Color.Empty ? viewInfo.GetForecolor() : panel.AppearanceDescription.ForeColor;
        }

        protected virtual void DrawCustomAnimation(ControlGraphicsInfoArgs info)
        {
            CircleProgressControlViewInfo viewInfo = (CircleProgressControlViewInfo)info.ViewInfo;
            ObjectInfoArgs e = new ObjectInfoArgs(info.Cache, viewInfo.ClientRectCore, ObjectState.Normal);
            viewInfo.OwnerControl.AnimationHelper.CustomIndicatorInfo.Painter.DrawObject(e);
        }

        protected virtual void DrawAnimation(ControlGraphicsInfoArgs info)
        {
            #region 画动画

            CircleProgressControlViewInfo viewInfo = (CircleProgressControlViewInfo)info.ViewInfo;
            float angle = 360.0F / _numberOfCircles;

            GraphicsState oldState = info.Graphics.Save();

            info.Graphics.TranslateTransform(12.5f + 30, info.Bounds.Height / 2.0F);
            info.Graphics.RotateTransform(angle * _value * (int)_rotation);
            info.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            info.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 1; i <= _numberOfCircles; i++)
            {
                Color drawColor;
                if (i < 7)
                    drawColor = Color.FromArgb(255, _circleColor);
                else if (i == 7)
                    drawColor = Color.FromArgb(178, _circleColor);
                else if (i == 8)
                    drawColor = Color.FromArgb(115, _circleColor);
                else if (i == 9)
                    drawColor = Color.FromArgb(76, _circleColor);
                else if (i == 10)
                    drawColor = Color.FromArgb(51, _circleColor);
                else
                    drawColor = Color.FromArgb(0, _circleColor);

                using (SolidBrush brush = new SolidBrush(drawColor))
                {
                    float sizeRate = 4.5F / _circleSize;
                    float size = info.Bounds.Width / sizeRate;

                    float diff = (info.Bounds.Width / 4.5F) - size;

                    float x = 10.5f + diff;
                    float y = 10.5f + diff;
                    info.Graphics.FillEllipse(brush, x, y, 6.0f, 6.0f);
                    info.Graphics.RotateTransform(angle * (int)_rotation);
                }
            }

            info.Graphics.Restore(oldState);

            string percent = GetDrawText(viewInfo);

            if (!string.IsNullOrEmpty(percent))
            {
                SizeF textSize = info.Graphics.MeasureString(percent, info.ViewInfo.Appearance.Font);
                float textX = 42.5f - (textSize.Width / 2.0F);
                float textY = (info.Bounds.Height / 2.0F) - (textSize.Height / 2.0F);
                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                RectangleF rectangle = new RectangleF(textX, textY, textSize.Width, textSize.Height);

                using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(10, 180, 236)))
                {
                    info.Graphics.DrawString(percent, DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont, textBrush, rectangle, format);
                }
            }
            timerAnimation.Interval = _interval;
            _stopped = false;
            timerAnimation.Start();

            #endregion 画动画

            viewInfo.OwnerControl.AnimationHelper.Animator.DrawAnimatedItem(info.Cache, new Rectangle());
        }

        private string GetDrawText(CircleProgressControlViewInfo viewInfo)
        {
            string percent = string.Format(CultureInfo.CurrentCulture, "{0:0.##}%", viewInfo.PercentProcess);

            if (!viewInfo.IsShowPercentage)
                return string.Empty;
            return percent;
        }

        protected virtual void DrawCaption(ControlGraphicsInfoArgs info)
        {
            CircleProgressControlViewInfo viewInfo = (CircleProgressControlViewInfo)info.ViewInfo;
            if (!viewInfo.ShouldDrawCaption)
                return;
            FrozenAppearance PaintAppearance = new FrozenAppearance();
            AppearanceHelper.Combine(PaintAppearance, new AppearanceObject[] { viewInfo.OwnerControl.AppearanceCaption }, viewInfo.OwnerControl.DefaultAppearanceCaption);
            DrawStringCore(PaintAppearance, info, viewInfo.Caption, CaptionBounds);
        }

        protected virtual void DrawDescription(ControlGraphicsInfoArgs info)
        {
            CircleProgressControlViewInfo viewInfo = (CircleProgressControlViewInfo)info.ViewInfo;
            if (!viewInfo.ShouldDrawDescription) return;
            FrozenAppearance PaintAppearance = new FrozenAppearance();
            AppearanceHelper.Combine(PaintAppearance, new AppearanceObject[] { viewInfo.OwnerControl.AppearanceDescription }, viewInfo.OwnerControl.DefaultAppearanceDescription);
            DrawStringCore(PaintAppearance, info, viewInfo.Description, DescriptionBounds);
        }

        private void DrawStringCore(AppearanceObject appearance, ControlGraphicsInfoArgs info, string text, Rectangle bounds)
        {
            CircleProgressControlViewInfo viewInfo = (CircleProgressControlViewInfo)info.ViewInfo;
            using (StringFormat format = appearance.GetStringFormat().Clone() as StringFormat)
            {
                appearance.DrawString(info.Cache, text, bounds, viewInfo.GetLabelForeBrush(appearance), format);
            }
        }
    }

    internal class CircleProgressControlViewInfoBase : BaseStyleControlViewInfo
    {
        public CircleProgressControlViewInfoBase(BaseStyleControl control) : base(control)
        {
        }

        protected Size CalcStringSizeCore(string value, Font font)
        {
            return CalcStringSizeCore(value, font, StringFormat.GenericDefault, 0);
        }

        protected Size CalcStringSizeCore(string value, Font font, StringFormat format, int maxWidth)
        {
            var g = GraphicsInfo.Default.AddGraphics(null);
            try
            {
                return TextUtils.GetStringSize(g, value, font, format, maxWidth);
            }
            finally
            {
                GraphicsInfo.Default.ReleaseGraphics();
            }
        }

        public new CircleProgressControl OwnerControl { get { return base.OwnerControl as CircleProgressControl; } }
    }

    internal class CircleProgressControlViewInfo : CircleProgressControlViewInfoBase
    {
        private Size originalSizeCore;
        private CircleProgressControl circleProgressControl;

        public CircleProgressControlViewInfo(BaseStyleControl control)
            : base(control)
        {
            OriginalSize = OwnerControl.Size;
            if (control is CircleProgressControl)
            {
                CircleProgressControl circleProgressControl = control as CircleProgressControl;
            }
        }

        public Size OriginalSize
        {
            get { return originalSizeCore; }
            set { originalSizeCore = value; }
        }

        public Rectangle ClientRectCore
        {
            get
            {
                Rectangle rect = ClientRect;
                rect.X += OwnerControl.Padding.Left;
                rect.Y += OwnerControl.Padding.Top;
                rect.Width = Math.Max(rect.Width - OwnerControl.Padding.Left - OwnerControl.Padding.Right, 0);
                rect.Height = Math.Max(rect.Height - OwnerControl.Padding.Top - OwnerControl.Padding.Bottom, 0);
                return rect;
            }
        }

        public string PercentProcess { get { return OwnerControl != null ? OwnerControl.PercentProcess : string.Empty; } }

        public bool IsShowPercentage { get { return OwnerControl != null ? OwnerControl.IsShowPercentage : true; } }

        protected internal virtual void UpdateOriginalSize(Size size)
        {
            if (!OwnerControl.AutoWidth) originalSizeCore.Width = size.Width;
            if (!OwnerControl.AutoHeight) originalSizeCore.Height = size.Height;
        }

        protected override BorderPainter GetBorderPainterCore()
        {
            if (OwnerControl == null || OwnerControl.BorderStyle == BorderStyles.Default)
                return new EmptyBorderPainter();
            return base.GetBorderPainterCore();
        }

        public Point GetCaptionCorner(Graphics g)
        {
            if (!ShouldDrawDescription)
                return new Point(TextOffsetX, ControlHeight / 2 - SizeCaption.Height / 2);
            int totalHeight = SizeCaption.Height + SizeDescription.Height + OwnerControl.CaptionToDescriptionDistance;
            return new Point(TextOffsetX, ClientRectCore.Top + Math.Max((ControlHeight - totalHeight) / 2 - 1, 0));
        }

        public Point GetDescriptionCorner(Graphics g)
        {
            if (!ShouldDrawCaption)
                return new Point(TextOffsetX, ControlHeight / 2 - SizeDescription.Height / 2);
            Point pt = GetCaptionCorner(g);
            return new Point(TextOffsetX, pt.Y + SizeCaption.Height + OwnerControl.CaptionToDescriptionDistance);
        }

        public Rectangle GetCaptionBounds(ControlGraphicsInfoArgs info)
        {
            Rectangle rect = new Rectangle(GetCaptionCorner(info.Graphics), SizeCaption);
            return Rectangle.Intersect(ClientRectCore, rect);
        }

        public Rectangle GetDescriptionBounds(ControlGraphicsInfoArgs info)
        {
            return Rectangle.Intersect(ClientRectCore, CalcDescriptionClipRectangle(info));
        }

        public string Caption { get { return OwnerControl != null ? OwnerControl.Caption : string.Empty; } }
        public string Description { get { return OwnerControl != null ? OwnerControl.Description : string.Empty; } }

        public bool ShouldDrawCaption { get { return OwnerControl != null && OwnerControl.ShowCaption && !string.IsNullOrEmpty(Caption); } }
        public bool ShouldDrawDescription { get { return OwnerControl != null && OwnerControl.ShowDescription && !string.IsNullOrEmpty(Description); } }

        public void UpdateStyle()
        {
            captionPaintAppearanceCore = null;
            descriptionPaintAppearance = null;
        }

        private FrozenAppearance captionPaintAppearanceCore;

        public Font CaptionFont
        {
            get
            {
                if (captionPaintAppearanceCore == null)
                {
                    captionPaintAppearanceCore = new FrozenAppearance();
                    AppearanceHelper.Combine(captionPaintAppearanceCore, new AppearanceObject[] { OwnerControl.AppearanceCaption }, OwnerControl.DefaultAppearanceCaption);
                }
                return captionPaintAppearanceCore.Font;
            }
        }

        private FrozenAppearance descriptionPaintAppearance;

        public Font DescriptionFont
        {
            get
            {
                if (descriptionPaintAppearance == null)
                {
                    descriptionPaintAppearance = new FrozenAppearance();
                    AppearanceHelper.Combine(descriptionPaintAppearance, new AppearanceObject[] { OwnerControl.AppearanceDescription }, OwnerControl.DefaultAppearanceDescription);
                }
                return descriptionPaintAppearance.Font;
            }
        }

        public Brush GetLabelForeBrush(AppearanceObject appearance)
        {
            if (appearance.ForeColor.IsEmpty)
            {
                ITransparentBackgroundManager manager = ITransparentBackgroundManagerImplementer;
                return new SolidBrush(manager.GetForeColor(OwnerControl));
            }
            return new SolidBrush(appearance.ForeColor);
        }

        protected Rectangle CalcDescriptionClipRectangle(ControlGraphicsInfoArgs info)
        {
            Point pt = GetDescriptionCorner(info.Graphics);
            return new Rectangle(pt, CalcDescriptionClipRectSize(pt));
        }

        protected Size CalcDescriptionClipRectSize(Point topLeftPt)
        {
            Size size = Size.Empty;
            AppearanceObject appearance = OwnerControl.AppearanceDescription;
            using (StringFormat format = appearance.GetStringFormat().Clone() as StringFormat)
            {
                size = CalcStringSizeCore(Description, DescriptionFont, format, Math.Max(OwnerControl.Width - topLeftPt.X, 0));
            }
            return size;
        }

        protected internal Size SizeCaption { get { return CalcStringSizeCore(Caption, CaptionFont); } }
        protected internal Size SizeDescription { get { return CalcStringSizeCore(Description, DescriptionFont); } }

        internal Size CalcPreferredSize()
        {
            int height = OwnerControl.AutoHeight ? PreferredHeight : OriginalHeight;
            int width = OwnerControl.AutoWidth ? PreferredWidth : OriginalWidth;
            return new Size(width, height);
        }

        public int PreferredHeight { get { return Math.Max(ImageHeight, SizeCaption.Height + SizeDescription.Height + OwnerControl.CaptionToDescriptionDistance) + OwnerControl.Padding.Top + OwnerControl.Padding.Bottom + 2; } }
        public int PreferredWidth { get { return Math.Max(SizeCaption.Width, SizeDescription.Width) + ImageWidth + OwnerControl.ImageHorzOffset + OwnerControl.AnimationToTextDistance + OwnerControl.Padding.Left + OwnerControl.Padding.Right + 2; } }
        public int OriginalWidth { get { return OriginalSize.Width; } }
        public int OriginalHeight { get { return OriginalSize.Height; } }

        public int GetHeight()
        {
            return OwnerControl.AutoHeight ? CalcPreferredSize().Height : OriginalHeight;
        }

        public int GetWidth()
        {
            return OwnerControl.AutoWidth ? CalcPreferredSize().Width : OriginalWidth;
        }

        internal Color GetForecolor()
        {
            ITransparentBackgroundManager manager = ITransparentBackgroundManagerImplementer;
            return manager.GetForeColor(OwnerControl);
        }

        private ITransparentBackgroundManager ITransparentBackgroundManagerImplementer
        {
            get
            {
                ITransparentBackgroundManager parentForm = OwnerControl.FindForm() as ITransparentBackgroundManager;
                return parentForm ?? OwnerControl;
            }
        }

        protected virtual int TextOffsetX
        {
            get { return ImageWidth + OwnerControl.ImageHorzOffset + OwnerControl.AnimationToTextDistance + ClientRectCore.Left; }
        }

        public Rectangle ImageBounds
        {
            get
            {
                Rectangle rect = new Rectangle(OwnerControl.ImageHorzOffset + ClientRectCore.Left, ClientRectCore.Height / 2 - ImageHeight / 2 + ClientRectCore.Top, ImageWidth, ImageHeight);
                return Rectangle.Intersect(rect, ClientRectCore);
            }
        }

        protected internal virtual int ImageWidth { get { return OwnerControl.AnimationHelper.Image.Size.Width; } }
        protected internal virtual int ImageHeight { get { return OwnerControl.AnimationHelper.Image.Size.Height; } }
        protected int ControlHeight { get { return ClientRectCore.Height; } }
    }

    [Flags]
    public enum TextDisplayModes
    {
        None = 0,

        Percentage = 1,

        Text = 2,

        Both = Percentage | Text
    }

    public enum RotationType
    {
        Clockwise = 1,

        CounterClockwise = -1,
    }
}