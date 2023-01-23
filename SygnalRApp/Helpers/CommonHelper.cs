using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using SignalRApp.Entities;
using SignalRApp.Enums;
using SignalRApp.Extensions;
using SignalRApp.Repositories;
using SignalRApp.Repositories.Interfaces;
using SignalRApp.Services;
using SignalRApp.Services.Interfaces;

namespace SignalRApp.Helpers
{
    /// <summary>
    /// Хелпер 
    /// </summary>
    public static class CommonHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Производит инициализацию Identity
        /// </summary>
        /// <param name="host">Абстракция хоста</param>
        public static async Task InitializeIdentityAsync(IHost host)
        {
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleIdentity>>();
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();

                    await CreateRolesAsync(roleManager);
                    await CreateUsersAsync(db, userManager);
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка во время инициализации Identity, {ex.Message}");
            }
        }

        /// <summary>
        /// Асинхронно создаёт роли пользователей приложения
        /// </summary>
        /// <param name="roleManager">Менеджер ролей.</param>
        public static async Task CreateRolesAsync(RoleManager<RoleIdentity> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(UserRoleEnum)))
            {
                var userRole = Enum.Parse<UserRoleEnum>(role);
                var roleDisplayName = EnumExtensions.GetDisplayName<UserRoleEnum>(userRole);

                var isRoleExists = await roleManager.RoleExistsAsync(role);
                if (isRoleExists)
                {
                    var roleIdentity = await roleManager.FindByNameAsync(role);
                    roleIdentity.DisplayName = roleDisplayName;

                    var updateRoleResult = await roleManager.UpdateAsync(roleIdentity);
                    if (!updateRoleResult.Succeeded)
                    {
                        _logger.Error($"Ошибка во время обновления роли {role}");
                        continue;
                    }
                }
                else
                {
                    var roleIdentity = new RoleIdentity
                    {
                        Name = role,
                        DisplayName = roleDisplayName
                    };

                    var createRoleResult = await roleManager.CreateAsync(roleIdentity);
                    if (!createRoleResult.Succeeded)
                    {
                        _logger.Error($"Ошибка во время создания роли {role}");
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// Асинхронно создаёт начальных пользователей приложения
        /// </summary>
        /// <param name="db">Контекст БД.</param>
        /// <param name="userManager">Менеджер пользователей.</param>
        private static async Task CreateUsersAsync(ApplicationContext db, UserManager<UserIdentity> userManager)
        {
            if (db.Users.Any())
            {
                return;
            }

            var admins = GetAdminUsers();
            foreach (var admin in admins)
            {
                var identity = new UserIdentity
                {
                    UserName = admin.User.Login,
                    Email = admin.User.EmailPrimary
                };

                var createUserResult = await userManager.CreateAsync(identity, admin.Password);
                if (!createUserResult.Succeeded)
                {
                    _logger.Error($"Не удалось создать пользователя", new { Email = admin.User.EmailPrimary, Reason = createUserResult.Errors.FirstOrDefault()?.Description });

                    continue;
                }

                var addRoleResult = await userManager.AddToRoleAsync(identity, UserRoleEnum.Administrator.ToString());
                if (!addRoleResult.Succeeded)
                {
                    _logger.Error($"Не удалось добавить пользователю роль администратора", new { UserName = identity.UserName, Reason = addRoleResult.Errors.FirstOrDefault()?.Description });

                    continue;
                }

                admin.User.Identity = identity;
                db.UserEntities.Add(admin.User);
            }

            db.SaveChanges();
        }

        /// <summary>
        /// Возвращает учётные данные администраторов приложения в виде кортежа (пользователь, пароль)
        /// </summary>
        /// <remarks>
        /// Список данных - пользователь и пароль
        /// </remarks>
        private static IEnumerable<(UserEntity User, string Password)> GetAdminUsers()
        {
            return new List<(UserEntity, string)>  {
                    (new UserEntity("Inna", "Shutova", "inna.shutova", "i.schutowa2011@yandex.ru", ""), "1922091998Test"),
                    (new UserEntity("Nataly", "Myasnikova", "nataly.myasn", "nataly@yandex.ru", ""), "1922091998Test")
                };
        }

        /// <summary>
        /// Производит конфигурацию Identity для приложения
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddScoped<UserManager<UserIdentity>>();
            services.AddScoped<RoleManager<RoleIdentity>>();
            services.AddScoped<SignInManager<UserIdentity>>();

            services.AddIdentity<UserIdentity, RoleIdentity>()
               .AddEntityFrameworkStores<ApplicationContext>()
               .AddDefaultTokenProviders();

            const int passwordRequiredLength = 8;
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = passwordRequiredLength;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });
        }

        /// <summary>
        /// Производит конфигурацию сервисов
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessengerService, MessengerService >();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUsersService, UsersService>();
        }

        /// <summary>
        /// Производит инициализацию контекста БД
        /// </summary>
        /// <param name="host">Абстракция хоста</param>
        public static void InitializeDatabase(IHost host)
        {
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Ошибка во время инициализации БД, {ex.Message}");
            }
        }
    }
}

