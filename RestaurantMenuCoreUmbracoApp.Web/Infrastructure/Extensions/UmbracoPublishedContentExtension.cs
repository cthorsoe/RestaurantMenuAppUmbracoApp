using HashidsNet;
using Microsoft.Extensions.DependencyInjection;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace RestaurantMenuCoreUmbracoApp.Web.Extensions
{
    //public static class UmbracoPublishedContentExtension
    //{

    //    private readonly IHashIdsService _hashIdsService;
    //    public UmbracoPublishedContentExtension(IHashIdsService hashIdsService)
    //    {
    //        _hashIdsService = hashIdsService;
    //    }
    //    public string IdToHashId(this IPublishedContent node)
    //    {
    //        return _hashIdsService.IntToHashId(node.Id);
    //    }
    //}
}