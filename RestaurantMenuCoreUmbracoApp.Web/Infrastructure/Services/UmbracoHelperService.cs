using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services
{
    public class UmbracoHelperService : IUmbracoHelperService
    {
        private readonly IUmbracoHelperAccessor _umbracoHelperAccessor;

        public UmbracoHelperService(IUmbracoHelperAccessor umbracoHelperAccessor)
        {
            _umbracoHelperAccessor = umbracoHelperAccessor;
        }
        public UmbracoHelper GetHelper()
        {
            var success = _umbracoHelperAccessor.TryGetUmbracoHelper(out var umbracoHelper);
            if (success is false)
            {
                return null;
            }
            return umbracoHelper;
        }
    }
}
