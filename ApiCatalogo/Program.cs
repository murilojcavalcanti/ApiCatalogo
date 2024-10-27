using ApiCatalogo.Context;
using ApiCatalogo.Extensions;
using ApiCatalogo.Filters;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.RepositoryCategoria;
using ApiCatalogo.Repositories.RepositoryProduto;
using ApiCatalogo.Repositories.RespositoryProduto;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opts =>
    //Ignora o objeto quando um ciclo de referencia é detectado durante a serialização.
    opts.JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

                //Este aqui é um exemplo de quando usamos o builder pra ler o arquivo de configuração.
var ConString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opts =>
                                            opts.UseMySql(ConString,
                                            ServerVersion.AutoDetect(ConString)));

//Define o registro do serviço usando o tempo de vida addScoped, para que o seriço seja criado a cada request.
builder.Services.AddScoped<ApiLoggingFilter>();

//o tempo de vida AddScoped cria uma instancia por request
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, Produtorepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
