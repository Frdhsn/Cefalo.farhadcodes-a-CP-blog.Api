using Cefalo.farhadcodes_a_CP_blog.Api.ErrorHandler;
using Cefalo.farhadcodes_a_CP_blog.Database.Context;
using Cefalo.farhadcodes_a_CP_blog.Repository.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Repository.Repositories;
using Cefalo.farhadcodes_a_CP_blog.Service.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.Story;
using Cefalo.farhadcodes_a_CP_blog.Service.DTO.User;
using Cefalo.farhadcodes_a_CP_blog.Service.DTOValidators;
using Cefalo.farhadcodes_a_CP_blog.Service.Formatters;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Contracts;
using Cefalo.farhadcodes_a_CP_blog.Service.Handler.Services;
using Cefalo.farhadcodes_a_CP_blog.Service.Services;
using Cefalo.TechDaily.Service.DtoValidators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Added this block
builder.Services.AddDbContext<CPContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//for auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
}).AddXmlDataContractSerializerFormatters()
            .AddMvcOptions(option =>
            {
                option.OutputFormatters.Add(new CSVOutputFormatter());
                option.OutputFormatters.Add(new PlainTextOutputFormatter());
                option.OutputFormatters.Add(new HtmlOutputFormatter());
            });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("EnvironmentVariable:token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// User
// repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
// services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPassword, Password>();
// Story
builder.Services.AddScoped<IStoryRepository, StoryRepository>();
builder.Services.AddScoped<IStoryService, StoryService>();

// dtovalidators
builder.Services.AddScoped<BaseDTOValidator<LoginDTO>, LoginDTOValidator>();
builder.Services.AddScoped<BaseDTOValidator<SignUpDTO>, SignUpDTOValidator>();
builder.Services.AddScoped<BaseDTOValidator<StoryDTO>, StoryDTOValidator>();
builder.Services.AddScoped<BaseDTOValidator<UpdateStory>, UpdateStoryValidator>();
builder.Services.AddScoped<BaseDTOValidator<UserDTO>, UserDTOValidator>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
