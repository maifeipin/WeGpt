using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WEBGPT.Properties
{
    public partial class WebViewer : Form
    { 
        public WebViewer(string uri,string clienProxy)
        {
            InitializeComponent(); 
            this.WindowState  = FormWindowState.Maximized;
            InitializeAsync(uri,clienProxy);
        } 
        async void InitializeAsync(string uri, string clienProxy)
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
                    
                    if (webView21 != null)
                    {
                        if (webView21 != null)
                        {
                            CoreWebView2EnvironmentOptions Options = new CoreWebView2EnvironmentOptions();
                            Options.AdditionalBrowserArguments = clienProxy;
                            CoreWebView2Environment env =
                                await CoreWebView2Environment.CreateAsync(null, null, Options);
                            await webView21.EnsureCoreWebView2Async(env);
                            webView21.Source = new Uri(uri, UriKind.Absolute); 

                            // 订阅导航开始事件
                            webView21.CoreWebView2.NavigationStarting += (s, e) =>
                            {
                                statusLabel1.Text = "Loading...";
                            };

                            //订阅内容加载事件
                            webView21.CoreWebView2.ContentLoading += (s, e) =>
                            {
                                statusLabel1.Text = "Content is loading";
                                SetPageText();
                            };

                            //订阅导航完成事件
                            webView21.CoreWebView2.NavigationCompleted += (s, e) =>
                            {
                                if (e.IsSuccess == true)
                                { 
                                    statusLabel1.Text = "Load completed";
                                    SetPageText();
                                }
                                else
                                {
                                    statusLabel1.Text = "Load failed. Error code: " + e.WebErrorStatus.ToString();
                                }
                            };

                            // 订阅Web资源请求开始事件
                            webView21.CoreWebView2.WebResourceRequested += (s, e) =>
                            {
                                statusLabel1.Text = "Requesting: " + e.Request.Uri;
                            };
                        

                    }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void SetPageText()
        {
            if (webView21.CoreWebView2 != null && webView21.CoreWebView2.DocumentTitle != null)
                this.Text = webView21.CoreWebView2.DocumentTitle;
        }
         
    }
}
