using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Business.Services.FileServices; // EKLENDÝ: IFileService için
using HumanResources.Entity.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Business.Services.UserServices
{
    public class UserService(UserManager<AppUser> _userManager
                            , IValidator<CreateUserDto> _createValidator
                            , IValidator<UpdateUserDto> _updateValidator
                            , IFileService _fileService) : IUserService // EKLENDÝ: IFileService dependency
    {

        public async Task<BaseResult<object>> CreateAsync(CreateUserDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var user = createDto.Adapt<AppUser>();

            user.IseGirisTarihi = DateTime.UtcNow;

            if (user.DogumTarihi != default)
            {
                user.DogumTarihi = DateTime.SpecifyKind(user.DogumTarihi, DateTimeKind.Utc);
            }

            user.UserName = createDto.UserName;
            user.Email = createDto.Email;

            // --- FOTOÐRAF YÜKLEME ---
            if (createDto.Fotograf != null)
            {
                // Kullanýcý id'si henüz oluþmadýðý için UserName üzerinden isimlendiriyoruz
                string customName = $"Profil_{createDto.UserName}_{DateTime.Now:yyyyMMdd}";
                user.FotografUrl = await _fileService.UploadFileAsync(createDto.Fotograf, "profiles", customName);
            }

            var result = await _userManager.CreateAsync(user, createDto.Password);

            if (!result.Succeeded)
            {
                // Identity hatalarýný string listesine çeviriyoruz
                return BaseResult<object>.Fail(result.Errors);
            }

            return BaseResult<object>.Success(new { Message = "Kullanýcý baþarýyla oluþturuldu." });
        }


        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
            {
                return BaseResult<object>.Fail("User Not Found");
            }

            user.IstenAyrilisTarihi = DateTime.UtcNow;
            user.SilindiMi = true;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BaseResult<object>.Fail(result.Errors);
            }

            return BaseResult<object>.Success(new { Message = "Deleted Success" });
        }


        public async Task<BaseResult<List<UserDto>>> GetAllAsync()
        {
            var items = await _userManager.Users.Include(x => x.Amir).AsNoTracking().ToListAsync();

            TypeAdapterConfig<AppUser, UserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null);

            var mappedItem = items.Adapt<List<UserDto>>();

            return BaseResult<List<UserDto>>.Success(mappedItem);
        }

        public async Task<BaseResult<List<ResultUserDto>>> GetAllUserWithDepartmentAndUnitAsync()
        {
            var users = await _userManager.Users
               .Include(u => u.Amir)
               .Include(u => u.Departman)
               .Include(u => u.Birim)
               .AsNoTracking()
               .ToListAsync();

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
                .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null);

            var userDtos = users.Adapt<List<ResultUserDto>>();

            return BaseResult<List<ResultUserDto>>.Success(userDtos);
        }

        public async Task<BaseResult<UserDto>> GetByIdAsync(int id)
        {
            var items = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            TypeAdapterConfig<AppUser, UserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null);

            var mappedItem = items.Adapt<UserDto>();

            return BaseResult<UserDto>.Success(mappedItem);
        }

        public async Task<BaseResult<ResultUserDto>> GetUserWithDepartmentAndUnitAsync(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return BaseResult<ResultUserDto>.Fail("User Not Found");
            }

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null);

            var userDto = user.Adapt<ResultUserDto>();

            return BaseResult<ResultUserDto>.Success(userDto);
        }

        public async Task<BaseResult<ResultUserDto>> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .Include(u => u.Amir)
                .FirstOrDefaultAsync(u => u.UserName == loginUserDto.UserName && !u.SilindiMi);

            if (user is null)
            {
                return BaseResult<ResultUserDto>.Fail("Kullanýcý adý veya þifre hatalý.");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!isPasswordCorrect)
            {
                return BaseResult<ResultUserDto>.Fail("Kullanýcý adý veya þifre hatalý.");
            }

            // MAPSTER AYARLARI
            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
              .Map(dest => dest.Ad, src => src.Departman != null ? src.Departman.Ad : null)
              .Map(dest => dest.Ad, src => src.Birim != null ? src.Birim.Ad : null);

            var userDto = user.Adapt<ResultUserDto>();

            // DEÐÝÞÝKLÝK BURADA: Kullanýcýnýn rollerini çekip DTO'ya ekliyoruz
            var userRoles = await _userManager.GetRolesAsync(user);
            userDto.Roller = userRoles;

            return BaseResult<ResultUserDto>.Success(userDto);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateUserDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var user = await _userManager.FindByIdAsync(updateDto.Id.ToString());

            if (user is null)
            {
                return BaseResult<object>.Fail("User Not Found");
            }

            updateDto.Adapt(user);

            if (updateDto.Fotograf != null)
            {
                if (!string.IsNullOrEmpty(user.FotografUrl))
                {
                    _fileService.DeleteFile(user.FotografUrl); // Eski fotoðrafý sunucudan temizle
                }

                string customName = $"Profil_{user.UserName}_{DateTime.Now:yyyyMMdd}";
                user.FotografUrl = await _fileService.UploadFileAsync(updateDto.Fotograf, "profiles", customName);
            }

            if (user.DogumTarihi != default)
            {
                user.DogumTarihi = DateTime.SpecifyKind(user.DogumTarihi, DateTimeKind.Utc);
            }

            user.IstenAyrilisTarihi = null;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BaseResult<object>.Fail(result.Errors);
            }

            return BaseResult<object>.Success();
        }



        public async Task<BaseResult<List<ResultUserDto>>> GetSubordinatesAsync(int amirId)
        {
            // AmirId'si gönderilen ID'ye eþit olan VE silinmemiþ personelleri getiriyoruz.
            // Þýk görünmesi için Departman ve Birim adlarýný da Include ediyoruz.
            var subordinates = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .Where(u => u.AmirId == amirId && !u.SilindiMi)
                .AsNoTracking()
                .ToListAsync();

            if (subordinates == null || !subordinates.Any())
            {
                return BaseResult<List<ResultUserDto>>.Fail("Bu personele doðrudan baðlý herhangi bir çalýþan bulunmamaktadýr.");
            }

            var mappedSubordinates = subordinates.Adapt<List<ResultUserDto>>();

            return BaseResult<List<ResultUserDto>>.Success(mappedSubordinates);
        }

        public async Task<BaseResult<List<ResultUserDto>>> GetAllUsersForReportAsync()
        {
            // Raporlama için özel: IgnoreQueryFilters() ile silinenleri/ayrýlanlarý da çeker
            var users = await _userManager.Users
                .IgnoreQueryFilters()
                .Include(u => u.Amir)
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .AsNoTracking()
                .ToListAsync();

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
                .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null);

            var userDtos = users.Adapt<List<ResultUserDto>>();

            return BaseResult<List<ResultUserDto>>.Success(userDtos);
        }



    }
}