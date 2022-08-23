using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;
using Ticketing.Data;
using Ticketing.Data.Persistence;
using Ticketing.Services;
using Ticketing.Services.Identity.SeedDatabaseService;
using Ticketing.WebApi.WebFrameWork.Filters;
using WebFramework.Swagger;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(OkResultAttribute));
    options.Filters.Add(typeof(NotFoundResultAttribute));
    options.Filters.Add(typeof(ContentResultFilterAttribute));
    options.Filters.Add(typeof(ModelStateValidationAttribute));
    options.Filters.Add(typeof(BadRequestResultFilterAttribute));

}).ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; }).AddJsonOptions(
             options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
             );

builder.Services.AddSwagger();
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<SeedDataBase>();
        await initialiser.SeedAsync();
    }




}
app.UseSwaggerUI(c =>
{

    c.InjectStylesheet("/SwaggerUi/SwaggerDark.css");
    c.InjectJavascript("/SwaggerUi/swagger-ui-bundle.js");
    c.DefaultModelExpandDepth(depth: 1);
    c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Example);
    c.DocExpansion(DocExpansion.None);

});
app.UseSwagger();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");



});


app.Run();
