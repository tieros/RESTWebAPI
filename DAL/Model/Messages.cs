

namespace DAL.Model
    
{
    public class Message
    {
       
       public int status { get; set; }

       public string? message { get; set; }
       
        public List<Planes>? planeList { get; set; } 
    }
}
