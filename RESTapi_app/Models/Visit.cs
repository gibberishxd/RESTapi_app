namespace RESTapi_app.Models
{
    public class Visit
    {
        public DateTime DateOfVisit { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Visit()
        {

        }
    }
}