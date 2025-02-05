using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quorum.Application.Interfaces;
using Quorum.Infrastructure.Configuration;
using Quorum.Infrastructure.Repositories;

namespace Quorum.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register configurations
        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<DataSettings>>().Value;
            var basePath = sp.GetRequiredService<IHostEnvironment>().ContentRootPath;
            var dataPath = Path.Combine(basePath, settings.FolderPath);
            
            return new LegislatorFileConfig(dataPath, settings.Legislators.FileName);
        });

        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<DataSettings>>().Value;
            var basePath = sp.GetRequiredService<IHostEnvironment>().ContentRootPath;
            var dataPath = Path.Combine(basePath, settings.FolderPath);
            
            return new BillFileConfig(dataPath, settings.Bills.FileName);
        });

        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<DataSettings>>().Value;
            var basePath = sp.GetRequiredService<IHostEnvironment>().ContentRootPath;
            var dataPath = Path.Combine(basePath, settings.FolderPath);
            
            return new VoteFileConfig(dataPath, settings.Votes.FileName);
        });

        services.AddScoped(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<DataSettings>>().Value;
            var basePath = sp.GetRequiredService<IHostEnvironment>().ContentRootPath;
            var dataPath = Path.Combine(basePath, settings.FolderPath);
            
            return new VoteResultFileConfig(dataPath, settings.VoteResults.FileName);
        });

        // Register repositories
        services.AddScoped<ILegislatorRepository>(sp => 
            new CsvLegislatorRepository(
                sp.GetRequiredService<LegislatorFileConfig>()));

        services.AddScoped<IBillRepository>(sp => 
            new CsvBillRepository(
                sp.GetRequiredService<BillFileConfig>()));

        services.AddScoped<IVoteRepository>(sp => 
            new CsvVoteRepository(
                sp.GetRequiredService<VoteFileConfig>()));

        services.AddScoped<IVoteResultRepository>(sp => 
            new CsvVoteResultRepository(
                sp.GetRequiredService<VoteResultFileConfig>()));

        // Register Unit of Work
        services.AddScoped<IQuorumUnitOfWork, QuorumUnitOfWork>();

        return services;
    }
} 