namespace Repository.Model
{
    public class UriDatabaseSettings : IUriDatabaseSettings
    {
        public string UriCollection { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IUriDatabaseSettings
    {
        string UriCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
