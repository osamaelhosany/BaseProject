using System;
using Xamarin.Forms;

namespace BaseMvvmToolKIt
{
    public static class ViewModelResolver
    {
        public static IPageModelMapper PageModelMapper { get; set; } = new PageModelMapper();

        public static Page ResolvePageModel<T>() where T : BaseViewModel
        {
            return ResolvePageModel<T>(null);
        }

        public static Page ResolvePageModel<T>(object initData) where T : BaseViewModel
        {
            var pageModel = IOC.Container.Resolve<T>();

            return ResolvePageModel<T>(initData, pageModel);
        }

        public static Page ResolvePageModel<T>(object data, T pageModel) where T : BaseViewModel
        {
            var type = pageModel.GetType();
            return ResolvePageModel(type, data, pageModel);
        }

        public static Page ResolvePageModel(Type type, object data)
        {
            var pageModel = IOC.Container.Resolve(type) as BaseViewModel;
            return ResolvePageModel(type, data, pageModel);
        }

        public static Page ResolvePageModel(Type type, object data, BaseViewModel pageModel)
        {
            var name = PageModelMapper.GetPageTypeName(type);
            var pageType = Type.GetType(name);
            if (pageType == null)
                throw new Exception(name + " not found");

            var page = (Page)IOC.Container.Resolve(pageType);

            BindingPageModel(data, page, pageModel);

            return page;
        }

        public static Page BindingPageModel(object data, Page targetPage, BaseViewModel pageModel)
        {
            pageModel.WireEvents(targetPage);
            pageModel.CurrentPage = targetPage;
            pageModel.NavigationService = new PageModelCoreMethods(targetPage, pageModel);
            pageModel.Init(data);
            targetPage.BindingContext = pageModel;
            return targetPage;
        }
    }
}