namespace Lazy.MyPhotos.Shared.Models.User;

public class RegisterResponse
{


    public string type { get; set; }
    public string title { get; set; }
    public int status { get; set; }
    public string detail { get; set; }
    public string instance { get; set; }
    public Errors errors { get; set; }
    public string additionalProp1 { get; set; }
    public string additionalProp2 { get; set; }
    public string additionalProp3 { get; set; }


    public class Errors
    {
        public string[] additionalProp1 { get; set; }
        public string[] additionalProp2 { get; set; }
        public string[] additionalProp3 { get; set; }
    }

}