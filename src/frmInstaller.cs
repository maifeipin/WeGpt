using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;

namespace WEBGPT
{
    public partial class frmInstaller : Form
    {

        public static frmInstaller gui;
        public frmInstaller()
        {
            InitializeComponent();
            gui = this;
            this.label1.Visible = false;
        }
        public static class InstallCheck
        {
            public static bool IsInstallWebview2()
            {
                string res = "";
                try
                {
                    string psnamex64 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebView2Loader_x64.dll");
                    string runFoldx64 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "runtimes", "win-x64", "native");
                    if (File.Exists(psnamex64))
                    {
                        if (!Directory.Exists(runFoldx64))
                        {
                            Directory.CreateDirectory(runFoldx64);
                            File.Copy(psnamex64, Path.Combine(runFoldx64, "WebView2Loader.dll"));
                        }
                        if (!File.Exists(Path.Combine(runFoldx64, "WebView2Loader.dll")))
                        {
                            File.Copy(psnamex64, Path.Combine(runFoldx64, "WebView2Loader.dll"));
                        }
                    }
                    string psnamex86 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebView2Loader_x86.dll");
                    string runFoldx86 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "runtimes", "win-x86", "native");
                    if (File.Exists(psnamex86))
                    {
                        if (!Directory.Exists(runFoldx86))
                        {
                            Directory.CreateDirectory(runFoldx86);
                        }
                        if (!File.Exists(Path.Combine(runFoldx86, "WebView2Loader.dll")))
                        {
                            File.Copy(psnamex86, Path.Combine(runFoldx86, "WebView2Loader.dll"));
                        }
                    }
                    res = CoreWebView2Environment.GetAvailableBrowserVersionString();
                    if (!string.IsNullOrEmpty(res)) return true;
                    return IsWebView2RuntimeInstalled();
                }
                catch (System.Exception)
                {
                }
                if (res == "" || res == null)
                {
                    return false;
                }
                return true;
            }

            public static bool IsWebView2RuntimeInstalled()
            {
                string keyPath1 = @"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";
                string keyPath2 = @"Software\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";

                if (Registry.LocalMachine.OpenSubKey(keyPath1) != null || Registry.CurrentUser.OpenSubKey(keyPath2) != null)
                {
                    return true;
                }
                return false;
            }
            public static void SetAccessControl()
            {
                try
                {
                    // 获取当前运行目录的完整路径
                    string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    // 获取目录的访问控制列表
                    DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);
                    DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();

                    // 添加Users组的写入权限
                    SecurityIdentifier usersGroup = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);
                    directorySecurity.AddAccessRule(new FileSystemAccessRule(usersGroup, FileSystemRights.Write, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));

                    // 应用修改后的访问控制列表
                    directoryInfo.SetAccessControl(directorySecurity);

                    Console.WriteLine("写入权限已成功添加到当前运行目录的Users组。");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发生错误: {ex.Message}");
                }
            }
            public static async Task InstallWebview2Async()
            {
                if (!IsInstallWebview2())
                {
                    gui.Text = "正在下载安装必须组件";
                    gui.label1.Visible = true;
                    gui.label1.Text = "正在下载安装必须组件";
                    string MicrosoftEdgeWebview2Setup = Path.Combine(Application.StartupPath, "MicrosoftEdgeWebview2Setup.exe");
                    if (!File.Exists(MicrosoftEdgeWebview2Setup))
                    {
                        try
                        {
                            using (var webClient = new WebClient())
                            {
                                bool isDownload = false;
                                webClient.DownloadProgressChanged += (s, e) =>
                                {
                                    // Update progress here
                                    int percentage = (int)(((double)e.BytesReceived / e.TotalBytesToReceive) * 100);
                                    gui.label1.Text = $"正在下载安装必须组件... {percentage}%";
                                };

                                webClient.DownloadFileCompleted += (s, e) => { isDownload = true; };
                                await webClient.DownloadFileTaskAsync("https://go.microsoft.com/fwlink/p/?LinkId=2124703", MicrosoftEdgeWebview2Setup);

                                if (isDownload)
                                {
                                    Install(MicrosoftEdgeWebview2Setup);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle download exception
                            MessageBox.Show($"下载失败: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Install(MicrosoftEdgeWebview2Setup);
                    }
                }
            }
            private static void Install(string MicrosoftEdgeWebview2Setup)
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = MicrosoftEdgeWebview2Setup,
                    UseShellExecute = true
                    //Arguments = "/silent /install",
                    //UseShellExecute = false,
                    //CreateNoWindow = true
                };

                try
                {
                    using (Process process = new Process { StartInfo = psi })
                    {
                        process.Start();
                        process.WaitForExit();
                    }

                    var mainForm = Application.OpenForms["MainForm"];
                    if (mainForm != null)
                        mainForm.Close();
                    else
                    {
                        mainForm = Application.OpenForms["ProjectForm"];
                        if (mainForm != null)
                            mainForm.Close();
                    }
                    // Delete the setup file if it exists
                    if (File.Exists(MicrosoftEdgeWebview2Setup))
                    {
                        File.Delete(MicrosoftEdgeWebview2Setup);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"安装失败: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            public static void DeleteWebView2Folder()
            {
                string webview2Dir = $"{System.Environment.GetCommandLineArgs()[0]}.WebView2";
                if (Directory.Exists(webview2Dir))
                {
                    Directory.Delete(webview2Dir, true);
                }
            }
        }
    }


}
