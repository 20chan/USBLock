using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using USB.Config;

namespace LockConfigure
{
    public partial class ConfigureForm : Form
    {
        private Settings _settings;

        public ConfigureForm()
        {
            InitializeComponent();

            if(!File.Exists(Path.Combine(Environment.CurrentDirectory, "settings.cfg")))
            {
                _settings = Settings.Default;
                _settings.Save("settings.cfg");
            }
            _settings = Settings.LoadFrom("settings.cfg");

            SetControls();
            restoreUSBS();
            restoreSavedUSBS();
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
            restoreUSBS();
        }

        private void restoreUSBS()
        {
            listView2.Items.Clear();

            foreach (string device in Directory.GetLogicalDrives())
            {
                DriveInfo dr = new DriveInfo(device);
                if (dr.DriveType != DriveType.Removable) continue;

                ListViewItem item = new ListViewItem(device);
                item.SubItems.Add(dr.VolumeLabel);
                USBSerial usb = new USBSerial();
                string serial = usb.getSerialNumberFromDriveLetter(device.Substring(0, 2));
                item.SubItems.Add(serial);

                this.listView2.Items.Add(item);
            }
        }

        private void restoreSavedUSBS()
        {
            listView1.Items.Clear();
            foreach(var usb in _settings.AcceptedSerials)
            {
                ListViewItem item = new ListViewItem(usb.Name);
                item.SubItems.Add(usb.SerialNumber);
                item.SubItems.Add(usb.LastUsedDate.ToString());

                listView1.Items.Add(item);
            }
        }

        private void SetControls()
        {
            trackBar1.Value = (int)(_settings.FormOpacity * 100.0);
            checkBox1.Checked = _settings.IsStartUp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.label3.Text = (trackBar1.Value / 100.0).ToString();
        }

        private void 추가AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView2.FocusedItem;
            string drive = item.SubItems[0].Text;

            string serial = (new USBSerial()).getSerialNumberFromDriveLetter(drive.Substring(0, 2));

            if(!_settings.AcceptedSerials.TrueForAll(u => u.SerialNumber != serial)) //이미 동일 시리얼 존재
            {
                _settings.AcceptedSerials.RemoveAll(u => u.SerialNumber == serial);
            }

            DriveInfo dr = new DriveInfo(drive);
            USBDeviceInfo info = new USBDeviceInfo()
            {
                Name = dr.VolumeLabel,
                SerialNumber = serial,
                LastUsedDate = DateTime.Now
            };
            _settings.AcceptedSerials.Add(info);

            restoreSavedUSBS();
        }

        private void 제거DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settings.AcceptedSerials.RemoveAll(u => u.SerialNumber == listView1.FocusedItem.SubItems[1].Text);
            restoreSavedUSBS();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.IsStartUp = checkBox1.Checked;
            _settings.FormOpacity = trackBar1.Value / 100.0;
            _settings.Save("settings.cfg");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
