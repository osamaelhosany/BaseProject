using System;

namespace BaseMvvmToolKIt
{
    public class PageModelMapper : IPageModelMapper
    {
        public string GetPageTypeName(Type pageModelType)
        {
            return pageModelType.AssemblyQualifiedName
                .Replace("PageModel", "Page")
                .Replace("ViewModel", "Page");
        }
    }
}

