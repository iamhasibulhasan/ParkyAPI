using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.ParkyMapper;
using ParkyAPI.Repository;
using ParkyAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Database Information for v6
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Register Interface fro v6
builder.Services.AddScoped<INationalParkRepository, NationalParkRepository>();
builder.Services.AddScoped<ITrailRepository, TrailRepository>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(ParkyMappings));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("ParkyOpenAPISpecNP", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Parky API (National Park)",
        Version = "v1",
        Description="National Park API",
        Contact =new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email="mdhasibulhasan.dev@gmail.com",
            Name="Hasibul Hasan",
            Url=new Uri("https://hasibul-hasan.netlify.com")
        }
    });
    options.SwaggerDoc("ParkyOpenAPISpecTrails", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Parky API (Trails)",
        Version = "v1",
        Description = "This a test API project.",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "mdhasibulhasan.dev@gmail.com",
            Name = "Hasibul Hasan",
            Url = new Uri("https://hasibul-hasan.netlify.com")
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecNP/swagger.json", "National Park");
        options.SwaggerEndpoint("/swagger/ParkyOpenAPISpecTrails/swagger.json", "Trails");
        //options.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
