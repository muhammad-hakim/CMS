using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.RollingFile;

namespace CMS
{
    public class Startup
	{
		private readonly IWebHostEnvironment _env;
		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			Configuration = configuration;
			_env = env;
			Config.WWWPath = _env.ContentRootPath + "/wwwroot/cms3/";
			Config.BaseWWWPath = _env.ContentRootPath;


			Log.Logger = new LoggerConfiguration()
				.MinimumLevel
				.Information()
				.WriteTo.RollingFile("log/log-{Date}.txt",Serilog.Events.LogEventLevel.Information)
				.CreateLogger();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddSingleton<HtmlEncoder>(
	 HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
		 								   UnicodeRanges.Arabic }));

			services.AddSingleton<JavaScriptEncoder>(
	 JavaScriptEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin,
											UnicodeRanges.Arabic }));

		 

			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				// Set a short timeout for easy testing.
				options.Cookie.Name = ".CMS.Session";
				options.IdleTimeout = TimeSpan.FromSeconds(100);
				options.Cookie.HttpOnly = true;
				// Make the session cookie essential
				options.Cookie.IsEssential = true;

			});

			services.AddHttpContextAccessor();

			services.AddResponseCompression();

			services.AddControllers();

			services.AddCors(); // Make sure you call this previous to AddMvc
								//services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//services.AddAuthentication(IISDefaults.AuthenticationScheme);
			services.AddMvc(options => options.Filters.Add(new AuthorizeFilter()));
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			  .AddCookie(options =>
			  {
				  options.Cookie.HttpOnly = true;
				  //options.Cookie.SecurePolicy = _environment.IsDevelopment()
				  //? CookieSecurePolicy.None : CookieSecurePolicy.Always;
				  options.Cookie.SameSite = SameSiteMode.Lax;

				  options.Cookie.Name = Guid.NewGuid().ToString();
				  options.LoginPath = "/cms3/login/Index";
				  options.LogoutPath = "/cms3/login/Logout";
				  options.AccessDeniedPath = "/cms3/login/AccessDenied";
				  options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
				 


				  options.Events = new CookieAuthenticationEvents
				  {
					  OnRedirectToLogin = ctx =>
					  {
						  var requestPath = ctx.Request.Path;
						  ctx.Response.Redirect("/cms3/login?ReturnUrl=" + requestPath + ctx.Request.QueryString);
						  //if (requestPath.StartsWithSegments("/Backend"))
						  //{
						  //    ctx.Response.Redirect("/BackEnd?ReturnUrl=" + requestPath + ctx.Request.QueryString);
						  //}
						  //else
						  //{
						  //    ctx.Response.Redirect("/cms3/login?ReturnUrl=" + requestPath + ctx.Request.QueryString);
						  //}
						  return Task.CompletedTask;
					  }
				  };


			  });

			services.Configure<CookiePolicyOptions>(options =>
			{
				options.MinimumSameSitePolicy = SameSiteMode.Strict;
				options.HttpOnly = HttpOnlyPolicy.None;
				//options.Secure = _environment.IsDevelopment()
				//  ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
			});
			services.Configure<KestrelServerOptions>(options =>
			{
				options.AllowSynchronousIO = true;
			});
			/*
						// If using IIS:
						services.Configure<IISServerOptions>(options =>
						{
							options.AllowSynchronousIO = true;
						});
						services.AddElmah<XmlFileErrorLog>(options =>
						{
							options.Path = @"errors";
							options.LogPath = Configuration["ElmahXmlLog"];  //@"C:\tabadul\Source\Repos\CMS\Main\wwwroot\Logs\";
																			 options.Notifiers.Add(new ErrorMailNotifier("Email",emailOptions));
						});
			*/

			//services.AddElmah<XmlFileErrorLog>(options =>
			//{
			//	options.Path = @"errors";
			//	options.LogPath = Configuration["ElmahXmlLog"];  //@"C:\tabadul\Source\Repos\CMS\Main\wwwroot\Logs\";
			//	///options.Notifiers.Add(new ErrorMailNotifier("Email", emailOptions));
			//});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			 loggerFactory.AddSerilog();

			DefaultFilesOptions options = new DefaultFilesOptions();
			options.DefaultFileNames.Clear();
			options.DefaultFileNames.Add("category.html");
			app.UseDefaultFiles(options);
			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			//app.Use(async (context, next) => {
			//	// Ignore requests that don't point to static files.
			//	if (!context.Request.Path.Value.Contains("edit"))
			//	{
			//		await next();
			//		return;
			//	}




			//	///Don't return a 401 response if the user is already authenticated.
			//	bool canSeeThePage = context.Request.Cookies["user_Lognstatus"] == null;
			//	if (canSeeThePage)
			//	{
			//		context.Response.Redirect("/cms");
			//	}

			//	await next();
			//	return;
			//	// Stop processing the request and trigger a challenge.
			//	///await context.Authentication.ChallengeAsync("Cookies");
			//});

			//app.UseStaticFiles(new StaticFileOptions()
			//{
			//	FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
			//	RequestPath = new PathString("/staticfiles")
			//});

			app.UseFileServer(enableDirectoryBrowsing: false);
			app.UseCors(
						options => options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
						);
			
			app.UseSession();

			CMS.AppHttpContext.Services = app.ApplicationServices;
			CMS.AppHttpContext.Configuration = Configuration;

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			

			app.UseResponseCompression();

			app.UseEndpoints(endpoints =>
			{
				//endpoints.MapControllers();
				endpoints.MapControllerRoute("default", "cms3/{controller=Login}/{action=Index}/{id?}");
			});
			 // app.UseElmah();

			//app.Run(async (context) =>
			//{
			//	await context.Response.WriteAsync("Hello World!");
			//	int[] numbers = new int[5];
			//	await context.Response.WriteAsync(numbers[6].ToString());
			//});
		}
	}
}
