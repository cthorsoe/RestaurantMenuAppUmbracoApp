namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces
{
    public interface IHashIdsService
    {
        public int HashIdToInt(string hashId);
        public string IntToHashId(int id);
    }
}
