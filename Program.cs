using Microsoft.AspNetCore.Authentication.Cookies;///пространство имен для работы с аутентификацией на основе куки-файлов
using Microsoft.AspNetCore.Identity;///пространство имен для работы с системой управления пользователями (Identity)
using Microsoft.EntityFrameworkCore;///пространство имен для работы с Entity Framework Core, ORM для взаимодействия с базой данных
using test_back.Data;
using test_back.Models;
using test_back.Services;

var builder = WebApplication.CreateBuilder(args);///Создает объект builder, который используется для конфигурации и создания веб-приложения

///Настройка контекста базы данных с использованием Entity Framework и PostgreSQL.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));///Регистрирует ApplicationDbContext для использования с PostgreSQL через Entity Framework

///Настройка Identity для управления пользователями, ролями и аутентификацией.
builder.Services.AddIdentity<User, IdentityRole>(options => ///Настраивает систему Identity для управления пользователями и ролями. Задает требования к паролям и другую информацию для аутентификации.
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()///Настраивает Identity использовать ApplicationDbContext для хранения данных пользователей и ролей
    .AddDefaultTokenProviders();///Добавляет поддержку токенов для двухфакторной аутентификации и восстановления пароля.

///Настройка аутентификации с использованием куки-файлов.
///Настраивает аутентификацию с использованием куки-файлов. Указывает пути для входа в систему и обработки ошибок доступа.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Регистрация CORS для взаимодействия с фронтендом
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:5062") // URL вашего фронтенда Blazor WebAssembly
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Регистрация контроллеров
builder.Services.AddControllersWithViews();

///Регистрация сервисов для работы с бизнес-логикой.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();///Создает экземпляр веб-приложения, используя все настройки, сделанные выше.

// Используем CORS
app.UseCors();

// Используем статические файлы
app.UseStaticFiles();

// Настройка маршрутизации
app.UseRouting();

// Используем аутентификацию и авторизацию
app.UseAuthentication();
app.UseAuthorization();

// Настройка маршрутов
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Для использования страниц Razor, если они есть















//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using BlazorApp;
//using BlazorApp.Services;
//using test_back.Data;
//using test_back.Services;

//var builder = WebApplication.CreateBuilder(args);

/////Регистрирует ApplicationDbContext в контейнере зависимостей и настраивает его на использование postgres
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Настройка Identity для управления пользователями
//builder.Services.AddIdentity<User, IdentityRole>(options => {
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();

//// Настройка аутентификации с использованием куки-файлов
//builder.Services.ConfigureApplicationCookie(options => {
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//});

//// Регистрация сервисов для работы с бизнес-логикой
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IOrderService, OrderService>();

//// Настройка Blazor Server
//builder.Services.AddRazorComponents();
//builder.Services.AddServerSideBlazor();

//// Добавление контроллеров с представлениями и Blazor
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Настройка конвейера обработки запросов
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//// Настройка промежуточного ПО
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication(); // Подключение аутентификации
//app.UseAuthorization();  // Подключение авторизации










/////Добавление служб для аутентификации на основе куки
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";
//        options.LogoutPath = "/Account/Logout";
//    });
/////Добавление поддержки контроллеров и Razor Pages
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

/////Добавление сервисов для Dependency Injection
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

/////Добавление HttpClient для взаимодействия с API
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//var app = builder.Build();






//builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddScoped<ProductService>();
//builder.Services.AddScoped<OrderService>();

//builder.Services.AddControllers();
//builder.Services.AddRazorPages();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//});




///// Настройка HTTP-конвейера
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseCors("AllowAllOrigins");

//app.UseAuthentication();///добавление middleware аутентификации
//app.UseAuthorization();///добавление middleware авторизации 

//app.MapControllers();
//app.MapRazorPages();

//app.Run();
