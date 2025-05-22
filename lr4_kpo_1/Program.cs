using Microsoft.EntityFrameworkCore;
using lr4_kpo_1.Data;
using lr4_kpo_1.Repositories;
using lr4_kpo_1.Services;
using lr4_kpo_1.ViewModelBuilders;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudyTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация репозиториев и сервисов как Scoped
builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<CoursesVmBuilder>();

var app = builder.Build();

// Настройка HTTP pipeline
if (!app.Environment.IsDevelopment())
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