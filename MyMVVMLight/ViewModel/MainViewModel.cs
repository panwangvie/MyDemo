using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyMVVMLight.Model;
using System;
using System.Collections.Generic;

namespace MyMVVMLight.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            Welcome = new WelcomeModel() { Introduction = "Hello World！" };
            CombboxList = new List<ComplexInfoModel>() { new ComplexInfoModel() { Key = "1", Text = "aa" }, new ComplexInfoModel() { Key = "1", Text = "aa" }, new ComplexInfoModel() { Key = "2", Text = "aa2" }, new ComplexInfoModel() { Key = "3", Text = "aa3" }, };
       
        }
      

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

        private WelcomeModel welcome;
        /// <summary>
        /// 欢迎词属性
        /// </summary>
        public WelcomeModel Welcome
        {
            get { return welcome; }
            set { welcome = value; RaisePropertyChanged(() => Welcome); }
        }

        public bool IsSingleRadioCheck { get { return isSingleRadioCheck; } set { isSingleRadioCheck = value; RaisePropertyChanged(() => Welcome); } }
        #endregion

        private bool isSingleRadioCheck = false;

    }
}