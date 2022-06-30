using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Identity.Web;
using MealPlanAPI.Model;
using MealPlanAPI.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Create connectionstring using secrets
var conStrBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("MealPlan"));
conStrBuilder.Password = builder.Configuration["Dbpw"];
var connection = conStrBuilder.ConnectionString;

// Add DbContext to the builder
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connection));

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi()
            .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();

builder.Services.AddAzureClients(b => {
    b.AddBlobServiceClient(builder.Configuration.GetSection("Storage:ConnectionString").Value);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
