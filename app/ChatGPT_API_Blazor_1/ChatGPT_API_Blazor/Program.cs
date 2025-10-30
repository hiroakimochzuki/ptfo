using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ChatGPT_API_Blazor.Client.Pages;
using ChatGPT_API_Blazor.Components;
using ChatGPT_API_Blazor.Components.Account;
using ChatGPT_API_Blazor.Data;
using ChatGPT_API_Blazor.Services;
using ChatGPT_API_Blazor.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System.Globalization;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<ChatGPT_API_BlazorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChatGPT_API_BlazorContext") ?? throw new InvalidOperationException("Connection string 'ChatGPT_API_BlazorContext' not found.")));


builder.Services.AddQuickGridEntityFrameworkAdapter();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// 環境に応じてデータベースプロバイダーを選択
var isDevelopment = builder.Environment.IsDevelopment();
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

if (isDevelopment)
{
    // 開発環境: SQLite
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(defaultConnectionString));
}
else
{
    // 本番環境: SQL Server
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(defaultConnectionString));
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddScoped<ChatGPTService>();
builder.Services.AddScoped<PizzaService>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddScoped<OrderService>();

// ChatGPT_API_BlazorContextは常にSQL Serverを使用
var chatGptConnectionString = builder.Configuration.GetConnectionString("ChatGPT_API_BlazorContext") ?? throw new InvalidOperationException("Connection string 'ChatGPT_API_BlazorContext' not found.");
builder.Services.AddDbContext<ChatGPT_API_BlazorContext>(options =>
    options.UseSqlServer(chatGptConnectionString));
builder.Services.AddScoped<BookService>();

// ローカライゼーションサービスを追加
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// サポートするカルチャーを設定
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("ja-JP"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("ja-JP");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChatGPT_API_Blazor.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
