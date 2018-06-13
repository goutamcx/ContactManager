using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactApi.Models;

namespace ContactApi
{
    public class Startup
    {       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ContactContext>(opt => 
                opt.UseInMemoryDatabase("ContactList"));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}