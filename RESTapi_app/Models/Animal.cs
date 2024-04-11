using System.Collections.Generic;

namespace RESTapi_app.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public double weight { get; set; }
        public string fur_color { get; set; }
        public List<Visit> Visits { get; set; } = new List<Visit>();

        public Animal()
        {

        }
    }
}