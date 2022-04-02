using HashidsNet;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers
{
    public static class HashIdsHandler
    {
        private static readonly string _hashIdsSalt = Startup.StaticConfig["Application:HashIdsSalt"];
        private static readonly Hashids hashIds = new Hashids(_hashIdsSalt, 10);
        public static int HashIdToInt(string hashId)
        {
            var rawId = hashIds.Decode(hashId);
            if (rawId.Length == 0) return -1;
            return rawId[0];
        }
        public static string IntToHashId(int id)
        {
            var hashId = hashIds.Encode(id);
            return hashId;
        }
    }
}
