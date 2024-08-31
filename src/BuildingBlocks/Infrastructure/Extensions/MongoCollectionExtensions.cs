using MongoDB.Driver;
using Shared.SeedWord;

namespace Infrastructure.Extensions
{
    public static class MongoCollectionExtensions
    {
        public static Task<PageList<TDestination>> PaginatedListAsync<TDestination>(
            this IMongoCollection<TDestination> collection,
            FilterDefinition<TDestination> filter,
            int pageNumber, int pageSize) where TDestination : class
        {
            return PageList<TDestination>.ToPageList(collection, filter, pageNumber, pageSize);
        }
    }
}