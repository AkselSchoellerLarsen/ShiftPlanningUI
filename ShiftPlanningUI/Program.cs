using ShiftPlanningUI.Model;
using ShiftPlanningUI.Model.Shifts;
using ShiftPlanningUI.Model.Users;
using ShiftPlanningUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserCatalogue, UserCatalogue>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IShiftCatalogue, ShiftCatalogue>();
builder.Services.AddScoped<ISelectionService, SelectionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/Error"); app.UseHsts(); }

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

RESTHelper.BaseAddress = app.Configuration.GetValue<string>("RESTAddress");

app.Run();