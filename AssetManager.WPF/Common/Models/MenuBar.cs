using Prism.Mvvm;

namespace AssetManager.WPF.Common.Models
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuBar:BindableBase
    {
        private string? icon;
        private string? title;
        private string? nameSpace;

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string? Icon
        {
            get { return icon; }
            set { icon = value; }
        }

        /// <summary>
        /// 菜单标题
        /// </summary>
        public string? Title
        {
            get { return title; }
            set { title = value; }
        }

        /// <summary>
        /// 菜单命名空间
        /// </summary>
        public string? NameSpace
        {
            get { return nameSpace; }
            set { nameSpace = value; }
        }
    }
}
