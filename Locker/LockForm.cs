using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using USB.Config;

namespace Locker
{
    public partial class LockForm : Form
    {
        Settings _setting;
        public LockForm()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(10000, 10000);
            InitSecurity();
            SetLabelPosition();
        }
        
        ~LockForm()
        {
        }

        private void InitSecurity()
        {
            Hook.Start();
            TaskMngr.SetTaskManager(false);
            Taskbar.Hide();
            Cursor.Hide();
            SetLabelTime();
            TopMostAlways.SetTopmost(this.Handle);
        }

        private void LoadSettings()
        {
            _setting = Settings.LoadFrom("settings.cfg");
            //TODO: 구현

            if(_setting.AcceptedSerials.Count == 0)
            {
                //요긴 어떻게 하지...? 비상 키라도..?
            }
        }
        
        private void SetLabelTime()
        {
            this.lbTime.Text = DateTime.Now.ToString("hh:mm:ss");
            this.lbDate.Text = DateTime.Now.ToString("MMMM d일, dddd");
        }

        private void SetLabelPosition()
        {
            this.lbTime.Location = new System.Drawing.Point(Width / 2 - lbTime.Width / 2, lbTime.Location.Y);
            this.lbDate.Location = new System.Drawing.Point(Width / 2 - lbDate.Width / 2, lbDate.Location.Y);
            this.lbKey.Location = new System.Drawing.Point(Width / 2 - lbKey.Width / 2, lbKey.Location.Y);
        }

        protected override void WndProc(ref Message m)
        {
            UInt32 WM_DEVICECHANGE = 0x0219;
            UInt32 DBT_DEVTUP_VOLUME = 0x02;
            UInt32 DBT_DEVICEARRIVAL = 0x8000;
            UInt32 DBT_DEVICEREMOVECOMPLETE = 0x8004;

            if ((m.Msg == WM_DEVICECHANGE) && (m.WParam.ToInt32() == DBT_DEVICEARRIVAL))//디바이스 연결
            {
                //int m_Count = 0;
                int devType = Marshal.ReadInt32(m.LParam, 4);

                if (devType == DBT_DEVTUP_VOLUME)
                {
                    System.Threading.Thread t = new System.Threading.Thread(USBStateChanged);
                    t.Start();
                }
            }

            if ((m.Msg == WM_DEVICECHANGE) && (m.WParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE))  //디바이스 연결 해제
            {
                int devType = Marshal.ReadInt32(m.LParam, 4);
                if (devType == DBT_DEVTUP_VOLUME)
                {
                    System.Threading.Thread t = new System.Threading.Thread(USBStateChanged);
                    t.Start();
                }
            }

            base.WndProc(ref m);
        }

        private void USBStateChanged()
        {
            foreach (string device in System.IO.Directory.GetLogicalDrives())
            {
                System.IO.DriveInfo dr = new System.IO.DriveInfo(device);
                if (dr.DriveType != System.IO.DriveType.Removable) continue;

                Application.Exit();
            }
        }
        
        private void LockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskMngr.SetTaskManager(true);
            Hook.End();
            Cursor.Show();
            Taskbar.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetLabelTime();
            SetLabelPosition();
        }
    }
}
