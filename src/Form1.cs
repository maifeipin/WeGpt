using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WEBGPT
{
    
    public partial class Form1 : Form
    {
        private string PageTitle;
        private bool CreateNoWindow = true;
        private SQLiteConnection connection;
        private CancellationTokenSource cancellationTokenSource;
        private AutoCompleteStringCollection autoCompleteCollection;


        private string clientproxy { get; set; } = "--proxy-server=socks5://127.0.0.1:18988";
        public Form1()
        {
            InitializeComponent();
            InitDATA();
            InitializeAsync();
        }

        private void InitDATA()
        {
            connection = new SQLiteConnection("Data Source=ai.db;Version=3;");
            connection.Open();
             
            autoCompleteCollection = new AutoCompleteStringCollection();
            txtUrl.AutoCompleteCustomSource = autoCompleteCollection;
            txtUrl.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtUrl.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // ensure ComboBox allows manual input
            try { txtUrl.DropDownStyle = ComboBoxStyle.DropDown; } catch { }
            LoadData();

            this.timer1.Interval =300*1000;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 清空自动完成的下拉列表
            txtUrl.AutoCompleteCustomSource.Clear();

            // 重新从数据库加载匹配项
            string query = $"SELECT * FROM url WHERE url LIKE '%{txtUrl.Text}%'";
            SQLiteCommand cmd = new SQLiteCommand(query, connection); 
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtUrl.AutoCompleteCustomSource.Add(reader["url"].ToString());
            }

            reader.Close();
        }
        private void LoadData()
        {
            if (cbNetRouter.Tag != null) return;
            string query = "SELECT * FROM url";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                autoCompleteCollection.Add(reader["url"].ToString());
                // also add to ComboBox items so dropdown shows favorites
                try { txtUrl.Items.Add(reader["url"].ToString()); } catch { }
            }

            reader.Close();

            query = "SELECT * FROM proxyserver";
            cmd = new SQLiteCommand(query, connection);
            reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader); 
            reader.Close();

            cbNetRouter.DisplayMember = "serverName";
            cbNetRouter.ValueMember = "runcmd";
            cbNetRouter.DataSource = dt;
            if (cbNetRouter.Items.Count>0) cbNetRouter.SelectedIndex = 0;
            cbNetRouter.Tag = dt;
        }

        async void InitializeAsync()
        {
            try
            {
                bool bExists = frmInstaller.InstallCheck.IsInstallWebview2();
                if (!bExists)
                {
                    var dlg = MessageBox.Show(this, "缺少浏览器组件,是否现在安装,安装后要重新登录才能生效", "安装组件", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dlg == DialogResult.Cancel) return;
                    await frmInstaller.InstallCheck.InstallWebview2Async();
                }
                bExists = frmInstaller.InstallCheck.IsInstallWebview2();
                if (bExists)
                {
                   StartAgent(cbNetRouter.SelectedValue.ToString());
                    if (webView21 != null)
                    {
                        if (webView21 != null)
                        {
                            CoreWebView2EnvironmentOptions Options = new CoreWebView2EnvironmentOptions();
                            Options.AdditionalBrowserArguments = clientproxy; 
                            CoreWebView2Environment env =
                                await CoreWebView2Environment.CreateAsync(null, null, Options);
                            await webView21.EnsureCoreWebView2Async(env);
                            webView21.Source = new Uri("https://chat.openai.com/", UriKind.Absolute);
                            webView21.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                            webView21.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void CoreWebView2_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
             
        }

        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            PageTitle = webView21.CoreWebView2.DocumentTitle;
        }


        private void btnGoTo_Click(object sender, EventArgs e)
        {
            string uri = txtUrl.Text.Trim();
            if (webView21 == null|| uri.Length==0) return;
            try
            {
                if (!uri.StartsWith("http"))
                { 
                    uri = "https://" + uri;
                }
                webView21.CoreWebView2.Navigate(uri);
            }
            catch (Exception ex) { }
        }

     
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) { btnGoTo_Click(sender, e); } 
        }

        private void btnMngUrl_Click(object sender, EventArgs e)
        {
            DbManager dbManager = new DbManager(connection);
            dbManager.ClientProxy = clientproxy;
            dbManager.Show();
        }

        private void btnAddFavorit_Click(object sender, EventArgs e)
        {
            try
            {
                // allow adding current webview url or manual input url
                string urlToAdd = null;
                if (webView21?.Source != null)
                {
                    urlToAdd = webView21.Source.AbsoluteUri;
                }
                else
                {
                    urlToAdd = txtUrl.Text.Trim();
                }
                if (string.IsNullOrEmpty(urlToAdd)) return;

                string title = PageTitle == null ? (new Uri(urlToAdd)).Host : PageTitle;

                // check if exists
                string checkQuery = $"SELECT COUNT(1) FROM url WHERE url='{urlToAdd.Replace("'","''")}'";
                SQLiteCommand chkCmd = new SQLiteCommand(checkQuery, connection);
                object o = chkCmd.ExecuteScalar();
                int exists = 0;
                if (o != null && int.TryParse(o.ToString(), out exists) && exists > 0)
                {
                    MessageBox.Show("收藏已存在");
                    return;
                }

                string query = $" INSERT INTO url(url, memo) VALUES ('{urlToAdd.Replace("'","''")}', '{title.Replace("'","''")}') ";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                if (connection.State != ConnectionState.Open) connection.Open();
                cmd.ExecuteNonQuery();

                // update memo just in case
                query = $" \n\r UPDATE url set memo=  '{title.Replace("'","''")}' WHERE  url='{urlToAdd.Replace("'","''")}' ";
                cmd = new SQLiteCommand(query, connection);
                if (connection.State != ConnectionState.Open) connection.Open();
                cmd.ExecuteNonQuery();

                // update UI lists
                try
                {
                    if (!autoCompleteCollection.Contains(urlToAdd)) autoCompleteCollection.Add(urlToAdd);
                }
                catch { }
                try
                {
                    if (!txtUrl.Items.Contains(urlToAdd)) txtUrl.Items.Add(urlToAdd);
                }
                catch { }

                MessageBox.Show("添加成功！");
            }
            catch (Exception ex) 
            {
                MessageBox.Show("添加出错：" + ex.Message);
            }
        }

        private void cbNetRouter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pcmd = cbNetRouter.SelectedValue.ToString();
            KillAgent();
            StartAgent(pcmd);
            if (webView21.CoreWebView2 != null)
                webView21.CoreWebView2.Navigate(txtUrl.Text);
        }

        private void StartAgent(string pcmd)
        {
            KillAgent();
            string pname = pcmd.Split(' ')[0];
            if (cbNetRouter.Tag != null)
            {
                DataTable dt = cbNetRouter.Tag as DataTable;
                if (dt != null)
                {
                    string expression;
                    expression = $"runcmd='{pcmd}'";
                    clientproxy = dt.Select(expression)[0]["clientproxy"].ToString();
                }
            }
            else
            {
                LoadData();
            }
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Path.Combine(Application.StartupPath, $"{pname}.exe"),
                Arguments = pcmd.Replace(pname, ""),
                //RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = CreateNoWindow
            };

            try
            {
                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();
                    //process.WaitForExit();
                }
            }
            catch (Exception ex) { }
        }

        private void KillAgent()
        {
            var ps = Process.GetProcesses();
            try
            {
                foreach (Process p in ps)
                {
                    if (p.ProcessName == "brook" ||
                        p.ProcessName == "naive")
                        p.Kill();
                }
            }catch (Exception ex) { }
        } 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            KillAgent();
        }

      
        private void btnDebug_Click(object sender, EventArgs e)
        {
            CreateNoWindow = !CreateNoWindow;
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            // 如果已经有任务在执行，取消它
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
            cancellationTokenSource = new CancellationTokenSource();
            if (cbNetRouter.Tag == null) return;
            DataTable dataTable = cbNetRouter.Tag as DataTable;
            if (dataTable == null) return;

            var hostList = from DataRow row in dataTable.Rows select row["host"].ToString();
            try
            {
                await Task.Run(async () =>
                {
                    string beterip = await BetterServerIP.GetBestDomainAsync(hostList.ToArray());
                    if (beterip == null) return;
                    string expression;
                    expression = $"host='{beterip}'";
                    string servername = dataTable.Select(expression)[0]["servername"].ToString();
                    if (lblBetterIP.InvokeRequired)
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblBetterIP.Text = servername;
                        }));
                    }
                    else
                    {
                        this.lblBetterIP.Text = servername;
                    }
                }, cancellationTokenSource.Token);
            }
            catch (Exception ex) { }
        }

        private void txtUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // when user selects item from dropdown, navigate
            try
            {
                string uri = txtUrl.SelectedItem?.ToString() ?? txtUrl.Text;
                if (string.IsNullOrEmpty(uri)) return;
                if (!uri.StartsWith("http")) uri = "https://" + uri;
                if (webView21?.CoreWebView2 != null) webView21.CoreWebView2.Navigate(uri);
            }
            catch { }
        }

    }

}
