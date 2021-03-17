using MongoDB.Bson;
using MongoDB.Driver;
using Repository.Model;
using System;

namespace Repository.Service
{
    public class UriService
    {
        private readonly IMongoCollection<UriModel> _uris;

        public UriService(IUriDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _uris = database.GetCollection<UriModel>(settings.UriCollection);
        }

        public UriModel GetByUri(string uriName) =>
            _uris.Find<UriModel>(item => item.Uri == uriName).FirstOrDefault();

        public UriModel GetByShortUri(string shortUri) =>
            _uris.Find<UriModel>(item => item.ShortUri == shortUri).FirstOrDefault();

        public UriModel Create(string uri, string shortUri)
        {
            UriModel dto = new UriModel();
            dto.Uri = uri;
            dto.ShortUri = shortUri;
            _uris.InsertOne(dto);
            return dto;
        }
    }
}
