using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AssetManager.WPF.Common;
using AssetManager.WPF.Extensions;
using AssetManager.WPF.Service.IService;
using DryIoc;
using Prism.Commands;
using Prism.Ioc;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Windows;

namespace AssetManager.WPF.ViewModels
{
    public class ProjectMgrViewModel: NavigationViewModel
    {
        private bool isRightDrawerOpen;
        private string searchProject;
        private string searchAssetPackageVersion;

        private ProjectDto currentProject;
        
        private readonly IProjectService projectService;
        private readonly IAssetPackageService assetPackageService;
        private readonly IDialogHostService dialogHost;
        
        private ObservableCollection<ProjectDto> projects;
        private ObservableCollection<AssetPackageDto> assetPackages;

        /// <summary>
        /// 右侧抽屉打开
        /// </summary>
        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 当前项目
        /// </summary>
        public ProjectDto CurrentProject
        {
            get { return currentProject; }
            set { currentProject = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 项目搜索
        /// </summary>
        public string SearchProject
        {
            get { return searchProject; }
            set { searchProject = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 资源包版本搜索
        /// </summary>
        public string SearchAssetPackageVersion
        {
            get { return searchAssetPackageVersion; }
            set { searchAssetPackageVersion = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 项目集合
        /// </summary>
        public ObservableCollection<ProjectDto> Projects
        {
            get { return projects; }
            set { projects = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 资源平台
        /// </summary>
        public ObservableCollection<AssetPackageDto> AssetPackages
        {
            get { return assetPackages; }
            set { assetPackages = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 删除命令
        /// </summary>
        public DelegateCommand<ProjectDto> DeleteCommand { get;private set; }

        /// <summary>
        /// 添加按钮命令
        /// </summary>
        public DelegateCommand AddBtnCommand { get;private set; }

        /// <summary>
        /// 拷贝按钮命令
        /// </summary>
        public DelegateCommand<ProjectDto> CopyBtnCommand { get;private set; }

        /// <summary>
        /// 编辑按钮命令
        /// </summary>
        public DelegateCommand<ProjectDto> EditBtnCommand { get;private set; }

        /// <summary>
        /// 显示下载界面命令
        /// </summary>
        public DelegateCommand<ProjectDto> DisplayDownloadViewCommand { get;private set; }

        /// <summary>
        /// 搜索项目命令
        /// </summary>
        public DelegateCommand SearchProjectCommand { get;private set; }

        /// <summary>
        /// 搜索资源包命名
        /// </summary>
        public DelegateCommand SearchAssetVersionCommand { get;private set; }

        /// <summary>
        /// 添加版本命令
        /// </summary>
        public DelegateCommand AddVersionCommand { get;private set; }

        /// <summary>
        /// 平台资源命令
        /// </summary>
        public DelegateCommand<AssetPackageDto> PlatformAssetCommand { get;private set; }



        public ProjectMgrViewModel(IProjectService projectService,IAssetPackageService assetService, IContainerProvider container) : base(container)
        {
            Projects = new ObservableCollection<ProjectDto>();
            
            AssetPackages = new ObservableCollection<AssetPackageDto>();

            this.dialogHost = container.Resolve<IDialogHostService>();
            
            this.projectService = projectService;

            this.assetPackageService = assetService;

            DeleteCommand = new DelegateCommand<ProjectDto>(Delete);

            AddBtnCommand = new DelegateCommand(AddBtn);

            EditBtnCommand = new DelegateCommand<ProjectDto>(EditBtn);

            CopyBtnCommand = new DelegateCommand<ProjectDto>(CopyGuid);

            DisplayDownloadViewCommand = new DelegateCommand<ProjectDto>(DisplayDownloadView);

            SearchProjectCommand = new DelegateCommand(SearchProjectBtn);

            SearchAssetVersionCommand = new DelegateCommand(SearchAssetVersion);

            AddVersionCommand = new DelegateCommand(AddVersion);

            PlatformAssetCommand = new DelegateCommand<AssetPackageDto>(PlatformAsset);
        }



        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            await GetProjectData();
        }

        private async void PlatformAsset(AssetPackageDto packageDto)
        {
            try
            {
                DialogParameters param = new DialogParameters();
                
                param.Add("Value", packageDto);
                
                var dialogResult = await dialogHost.ShowDialog("PlatformAssetView", param);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private async void AddVersion()
        {
            try
            {

                DialogParameters param = new DialogParameters();

                param.Add("Value", new AssetPackageDto());

                var dialogResult = await dialogHost.ShowDialog("AddVersionView", param);

                if (dialogResult.Result == ButtonResult.OK)
                {
                    UpdateLoading(true);
                    var assetDto = dialogResult.Parameters.GetValue<AssetPackageDto>("Value");
                    var response = await assetPackageService.AddAsync(new AssetPackageParameter()
                    {
                        Id = currentProject.Id,
                        Max = assetDto.Max,
                        Min = assetDto.Min,
                        Patch = assetDto.Patch,
                    });

                    if (response.Code == 200)
                    {
                        await GetAssetPackageData();

                        aggregator.SendMessage("添加成功");
                    }
                    else
                    {
                        aggregator.SendMessage(response.Message);
                    }
                }
            }
            catch (Exception e)
            {

                aggregator.SendMessage("添加失败:", e.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 搜索资源版本
        /// </summary>
        private async void SearchAssetVersion()
        {
            await GetAssetPackageData();
        }

        /// <summary>
        /// 搜索项目
        /// </summary>
        private async void SearchProjectBtn()
        {
            await GetProjectData();
        }

        /// <summary>
        /// 显示下载界面
        /// </summary>
        /// <param name="dto"></param>
        private async void DisplayDownloadView(ProjectDto dto)
        {
            if (dto == null)
            {
                aggregator.SendMessage("当前项目为空");
                return;
            }

            IsRightDrawerOpen = true;

            CurrentProject = dto;

            await GetAssetPackageData();
        }

        /// <summary>
        /// 获取资源包数据
        /// </summary>
        /// <returns></returns>
        private async Task GetAssetPackageData()
        {
            try
            {
                UpdateLoading(true);

                var response = await assetPackageService.GetAllAsync(new QueryParameter()
                {
                    Id = CurrentProject.Id,
                    PageIndex = 0,
                    PageSize = 10,
                    Search = SearchAssetPackageVersion
                });

                if (response.Code == 200)
                {
                    AssetPackages.Clear();

                    foreach (var item in response.Data.Items)
                    {
                        item.Version = item.Max + "." + item.Min + "." + item.Patch;
                        AssetPackages.Add(item);
                    }
                }
                else
                {
                    aggregator.SendMessage("数据获取失败：" + response.Message);
                }
            }
            catch (Exception e)
            {
                aggregator.SendMessage("数据获取失败：" + e.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 拷贝Guid
        /// </summary>
        /// <param name="dto"></param>
        private void CopyGuid(ProjectDto dto)
        {
            Clipboard.SetText(dto.Guid);

            aggregator.SendMessage("拷贝成功：" + dto.Guid);
        }

        /// <summary>
        /// 编辑按钮实现
        /// </summary>
        /// <param name="dto"></param>
        private async void EditBtn(ProjectDto dto)
        {
            CurrentProject = dto;

            await AddOrUpdate();
        }

        /// <summary>
        /// 添加按钮实现
        /// </summary>
        private async void AddBtn()
        {
            CurrentProject = new ProjectDto();

            await AddOrUpdate();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void Delete(ProjectDto dto)
        {
            try
            {
                var dialogResult = await dialogHost.Question("温馨提示", $"确认删除此项目:{dto.Name}吗");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
                var response = await projectService.DeleteAsync(dto.Id);
                if (response.Code == 200)
                {
                    await GetProjectData();

                    aggregator.SendMessage("删除成功");
                }
                else
                {
                    aggregator.SendMessage("删除失败：" + response.Message);
                }
            }
            catch (Exception e)
            {
                aggregator.SendMessage("删除失败：" + e.Message);
                throw;
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private async Task GetProjectData()
        {
            try
            {
                UpdateLoading(true);
                var response = await projectService.GetAllAsync(new QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 10,
                    Search = SearchProject
                });

                if (response.Code == 200)
                {
                    Projects.Clear();

                    foreach (var item in response.Data.Items)
                    {
                        Projects.Add(item);
                    }
                }
                else
                {
                    aggregator.SendMessage("数据获取失败：" + response.Message);
                }
            }
            catch (Exception e)
            {

                aggregator.SendMessage("数据获取失败：" + e.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
           
        }

        /// <summary>
        /// 添加或更新
        /// </summary>
        /// <param name="model"></param>
        private async Task AddOrUpdate()
        {
            try
            {
                DialogParameters param = new DialogParameters();

                param.Add("Value", CurrentProject);

                var dialogResult = await dialogHost.ShowDialog("AddProjectView", param);

                if (dialogResult.Result == ButtonResult.OK)
                {
                    var project = dialogResult.Parameters.GetValue<ProjectDto>("Value");

                    ApiResponse<ProjectDto> response = null;

                    UpdateLoading(true);
                    if (project.Id > 0)
                    {
                        response = await projectService.UpdateAsync(new ProjectItemFromBody()
                        {
                            Name = project.Name,
                            Description = project.Description,
                        });
                    }
                    else
                    {
                        response = await projectService.AddAsync(new ProjectItemFromBody()
                        {
                            Name = project.Name,
                            Description = project.Description,
                        });
                    }

                    if (response.Code == 200)
                    {
                        aggregator.SendMessage("执行成功");

                        await GetProjectData();
                    }
                    else
                    {
                        aggregator.SendMessage("执行失败:" + response.Message);
                    }
                }
            }
            catch (Exception e)
            {
                aggregator.SendMessage("执行失败:" + e.Message);
            }
            finally
            {
                UpdateLoading(false);
            }
        }
    }
}
