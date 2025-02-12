using ForthAssignment.Core.Aplication;
using ForthAssignment.Infraestructure.Persistence;
using ForthAssignment.Infraestructure.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfraestructureLayer(builder.Configuration);
builder.Services.AddSharedInfraestructureLayer(builder.Configuration);
builder.Services.AddAplicationLayer();


builder.Services.AddCors( options =>  options.AddPolicy("app", op =>
{
	op.AllowAnyOrigin();
	op.AllowAnyHeader();

}
	
) );

builder.Services.AddSession();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseCors("app");
app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
