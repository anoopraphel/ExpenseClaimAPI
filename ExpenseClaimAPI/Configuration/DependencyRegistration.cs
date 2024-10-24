using ExpenseClaimAPI.Filters;
using ExpenseClaimAPI.Logging;
using ExpenseClaimAPI.Services.Implementations;
using ExpenseClaimAPI.Services.Interfaces;
using Microsoft.Extensions.ApiDescriptions;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace ExpenseClaimAPI.Configuration
{
    public static class DependencyRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<ITextParserService, TextParserService>();
            services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<InvalidExpenseClaimExceptionFilter>(); 
        }
    }
}
