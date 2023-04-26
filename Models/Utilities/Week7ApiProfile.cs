using AutoMapper;
using Data.Entities;
using Models.DTOs;
using MyWebApi.Models.Authentication.Login;
using MyWebApi.Models.Authentication.SignUp;
using TutorialHell.Models;

namespace Models.Utilities
{
    public class Week7ApiProfile : Profile
    {
        public Week7ApiProfile()
        {
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Contact, AddContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
        }
       
    }
}
