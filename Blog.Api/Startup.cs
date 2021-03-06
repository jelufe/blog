using Blog.Api.Configurations;
using Blog.Domain.Interfaces.Repositories;
using Blog.Domain.Interfaces.Services;
using Blog.Domain.Services;
using Blog.Infrastructure.Data;
using Blog.Infrastructure.Filters;
using Blog.Infrastructure.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Blog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog.Api", Version = "v1" });
            });

            services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Blog"))
            );

            services.AddCors();

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthRepository, AuthRepository>();

            services.AddTransient<IAuthService, AuthService>();

            services.AddTransient<IPostRepository, PostRepository>();

            services.AddTransient<IPostService, PostService>();

            services.AddTransient<ICommentRepository, CommentRepository>();

            services.AddTransient<ICommentService, CommentService>();

            services.AddTransient<IImageRepository, ImageRepository>();

            services.AddTransient<IImageService, ImageService>();

            services.AddTransient<INotificationRepository, NotificationRepository>();

            services.AddTransient<INotificationService, NotificationService>();

            services.AddTransient<IVisualizationRepository, VisualizationRepository>();

            services.AddTransient<IVisualizationService, VisualizationService>();

            services.AddTransient<ILikeRepository, LikeRepository>();

            services.AddTransient<ILikeService, LikeService>();

            services.AddTransient<ISharingRepository, SharingRepository>();

            services.AddTransient<ISharingService, SharingService>();

            services.AddTransient<IDashboardRepository, DashboardRepository>();

            services.AddTransient<IDashboardService, DashboardService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog.Api v1"));
            }

            app.UseCustomCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
