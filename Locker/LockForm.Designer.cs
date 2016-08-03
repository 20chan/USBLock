namespace Locker
{
    partial class LockForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LockForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbKey = new Locker.ClearLabel();
            this.lbDate = new Locker.ClearLabel();
            this.lbTime = new Locker.ClearLabel();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbKey
            // 
            this.lbKey.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lbKey.AutoSize = true;
            this.lbKey.BackColor = System.Drawing.Color.Transparent;
            this.lbKey.Font = new System.Drawing.Font("Noto Sans CJK KR Thin", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbKey.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbKey.Location = new System.Drawing.Point(411, 624);
            this.lbKey.Name = "lbKey";
            this.lbKey.Size = new System.Drawing.Size(282, 28);
            this.lbKey.TabIndex = 6;
            this.lbKey.Text = "잠금을 해제하려면 열쇠를 넣으세요";
            this.lbKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbKey.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // lbDate
            // 
            this.lbDate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbDate.AutoSize = true;
            this.lbDate.BackColor = System.Drawing.Color.Transparent;
            this.lbDate.Font = new System.Drawing.Font("Noto Sans CJK KR Thin", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbDate.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbDate.Location = new System.Drawing.Point(221, 403);
            this.lbDate.Name = "lbDate";
            this.lbDate.Size = new System.Drawing.Size(300, 78);
            this.lbDate.TabIndex = 5;
            this.lbDate.Text = "clearLabel1";
            this.lbDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbDate.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // lbTime
            // 
            this.lbTime.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTime.AutoSize = true;
            this.lbTime.BackColor = System.Drawing.Color.Transparent;
            this.lbTime.Font = new System.Drawing.Font("Noto Sans CJK KR Thin", 80.24999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbTime.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lbTime.Location = new System.Drawing.Point(209, 261);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(607, 158);
            this.lbTime.TabIndex = 4;
            this.lbTime.Text = "clearLabel1";
            this.lbTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbTime.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // LockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1055, 757);
            this.Controls.Add(this.lbKey);
            this.Controls.Add(this.lbDate);
            this.Controls.Add(this.lbTime);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LockForm";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LockForm_FormClosed);
            this.Shown += new System.EventHandler(this.LockForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private ClearLabel lbTime;
        private ClearLabel lbDate;
        private ClearLabel lbKey;
    }
}

