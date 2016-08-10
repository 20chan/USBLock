using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using USB.Config;

namespace Locker
{
    public partial class LockForm : Form
    {
        static readonly List<string> ImageExtensions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };
        List<Image> _backgroundImages;
        int prevIndex = 0;
        int currentIndex = 0;
        Settings _setting;

        Font _dateFont;
        string _dateText = DateTime.Now.ToString("hh:mm:ss");
        Font _timeFont;
        string _timeText = DateTime.Now.ToString("MMMM d, dddd");
        Font _descFont;
        string _descText = "잠금을 해제하려면 열쇠를 넣으세요";
        StringFormat _format = new StringFormat();

        public LockForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.Size = new Size(10000, 10000);
            LoadSettings();
            LoadFonts();
            InitSecurity();
            LoadBackgroundImages();
        }

        private void LockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            TaskMngr.SetTaskManager(true);
            Hook.End();
            Cursor.Show();
            Taskbar.Show();
        }

        private void InitSecurity()
        {
#if DEBUG
#else
            Hook.Start();
#endif
            TaskMngr.SetTaskManager(false);
            Taskbar.Hide();
            Cursor.Hide();
            TopMostAlways.SetTopmost(this.Handle);
        }
        
        private void LoadSettings()
        {
            if(!File.Exists(Path.Combine(Environment.CurrentDirectory, "settings.cfg")))
            {
                MessageBox.Show("설정 파일이 없어서 프로그램을 종료합니다.", "USB Locker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            _setting = Settings.LoadFrom("settings.cfg");

            StartUp.SetStartup(_setting.IsStartUp);
            this.Opacity = _setting.FormOpacity;
            
            if(_setting.AcceptedSerials.Count == 0)
            {
                MessageBox.Show("등록된 USB가 없어 프로그램을 종료합니다.", "USB Locker", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void LoadBackgroundImages()
        {
            if(!_setting.IsShowSlideShow)
            {
                imageslide.Enabled = false;
                timer1_Tick(null, null);
                return;
            }
            try
            {
                _backgroundImages = new List<Image>();
                DirectoryInfo di = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Backgrounds"));
                foreach(var f in di.GetFiles())
                {
                    if (ImageExtensions.Contains(f.Extension.ToUpper()))
                        _backgroundImages.Add(Image.FromFile(f.FullName));
                }

                //mergeImg(_backgroundImages[0], _backgroundImages[0], 0);
                imageslide_Tick(null, null);
                timer1_Tick(null, null);
            }
            catch
            {

            }
        }

        private void LoadFonts()
        {
            System.Drawing.Text.PrivateFontCollection fc = new System.Drawing.Text.PrivateFontCollection();
            fc.AddFontFile("NotoSansCJKkr-Light.otf");
            
            _timeFont = new Font(fc.Families[0], 80f);
            _dateFont = new Font(fc.Families[0], 30f);
            _descFont = new Font(fc.Families[0], 15f);
            _format.LineAlignment = StringAlignment.Center;
            _format.Alignment = StringAlignment.Center;
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

                if (CheckKey(device))
                    Application.Exit();
            }
        }

        private bool CheckKey(string device)
        {
            USBSerial usb = new USBSerial();
            string serial = usb.getSerialNumberFromDriveLetter(device.Substring(0, 2));
            return !_setting.AcceptedSerials.TrueForAll(u => u.SerialNumber != serial); //다 시리얼과 불일치 -> 맞지 않는 키.
        }
        private void LockForm_Shown(object sender, EventArgs e)
        {
            USBStateChanged();
        }

        private void StartSlideShow()
        {
            float merge = 0f;
            Timer fade = new Timer() { Enabled = false, Interval = 20 };
            fade.Tick += (b, d) =>
            {
                mergeImg(_backgroundImages[prevIndex], _backgroundImages[currentIndex], merge);
                merge += 0.1f;
                if (merge >= 1f)
                {
                    fade.Stop();
                }
            };
            fade.Start();
        }

        private void mergeImg(Image FromImage, Image ToImage, float opacity)
        {
            Bitmap back;
            mergeImg(FromImage, ToImage, opacity, out back);
            this.CreateGraphics().DrawImage(back, 0, 0);
            back.Dispose();
        }

        private void mergeImg(Image FromImage, Image ToImage, float opacity, out Bitmap output)
        {
            output = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(output))
            {
                g.Clear(BackColor);

                //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                if (_setting.IsShowSlideShow)
                {
                    ColorMatrix matrix1 = new ColorMatrix();
                    matrix1.Matrix33 = 1 - opacity;
                    ImageAttributes attributes1 = new ImageAttributes();
                    attributes1.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);


                    ColorMatrix matrix2 = new ColorMatrix();
                    matrix2.Matrix33 = opacity;
                    ImageAttributes attributes2 = new ImageAttributes();
                    attributes2.SetColorMatrix(matrix2, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    g.DrawImage(FromImage, new Rectangle(0, 0, this.Width, this.Height), 0, 0, FromImage.Width,
                                         FromImage.Height, GraphicsUnit.Pixel, attributes1);
                    g.DrawImage(ToImage, new Rectangle(0, 0, this.Width, this.Height), 0, 0, ToImage.Width,
                                             ToImage.Height, GraphicsUnit.Pixel, attributes2);
                }
                else
                {
                    g.DrawImage(_backgroundImages[0], 0, 0);
                }
            }
        }

        private void DrawLabels(Graphics g)
        {
            int width = Screen.PrimaryScreen.Bounds.Width;

            g.Clear(BackColor);
            //g.DrawImage()
            g.DrawString(_timeText, _timeFont, Brushes.White, new PointF(width / 2, 260), _format);
            g.DrawString(_dateText, _dateFont, Brushes.White, new PointF(width / 2, 350), _format);
            g.DrawString(_descText, _descFont, Brushes.White, new PointF(width / 2, Height - 150), _format);
        }
        
        private void imageslide_Tick(object sender, EventArgs e)
        {
            prevIndex = currentIndex;
            currentIndex++;
            if (currentIndex == _backgroundImages.Count) currentIndex = 0;

            Bitmap buffer = new Bitmap(1, 1);
            lock (ImageExtensions)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    float merge = 0f;
                    while (merge < 1)
                    {
                        try
                        {
                            mergeImg(_backgroundImages[prevIndex], _backgroundImages[currentIndex], merge, out buffer);
                            merge += 0.03f;

                            this.CreateGraphics().DrawImage(buffer, 0, 0);
                        }
                        catch { }
                        finally
                        {
                            buffer.Dispose();
                            System.Threading.Tasks.Task.Delay(10);
                        }
                    }
                });
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _timeText = DateTime.Now.ToString("hh:mm:ss");
            _dateText = DateTime.Now.ToString("MMMM d, dddd");
            
            lock (ImageExtensions)
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    float merge = 0f;
                    while (merge < 1)
                    {
                        try
                        {
                            using (Bitmap b = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                            using (Graphics g = Graphics.FromImage(b))
                            {
                                DrawLabels(g);
                                this.CreateGraphics().DrawImage(b, 0, 0);
                            }
                        }
                        catch { }
                    }
                });
            }
        }
    }
}
