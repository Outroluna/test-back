using Microsoft.AspNetCore.Authentication.Cookies;///������������ ���� ��� ������ � ��������������� �� ������ ����-������
using Microsoft.AspNetCore.Identity;///������������ ���� ��� ������ � �������� ���������� �������������� (Identity)
using Microsoft.EntityFrameworkCore;///������������ ���� ��� ������ � Entity Framework Core, ORM ��� �������������� � ����� ������
using test_back.Data;
using test_back;
using test_back.Services;

var builder = WebApplication.CreateBuilder(args);///������� ������ builder, ������� ������������ ��� ������������ � �������� ���-����������
builder.Services.AddRazorComponents();
////��������� ��������� ���� ������ 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));///������������ ApplicationDbContext ��� ������������� � PostgreSQL ����� Entity Framework
///����������� ��������
builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IOrderService, OrderService>();
/////Configure Blazor WebAssembly
//builder.Services.AddServerSideBlazor();
var app = builder.Build();///������� ��������� ���-����������, ��������� ��� ���������, ��������� ����
app.UseAntiforgery();
app.MapRazorComponents<App>();
app.MapGet("/", () => "Hello World!");
app.Run();













//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using BlazorApp;
//using BlazorApp.Services;
//using test_back.Data;
//using test_back.Services;

//var builder = WebApplication.CreateBuilder(args);

/////������������ ApplicationDbContext � ���������� ������������ � ����������� ��� �� ������������� postgres
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//// ��������� Identity ��� ���������� ��������������
//builder.Services.AddIdentity<User, IdentityRole>(options => {
//    options.Password.RequireDigit = false;
//    options.Password.RequiredLength = 6;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//})
//.AddEntityFrameworkStores<ApplicationDbContext>()
//.AddDefaultTokenProviders();

//// ��������� �������������� � �������������� ����-������
//builder.Services.ConfigureApplicationCookie(options => {
//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//});

//// ����������� �������� ��� ������ � ������-�������
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IOrderService, OrderService>();

//// ��������� Blazor Server
//builder.Services.AddRazorComponents();
//builder.Services.AddServerSideBlazor();

//// ���������� ������������ � ��������������� � Blazor
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// ��������� ��������� ��������� ��������
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//// ��������� �������������� ��
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication(); // ����������� ��������������
//app.UseAuthorization();  // ����������� �����������










/////���������� ����� ��� �������������� �� ������ ����
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/Account/Login";
//        options.LogoutPath = "/Account/Logout";
//    });
/////���������� ��������� ������������ � Razor Pages
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

/////���������� �������� ��� Dependency Injection
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

/////���������� HttpClient ��� �������������� � API
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




///// ��������� HTTP-���������
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

//app.UseAuthentication();///���������� middleware ��������������
//app.UseAuthorization();///���������� middleware ����������� 

//app.MapControllers();
//app.MapRazorPages();

//app.Run();
