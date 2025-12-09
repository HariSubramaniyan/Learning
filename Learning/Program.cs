using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore; // Ensure this is included for UseSqlServer  
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Learning.Data;
using Microsoft.AspNetCore.Identity;
using Learning.Repo.Iservices;
using Learning.Repo.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.  

        builder.Services.AddControllers();

        builder.Services.AddScoped<ITokenRepo, TokenRepo>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<LearningAuthDB>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("LearningConnectionString"))
        );

        builder.Services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Learning")
            .AddEntityFrameworkStores<LearningAuthDB>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>                              
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            //options.Password.RequireAlphanumeric = false;
            options.Password.RequireUppercase = false;
            //options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 6;
            //options.Password.RequiredUnichr = 1;

        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = builder.Configuration["Jwt:Issuer"], // Fixed the key here  
             ValidAudience = builder.Configuration["Jwt:Audience"],
             IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
         });

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
    }
}