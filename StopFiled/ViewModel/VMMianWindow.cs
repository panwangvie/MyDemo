using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StopFiled.ViewModel
{
    public class VMMianWindow: BaseNotify
    {

        private string _filePath;

        public string FilePath { get => _filePath; set { _filePath = value; OnPropertyChanged(); } }

        public ICommand StopCommand
        {
            get 
            {
                if (_stopCommand == null)
                {
                    _stopCommand = new BaseCommand(StopFile);
                }    
                return _stopCommand ;
            }
            set 
            { 
                _stopCommand =value;
                OnPropertyChanged();
            }
        }

        private ICommand _stopCommand;

        private void StopFile()
        {
            KillProcess(FilePath);
        }

        /// <summary>
        /// 结束被使用的进程
        /// </summary>
        /// <param name="filePath">正在使用的文件具体路径</param>
        public bool KillProcess(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            bool res = false;
            try
            {
                Process tool = new Process();
                tool.StartInfo.FileName = "handle64.exe";
                tool.StartInfo.Arguments = fileName + " /accepteula";
                tool.StartInfo.UseShellExecute = false;
                tool.StartInfo.RedirectStandardOutput = true;
                tool.Start();
                string outputTool = tool.StandardOutput.ReadToEnd();
                string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
                foreach (Match match in Regex.Matches(outputTool, matchPattern))
                {
                    Process.GetProcessById(int.Parse(match.Value)).Kill();
                    res = true;
                }
            }
            catch (Exception ex)
            {
              
            }
            return res;
        }

        /// <summary>
        /// 关闭某个应用
        /// </summary>
        /// <param name="processName"></param>
        public void CloseSoundApp(string processName)
        {
            Process[] pProcess;
            pProcess = Process.GetProcesses();
            for (int i = 1; i <= pProcess.Length - 1; i++)
            {
                if (pProcess[i].ProcessName == processName)
                {
                    pProcess[i].Kill();
                    break;
                }
            }

        }
    }
}
