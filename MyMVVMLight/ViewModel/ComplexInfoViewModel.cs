using GalaSoft.MvvmLight;
using MyMVVMLight.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMVVMLight.ViewModel
{
   public  class ComplexInfoViewModel: ViewModelBase
    {
        #region 下拉框相关
        private ComplexInfoModel combboxItem;
        /// <summary>
        /// 下拉框选中信息
        /// </summary>
        public ComplexInfoModel CombboxItem
        {
            get { return combboxItem; }
            set { combboxItem = value; RaisePropertyChanged(() => CombboxItem); }
        }


        private List<ComplexInfoModel> combboxList;
        /// <summary>
        /// 下拉框列表
        /// </summary>
        public List<ComplexInfoModel> CombboxList
        {
            get { return combboxList; }
            set { combboxList = value; RaisePropertyChanged(() => CombboxList); }
        }
        #endregion
    }
}
