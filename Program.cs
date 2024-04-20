using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

Trace.Listeners.Add(new TextWriterTraceListener("./AppData/data_trace.log"));
Trace.AutoFlush = true;
Trace.WriteLine($"Started at {DateTime.Now}:");
Trace.Indent();

// Add services to the container.
builder.Services.AddSession(); // New!
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession(); // New
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
