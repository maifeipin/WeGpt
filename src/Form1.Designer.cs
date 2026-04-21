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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDebug = new System.Windows.Forms.Button();
            this.lblBetterIP = new System.Windows.Forms.Label();
            this.cbNetRouter = new System.Windows.Forms.ComboBox();
            this.btnAddFavorit = new System.Windows.Forms.Button();
            this.btnMngUrl = new System.Windows.Forms.Button();
            this.btnGoTo = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDebug);
            this.panel1.Controls.Add(this.lblBetterIP);
            this.panel1.Controls.Add(this.cbNetRouter);
            this.panel1.Controls.Add(this.btnAddFavorit);
            this.panel1.Controls.Add(this.btnMngUrl);
            this.panel1.Controls.Add(this.btnGoTo);
            this.panel1.Controls.Add(this.txtUrl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1365, 46);
            this.panel1.TabIndex = 0;
            // 
            // btnDebug
            // 
            this.btnDebug.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDebug.Location = new System.Drawing.Point(1320, 0);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(45, 46);
            this.btnDebug.TabIndex = 8;
            this.btnDebug.Text = "dbg";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // lblBetterIP
            // 
            this.lblBetterIP.AutoSize = true;
            this.lblBetterIP.Location = new System.Drawing.Point(1232, 20);
            this.lblBetterIP.Name = "lblBetterIP";
            this.lblBetterIP.Size = new System.Drawing.Size(0, 15);
            this.lblBetterIP.TabIndex = 7;
            // 
            // cbNetRouter
            // 
            this.cbNetRouter.FormattingEnabled = true;
            this.cbNetRouter.Location = new System.Drawing.Point(1055, 11);
            this.cbNetRouter.Name = "cbNetRouter";
            this.cbNetRouter.Size = new System.Drawing.Size(143, 25);
            this.cbNetRouter.TabIndex = 6;
            this.cbNetRouter.SelectedIndexChanged += new System.EventHandler(this.cbNetRouter_SelectedIndexChanged);
            // 
            // btnAddFavorit
            // 
            this.btnAddFavorit.Location = new System.Drawing.Point(845, 4);
            this.btnAddFavorit.Name = "btnAddFavorit";
            this.btnAddFavorit.Size = new System.Drawing.Size(67, 35);
            this.btnAddFavorit.TabIndex = 5;
            this.btnAddFavorit.Text = "收藏";
            this.btnAddFavorit.UseVisualStyleBackColor = true;
            this.btnAddFavorit.Click += new System.EventHandler(this.btnAddFavorit_Click);
            // 
            // btnMngUrl
            // 
            this.btnMngUrl.Location = new System.Drawing.Point(918, 4);
            this.btnMngUrl.Name = "btnMngUrl";
            this.btnMngUrl.Size = new System.Drawing.Size(116, 35);
            this.btnMngUrl.TabIndex = 4;
            this.btnMngUrl.Text = "管理收藏夹";
            this.btnMngUrl.UseVisualStyleBackColor = true;
            this.btnMngUrl.Click += new System.EventHandler(this.btnMngUrl_Click);
            // 
            // btnGoTo
            // 
            this.btnGoTo.Location = new System.Drawing.Point(714, 3);
            this.btnGoTo.Name = "btnGoTo";
            this.btnGoTo.Size = new System.Drawing.Size(66, 35);
            this.btnGoTo.TabIndex = 2;
            this.btnGoTo.Text = "转动";
            this.btnGoTo.UseVisualStyleBackColor = true;
            this.btnGoTo.Click += new System.EventHandler(this.btnGoTo_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(57, 11);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(651, 25);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "https://chat.openai.com/";
            this.txtUrl.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "网址：";
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 46);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1365, 570);
            this.webView21.TabIndex = 1;
            this.webView21.ZoomFactor = 1D;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1365, 616);
            this.Controls.Add(this.webView21);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "AI专用浏览器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGoTo;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Button btnMngUrl;
        private System.Windows.Forms.Button btnAddFavorit;
        private System.Windows.Forms.ComboBox cbNetRouter;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblBetterIP;
        private System.Windows.Forms.Button btnDebug;
    }
}

