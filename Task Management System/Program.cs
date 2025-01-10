using MyTask_Management_System.Core.Helper;
using MyTask_Management_System.Extensions;

var builder = WebApplication.CreateBuilder(args);
string text = "";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices(builder);
builder.Services.AddIdentityService(builder.Configuration);
//builder.Services.AddSwaggerDocumantion();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Token>(builder.Configuration.GetSection("Token"));

//add cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(text,
    builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseCors(text);

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
