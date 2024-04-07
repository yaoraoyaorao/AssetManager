using AssetManager.WPF.Extensions;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace AssetManager.WPF.ViewModels
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        public readonly IEventAggregator aggregator;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            aggregator = containerProvider.Resolve<IEventAggregator>();
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public void UpdateLoading(bool isOpen)
        {
            aggregator.UpdateLoading(new Common.Event.UpdateModel()
            {
                IsOpen = isOpen
            });
        }
    }
}
