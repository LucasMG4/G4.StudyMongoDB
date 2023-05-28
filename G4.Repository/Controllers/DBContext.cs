using MongoDB.Driver;
using System.Diagnostics;

namespace G4.Repository.Controllers {
    public static class DBContext {

        public static string ConnectionString { private get; set; } = "mongodb://localhost:27017/";

        public static MongoClient CreateConnection() => new MongoClient(ConnectionString);

        public static IMongoDatabase GetDatabase(string database) => CreateConnection().GetDatabase(database);

        public static UpdateDefinition<TDocument>? CreateUpdate<TDocument>(TDocument old, TDocument _new, bool ignoreID = true) {

            var results = new List<UpdateDefinition<TDocument>>();

            if (old == null || _new == null)
                throw new Exception("Old Or New Document is Null, is not possible to create UpdateDefinition");

            var properties = _new.GetType().GetProperties().ToList();

            if (ignoreID)
                properties = properties.Where(x => !x.Name.Equals("id")).ToList();

            foreach (var property in properties) {

                var oldValue = property.GetValue(old);
                var newValue = property.GetValue(_new);

                var isDiferent = false;

                isDiferent = (oldValue == null && newValue != null);
                isDiferent = isDiferent || (oldValue != null && newValue == null);

                if (oldValue != null && newValue != null)
                    isDiferent = isDiferent || !oldValue.ToString().Equals(newValue.ToString());

                if(isDiferent) 
                    results.Add(Builders<TDocument>.Update.Set(property.Name, newValue));

            }

            if (results.Count == 0)
                return null;

            var result = results.FirstOrDefault();

            for(var position = 1; position < results.Count; position++) {
                Builders<TDocument>.Update.Combine(result, results.ElementAt(position));
            }

            return result;


        }
    }
}
