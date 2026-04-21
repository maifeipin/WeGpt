using Microsoft.Web.WebView2.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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


        private string clientproxy { get; set; } = "--proxy-server=socks5://127.0.0.1:18988";
        public Form1()
        {
            InitializeComponent();
            InitDATA();
            InitializeAsync();
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitDATA()
        {
            connection = new SQLiteConnection("Data Source=ai.db;Version=3;");
            connection.Open();

            LoadData();

            this.timer1.Interval = 300 * 1000;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // 由 ComboBox AutoComplete 原生处理，无需手动过滤
        }

        private void txtUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtUrl.SelectedIndex >= 0)
                btnGoTo_Click(sender, e);
        }

        private void LoadData()
        {
            if (cbNetRouter.Tag != null) return;

            string query = "SELECT url FROM url";
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                txtUrl.Items.Add(reader["url"].ToString());
            reader.Close();

            string query2 = "SELECT * FROM proxyserver";
            SQLiteCommand cmd2 = new SQLiteCommand(query2, connection);
            SQLiteDataReader reader2 = cmd2.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader2);
            reader2.Close();

            cbNetRouter.DisplayMember = "serverName";
            cbNetRouter.ValueMember = "runcmd";
            cbNetRouter.DataSource = dt;
            cbNetRouter.SelectedIndex = 0;
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
            catch (Exception ex)
            {

            }
        }

        private void CoreWebView2_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {

        }

        private async void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            PageTitle = webView21.CoreWebView2.DocumentTitle;
            if (IsExportCookie)
            {
                if (e.IsSuccess)
                {
                    var cookies = await webView21.CoreWebView2.CookieManager.GetCookiesAsync(webView21.Source.AbsoluteUri);
                    if (cookies != null && cookies.Count > 0)
                    {
                        // 提示用户保存文件位置
                        SaveFileDialog saveFileDialog = new SaveFileDialog
                        {
                            Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                            FileName = "cookies.json",
                            Title = "Save Cookies"
                        };

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;
                            // 创建一个 cookie 列表
                            Uri webViewUri = new Uri(webView21.Source.AbsoluteUri);
                            string webViewDomain = webViewUri.Host;
                       
                            
                            List<SiteCookie> cookieList = new List<SiteCookie>();
                            foreach (var cookie in cookies)
                            {
                                long ts  = (long)(cookie.Expires - new DateTime(1970, 1, 1)).TotalSeconds;
                                if (ts < 0) ts = -1;
                                cookieList.Add(new SiteCookie
                                {
                                    name = cookie.Name,
                                    value = cookie.Value,
                                    domain = cookie.Domain,
                                    path = cookie.Path,
                                    expires = ts,
                                    httpOnly = cookie.IsHttpOnly,
                                    secure = cookie.IsSecure,
                                    sameSite = webViewDomain.EndsWith(cookie.Domain) ? 1 : 2
                                });
                            }

                            try
                            {
                                // 将 cookie 列表序列化为 JSON 并保存到文件
                                string jsonContent = JsonConvert.SerializeObject(cookieList, Formatting.Indented);
                                File.AppendAllText(filePath, jsonContent);
                                //System.Diagnostics.Process.Start(Path.GetDirectoryName(filePath));
                                Debug.WriteLine($"Cookies saved to: {filePath}");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"Failed to save cookies: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Navigation failed.");
                    }

                }
            }

        }
        private void btnGoTo_Click(object sender, EventArgs e)
        {
            string uri = txtUrl.Text.Trim();
            if (webView21 == null || uri.Length == 0) return;
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
            if (e.KeyCode == Keys.Enter) { btnGoTo_Click(sender, e); }
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
                string urlStr = webView21?.Source?.AbsoluteUri;
                if (string.IsNullOrEmpty(urlStr))
                    urlStr = txtUrl.Text.Trim();

                if (string.IsNullOrEmpty(urlStr)) return;
                if (!urlStr.StartsWith("http")) urlStr = "https://" + urlStr;

                string title = PageTitle ?? new Uri(urlStr).Host;

                string checkQuery = $"SELECT COUNT(1) FROM url WHERE url='{urlStr}'";
                SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, connection);
                if (connection.State != ConnectionState.Open) connection.Open();
                long count = (long)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    MessageBox.Show("该地址已存在于收藏夹中！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string query = $"INSERT INTO url(url, memo) VALUES('{urlStr}', '{title}')";
                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.ExecuteNonQuery();

                txtUrl.Items.Add(urlStr);
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
            }
            catch (Exception ex) { }
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

        private async void btnCapture_Click(object sender, EventArgs e)
        {
            // 获取 WebView 的屏幕坐标
            Rectangle webViewBounds = webView21.RectangleToScreen(webView21.ClientRectangle);
            Point webViewTop = new Point(
          webViewBounds.Left + webViewBounds.Width / 2,  // 中心位置
          webViewBounds.Top + 10  // 设置为顶部附近，避免鼠标超出屏幕
          );
            // 确保鼠标激活 WebView 控件
            MouseHelper.MoveMouse(webViewTop);
            MouseHelper.MouseClick(webViewTop); // 模拟点击激活
            Thread.Sleep(500); // 确保控件获得焦点

            List<Bitmap> screenshots = new List<Bitmap>();
            Bitmap lastScreenshot = null;

            int scrollStep = -120; // 鼠标滚轮向下滚动的步长（负数向下，正数向上）
            int dragStep = await GetPageHeightAsync(webView21) - 10;    // 模拟拖动的像素步长
            int maxAttempts = 100; // 最大滚动次数，防止死循环

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                // 截图当前屏幕内容
                Bitmap currentScreenshot = await CaptureWebViewAsync(webView21);

                // 如果与上一张截图一致，说明滚动到底，停止操作
                if (lastScreenshot != null && CompareBitmaps(currentScreenshot, lastScreenshot))
                {
                    break;
                }

                // 保存当前截图到列表
                screenshots.Add(currentScreenshot);
                lastScreenshot = currentScreenshot;

                // 模拟滚轮滚动和鼠标拖动组合
                MouseHelper.ScrollWheel(scrollStep, webViewTop); // 滚动一点内容
                Point dragStart = webViewTop;
                Point dragEnd = new Point(dragStart.X, dragStart.Y + dragStep);
                MouseHelper.DragMouse(dragStart, dragEnd); // 拖动剩余的部分

                // 等待内容刷新
                Thread.Sleep(500);
            }

            // 拼接所有截图成一个完整的长截图
            Bitmap finalScreenshot = StitchScreenshots(screenshots);

            // 保存最终截图到文件
            string screenshotFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ScreenShots");
            if (!Directory.Exists(screenshotFolder)) Directory.CreateDirectory(screenshotFolder);

            string fileName = Path.Combine(screenshotFolder, $"{DateTime.Now:yyyyMMdd_HHmmss}.png");
            finalScreenshot.Save(fileName, ImageFormat.Png);

            // 打开保存的截图文件夹
            System.Diagnostics.Process.Start(screenshotFolder);
        }

        private async Task<int> GetPageHeightAsync(Microsoft.Web.WebView2.WinForms.WebView2 webView)
        {
            string script = "document.documentElement.scrollHeight"; // 获取页面的总高度
            var result = await webView.ExecuteScriptAsync(script);
            return int.Parse(result); // 返回页面的高度
        }

        /// <summary>
        /// 比较两张图片是否相同
        /// </summary>
        private bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1.Width != bmp2.Width || bmp1.Height != bmp2.Height)
            {
                return false;
            }

            for (int x = 0; x < bmp1.Width; x++)
            {
                for (int y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 拼接多张截图
        /// </summary>
        private Bitmap StitchScreenshots(List<Bitmap> screenshots)
        {
            int totalHeight = screenshots.Sum(bmp => bmp.Height);
            int width = screenshots[0].Width;

            Bitmap finalImage = new Bitmap(width, totalHeight);
            using (Graphics g = Graphics.FromImage(finalImage))
            {
                int currentHeight = 0;
                List<Bitmap> batch = new List<Bitmap>();

                foreach (Bitmap bmp in screenshots)
                {
                    batch.Add(bmp);
                    if (batch.Count >= 20) // 每20张拼接一次
                    {
                        // 拼接当前batch
                        currentHeight = StitchBatch(g, batch, currentHeight);
                        batch.Clear(); // 清空当前批次

                        // 手动触发垃圾回收
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }

                // 拼接剩余的部分
                if (batch.Count > 0)
                {
                    StitchBatch(g, batch, currentHeight);
                }
            }

            return finalImage;
        }

        // 拼接一个批次
        private int StitchBatch(Graphics g, List<Bitmap> batch, int currentHeight)
        {
            foreach (Bitmap bmp in batch)
            {
                g.DrawImage(bmp, 0, currentHeight);
                currentHeight += bmp.Height;
                bmp.Dispose(); // 及时释放当前图片占用的资源
            }

            return currentHeight;
        }


        /// <summary>
        /// 截取 WebView2 控件的内容
        /// </summary>
        private async Task<Bitmap> CaptureWebViewAsync(Microsoft.Web.WebView2.WinForms.WebView2 webView)
        {
            using (var stream = new MemoryStream())
            {
                // 调用 CoreWebView2 的 CapturePreviewAsync 方法截取当前内容
                await webView.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, stream);

                // 将流转换为 Bitmap 并返回
                stream.Seek(0, SeekOrigin.Begin); // 确保从流的起始位置读取
                return new Bitmap(stream);
            }
        }
        bool IsExportCookie = false;
        private void btnExportCookie_Click(object sender, EventArgs e)
        {
            IsExportCookie = !IsExportCookie;
            btnGoTo_Click(sender, e);
        }

        private void lblBetterIP_Click(object sender, EventArgs e)
        {
           string pcmd =  cbNetRouter.SelectedValue.ToString();
            if (cbNetRouter.Tag != null)
            {
                DataTable dt = cbNetRouter.Tag as DataTable;
                if (dt != null)
                {
                    string expression;
                    expression = $"runcmd='{pcmd}'";
                    string hostIP = dt.Select(expression)[0]["host"].ToString();
                    Clipboard.SetText(hostIP);
                }
            }
        }

        private void btnProxyManger_Click(object sender, EventArgs e)
        {
            ProxyManager proxy = new ProxyManager( );
            proxy.connection = connection;
            proxy.Show();
        }
    }

}
