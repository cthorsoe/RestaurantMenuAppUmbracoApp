using HashidsNet;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services
{
    public class HashIdsService : IHashIdsService
    {
        private readonly IHashids _hashids;
        public HashIdsService(IHashids hashids)
        {
            _hashids = hashids;
        }

        public int HashIdToInt(string hashId)
        {
            var rawId = _hashids.Decode(hashId);
            if (rawId.Length == 0) return -1;
            return rawId[0];
        }
        public string IntToHashId(int id)
        {
            var hashId = _hashids.Encode(id);
            return hashId;
        }
    }
}
