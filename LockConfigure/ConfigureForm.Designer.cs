namespace LockConfigure
{
    partial class ConfigureForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpKeys = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tpKeys.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpKeys);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(571, 317);
            this.tabControl1.TabIndex = 0;
            // 
            // tpKeys
            // 
            this.tpKeys.Controls.Add(this.listView2);
            this.tpKeys.Controls.Add(this.label1);
            this.tpKeys.Controls.Add(this.label2);
            this.tpKeys.Controls.Add(this.listView1);
            this.tpKeys.Location = new System.Drawing.Point(4, 24);
            this.tpKeys.Name = "tpKeys";
            this.tpKeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpKeys.Size = new System.Drawing.Size(563, 289);
            this.tpKeys.TabIndex = 0;
            this.tpKeys.Text = "열쇠";
            this.tpKeys.UseVisualStyleBackColor = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.groupBox1);
            this.tpSettings.Location = new System.Drawing.Point(4, 24);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(563, 289);
            this.tpSettings.TabIndex = 1;
            this.tpSettings.Text = "추가 설정";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(366, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(388, 335);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(191, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "종료";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(282, 43);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(257, 232);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(282, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 37);
            this.label2.TabIndex = 2;
            this.label2.Text = "저장된 USB";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "이름";
            this.columnHeader1.Width = 71;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "시리얼";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "마지막 사용";
            this.columnHeader3.Width = 111;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "삽입된 USB";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(19, 43);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(257, 232);
            this.listView2.TabIndex = 4;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "드라이브";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 2;
            this.columnHeader5.Text = "시리얼";
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 1;
            this.columnHeader6.Text = "이름";
            this.columnHeader6.Width = 106;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(19, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(242, 266);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "디자인";
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 370);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ConfigureForm";
            this.Text = "설정";
            this.tabControl1.ResumeLayout(false);
            this.tpKeys.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpKeys;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

