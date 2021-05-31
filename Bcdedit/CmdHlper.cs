
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Bcdedit
{
    /// <summary>
    ///  
    /// </summary>
    public class CMDHelper
    {
        /// <summary>
        /// 执行Cmd命令并返回结果
        /// </summary>
        /// <param name="commands">执行的CMD命名</param>
        /// <returns>返回的结果</returns>
        public static async Task<string> CmdAsync(params string[] commands)
        {
            try
            {
           
            System.Diagnostics.Process startInfo = new System.Diagnostics.Process();
            startInfo.StartInfo.FileName = "cmd.exe";
            startInfo.StartInfo.UseShellExecute = false;
            startInfo.StartInfo.RedirectStandardError = true;
            startInfo.StartInfo.RedirectStandardInput = true;
            startInfo.StartInfo.RedirectStandardOutput = true;
            startInfo.StartInfo.CreateNoWindow = true;
            startInfo.StartInfo.Verb = "runas";
            startInfo.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.Start();
            foreach (var cmd in commands)
            {
                await startInfo.StandardInput.WriteLineAsync(cmd);
            }

            await startInfo.StandardInput.WriteLineAsync("exit");
            startInfo.StandardInput.AutoFlush = true;
            //获取cmd窗口的输出信息
            string output = await startInfo.StandardOutput.ReadToEndAsync();
            startInfo.Close();
            return output;
            }
            catch (Exception e)
            {

            }

            return null;
        }

        public static async Task<string> CmdAsync(string command)
        {
            System.Diagnostics.Process startInfo = new System.Diagnostics.Process();
            startInfo.StartInfo.FileName = "cmd.exe";
            startInfo.StartInfo.UseShellExecute = false;
            startInfo.StartInfo.RedirectStandardError = true;
            startInfo.StartInfo.RedirectStandardInput = true;
            startInfo.StartInfo.RedirectStandardOutput = true;
            startInfo.StartInfo.CreateNoWindow = true;
            startInfo.StartInfo.Verb = "runas";
            startInfo.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.Start();

            await startInfo.StandardInput.WriteLineAsync(command);

            await startInfo.StandardInput.WriteLineAsync("exit");
            startInfo.StandardInput.AutoFlush = true;
            //获取cmd窗口的输出信息
            string output = await startInfo.StandardOutput.ReadToEndAsync();
            startInfo.Close();
            return output;
        }

        /// <summary>
        /// 以管理员身份运行某个文件
        /// </summary>
        /// <param name="programPath">运行的文件路径</param>
        /// <returns></returns>
        public static bool StartAdministrator(string programPath)
        {

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = programPath;
                startInfo.CreateNoWindow = true;
                //设置启动动作,确保以管理员身份运行
                startInfo.Verb = "runas";

                Process myProcess = new Process();
                myProcess.EnableRaisingEvents = false;
                var pc = Process.Start(startInfo);

                return true;
            }
            catch (Exception ex)
            {
                try
                {

                    System.Diagnostics.Process.Start(programPath);
                    return true;
                }
                catch (Exception ex2)
                {
                }
                return false;
            }

        }
        public static bool Start(string programPath)
        {
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = programPath;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.EnableRaisingEvents = false;
                bool boo = myProcess.Start();
                return boo;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 当前用户是管理员的时候，直接启动应用程序
        /// 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
        
            //获得当前登录的Windows用户标示
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            //判断当前登录用户是否为管理员
            if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {

                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsRunProcess(string processName)
        {
            bool result = false;
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.ProcessName.ToUpper() == processName.ToUpper())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static void KillProcess(string monitorProcessName)
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                string processNameStrList = string.Join(",", (ps.Select(p => p.ProcessName).ToArray()));
                Console.WriteLine("系统当前运行全部进程：" + processNameStrList);
                foreach (Process item in ps)
                {
                    string processName = item.ProcessName.ToLower();
                    monitorProcessName = monitorProcessName.ToLower();
                    if (monitorProcessName.Contains(processName))
                    {
                        Console.WriteLine("发现当前运行的进程" + monitorProcessName);
                        if (!item.HasExited)
                        {
                            Console.WriteLine("执行Kill进程" + monitorProcessName);
                            item.Kill();
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public static int AutoRunApplication(string fileName, bool isAutoRun)
        {
            int resset = 0;
            RegistryKey reg = null;
            try
            {
                if (isAutoRun && !System.IO.File.Exists(fileName))//如果是启动 检测软件是否存在
                {
                    resset = -1;
                    Console.WriteLine("软件地址不存在：" + fileName);
                    return resset;
                }
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                {
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    if (reg == null)
                    {
                        Console.WriteLine("设置自启动软件失败：" + fileName);
                        return -1;
                    }
                }
                System.Threading.Thread.Sleep(1000);
                if (isAutoRun)
                {
                    reg.SetValue(name, fileName);
                }
                else
                {
                    reg.DeleteValue(name, false);
                }
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
               
                return -1;
            }
            finally
            {
                if (reg != null)
                {
                    reg.Close();
                }
            }
            Console.WriteLine("设置自启动软件成功：" + fileName);
            return resset;
        }
    }
}
