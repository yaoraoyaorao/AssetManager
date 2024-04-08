using AssetManager.Shared.Dtos;
using AssetManager.Shared.Extensions;
using AssetManager.WPF.Common;
using AssetManager.WPF.Extensions;
using AssetManager.WPF.Service.IService;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using System.Collections.ObjectModel;
namespace AssetManager.WPF.ViewModels
{
    public class PlatformViewModel : NavigationViewModel
    {
        private readonly IPlatformService service;
        private readonly IDialogHostService dialogHost;
        private bool isRightDrawerOpen;
        private PlatformDto currentPlatform;
        private ObservableCollection<PlatformDto> platforms;
        private string search;
        private int _selectedItem;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        public PlatformDto CurrentPlatform
        {
            get { return currentPlatform; }
            set { currentPlatform = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<PlatformDto> Platforms
        {
            get { return platforms; }
            set { platforms = value; RaisePropertyChanged(); }
        }

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        public int SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; RaisePropertyChanged(); }
        }

        public DelegateCommand SearchCommand { get; private set; }

        public DelegateCommand AddPlatformBtnCommand { get; private set; }

        public DelegateCommand AddPlatformCommand { get; private set; }

        public DelegateCommand<PlatformDto> DeletePlatformCommand { get; private set; }
        public DelegateCommand<PlatformDto> SetPlatformCommand { get; private set; }

        public PlatformViewModel(IPlatformService service,IContainerProvider containerProvider) : base(containerProvider)
        {
            Platforms = new ObservableCollection<PlatformDto>();

            this.service = service;

            this.dialogHost = containerProvider.Resolve<IDialogHostService>();

            SearchCommand = new DelegateCommand(SearchContent);

            AddPlatformBtnCommand = new DelegateCommand(AddPlatformBtn);

            AddPlatformCommand = new DelegateCommand(AddPlatform);

            DeletePlatformCommand = new DelegateCommand<PlatformDto>(DeletePlatform);

            SetPlatformCommand = new DelegateCommand<PlatformDto>(SetPlatformBtn);
        }

       

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            await GetPlatformsData();
        }

        /// <summary>
        /// 删除平台
        /// </summary>
        /// <param name="dto"></param>
        private async void DeletePlatform(PlatformDto dto)
        {
            try
            {
                var dialogResult = await dialogHost.Question("温馨提示", $"确认删除此平台吗:{dto.Name}吗");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
                var response = await service.DeleteAsync(dto.Id);
                if (response.Code == 200)
                {
                    await GetPlatformsData();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 设置平台
        /// </summary>
        /// <param name="dto"></param>
        private void SetPlatformBtn(PlatformDto dto)
        {
            IsRightDrawerOpen = true;

            CurrentPlatform = dto;

            int iconId = Utilites.GetIconId(dto.Icon);

            SelectedItem = iconId == -1 ? 0 : iconId;
        }

        /// <summary>
        /// 添加平台按钮
        /// </summary>
        private void AddPlatformBtn()
        {
            IsRightDrawerOpen = true;

            CurrentPlatform = new PlatformDto();
        }

        /// <summary>
        /// 搜索平台
        /// </summary>
        private async void SearchContent()
        {
            await GetPlatformsData();
        }

        /// <summary>
        /// 获取平台数据接口
        /// </summary>
        private async Task GetPlatformsData()
        {
            try
            {
                UpdateLoading(true);
                var response = await service.GetAllAsync(new Shared.Parameters.QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 10,
                    Search = Search
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
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 添加平台接口
        /// </summary>
        private async void AddPlatform()
        {
            try
            {
                var dialogResult = await dialogHost.Question("温馨提示", $"确认执行此次操作吗");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);

                //判断是添加还是修改
                if (CurrentPlatform.Id > 0)
                {
                    CurrentPlatform.Icon = SelectedItem.ToString();
                    var response = await service.UpdateAsync(CurrentPlatform);
                    if (response.Code == 200)
                    {
                        await GetPlatformsData();
                        IsRightDrawerOpen = false;
                    }
                }
                else
                {
                    CurrentPlatform.Icon = SelectedItem.ToString();
                    var response = await service.AddAsync(CurrentPlatform);
                    if (response.Code == 200)
                    {
                        await GetPlatformsData();
                        IsRightDrawerOpen = false;
                    }
                }

            }
            catch (Exception e)
            {

                throw;
            }
            finally
            {
                UpdateLoading(false);
            }
        }
    }
}
