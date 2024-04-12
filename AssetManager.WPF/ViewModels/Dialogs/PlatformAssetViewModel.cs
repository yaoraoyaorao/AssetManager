using AssetManager.Shared.Dtos;
using AssetManager.Shared.Extensions;
using AssetManager.WPF.Common;
using AssetManager.WPF.Extensions;
using AssetManager.WPF.Service.IService;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace AssetManager.WPF.ViewModels.Dialogs
{
    public class PlatformAssetViewModel : BindableBase, IDialogHostAware
    {
        private AssetPackageDto assetPackage;
        
        private PlatformDto selectedPlatform;
        
        private ObservableCollection<PlatformAssetDto> platformAssets;
        
        private ObservableCollection<PlatformDto> platforms;
        
        private readonly IPlatformAssetService platformAssetService;
        
        private readonly IPlatformService platformService;
        
        public readonly IEventAggregator aggregator;
        
        public string DialogHostName { get; set; }
        
        public DelegateCommand SaveCommand { get; set; }
        
        public DelegateCommand CancelCommand { get; set; }
        
        public DelegateCommand AddPlatformCommand { get; set; }
        
        public AssetPackageDto AssetPackage
        {
            get { return assetPackage; }
            set { assetPackage = value; RaisePropertyChanged(); }
        }
        
        public ObservableCollection<PlatformAssetDto> PlatformAssets
        {
            get { return platformAssets; }
            set { platformAssets = value; RaisePropertyChanged(); }
        }
        
        public ObservableCollection<PlatformDto> Platforms
        {
            get { return platforms; }
            set
            {
                platforms = value; RaisePropertyChanged();
            }
        }
        
        public PlatformDto SelectedPlatform
        {
            get { return selectedPlatform; }
            set { selectedPlatform = value; RaisePropertyChanged(); }
        }

        public PlatformAssetViewModel(IPlatformAssetService service,IPlatformService platformService, IContainerProvider container)
        {
            CancelCommand = new DelegateCommand(Cancel);

            AddPlatformCommand = new DelegateCommand(AddPlatform);

            PlatformAssets = new ObservableCollection<PlatformAssetDto>();

            Platforms = new ObservableCollection<PlatformDto>();

            this.platformAssetService = service;

            this.platformService = platformService;

            this.aggregator = container.Resolve<IEventAggregator>();
        }

        private async void AddPlatform()
        {
            try
            {
                if (SelectedPlatform == null)
                {
                    aggregator.SendMessage("请选择平台");
                    return;
                }

                var response = await platformAssetService.AddAsync(new Shared.Parameters.PlatformAssetParameter()
                {
                    AssetPackageId = AssetPackage.Id,
                    PlatformId = SelectedPlatform.Id,
                    Max = AssetPackage.Max,
                    Min = AssetPackage.Min,
                    Patch = AssetPackage.Patch
                });

                if (response.Code == 200)
                {
                    await GetPlatformAssetData();
                    await GetPlatformData();
                }
            }
            catch (Exception e)
            {
                aggregator.SendMessage("添加失败:" + e.Message);
            }
        }

        public async void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                AssetPackage = parameters.GetValue<AssetPackageDto>("Value");
            }
            else
            {
                AssetPackage = new AssetPackageDto();
            }

            await GetPlatformAssetData();

            await GetPlatformData();
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        /// <summary>
        /// 获取平台资产数据
        /// </summary>
        /// <returns></returns>
        private async Task GetPlatformAssetData()
        {
            try
            {
                var response = await platformAssetService.GetAllAsync(AssetPackage.Id);

                if (response.Code == 200)
                {
                    PlatformAssets.Clear();
                    foreach (var item in response.Data)
                    {
                        item.TargetPlatform.Icon = Utilites.GetIconName(item.TargetPlatform.Icon);
                        PlatformAssets.Add(item);
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        /// <summary>
        /// 获取平台数据
        /// </summary>
        /// <returns></returns>
        private async Task GetPlatformData()
        {
            try
            {
                var response = await platformService.GetCanUsePlatform(AssetPackage.Id);

                if (response.Code == 200)
                {
                    Platforms.Clear();

                    foreach (var item in response.Data)
                    {
                        Platforms.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
