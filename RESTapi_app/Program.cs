using RESTapi_app.Database;
using RESTapi_app.Endpoints;
using RESTapi_app.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<AnimalDB>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


AnimalDB animalDb = app.Services.GetRequiredService<AnimalDB>();

var animals = new List<Animal>
{
    new Animal { name = "Lion", category = "Mammal", weight = 200.5, fur_color = "Golden" },
    new Animal { name = "Elephant", category = "Mammal", weight = 6000, fur_color = "Grey" },
    new Animal { name = "Cheetah", category = "Mammal", weight = 60, fur_color = "Yellow" },
    new Animal { name = "Eagle", category = "Bird", weight = 5, fur_color = "Brown" },
    new Animal { name = "Shark", category = "Fish", weight = 1100, fur_color = "Grey" }
};

var visits = new List<Visit>
{
    new Visit { DateOfVisit = DateTime.Now.AddDays(-10), Description = "Regular Checkup", Price = 50 },
    new Visit { DateOfVisit = DateTime.Now.AddDays(-5), Description = "Vaccination", Price = 70 },
    new Visit { DateOfVisit = DateTime.Now.AddDays(-2), Description = "Emergency Treatment", Price = 150 }
};

for (int i = 0; i < animals.Count; i++)
{
    var animal = animals[i];
    animalDb.AddAnimal(animal);
    
    if (i < visits.Count)
    {
        var visit = visits[i];
        if (i < 3)
        {
            animal.Visits.Add(visit);
        }
    }
}


AnimalEndpoints animalEndpoints = new AnimalEndpoints(app.Services.GetRequiredService<AnimalDB>());
animalEndpoints.MapAnimalEndpoints(app);



app.Run();