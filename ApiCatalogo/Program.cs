using ApiCatalogo.Context;
using ApiCatalogo.DTO.Mapping.Profiles;
using ApiCatalogo.Extensions;
using ApiCatalogo.Filters;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.RepositoryCategoria;
using ApiCatalogo.Repositories.RepositoryProduto;
using ApiCatalogo.Repositories.RespositoryProduto;
using ApiCatalogo.Repositories.UnitOfWork;
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

//injetando o Automapper 
builder.Services.AddAutoMapper(typeof(Program));
/*
 Essa configuração é usada para informar ao automapper quais tipos do seu aplicativo contem os perfis de mapeamento
 o parametro typeof espeficica que os perfis de mapeamento devem ser procurados no assembly, onde a classe está localizada.
 
 Normalmente esse é o assembly principal ou assembly de inicialização da aplicação.

 */

//assembly:UNidade de umplantação e distribuição do .NET, que inclui código executavel, metadados e outros recursos necessarios
//para executar um aplicativo

//Define o registro do serviço usando o tempo de vida addScoped, para que o seriço seja criado a cada request.
builder.Services.AddScoped<ApiLoggingFilter>();

//o tempo de vida AddScoped cria uma instancia por request
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, unitOfWork>();


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
