using HumanResouerces.WebUI.Options;
using HumanResources.WebUI.Services.CertificateServices;
using HumanResources.WebUI.Services.CertificateTypeServices;
using HumanResources.WebUI.Services.DemirbasServices;
using HumanResources.WebUI.Services.DepartmentServices;
using HumanResources.WebUI.Services.DiciplineServices;
using HumanResources.WebUI.Services.EducationServices;
using HumanResources.WebUI.Services.ItemServices;
using HumanResources.WebUI.Services.ItemTypeServices;
using HumanResources.WebUI.Services.PermissionServices;
using HumanResources.WebUI.Services.PermissionTypeServices;
using HumanResources.WebUI.Services.ReportServices;
using HumanResources.WebUI.Services.RoleServices;
using HumanResources.WebUI.Services.ShiftServices;
using HumanResources.WebUI.Services.UnitServices;
using HumanResources.WebUI.Services.UserEducationServices;
using HumanResources.WebUI.Services.UserServices;

namespace HumanResouerces.WebUI.Extensions
{
    public static class HttpClientServices
    {
        public static void AddHttpClientService(this IServiceCollection services, IConfiguration configuration)
        {
            var apiOptions = configuration.GetSection(nameof(ApiOptions)).Get<ApiOptions>();

            // API tarafındaki routelar "api/[controller]" olduğu için base adrese /api/ ekliyoruz.
            // Örn: https://localhost:7018/api/
            string apiBaseUrl = $"{apiOptions.baseUrl}/api/";

            services.AddHttpClient<ICertificateService, CertificateService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<ICertificateTypeService, CertificateTypeService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IDepartmentService, DepartmentService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IDiciplineService, DiciplineService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IEducationService, EducationService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IItemService, ItemService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IItemTypeService, ItemTypeService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IPermissionService, PermissionService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IPermissionTypeService, PermissionTypeService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IReportService, ReportService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IRoleService, RoleService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IShiftService, ShiftService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IUnitService, UnitService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IUserEducationService, UserEducationService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IUserService, UserService>(options =>
            {
                options.BaseAddress = new Uri(apiBaseUrl);
            });

            services.AddHttpClient<IDemirbasService, DemirbasService>(c =>
            {
                c.BaseAddress = new Uri(apiBaseUrl); // diğer client'larla aynı base adres
            });
        }
    }
}