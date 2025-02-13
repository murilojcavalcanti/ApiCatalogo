using ApiCatalogo.Context;
using ApiCatalogo.Extensions;
using ApiCatalogo.Filters;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.RepositoryCategoria;
using ApiCatalogo.Repositories.RepositoryProduto;
using ApiCatalogo.Repositories.RespositoryProduto;
using ApiCatalogo.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opts =>
    //Ignora o objeto quando um ciclo de referencia � detectado durante a serializa��o.
    opts.JsonSerializerOptions
    .ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddNewtonsoftJson();//Adicionando o Newtonsoft as controllers

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
 * Realizando a configura��o do identity, o usuario com Identity user as fun��es com o Role, o mecanismo de salvamento no banco de dados com Stores.
 */
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
                //Este aqui � um exemplo de quando usamos o builder pra ler o arquivo de configura��o.
var ConString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opts =>
                                            opts.UseMySql(ConString,
                                            ServerVersion.AutoDetect(ConString)));
//Obtendo a secretKey do appsetting
var secretkey = builder.Configuration["JWT:SecretKey"] ?? throw new ArgumentException("SecretKey inv�lida");

builder.Services.AddAuthentication(Opts =>
{
    Opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    opts.RequireHttpsMetadata = false;
    opts.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
    };
});


//injetando o Automapper 
builder.Services.AddAutoMapper(typeof(Program));
/*
 Essa configura��o � usada para informar ao automapper quais tipos do seu aplicativo contem os perfis de mapeamento
 o parametro typeof espeficica que os perfis de mapeamento devem ser procurados no assembly, onde a classe est� localizada.
 
 Normalmente esse � o assembly principal ou assembly de inicializa��o da aplica��o.

 */

//assembly:UNidade de umplanta��o e distribui��o do .NET, que inclui c�digo executavel, metadados e outros recursos necessarios
//para executar um aplicativo

//Define o registro do servi�o usando o tempo de vida addScoped, para que o seri�o seja criado a cada request.
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
