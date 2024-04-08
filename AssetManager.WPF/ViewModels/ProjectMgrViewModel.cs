using AssetManager.Shared;
using AssetManager.Shared.Dtos;
using AssetManager.Shared.Parameters;
using AssetManager.WPF.Common;
using AssetManager.WPF.Extensions;
using AssetManager.WPF.Service.IService;
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
        private ObservableCollection<ProjectDto> projects;
        private bool isRightDrawerOpen;
        private ProjectDto currentProject;
        private readonly IProjectService service;
        private readonly IDialogHostService dialogHost;

        /// <summary>
        /// 项目集合
        /// </summary>
        public ObservableCollection<ProjectDto> Projects
        {
            get { return projects; }
            set { projects = value; RaisePropertyChanged(); }
        }

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


        public ProjectMgrViewModel(IProjectService service, IContainerProvider container) : base(container)
        {
            Projects = new ObservableCollection<ProjectDto>();
            
            this.dialogHost = container.Resolve<IDialogHostService>();
            
            this.service = service;

            DeleteCommand = new DelegateCommand<ProjectDto>(Delete);

            AddBtnCommand = new DelegateCommand(AddBtn);

            EditBtnCommand = new DelegateCommand<ProjectDto>(EditBtn);

            CopyBtnCommand = new DelegateCommand<ProjectDto>(CopyGuid);

            DisplayDownloadViewCommand = new DelegateCommand<ProjectDto>(DisplayDownloadView);
        }

        /// <summary>
        /// 显示下载界面
        /// </summary>
        /// <param name="dto"></param>
        private void DisplayDownloadView(ProjectDto dto)
        {
            
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
                var dialogResult = await dialogHost.Question("温馨提示", $"确认删除此项目吗:{dto.Name}吗");
                if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;

                UpdateLoading(true);
                var response = await service.DeleteAsync(dto.Id);
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
                var response = await service.GetAllAsync(new QueryParameter()
                {
                    PageIndex = 0,
                    PageSize = 10,
                    Search = ""
                });

                if (response.Code == 200)
                {
                    Projects.Clear();

                    foreach (var item in response.Data.Items)
                    {
                        Projects.Add(item);
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
                        response = await service.UpdateAsync(new ProjectItemFromBody()
                        {
                            Name = project.Name,
                            Description = project.Description,
                        });
                    }
                    else
                    {
                        response = await service.AddAsync(new ProjectItemFromBody()
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

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            await GetProjectData();
        }
    }
}
