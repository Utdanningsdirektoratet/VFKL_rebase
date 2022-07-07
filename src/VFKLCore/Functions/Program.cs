using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VFKLCore.Functions.Config;
using VFKLCore.Functions.Externsions;
using VFKLCore.Functions.Repository;
using VFKLCore.Functions.Services.Implementation;
using VFKLCore.Functions.Services.Interface;

namespace VFKLCore.Functions
{
    /// <summary>
    /// Host program for Azure Function
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        public static async Task Main()
        {
            bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            var host = new HostBuilder();
            
            host.ConfigureFunctionsWorkerDefaults();
            
            if (isDevelopment)
            {
                host.ConfigureServices(s =>
                {
                    s.AddOptions<VFKLCoreSettings>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("VFKLCoreSettings").Bind(settings);
                    });
                    s.AddOptions<KeyVaultSettings>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("KeyVault").Bind(settings);
                    });

                    s.AddOptions<QueueStorageSettings>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("QueueStorageSettings").Bind(settings);
                    });

                    s.AddTransient<IKeyVaultService, LocalKeyVaultService>();

                    s.AddSingleton<IAuthenticationService, AuthenticationService>();
                    s.AddSingleton<IQueueService, QueueService>();
                    s.AddSingleton<IStorage, StorageService>();
                    s.AddSingleton<IAssessmentRepository, AssessmentRepository>();
                    s.AddSingleton<IAnswerRepository, AnswerRepository>();
                    s.AddSingleton<IGroupInvitationRepository, GroupInvitationRepository>();
                    s.AddSingleton<IUserRepository, UserRepository>();
                    s.AddHttpClient<ISubscription, SubscriptionService>();
                    s.AddHttpClient<IAltinnApp, AltinnAppService>();
                    s.AddHttpClient<IPlatform, PlatformService>();
                    s.AddHttpClient<IAuthenticationClientWrapper, AuthenticationClientWrapper>();
                    s.AddHttpClient<IMaskinPortenClientWrapper, MaskinportenClientWrapper>();
                });
            }
            else
            {
                host.ConfigureServices(s =>
                {
                    s.AddOptions<VFKLCoreSettings>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("VFKLCoreSettings").Bind(settings);
                    });
                    s.AddOptions<KeyVaultSettings>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("KeyVault").Bind(settings);
                    });

                    s.AddOptions<QueueStorageSettings>()
                    .Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection("QueueStorageSettings").Bind(settings);
                    });

                    s.AddTransient<IKeyVaultService, KeyVaultService>();

                    s.AddSingleton<IAuthenticationService, AuthenticationService>();
                    s.AddSingleton<IQueueService, QueueService>();
                    s.AddSingleton<IStorage, StorageService>();
                    s.AddSingleton<IAssessmentRepository, AssessmentRepository>();
                    s.AddSingleton<IAnswerRepository, AnswerRepository>();
                    s.AddSingleton<IGroupInvitationRepository, GroupInvitationRepository>();
                    s.AddSingleton<IUserRepository, UserRepository>();
                    s.AddHttpClient<ISubscription, SubscriptionService>();
                    s.AddHttpClient<IAltinnApp, AltinnAppService>();
                    s.AddHttpClient<IPlatform, PlatformService>();
                    s.AddHttpClient<IAuthenticationClientWrapper, AuthenticationClientWrapper>();
                    s.AddHttpClient<IMaskinPortenClientWrapper, MaskinportenClientWrapper>();
                });
            }
                          
            await host.Build().RunAsync();
           
        }
    }
}
