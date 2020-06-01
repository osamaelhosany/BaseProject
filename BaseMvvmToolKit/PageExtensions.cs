using Xamarin.Forms;

namespace BaseMvvmToolKIt
{
    public static class PageExtensions
    {
        public static BaseViewModel GetModel(this Page page)
        {
            var pageModel = page.BindingContext as BaseViewModel;
            return pageModel;
        }

        public static void NotifyAllChildrenPopped(this NavigationPage navigationPage)
        {
            foreach (var page in navigationPage.Navigation.ModalStack)
            {
                var pageModel = page.GetModel();
                if (pageModel != null)
                    pageModel.RaisePageWasPopped();
            }

            foreach (var page in navigationPage.Navigation.NavigationStack)
            {
                var pageModel = page.GetModel();
                if (pageModel != null)
                    pageModel.RaisePageWasPopped();
            }
        }
    }
}

