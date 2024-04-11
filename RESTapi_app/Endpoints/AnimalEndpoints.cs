using RESTapi_app.Database;
using RESTapi_app.Models;

namespace RESTapi_app.Endpoints;

public class AnimalEndpoints
{
    public AnimalDB _animalDB;
    
    public AnimalEndpoints(AnimalDB animalDB)
    {
        _animalDB = animalDB;
    }
    
    public void MapAnimalEndpoints(WebApplication app)
    {
        app.MapGet("/animals/{id}", (int id) =>
        {
            bool exists = _animalDB.Animals.Any(a => a.Id == id);
            
            if (!exists)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(_animalDB.Animals.First(a => a.Id == id));
        
            

        });
        
        app.MapGet("/animals", () =>
        {
            return Results.Ok(_animalDB.Animals);
        });
        
        app.MapPost("/animals", (Animal animal) =>
        {
            _animalDB.AddAnimal(animal);
            return Results.Created($"/animals/{animal.Id}", animal);
        });
        
        app.MapPut("/animals/{id}", (int id, Animal updatedAnimal) =>
        {
            var animal = _animalDB.Animals.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return Results.NotFound();
            }

            animal.name = updatedAnimal.name;
            animal.category = updatedAnimal.category;
            animal.weight = updatedAnimal.weight;
            animal.fur_color = updatedAnimal.fur_color;

            return Results.Ok(animal);
        });

        app.MapDelete("/animals/delete/{id}", (int id) =>
        {
            var existingAnimal = _animalDB.Animals.FirstOrDefault(a => a.Id == id);

            if (existingAnimal == null)
            {
                return Results.NotFound();
            }

            _animalDB.Animals.Remove(existingAnimal);

            return Results.Ok();
        });
        
        app.MapGet("/animals/{id}/visits", (int id) =>
        {
            var animal = _animalDB.Animals.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(animal.Visits);
        });
        
        app.MapPost("/animals/{id}/visits", (int id, Visit newVisit) =>
        {
            var animal = _animalDB.Animals.FirstOrDefault(a => a.Id == id);

            if (animal == null)
            {
                return Results.NotFound();
            }

            animal.Visits.Add(newVisit);

            return Results.Created($"/animals/{id}/visits/{newVisit.DateOfVisit}", newVisit);
        });
    }
    
    
    
}