using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ticketing.businessLogicLayer.Services.Implementations;
using Ticketing.businessLogicLayer.Services.Interfaces;
using Ticketing.businessLogicLayer.Tools.AutoMapperProfiles;
using Ticketing.businessLogicLayer.Validators.MessageValidator;
using Ticketing.businessLogicLayer.Validators.TicketValidator;
using Ticketing.businessLogicLayer.Validators.UserValidator;
using Ticketing.DataAccessLayer.Context;
using Ticketing.DataAccessLayer.Implementations;
using Ticketing.DataAccessLayer.Interfaces;
using Ticketing.Dtos.MessageDtos;
using Ticketing.Dtos.TicketDtos;
using Ticketing.Dtos.UserDtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IMessageService, MessageService>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISupporterRatingRepository, SupporterRatingRepository>();
builder.Services.AddTransient<IValidator<CreateUserInputDto>, CreateUserInputDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateUserInputDto>, UpdateUserInputDtoValidator>();
builder.Services.AddTransient<IValidator<TicketInputDto>, TicketInputDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateTicketInputDto>, UpdateTicketInputDtoValidator>();
builder.Services.AddTransient<IValidator<MessageInputDto>, MessageInputDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateMessageDto>, UpdateMessageDtoValidator>();
builder.Services.AddControllers();
// Add services to the container.
// builder.Services.AddControllers().AddJsonOptions(x =>
//     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// builder.Services.Configure<ApiBehaviorOptions>(options =>
// {
//     options.SuppressModelStateInvalidFilter = true;
// });
//configure mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

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
