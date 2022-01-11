using WebApplication.Extensions;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJWTTokenServices(builder.Configuration);
//var bindJwtSettings = new JwtSettings();
//builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
//builder.Services.AddSingleton(bindJwtSettings);
//builder.Services.AddAuthentication(options => {
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options => {
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
//        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
//        ValidateIssuer = bindJwtSettings.ValidateIssuer,
//        ValidIssuer = bindJwtSettings.ValidIssuer,
//        ValidateAudience = bindJwtSettings.ValidateAudience,
//        ValidAudience = bindJwtSettings.ValidAudience,
//        RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
//        ValidateLifetime = bindJwtSettings.RequireExpirationTime,
//        ClockSkew = TimeSpan.FromDays(1),
//    };
//});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.UseAuthorization(); //the place where we use UseAuthorization is very important
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
