using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;

using MongoDB.Driver;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories
{
    public class ActivationsRepository : IActivationsRepository
    {
        private readonly IMongoCollection<Activation> _collection;

        public ActivationsRepository(IMongoCollection<Activation> collection)
        {
            _collection = collection;
        }

        public async Task<Activation> ReadOneAsync(string id)
        {
            if (id.IsNot24BitHex())
                return null;

            var filter = Builders<Activation>.Filter.Eq(act => act.Id, id);
            var result = await _collection.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Activation>> ReadManyAsync(string exerciseId)
        {
            var filter = Builders<Activation>.Filter.Eq(act => act.ExerciseId, exerciseId);
            var result = await _collection.FindAsync(filter);
            return result.ToList();
        }

        public async Task<IEnumerable<Activation>> ReadAllAsync()
        {
            var filter = Builders<Activation>.Filter.Empty;
            var results = await _collection.FindAsync(filter);
            return await results.ToListAsync();
        }

        public async Task<Activation> CreateOneAsync(Activation activation)
        {
            await _collection.InsertOneAsync(activation);

            return (activation.Id == null) ? null : activation;
        }

        public async Task<Activation> DeleteOneAsync(string id)
        {
            var activationToDelete = await ReadOneAsync(id);

            if (activationToDelete == null)
                return null;

            var filter = Builders<Activation>.Filter.Eq(act => act.Id, id);
            await _collection.DeleteOneAsync(filter);

            return activationToDelete;
        }
    }
}
