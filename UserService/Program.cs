using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserService.Models;
using UserService.GraphQL;

var builder = WebApplication.CreateBuilder(args);

var conString = builder.Configuration.GetConnectionString("MyDatabase");
builder.Services.AddDbContext<SimpleOrderKafkaContext>(options =>
     options.UseSqlServer(conString)
);

// graphql
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddAuthorization();

builder.Services.AddControllers();

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
// role-based identity
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
            ValidateIssuer = true,
            ValidAudience = builder.Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings").GetValue<string>("Key"))),
            ValidateIssuerSigningKey = true
        };

    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowOrigin", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.MapGet("/", () => "Hello World!");

app.Run();
