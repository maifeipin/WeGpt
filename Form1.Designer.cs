namespace WEBGPT
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnCapture = new System.Windows.Forms.Button();
            this.cbNetRouter = new System.Windows.Forms.ComboBox();
            this.btnAddFavorit = new System.Windows.Forms.Button();
            this.btnMngUrl = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnProxyManger = new System.Windows.Forms.Button();
            this.lblBetterIP = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnExportCookie = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(936, 0);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(0);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(54, 25);
            this.btnCapture.TabIndex = 9;
            this.btnCapture.Text = "截图";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // cbNetRouter
            // 
            this.cbNetRouter.FormattingEnabled = true;
            this.cbNetRouter.Location = new System.Drawing.Point(775, 3);
            this.cbNetRouter.Name = "cbNetRouter";
            this.cbNetRouter.Size = new System.Drawing.Size(156, 23);
            this.cbNetRouter.TabIndex = 6;
            this.cbNetRouter.SelectedIndexChanged += new System.EventHandler(this.cbNetRouter_SelectedIndexChanged);
            // 
            // btnAddFavorit
            // 
            this.btnAddFavorit.Location = new System.Drawing.Point(561, 0);
            this.btnAddFavorit.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddFavorit.Name = "btnAddFavorit";
            this.btnAddFavorit.Size = new System.Drawing.Size(67, 26);
            this.btnAddFavorit.TabIndex = 5;
            this.btnAddFavorit.Text = "收藏";
            this.btnAddFavorit.UseVisualStyleBackColor = true;
            this.btnAddFavorit.Click += new System.EventHandler(this.btnAddFavorit_Click);
            // 
            // btnMngUrl
            // 
            this.btnMngUrl.Location = new System.Drawing.Point(649, 0);
            this.btnMngUrl.Margin = new System.Windows.Forms.Padding(0);
            this.btnMngUrl.Name = "btnMngUrl";
            this.btnMngUrl.Size = new System.Drawing.Size(116, 25);
            this.btnMngUrl.TabIndex = 4;
            this.btnMngUrl.Text = "管理收藏夹";
            this.btnMngUrl.UseVisualStyleBackColor = true;
            this.btnMngUrl.Click += new System.EventHandler(this.btnMngUrl_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(458, 0);
            this.btnGoTo.Margin = new System.Windows.Forms.Padding(0);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(54, 26);
            this.btnGoTo.TabIndex = 2;
            this.btnGoTo.Text = "转动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.FormattingEnabled = true;
            this.txtUrl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.txtUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtUrl.Location = new System.Drawing.Point(63, 3);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(392, 23);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "https://chat.openai.com/";
            this.txtUrl.SelectedIndexChanged += new System.EventHandler(this.txtUrl_SelectedIndexChanged);
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "网址：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(3, 35);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1359, 582);
            this.webView21.TabIndex = 1;
            this.webView21.ZoomFactor = 1D;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 360000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.webView21, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1365, 616);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 11;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 398F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.lblBetterIP, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtUrl, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnCapture, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGoTo, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAddFavorit, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMngUrl, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 10, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnProxyManger, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExportCookie, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbNetRouter, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1359, 26);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnProxyManger
            // 
            this.btnProxyManger.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProxyManger.Location = new System.Drawing.Point(1204, 0);
            this.btnProxyManger.Margin = new System.Windows.Forms.Padding(0);
            this.btnProxyManger.Name = "btnProxyManger";
            this.btnProxyManger.Size = new System.Drawing.Size(100, 26);
            this.btnProxyManger.TabIndex = 15;
            this.btnProxyManger.Text = "设置";
            this.btnProxyManger.UseVisualStyleBackColor = true;
            this.btnProxyManger.Click += new System.EventHandler(this.btnProxyManger_Click);
            // 
            // lblBetterIP
            // 
            this.lblBetterIP.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBetterIP.AutoSize = true;
            this.lblBetterIP.Location = new System.Drawing.Point(999, 5);
            this.lblBetterIP.Name = "lblBetterIP";
            this.lblBetterIP.Size = new System.Drawing.Size(63, 15);
            this.lblBetterIP.TabIndex = 14;
            this.lblBetterIP.Text = "preping";
            this.lblBetterIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBetterIP.Click += new System.EventHandler(this.lblBetterIP_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnDebug);
            this.panel2.Location = new System.Drawing.Point(1304, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(55, 26);
            this.panel2.TabIndex = 10;
            // 
            // btnDebug
            // 
            this.btnDebug.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDebug.Location = new System.Drawing.Point(9, 0);
            this.btnDebug.Margin = new System.Windows.Forms.Padding(0);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(46, 26);
            this.btnDebug.TabIndex = 9;
            this.btnDebug.Text = "dbg";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnExportCookie
            // 
            this.btnExportCookie.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportCookie.Location = new System.Drawing.Point(1080, 0);
            this.btnExportCookie.Margin = new System.Windows.Forms.Padding(0);
            this.btnExportCookie.Name = "btnExportCookie";
            this.btnExportCookie.Size = new System.Drawing.Size(114, 26);
            this.btnExportCookie.TabIndex = 11;
            this.btnExportCookie.Text = "导出 Cookie";
            this.btnExportCookie.UseVisualStyleBackColor = true;
            this.btnExportCookie.Click += new System.EventHandler(this.btnExportCookie_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 616);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AI专用浏览器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.ComboBox txtUrl;
        private System.Windows.Forms.Label label1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Button btnMngUrl;
        private System.Windows.Forms.Button btnAddFavorit;
        private System.Windows.Forms.ComboBox cbNetRouter;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExportCookie;
        private System.Windows.Forms.Label lblBetterIP;
        private System.Windows.Forms.Button btnProxyManger;
    }
}

