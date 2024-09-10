using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Translator.Core.Interfaces;
using Translator.Core.Services.Translators;
using Translator.Data.Contexts;
using Translator.Data.Repositories;
using Constants = Translator.Shared.Constants.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var startup = new Translator.Web.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<ITranslator>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var cache = sp.GetRequiredService<IMemoryCache>();

    return 
        new CachedTranslator(
            new LimitedRateTranslator(
                new LeetSpeakTranslator(httpClient),
                Constants.AppSettings.LeetSpeak.RateLimitCount,
                Constants.AppSettings.LeetSpeak.RateLimitPeriod), 
            cache);
});

builder.Services.AddScoped<ITranslationRepository, TranslationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

startup.Configure(app, builder.Environment);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Translation}/{action=Index}/{id?}");

app.Run();
