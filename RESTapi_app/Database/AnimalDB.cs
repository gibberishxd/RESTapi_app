using RESTapi_app.Models;

namespace RESTapi_app.Database;


public class AnimalDB
{
    public List<Animal> Animals { get; set; } = new List<Animal>();
    private int _nextId = 1;
    
    public AnimalDB()
    {
        
    }

    public void AddAnimal(Animal animal)
    {
        animal.Id = _nextId++;
        Animals.Add(animal);
    }
}