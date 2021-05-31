
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BcdeditDemo;


namespace Bcdedit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // WMIQuery.Query();
          

        }

        private void GetStdOSEntries()
        {
            
             Dictionary<string, string> StdOSEntries = BCDWMI.EnumerateObjectsByType(BCDWMI.BCDE_STANDARD_OS_ENTRY, String.Empty);
            // Dictionary<string, string> StdOSEntries = BCDWMI.EnumerateObjectsByType((uint)BcdBootMgrElementTypes.BcdBootMgrObjectList_BootSequence, String.Empty);
            foreach (String guid in StdOSEntries.Keys)
            {
                this.tbRes.Text += String.Format("Id={0} {1}", guid, StdOSEntries[guid]) + "\n";

                Debug.WriteLine(String.Format("Id={0} {1}", guid, StdOSEntries[guid]));
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private BcdElement bcdElementWindows=new BcdElement(){};
        private BcdElement bcdElementAndroid=new BcdElement(){};
        private async void UIElement_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var  strCmd =this.tbCmd.Text.Trim();
                var result=await CMDHelper.CmdAsync(strCmd);
                this.tbRes.Text += strCmd + "\n";
                this.tbRes.Text += result + "\n";
               bcdElementWindows.Device=AnalysisResult(result, BcdeditCommand.deviceStr);
               bcdElementWindows.Path=AnalysisResult(result, BcdeditCommand.pathStr);
               bcdElementWindows.Description = AnalysisResult(result, BcdeditCommand.descriptionStr);
            }
        }

        private async void SetBcdWin()
        {
            string setBootmgrStr = @"bcdedit /set {bootmgr}";
            string SetEFUI = @"bcdedit /set {4e542f75-bf4f-11eb-997f-d99b009e83e4}";
            var ss = $"{SetEFUI} {BcdeditCommand.deviceStr} {bcdElementWindows.Device}";
            await  CmdAsync( 
                $"{setBootmgrStr} {BcdeditCommand.deviceStr} {bcdElementAndroid.Device}", 
                $"{setBootmgrStr} {BcdeditCommand.pathStr} {bcdElementAndroid.Path}", 
                $"{setBootmgrStr} {BcdeditCommand.descriptionStr} \"{bcdElementAndroid.Description}\"",
                $"{SetEFUI} {BcdeditCommand.deviceStr} {bcdElementWindows.Device}",
                $"{SetEFUI} {BcdeditCommand.pathStr} {bcdElementWindows.Path}",
                $"{SetEFUI} {BcdeditCommand.descriptionStr} \"{bcdElementWindows.Description}\""
                );
        }

        /// <summary>
        /// 设置启动引导参数
        /// </summary>
        /// <param name="bcd"></param>
        /// <returns></returns>
        public async Task SetBcdEdit(BcdElement bcd, string id = "")
        {
            //若是传入id则修改id的启动
            id = string.IsNullOrWhiteSpace(id) ? bcd.Id : id;

            string setBootmgrStr = @"bcdedit /set {" + id + "}";
            await CmdAsync(
                $"{setBootmgrStr} {BcdeditCommand.deviceStr} {bcd.Device}",
                $"{setBootmgrStr} {BcdeditCommand.pathStr} {bcd.Path}",
                $"{setBootmgrStr} {BcdeditCommand.descriptionStr} \"{bcd.Description}\"");
        }

        /// <summary>
        /// 设置默认安卓引导和windows引导的启动项
        /// </summary>
        private async Task Init()
        {
            //todo 是否通过配置文件保存？
            var bcdWindows = new BcdElement()
            {
                Description = BcdeditCommand.osWindows,
                Path = @"\EFI\MICROSOFT\BOOT\BOOTMGFW.EFI",
                Device = @"partition=\Device\HarddiskVolume17"
            };


            var bcdAndroid = new BcdElement()
            {
                Description = BcdeditCommand.UEFIOS,
                Path = @"\EFI\BOOT\BOOTX64.EFI",
                Device = @"partition=\Device\HarddiskVolume1"
            };

            var copyId =await BcdEditCopy("bootmgr", BcdeditCommand.osWindows);

            await SetBcdEdit(bcdWindows, copyId);
            await SetBcdEdit(bcdAndroid, "bootmgr");
        }

        /// <summary>
        /// 获取安卓的启动信息
        /// </summary>
        /// <param name="strCmd"></param>
        private async void GetBcdWin(string strCmd)
        {
            var result =await CmdAsync(strCmd);
            bcdElementWindows.Device = AnalysisResult(result, "device");
            bcdElementWindows.Path = AnalysisResult(result, "path");
            bcdElementWindows.Description = AnalysisResult(result, "description");
        }
        private async void GetBcdAndroid(string strCmd)
        {
            var result = await CmdAsync(strCmd);

            bcdElementAndroid.Device = AnalysisResult(result, "device");
            bcdElementAndroid.Path = AnalysisResult(result, "path");
            bcdElementAndroid.Description = AnalysisResult(result, "description");
        }
   
        


        /// <summary>
        /// 通过Bcdedit的copy命令返回的启动器标识符
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private async Task<string> BcdEditCopy(string copyId, string description)
        {

            var copyCmd = "bcdedit /copy {" + copyId + "} -d \"" + description + "\"";
            var cmdResult = await CmdAsync(copyCmd);
            try
            {

                var result = cmdResult.Substring(cmdResult.IndexOf('{')+1, cmdResult.IndexOf('}') - cmdResult.IndexOf('{') - 1);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        /// <summary>
        /// 解析对应字段的内容
        /// </summary>
        /// <param name="cmdResult"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string AnalysisResult(string cmdResult, string value, string cmd = "")
        {
            var str = cmdResult.Remove(0, cmdResult.IndexOf(value, StringComparison.Ordinal) + value.Length);

            var result = str.Substring(0, str.IndexOf('\r') < 0 ? str.IndexOf('\r') : str.IndexOf('\n')).Trim();

            return result;
        }

        private string AnalysisResultId(string cmdResult, string cmd)
        {
            var str = cmdResult.Remove(0, cmdResult.IndexOf(cmd, StringComparison.Ordinal) + cmd.Length);

            var result = str.Substring(str.IndexOf('{')+1, str.IndexOf('}') - str.IndexOf('{')-1);

            return result;
        }

        private async Task<string> CmdAsync(params string[] strCmd)
        {
            var result =await CMDHelper.CmdAsync(strCmd);
            this.tbRes.Text += strCmd + "\r\n\r\n";
            this.tbRes.Text += result + "\r\n\r\n";
            //去除执行的命令返回的前面的部分
            //var start = result.IndexOf(BcdeditCommand.enumBootmgr, StringComparison.Ordinal) +
            //            BcdeditCommand.enumBootmgr.Length;
            //result = result.Remove(0, start);
            return result;
        }

        /// <summary>
        /// 执行Cmd命令，
        /// </summary>
        /// <param name="strCmd">执行的命令</param>
        /// <returns></returns>
        private async Task<string> CmdAsync(string strCmd)
        {
            var result = await CMDHelper.CmdAsync(strCmd);

            //todo 记录日志
            this.tbRes.Text += strCmd + "\r\n\r\n";
            this.tbRes.Text += result + "\r\n\r\n";
            //去除执行的命令返回的前面的部分
            var start = result.IndexOf(strCmd, StringComparison.Ordinal) + strCmd.Length;
            result = result.Remove(0, start).Trim();
            return result;
        }


        private async void BtnQuery_OnClick(object sender, RoutedEventArgs e)
        {
            //var a = System.Windows.Forms.MessageBox.Show("a");

            await CmdAsync(BcdeditCommand.enumAll);

        }

        private void BtnShutdown_OnClick(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("是否关机?");

            if (res == MessageBoxResult.OK)
            {
                CmdAsync(BcdeditCommand.shutdownStr);
            }

        }

        private void BtnRes_OnClick(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("是否重启?");
            if (res == MessageBoxResult.OK)
            {
                CmdAsync(BcdeditCommand.restartStr);

            }
        }

        private void BtnSwitching_OnClick(object sender, RoutedEventArgs e)
        {
            BtnTest2_OnClick(null,null);
        }

        

        private void BtnSwitchingRes_OnClick(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("是否重启?");
            if (res == MessageBoxResult.OK)
            {
                GetBcdAndroid(BcdeditCommand.enumEFUI);
                GetBcdWin(BcdeditCommand.enumBootmgr1);

                SetBcdWin();
                CmdAsync(BcdeditCommand.restartStr);

            }
        }

        private void BtnWin_OnClick(object sender, RoutedEventArgs e)
        {
            GetBcdWin(BcdeditCommand.enumBootmgr1);

        }

        private void BtnAnd_OnClick(object sender, RoutedEventArgs e)
        {
            GetBcdAndroid(BcdeditCommand.enumEFUI);

        }

        private void BtnTest_OnClick(object sender, RoutedEventArgs e)
        {
            GetStdOSEntries();
             
        }

        private async void BtnTest2_OnClick(object sender, RoutedEventArgs e)
        {
            var bcdList = await GetAllBcdElement();
            var osWindowsList = bcdList.FindAll(x => x.Description == BcdeditCommand.osWindows);
            var uefiosList = bcdList.FindAll(x => x.Description == BcdeditCommand.UEFIOS);

            //当描述BcdeditCommand.osWindows的数量过多则从新初始化
            if (osWindowsList.Count == 1 && uefiosList.Count == 1)
            {
                await SetBcdEdit(osWindowsList[0], uefiosList[0].Id);
                await SetBcdEdit(uefiosList[0], osWindowsList[0].Id);
            }
            else
            {

                if (osWindowsList.Count > 0)
                {
                    osWindowsList.ForEach(async x => await BcdDelete(x.Id));
                }
                if (uefiosList.Count > 0)
                {
                    uefiosList.ForEach(async x => await BcdDelete(x.Id));
                }
                await Init();
            }
        }

        /// <summary>
        /// 通过标识符 删除引导
        /// </summary>
        /// <param name="id">标识符</param>
        public async Task BcdDelete(string id)
        {
            if (id != BcdeditCommand.bootmgrId)
            {
                string deleteStr = @"bcdedit /delete {" + id + "}";
                var result = await CmdAsync(deleteStr);
            }
        }

        /// <summary>
        /// 获取全部的windows启动管理器
        /// </summary>
        /// <returns></returns>
        public async Task<List<BcdElement>> GetAllBcdElement()
        {
            List<BcdElement> bcdList = new List<BcdElement>();
            try
            {
                //通过Cmd获取所有的 windows启动管理器列表
                var res = await CmdAsync(BcdeditCommand.enumBootmgr);

                //通过每个windows启动管理器的开头都的前三十个字符作为分隔符
                var splitGuid = res.Substring(0, 30);
                var bootmgrs = res.Split(new string[] {splitGuid}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var bootmgr in bootmgrs)
                {
                    BcdElement bcd = new BcdElement();
                    bcd.Id = AnalysisResultId(bootmgr, "");
                    bcd.Device = AnalysisResult(bootmgr, "device");
                    bcd.Path = AnalysisResult(bootmgr, "path");
                    bcd.Description = AnalysisResult(bootmgr, "description");
                    bcdList.Add(bcd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return bcdList;
        }

    }
}
