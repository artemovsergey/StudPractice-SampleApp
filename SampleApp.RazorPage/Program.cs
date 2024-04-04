using Microsoft.EntityFrameworkCore;
using SampleApp.Domen.Models;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSassCompiler();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SampleAppContext>(options => options.UseSqlServer(connection));
builder.Services.AddFlashes();


//builder.Services.AddScoped<HttpClient>();


// Настройка прокси
var proxy = new WebProxy
{
    Address = new Uri("http://gate1.scc:3128")
};

// Создание HttpClientHandler с прокси
var httpClientHandler = new HttpClientHandler
{
    Proxy = proxy,
    UseProxy = false
};

// Регистрация HttpClient с настройкой прокси
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7225/api");
})
.ConfigurePrimaryHttpMessageHandler(() => httpClientHandler);


builder.Services.AddSession(options =>
{
    options.Cookie.Name = "SampleSession";
    //options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.UseSession();


app.Run();
