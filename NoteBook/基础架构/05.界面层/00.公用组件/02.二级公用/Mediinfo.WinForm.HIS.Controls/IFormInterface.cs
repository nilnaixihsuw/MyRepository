using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime;
using System.Windows.Forms;

namespace Mediinfo.WinForm.HIS.Controls
{
    /// <summary>
    /// 所有需要提供外部调用的窗口的接口，要继承该接口
    /// </summary>
    public interface IFormInterface : IDisposable
    {
        //
        // 摘要:
        //     获取或设置一个值，该值指示能否调整窗体的不透明度。
        //
        // 返回结果:
        //     如果可以更改窗体的不透明度，则为 true；否则为 false。
        bool AllowTransparency { get; set; }

        //
        // 摘要:
        //     获取窗体可调整到的最大大小。
        //
        // 返回结果:
        //     System.Drawing.Size，表示该窗体的最大大小。
        //
        // 异常:
        //   T:System.ArgumentOutOfRangeException:
        //     System.Drawing.Size 对象内的高或宽的值小于零。
        Size MaximumSize { get; set; }

        //
        // 摘要:
        //     获取或设置以屏幕坐标表示的代表 System.Windows.Forms.Form 左上角的 System.Drawing.Point。
        //
        // 返回结果:
        //     以屏幕坐标表示的代表 System.Windows.Forms.Form 左上角的 System.Drawing.Point。
        Point Location { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示在将键事件传递到具有焦点的控件前，窗体是否将接收此键事件。
        //
        // 返回结果:
        //     如果窗体将接收所有键事件，则为 true；如果窗体上当前选定控件接收键事件，则为 false。默认值为 false。
        bool KeyPreview { get; set; }

        //
        // 摘要:
        //     获取一个值，该值指示窗体是否可以不受限制地使用所有窗口和用户输入事件。
        //
        // 返回结果:
        //     如果窗体有限制，则为 true；否则为 false。默认值为 true。
        bool IsRestrictedWindow { get; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示窗体是否为多文档界面 (MDI) 子窗体的容器。
        //
        // 返回结果:
        //     如果该窗体是 MDI 子窗体的容器，则为 true；否则，为 false。默认值为 false。
        bool IsMdiContainer { get; set; }

        //
        // 摘要:
        //     获取一个值，该值指示窗体是否为多文档界面 (MDI) 子窗体。
        //
        // 返回结果:
        //     如果该窗体是 MDI 子窗体，则为 true；否则，为 false。
        bool IsMdiChild { get; }

        //
        // 摘要:
        //     获取或设置窗体的图标。
        //
        // 返回结果:
        //     System.Drawing.Icon，表示窗体的图标。
        Icon Icon { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否应在窗体的标题框中显示“帮助”按钮。
        //
        // 返回结果:
        //     如果为 true，则在窗体的标题栏中显示“帮助”按钮；否则，为 false。默认值为 false。
        bool HelpButton { get; set; }

        //
        // 摘要:
        //     获取或设置窗体的对话框结果。
        //
        // 返回结果:
        //     System.Windows.Forms.DialogResult，表示当窗体用作对话框时该窗体的结果。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不在有效值范围内。
        DialogResult DialogResult { get; set; }

        //
        // 摘要:
        //     获取或设置 Windows 桌面上窗体的位置。
        //
        // 返回结果:
        //     System.Drawing.Point，表示桌面上窗体的位置。
        Point DesktopLocation { get; set; }

        //
        // 摘要:
        //     获取或设置 Windows 桌面上窗体的大小和位置。
        //
        // 返回结果:
        //     System.Drawing.Rectangle，它使用桌面坐标表示 Windows 桌面上窗体的边界。
        Rectangle DesktopBounds { get; set; }

        //
        // 摘要:
        //     获取或设置窗体的主菜单容器。
        //
        // 返回结果:
        //     System.Windows.Forms.MenuStrip 表示窗体菜单结构的容器。默认值为 null。
        MenuStrip MainMenuStrip { get; set; }

        //
        // 摘要:
        //     获取或设置窗体工作区的大小。
        //
        // 返回结果:
        //     System.Drawing.Size，表示窗体工作区的大小。
        Size ClientSize { get; set; }

        //
        // 摘要:
        //     获取或设置当用户按 Esc 键时单击的按钮控件。
        //
        // 返回结果:
        //     System.Windows.Forms.IButtonControl，表示窗体的“取消”按钮。
        IButtonControl CancelButton { get; set; }

        //
        // 摘要:
        //     获取或设置窗体的边框样式。
        //
        // 返回结果:
        //     System.Windows.Forms.FormBorderStyle，表示要为窗体显示的边框样式。默认值为 FormBorderStyle.Sizable。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不在有效值范围内。
        FormBorderStyle FormBorderStyle { get; set; }

        //
        // 返回结果:
        //     表示控件背景色的 System.Drawing.Color。默认值为 System.Windows.Forms.Control.DefaultBackColor
        //     属性的值。
        Color BackColor { get; set; }

        //
        // 返回结果:
        //     一个 System.Windows.Forms.AutoValidate 枚举值，指示焦点更改时是否隐式验证所含控件。默认为 System.Windows.Forms.AutoValidate.Inherit。
        AutoValidate AutoValidate { get; set; }

        //
        // 摘要:
        //     获取或设置窗体自动调整自身大小的模式。
        //
        // 返回结果:
        //     System.Windows.Forms.AutoSizeMode 枚举值。默认值为 System.Windows.Forms.AutoSizeMode.GrowOnly。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     该值不是有效的 System.Windows.Forms.AutoSizeMode 值。
        AutoSizeMode AutoSizeMode { get; set; }

        //
        // 摘要:
        //     根据 System.Windows.Forms.Form.AutoSizeMode 的设置调整窗体的大小。
        //
        // 返回结果:
        //     如果窗体将自动调整大小，则为 true；如果必须手动调整大小，则为 false。
        bool AutoSize { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示窗体是否实现自动滚动。
        //
        // 返回结果:
        //     若要在窗体上启用自动滚动，为 true；否则，为 false。默认值为 false。
        bool AutoScroll { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示窗体是否调整其大小以适合该窗体上使用的字体高度，以及是否缩放其控件。
        //
        // 返回结果:
        //     如果窗体根据分配给它的当前字体自动缩放本身及其控件，则为 true；否则，为 false。默认值为 true。
        bool AutoScale { get; set; }

        //
        // 摘要:
        //     获取当前活动的多文档界面 (MDI) 子窗口。
        //
        // 返回结果:
        //     返回表示当前活动的 MDI 子窗口的 System.Windows.Forms.Form，或者如果当前没有子窗口，则返回 null。
        Form ActiveMdiChild { get; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示在该窗体的标题栏中是否显示控件框。
        //
        // 返回结果:
        //     如果该窗体在窗体的左上角显示控件框，则为 true；否则，为 false。默认值为 true。
        bool ControlBox { get; set; }

        //
        // 摘要:
        //     获取或设置控件之间的空间。
        //
        // 返回结果:
        //     表示控件之间的间距的值。
        Padding Margin { get; set; }

        //
        // 摘要:
        //     获取或设置窗体可调整到的最小大小。
        //
        // 返回结果:
        //     System.Drawing.Size，表示该窗体的最小大小。
        //
        // 异常:
        //   T:System.ArgumentOutOfRangeException:
        //     System.Drawing.Size 对象内的高或宽的值小于零。
        Size MinimumSize { get; set; }

        //
        // 摘要:
        //     获取或设置窗体的窗口状态。
        //
        // 返回结果:
        //     System.Windows.Forms.FormWindowState，表示窗体的窗口状态。默认值为 FormWindowState.Normal。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不在有效值范围内。
        FormWindowState WindowState { get; set; }

        //
        // 摘要:
        //     获取或设置将表示窗体透明区域的颜色。
        //
        // 返回结果:
        //     System.Drawing.Color，表示要在窗体上透明显示的颜色。
        Color TransparencyKey { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，指示该窗体是否应显示为最顶层窗体。
        //
        // 返回结果:
        //     如果将窗体显示为最顶层窗体，则为 true；否则为 false。默认值为 false。
        bool TopMost { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否将窗体显示为顶级窗口。
        //
        // 返回结果:
        //     如果为 true，则将窗体显示为顶级窗口；否则，为 false。默认值为 true。
        //
        // 异常:
        //   T:System.Exception:
        //     多文档界面 (MDI) 父窗体必须是顶级窗口。
        bool TopLevel { get; set; }

        //
        // 返回结果:
        //     与该控件关联的文本。
        string Text { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示用户能否使用 Tab 键将焦点放到该控件上。
        //
        // 返回结果:
        //     如果用户可以用 Tab 键将焦点放到此控件上，则为 true；反之，则为 false。默认值为 true。
        bool TabStop { get; set; }

        //
        // 摘要:
        //     获取或设置在控件的容器的控件的 Tab 键顺序。
        //
        // 返回结果:
        //     一个 System.Int32，它包含其容器内按 Tab 键顺序包括的控件集内的控件索引。
        int TabIndex { get; set; }

        //
        // 摘要:
        //     获取或设置运行时窗体的起始位置。
        //
        // 返回结果:
        //     System.Windows.Forms.FormStartPosition，表示窗体的起始位置。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不在有效值范围内。
        FormStartPosition StartPosition { get; set; }

        //
        // 摘要:
        //     获取或设置在窗体右下角显示的大小手柄的样式。
        //
        // 返回结果:
        //     System.Windows.Forms.SizeGripStyle，表示要显示的大小手柄的样式。默认值为 System.Windows.Forms.SizeGripStyle.Auto
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不在有效值范围内。
        SizeGripStyle SizeGripStyle { get; set; }

        //
        // 摘要:
        //     获取或设置窗体的大小。
        //
        // 返回结果:
        //     System.Drawing.Size，它表示窗体的大小。
        Size Size { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否在窗体的标题栏中显示图标。
        //
        // 返回结果:
        //     如果窗体在标题栏中显示图标，则为 true；否则为 false。默认值为 true。
        bool ShowIcon { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否在 Windows 任务栏中显示窗体。
        //
        // 返回结果:
        //     如果为 true，则运行时在 Windows 任务栏中显示窗体；否则，为 false。默认值为 true。
        bool ShowInTaskbar { get; set; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否打开从右向左的镜像放置。
        //
        // 返回结果:
        //     如果打开了从右到左的镜像放置，则为 true；否则对于标准子控件放置，为 false。默认值为 false。
        bool RightToLeftLayout { get; set; }

        //
        // 摘要:
        //     获取窗体在其正常窗口状态下的位置和大小。
        //
        // 返回结果:
        //     一个 System.Drawing.Rectangle，包含窗体在正常窗口状态下的位置和大小。
        Rectangle RestoreBounds { get; }

        //
        // 摘要:
        //     获取或设置拥有此窗体的窗体。
        //
        // 返回结果:
        //     System.Windows.Forms.Form，表示作为此窗体的所有者的窗体。
        //
        // 异常:
        //   T:System.Exception:
        //     顶级窗口不能具有所有者。
        Form Owner { get; set; }

        //
        // 摘要:
        //     获取 System.Windows.Forms.Form 对象的数组，这些对象表示此窗体拥有的所有窗体。
        //
        // 返回结果:
        //     System.Windows.Forms.Form 数组，它表示此窗体的附属窗体。
        Form[] OwnedForms { get; }

        //
        // 摘要:
        //     获取或设置窗体的不透明度级别。
        //
        // 返回结果:
        //     窗体的不透明度级别。默认值为 1.00。
        double Opacity { get; set; }

        //
        // 摘要:
        //     获取一个值，该值指示是否有模式地显示此窗体。
        //
        // 返回结果:
        //     如果该窗体进行模式显示，则为 true；否则为 false。
        bool Modal { get; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否在窗体的标题栏中显示“最小化”按钮。
        //
        // 返回结果:
        //     如果为 true，则显示窗体的“最小化”按钮；否则为 false。默认值为 true。
        bool MinimizeBox { get; set; }

        //
        // 摘要:
        //     获取窗体的合并菜单。
        //
        // 返回结果:
        //     System.Windows.Forms.MainMenu，表示窗体的合并菜单。
        MainMenu MergedMenu { get; }

        //
        // 摘要:
        //     获取或设置此窗体的当前多文档界面 (MDI) 父窗体。
        //
        // 返回结果:
        //     System.Windows.Forms.Form，表示 MDI 父窗体。
        //
        // 异常:
        //   T:System.Exception:
        //     分配给此属性的 System.Windows.Forms.Form 没有被标记为 MDI 容器。- 或 -分配给此属性的 System.Windows.Forms.Form
        //     同时作为子 MDI 窗体和 MDI 容器窗体。- 或 -分配给此属性的 System.Windows.Forms.Form 位于其他线程上。
        Form MdiParent { get; set; }

        //
        // 摘要:
        //     获取窗体的数组，这些窗体表示以此窗体作为父级的多文档界面 (MDI) 子窗体。
        //
        // 返回结果:
        //     System.Windows.Forms.Form 对象的数组，每个对象都标识此窗体的一个 MDI 子窗体。
        Form[] MdiChildren { get; }

        //
        // 摘要:
        //     获取或设置一个值，该值指示是否在窗体的标题栏中显示“最大化”按钮。
        //
        // 返回结果:
        //     如果为 true，则显示窗体的“最大化”按钮；否则为 false。默认值为 true。
        bool MaximizeBox { get; set; }

        //
        // 摘要:
        //     获取或设置在窗体中显示的 System.Windows.Forms.MainMenu。
        //
        // 返回结果:
        //     System.Windows.Forms.MainMenu，表示要在窗体中显示的菜单。
        MainMenu Menu { get; set; }

        //
        // 摘要:
        //     获取或设置当用户按 Enter 键时所单击的窗体上的按钮。
        //
        // 返回结果:
        //     System.Windows.Forms.IButtonControl，表示要用作窗体的“接受”按钮的按钮。
        IButtonControl AcceptButton { get; set; }

        //
        // 摘要:
        //     获取或设置用于自动缩放窗体的基大小。
        //
        // 返回结果:
        //     一种 System.Drawing.Size，表示此窗体用于自动缩放的基大小。
        Size AutoScaleBaseSize { get; set; }

        //
        // 摘要:
        //     关闭窗体后发生。
        event EventHandler Closed;

        //
        // 摘要:
        //     在第一次显示窗体前发生。
        event EventHandler Load;

        //
        // 摘要:
        //     在多文档界面 (MDI) 应用程序内激活或关闭 MDI 子窗体时发生。
        event EventHandler MdiChildActivate;

        //
        // 摘要:
        //     更改窗体的输入语言后发生。
        event InputLanguageChangedEventHandler InputLanguageChanged;

        //
        // 摘要:
        //     关闭窗体后发生。
        event FormClosedEventHandler FormClosed;

        //
        // 摘要:
        //     当窗体菜单接收焦点时发生。
        event EventHandler MenuStart;

        //
        // 摘要:
        //     当 System.Windows.Forms.Form.AutoSize 属性更改时发生。
        event EventHandler AutoSizeChanged;

        //
        // 摘要:
        //     当 System.Windows.Forms.Form.AutoValidate 属性更改时发生。
        event EventHandler AutoValidateChanged;

        //
        // 摘要:
        //     当窗体失去焦点并不再是活动窗体时发生。
        event EventHandler Deactivate;

        //
        // 摘要:
        //     单击“帮助”按钮时发生。
        event CancelEventHandler HelpButtonClicked;

        //
        // 摘要:
        //     在 System.Windows.Forms.Form.MaximumSize 属性的值更改后发生。
        event EventHandler MaximumSizeChanged;

        //
        // 摘要:
        //     关闭窗体前发生。

        event FormClosingEventHandler FormClosing;

        //
        // 摘要:
        //     在 System.Windows.Forms.Form.MinimumSize 属性的值更改后发生。
        event EventHandler MinimumSizeChanged;

        //
        // 摘要:
        //     当 System.Windows.Forms.Form.TabIndex 属性的值更改时发生。
        event EventHandler TabIndexChanged;

        //
        // 摘要:
        //     当 System.Windows.Forms.Form.TabStop 属性更改时发生。
        event EventHandler TabStopChanged;

        //
        // 摘要:
        //     当使用代码激活或用户激活窗体时发生。
        event EventHandler Activated;

        //
        // 摘要:
        //     在关闭窗体时发生。
        event CancelEventHandler Closing;

        //
        // 摘要:
        //     在 System.Windows.Forms.Form.MaximizedBounds 属性的值更改后发生。

        event EventHandler MaximizedBoundsChanged;

        //
        // 摘要:
        //     当 System.Windows.Forms.Form.Margin 属性更改时发生。

        event EventHandler MarginChanged;

        //
        // 摘要:
        //     当窗体菜单失去焦点时发生。
        event EventHandler MenuComplete;

        //
        // 摘要:
        //     窗体退出大小调整模式时发生。
        event EventHandler ResizeEnd;

        //
        // 摘要:
        //     只要窗体是首次显示就发生。
        event EventHandler Shown;

        //
        // 摘要:
        //     更改 System.Windows.Forms.Form.RightToLeftLayout 属性值之后发生。
        event EventHandler RightToLeftLayoutChanged;

        //
        // 摘要:
        //     当用户尝试更改窗体的输入语言时发生。
        event InputLanguageChangingEventHandler InputLanguageChanging;

        //
        // 摘要:
        //     窗体进入调整大小模式时发生。
        event EventHandler ResizeBegin;

        //
        // 摘要:
        //     激活窗体并给予它焦点。
        void Activate();

        //
        // 摘要:
        //     向此窗体添加附属窗体。
        //
        // 参数:
        //   ownedForm:
        //     此窗体将拥有的 System.Windows.Forms.Form。
        void AddOwnedForm(Form ownedForm);

        //
        // 摘要:
        //     关闭窗体。
        //
        // 异常:
        //   T:System.InvalidOperationException:
        //     在创建句柄时关闭了窗体。
        //
        //   T:System.ObjectDisposedException:
        //     当 System.Windows.Forms.Form.WindowState 设置为 System.Windows.Forms.FormWindowState.Maximized
        //     时，无法从 System.Windows.Forms.Form.Activated 事件调用此方法。
        void Close();

        //
        // 摘要:
        //     在 MDI 父窗体内排列多文档界面 (MDI) 子窗体。
        //
        // 参数:
        //   value:
        //     System.Windows.Forms.MdiLayout 值之一，定义 MDI 子窗体的布局。
        void LayoutMdi(MdiLayout value);

        //
        // 摘要:
        //     从此窗体移除附属窗体。
        //
        // 参数:
        //   ownedForm:
        //     System.Windows.Forms.Form，表示要从此窗体的附属窗体列表中移除的窗体。
        void RemoveOwnedForm(Form ownedForm);

        //
        // 摘要:
        //     以桌面坐标设置窗体的边界。
        //
        // 参数:
        //   x:
        //     窗体位置的 x 坐标。
        //
        //   y:
        //     窗体位置的 y 坐标。
        //
        //   width:
        //     窗体的宽度。
        //
        //   height:
        //     窗体的高度。
        void SetDesktopBounds(int x, int y, int width, int height);

        //
        // 摘要:
        //     以桌面坐标设置窗体的位置。
        //
        // 参数:
        //   x:
        //     窗体位置的 x 坐标。
        //
        //   y:
        //     窗体位置的 y 坐标。
        void SetDesktopLocation(int x, int y);

        //
        // 摘要:
        //     向用户显示具有指定所有者的窗体。
        //
        // 参数:
        //   owner:
        //     任何实现 System.Windows.Forms.IWin32Window 并表示将拥有此窗体的顶级窗口的对象。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     owner 参数中指定的窗体就是显示的窗体。
        void Show(IWin32Window owner);

        void Show();

        //
        // 摘要:
        //     将窗体显示为具有指定所有者的模式对话框。
        //
        // 参数:
        //   owner:
        //     任何实现 System.Windows.Forms.IWin32Window（表示将拥有模式对话框的顶级窗口）的对象。
        //
        // 返回结果:
        //     System.Windows.Forms.DialogResult 值之一。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     owner 参数中指定的窗体就是显示的窗体。
        //
        //   T:System.InvalidOperationException:
        //     要显示的窗体已经可见。- 或 -所显示窗体被禁用。- 或 -显示的窗体不是顶级窗口。- 或 -显示为对话框的窗体已经是模式窗体。- 或 -当前进程不是以用户交互模式运行的（有关更多信息，请参见
        //     System.Windows.Forms.SystemInformation.UserInteractive）。
        DialogResult ShowDialog(IWin32Window owner);

        //
        // 摘要:
        //     将窗体显示为模式对话框，并将当前活动窗口设置为它的所有者。
        //
        // 返回结果:
        //     System.Windows.Forms.DialogResult 值之一。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     owner 参数中指定的窗体就是显示的窗体。
        //
        //   T:System.InvalidOperationException:
        //     要显示的窗体已经可见。- 或 -所显示窗体被禁用。- 或 -显示的窗体不是顶级窗口。- 或 -显示为对话框的窗体已经是模式窗体。- 或 -当前进程不是以用户交互模式运行的（有关更多信息，请参见
        //     System.Windows.Forms.SystemInformation.UserInteractive）。
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        DialogResult ShowDialog();

        //
        // 摘要:
        //     获取表示当前窗体实例的字符串。
        //
        // 返回结果:
        //     由窗体对象类的完全限定名组成的字符串，窗体的 System.Windows.Forms.Form.Text 属性追加到字符串的末尾。例如，如果该窗体派生自
        //     MyNamespace 命名空间中的类 MyForm，并且 System.Windows.Forms.Form.Text 属性设置为 Hello、World，则此方法会返回
        //     MyNamespace.MyForm, Text: Hello, World。
        string ToString();

        //
        // 参数:
        //   validationConstraints:
        //     对哪些控件引发其 System.Windows.Forms.Control.Validating 事件作出限制。
        //
        // 返回结果:
        //     如果成功验证所有子级，则为 true；否则为 false。如果是从 System.Windows.Forms.Control.Validating 或 System.Windows.Forms.Control.Validated
        //     事件处理程序调用的，则该方法将始终返回 false。
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        bool ValidateChildren(ValidationConstraints validationConstraints);

        //
        // 返回结果:
        //     如果成功验证所有子级，则为 true；否则为 false。如果是从 System.Windows.Forms.Control.Validating 或 System.Windows.Forms.Control.Validated
        //     事件处理程序调用的，则该方法将始终返回 false。
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        bool ValidateChildren();

        /// <summary>
        /// 在主窗口中加载
        /// </summary>
        void ShowInWindow();
    }
}