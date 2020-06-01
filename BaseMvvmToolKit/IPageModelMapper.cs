using System;

namespace BaseMvvmToolKIt
{
    public interface IPageModelMapper
    {
        string GetPageTypeName(Type pageModelType);
    }
}

