using G4.Repository.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G4.Repository.Controllers {
    public class BrandDao {

        private IMongoDatabase Database;
        private IMongoCollection<Brand> Collection;

        public BrandDao() {
            Database = DBContext.GetDatabase("cars");
            Collection = Database.GetCollection<Brand>("brands");
        }

        public List<Brand> GetAll() => Collection.Aggregate().ToList();

        public void Insert(Brand brand) {

            Collection.InsertOne(brand);

        }

        public Brand? FindByID(string id) {

            var filter = Builders<Brand>.Filter.Eq("id", id);
            var result = Collection.Find(filter);

            if (result == null)
                return null;

            return result.FirstOrDefault();

        }

        public bool Update(Brand brand) {

            var filter = Builders<Brand>.Filter.Eq("id", brand.id);
            var old = FindByID(brand.id);

            if(old == null) return false;

            var update = DBContext.CreateUpdate<Brand>(old, brand);

            if (update == null)
                return false;

            var result = Collection.UpdateOne(filter, update);

            if (result == null) return false;

            return result.MatchedCount > 0;

        }

        public bool Delete(string id) {

            var filter = Builders<Brand>.Filter.Eq("id", id);
            var result = Collection.DeleteOne(filter);

            return result.DeletedCount > 0;

        }

    }
}
