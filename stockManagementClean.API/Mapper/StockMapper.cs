using AutoMapper;
using stockManagement.Models.Entity;
using stockManagement.Models.Enums;
using stockManagement.Models.StockDTO.UsersDTO;
using System.Linq.Expressions;
namespace stockManagementClean.API.Mapper
{
    public class StockMapper : Profile
    {
        public StockMapper() 
        {
            CreateMap<AddUpdateUserRequestDTO, Users>()
                .ForMember(a=>a.UserName, dest=>dest.MapFrom(dest=>dest.UserName))
                .ForMember(a => a.Password, dest => dest.MapFrom(dest => dest.Password))
                .ForMember(a => a.PasswordHashed, dest => dest.MapFrom(a=>"HashedPassword"))
                .ForMember(a => a.UserRole, dest => dest.MapFrom(a=>"New User"))
                .ForMember(a => a.CreatedOn, dest => dest.MapFrom(a=>DateTime.Now.ToShortDateString()))
                .ForMember(a => a.UpdatedOn, dest => dest.MapFrom(a=>DateTime.Now.ToShortDateString()))
                .ForMember(a => a.CreatedBy, dest => dest.MapFrom(a=>"Admin".ToString()))
                .ForMember(a => a.UpdatedBy, dest => dest.MapFrom(a=>"Admin".ToString()));

            CreateMap<Users, AddCreateUsersResponse>()
                .ForMember(a => a.UserName, dest => dest.MapFrom(dest => dest.UserName))
                .ForMember(a => a.Password, dest => dest.MapFrom(dest => dest.Password))
                .ForMember(a => a.UserRole, dest => dest.MapFrom(dest => dest.UserRole));
        }

        private string HashPassword(string v)
        {
            return v + "aaaaaaaaa"; 
        }
    }
}
