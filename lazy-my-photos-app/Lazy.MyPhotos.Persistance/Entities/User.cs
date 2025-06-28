using SQLite;

namespace Lazy.MyPhotos.Persistence.Entities
{
    public class User
    {
        [PrimaryKey]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int PhotoCount { get; set; }
    }
}