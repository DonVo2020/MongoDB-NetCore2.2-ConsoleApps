using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;

using MongoDB.Driver;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories
{
    public class MusclesRepository : IMusclesRepository
    {
        private readonly IMongoCollection<Muscle> _collection;

        public MusclesRepository(IMongoCollection<Muscle> collection)
        {
            _collection = collection;
        }

        public async Task<Muscle> ReadOneAsync(string id)
        {
            if (id.IsNot24BitHex())
                return null;

            var filter = Builders<Muscle>.Filter.Eq(ex => ex.Id, id);
            var result = await _collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Muscle>> ReadAllAsync()
        {
            var filter = Builders<Muscle>.Filter.Empty;
            var results = await _collection.FindAsync(filter);
            return await results.ToListAsync();
        }

        public async Task<Muscle> CreateOneAsync(Muscle muscle)
        {
            await _collection.InsertOneAsync(muscle);

            return (muscle.Id == null) ? null : muscle;
        }

        public async Task<Muscle> DeleteOneAsync(string id)
        {
            var muscleToDelete = await ReadOneAsync(id);

            if (muscleToDelete == null)
                return null;

            var filter = Builders<Muscle>.Filter.Eq(ex => ex.Id, id);
            await _collection.DeleteOneAsync(filter);

            return muscleToDelete;
        }
    }
}
