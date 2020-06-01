using System.Threading.Tasks;
using Xamarin.Forms;

namespace BaseMvvmToolKIt
{
    public interface INavigationService
    {
        Task PopToRoot(bool animate = true);

        Task PushPage(Page page, BaseViewModel model, bool modal = false, bool animate = true);

        Task PopPage(bool modal = false, bool animate = true);

        /// <summary>
        /// This method switches the selected main page, TabbedPage the selected tab or if MasterDetail, works with custom pages also
        /// </summary>
        /// <returns>The BagePageModel, allows you to PopToRoot, Pass Data</returns>
        /// <param name="newSelected">The pagemodel of the root you want to change</param>
        Task<BaseViewModel> SwitchSelectedRootPageModel<T>() where T : BaseViewModel;

        void NotifyChildrenPageWasPopped();

        string NavigationServiceName { get; }
    }
}

