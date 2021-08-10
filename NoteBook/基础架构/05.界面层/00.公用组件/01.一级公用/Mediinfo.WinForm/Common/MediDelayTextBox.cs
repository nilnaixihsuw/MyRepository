using System;
using System.Timers;
using System.Windows.Forms;

namespace Mediinfo.WinForm
{
    public partial class MediDelayTextBox : MediTextBox
    {
        #region private globals

        private System.Timers.Timer DelayTimer;     // used for the delay
        private bool TimerElapsed = false;          // if true OnTextChanged is fired.
        private bool KeysPressed = false;           // makes event fire immediately if it wasn't a keypress
        private int DELAY_TIME = 1000;              // 延迟时间

        #endregion

        #region object model

        // Delay property
        public int Delay
        {
            set { DELAY_TIME = value; }
        }

        #endregion

        #region ctor

        public MediDelayTextBox()
        {
            InitializeComponent();

            // Initialize Timer
            DelayTimer = new System.Timers.Timer(DELAY_TIME);
            DelayTimer.Elapsed += new ElapsedEventHandler(DelayTimer_Elapsed);
        }

        #endregion

        #region event handlers

        void DelayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // stop timer.
            DelayTimer.Enabled = false;

            // set timer elapsed to true, so the OnTextChange knows to fire
            TimerElapsed = true;

            // use invoke to get back on the UI thread.
            this.Invoke(new DelayOverHandler(DelayOver), null);
        }

        #endregion

        #region overrides

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            DelayTimer.Enabled = true;
            KeysPressed = true;
            base.OnKeyPress(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            //base.OnTextChanged(e); //不加该方法光标会置到第一位， 
        }

        protected override void OnEditValueChanged()
        {
            if (TimerElapsed || !KeysPressed)
            {
                TimerElapsed = false;
                KeysPressed = false;
                base.OnEditValueChanged();
            }
        }

        #endregion

        #region delegates

        public delegate void DelayOverHandler();

        #endregion

        #region private helpers

        private void DelayOver()
        {
            OnEditValueChanged();
        }

        #endregion
    }
}