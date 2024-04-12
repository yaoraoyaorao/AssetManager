using AssetManager.Shared.Dtos;
using AssetManager.Shared.Extensions;
using AssetManager.WPF.Common;
using AssetManager.WPF.Service.IService;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace AssetManager.WPF.ViewModels.Dialogs
{
    public class AddVersionViewModel : BindableBase, IDialogHostAware
    {
        private AssetPackageDto assetPackage;
        private readonly IPlatformService service;
        private ObservableCollection<PlatformDto> platforms;

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public ObservableCollection<PlatformDto> Platforms
        {
            get { return platforms; }
            set { platforms = value; RaisePropertyChanged(); }
        }

        public AssetPackageDto AssetPackage
        {
            get { return assetPackage; }
            set { assetPackage = value; RaisePropertyChanged(); }
        }

        public AddVersionViewModel(IPlatformService service)
        {
            SaveCommand = new DelegateCommand(Save);
            
            CancelCommand = new DelegateCommand(Cancel);

            Platforms = new ObservableCollection<PlatformDto>();

            this.service = service;
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void Save()
        {

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogParameters param = new DialogParameters
                {
                    { "Value", AssetPackage }
                };
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }
        
        /// <summary>
        /// 面板打开时
        /// </summary>
        /// <param name="parameters"></param>
        public async void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                assetPackage = parameters.GetValue<AssetPackageDto>("Value");
            }
            else
            {
                assetPackage = new AssetPackageDto();
            }

            await GetPlatformsData();
        }

        /// <summary>
        /// 获取平台数据接口
        /// </summary>
        private async Task GetPlatformsData()
        {
            try
            {
                var response = await service.GetAllAsync(new Shared.Parameters.QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 100,
                    Search = ""
                });
                if (response.Code == 200)
                {
                    Platforms.Clear();
                    foreach (var item in response.Data.Items)
                    {
                        if (string.IsNullOrEmpty(item.Remark))
                        {
                            item.Remark = "无";
                        }

                        item.Icon = Utilites.GetIconName(item.Icon);
                        Platforms.Add(item);
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
            }
        }

    }
}
