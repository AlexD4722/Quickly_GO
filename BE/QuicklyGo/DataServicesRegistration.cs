using Microsoft.EntityFrameworkCore;
using QuicklyGo.Contracts.IData;
using QuicklyGo.Data;
using QuicklyGo.Data.Responsive;
using QuicklyGo.Data.Respositories;
using QuicklyGo.Models;

namespace QuicklyGo
{
    public static class DataServicesRegistration
    {
        public static IServiceCollection ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuicklyGoDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("QuicklyGoDbContext")));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();           
            services.AddTransient<IConversationRepository, ConversationRepository>();
            services.AddTransient<IUserConversationRepository, UserConversationRepository>();
            services.AddTransient<IReadMessageRepository, ReadMessageRepository>();
            services.AddTransient<IRelationshipRepository, RelationshipRepository>();
            return services;
        }
    }
}
