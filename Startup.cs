using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Patient}/{action=Index}/{id?}");
        });

        // Add reverse proxy middleware here
        app.Run(async context =>
        {
            var targetUrl = $"http://40.81.129.124:5103{context.Request.Path}{context.Request.QueryString}";
            using var httpClient = new HttpClient();

            var requestMessage = new HttpRequestMessage(new HttpMethod(context.Request.Method), targetUrl);

            // Copy headers from the incoming request
            foreach (var header in context.Request.Headers)
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            var response = await httpClient.SendAsync(requestMessage);

            context.Response.StatusCode = (int)response.StatusCode;

            // Copy headers from the response
            foreach (var header in response.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in response.Content.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            var responseBody = await response.Content.ReadAsByteArrayAsync();
            await context.Response.Body.WriteAsync(responseBody);
        });
    }
}
