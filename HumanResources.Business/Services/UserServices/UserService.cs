using FluentValidation;
using FluentValidation.Results;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Business.Services.FileServices;
using HumanResources.Entity.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Business.Services.UserServices
{
    public class UserService(UserManager<AppUser> _userManager
                            , IValidator<CreateUserDto> _createValidator
                            , IValidator<UpdateUserDto> _updateValidator
                            , IFileService _fileService) : IUserService
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
                string customName = $"Profil_{createDto.UserName}_{DateTime.Now:yyyyMMdd}";
                user.FotografUrl = await _fileService.UploadFileAsync(createDto.Fotograf, "profiles", customName);
            }

            var result = await _userManager.CreateAsync(user, createDto.Password);
            if (!result.Succeeded)
            {
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

        public async Task<BaseResult<UserDto>> GetByIdAsync(int id)
        {
            var items = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            TypeAdapterConfig<AppUser, UserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null);

            var mappedItem = items.Adapt<UserDto>();
            return BaseResult<UserDto>.Success(mappedItem);
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
                    _fileService.DeleteFile(user.FotografUrl);
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

        // --- ÝLÝÞKÝLÝ (DEPARTMENT, BÝRÝM, VARDÝYA) METOTLAR ---

        // 1. EKSÝK OLAN METOT (CS0535 Hatasýnýn Çözümü)
        public async Task<BaseResult<List<ResultUserDto>>> GetAllUserWithDepartmentAndUnitAsync()
        {
            var users = await _userManager.Users
                .Include(u => u.Amir)
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .Include(u => u.Vardiya)
                .AsNoTracking()
                .ToListAsync();

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
                .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
                .Map(dest => dest.VardiyaAciklama, src => src.Vardiya != null ? src.Vardiya.Aciklama : null);

            var userDtos = users.Adapt<List<ResultUserDto>>();
            return BaseResult<List<ResultUserDto>>.Success(userDtos);
        }

        // 2. TEK KULLANICI GETÝREN METOT (ÇÝFT KOPYASI SÝLÝNDÝ - CS0111 Çözümü)
        public async Task<BaseResult<ResultUserDto>> GetUserWithDepartmentAndUnitAsync(int id)
        {
            var user = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .Include(u => u.Amir)
                .Include(u => u.Vardiya)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return BaseResult<ResultUserDto>.Fail("User Not Found");
            }

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
              .Map(dest => dest.VardiyaAciklama, src => src.Vardiya != null ? src.Vardiya.Aciklama : null);

            var userDto = user.Adapt<ResultUserDto>();
            return BaseResult<ResultUserDto>.Success(userDto);
        }

        // 3. BÝRÝME GÖRE PERSONEL GETÝREN METOT (ÇÝFT KOPYASI SÝLÝNDÝ - CS0111 Çözümü)
        public async Task<BaseResult<List<ResultUserDto>>> GetUsersByUnitIdAsync(int unitId)
        {
            var users = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .Include(u => u.Amir)
                .Include(u => u.Vardiya)
                .Where(u => u.BirimId == unitId && !u.SilindiMi)
                .AsNoTracking()
                .ToListAsync();

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
                .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
                .Map(dest => dest.VardiyaAciklama, src => src.Vardiya != null ? src.Vardiya.Aciklama : null);

            var mappedUsers = users.Adapt<List<ResultUserDto>>();
            return BaseResult<List<ResultUserDto>>.Success(mappedUsers);
        }

        // --- DÝÐER METOTLAR ---

        public async Task<BaseResult<ResultUserDto>> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .Include(u => u.Amir)
                .FirstOrDefaultAsync(u => u.UserName == loginUserDto.UserName && !u.SilindiMi);


            if (user is null)
            {
                return BaseResult<ResultUserDto>.Fail(new List<ValidationFailure>
    {
        new ValidationFailure(nameof(LoginUserDto.UserName), "Kullanýcý adý veya þifre hatalý.")
    });
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!isPasswordCorrect)
            {
                return BaseResult<ResultUserDto>.Fail(new List<ValidationFailure>
    {
        new ValidationFailure(nameof(LoginUserDto.Password), "Þifreniz hatalý lütfen kontrol ediniz.")
    });
            }

            TypeAdapterConfig<AppUser, ResultUserDto>.NewConfig()
              .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
              .Map(dest => dest.Ad, src => src.Departman != null ? src.Departman.Ad : null)
              .Map(dest => dest.Ad, src => src.Birim != null ? src.Birim.Ad : null);

            var userDto = user.Adapt<ResultUserDto>();

            var userRoles = await _userManager.GetRolesAsync(user);
            userDto.Roller = userRoles;

            return BaseResult<ResultUserDto>.Success(userDto);
        }

        public async Task<BaseResult<List<ResultUserDto>>> GetSubordinatesAsync(int amirId)
        {
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

        public async Task<BaseResult<List<UserDto>>> GetUsersByRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            var userIds = usersInRole.Select(u => u.Id).ToList();

            var users = await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .AsNoTracking()
                .ToListAsync();

            var result = users.Select(u => new UserDto
            {
                Id = u.Id,
                Ad = u.Ad,
                Soyad = u.Soyad,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                FotografUrl = u.FotografUrl,
                DepartmanId = u.DepartmanId,
                DepartmanAd = u.Departman != null ? u.Departman.Ad : null,
                BirimId = u.BirimId,
                BirimAd = u.Birim != null ? u.Birim.Ad : null
            }).ToList();

            return BaseResult<List<UserDto>>.Success(result);
        }



        public async Task<BaseResult<List<ResultUserDto>>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users
                .Include(u => u.Departman)
                .Include(u => u.Birim)
                .AsNoTracking()
                .ToListAsync();

            var list = new List<ResultUserDto>();
            foreach (var u in users)
            {
                var dto = u.Adapt<ResultUserDto>();          // Departman/Birim map olur
                dto.Roller = await _userManager.GetRolesAsync(u); // kullanýcýnýn rolleri
                list.Add(dto);
            }

            return BaseResult<List<ResultUserDto>>.Success(list);
        }


    }
}