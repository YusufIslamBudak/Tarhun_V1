using tarhunMVC.Services;

var builder = WebApplication.CreateBuilder(args);


// 📡 Tüm IP'lerden (özellikle yerel IP) bağlantı kabul et
builder.WebHost.UseUrls("http://0.0.0.0:5102");

// MVC
builder.Services.AddControllersWithViews();

// 👉 ArduinoDataService’i aynı nesne olarak hem Singleton hem HostedService olarak kaydet
builder.Services.AddSingleton<ArduinoDataService>();
builder.Services.AddSingleton<IHostedService>(provider => provider.GetService<ArduinoDataService>());

// Model servis
builder.Services.AddSingleton<ModelPredictionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();