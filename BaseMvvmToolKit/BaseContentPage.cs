using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace BaseMvvmToolKIt
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var pageModel = BindingContext as BaseViewModel;

            if (pageModel != null && pageModel.ToolbarItems != null && pageModel.ToolbarItems.Count > 0)
            {

                pageModel.ToolbarItems.CollectionChanged += PageModel_ToolbarItems_CollectionChanged;

                foreach (var toolBarItem in pageModel.ToolbarItems)
                {
                    if (!(this.ToolbarItems.Contains(toolBarItem)))
                    {
                        this.ToolbarItems.Add(toolBarItem);
                    }
                }
            }

        }

        void PageModel_ToolbarItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (ToolbarItem toolBarItem in e.NewItems)
            {
                if (!(this.ToolbarItems.Contains(toolBarItem)))
                {
                    this.ToolbarItems.Add(toolBarItem);
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ToolbarItem toolBarItem in e.OldItems)
                {
                    if (!(this.ToolbarItems.Contains(toolBarItem)))
                    {
                        this.ToolbarItems.Add(toolBarItem);
                    }
                }
            }
        }
        protected override bool OnBackButtonPressed()
        {
            var navContainer = IOC.Container.Resolve<INavigationService>("DefaultNavigationServiceName");
            if (Application.Current.MainPage.Navigation.NavigationStack.Count() == 1)
            {
                return true;
            } 
            return base.OnBackButtonPressed();
        }
    }
}

