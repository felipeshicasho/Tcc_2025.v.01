using AutoMapper;
using GestaoMensalidades.API.DTOs.Auth;
using GestaoMensalidades.API.DTOs.Customer;
using GestaoMensalidades.API.Models;

namespace GestaoMensalidades.API.Mappings;

/// <summary>
/// Perfil de mapeamento do AutoMapper
/// Define como mapear entre DTOs e Models
/// </summary>
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapeamentos para User
        CreateMap<User, UserInfoDto>();

        // Mapeamentos para Customer
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BusinessOwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.BusinessOwner, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

        CreateMap<UpdateCustomerDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.BusinessOwnerId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.BusinessOwner, opt => opt.Ignore())
            .ForMember(dest => dest.Subscriptions, opt => opt.Ignore());

        // Mapeamentos para Subscription (quando implementarmos)
        // CreateMap<Subscription, SubscriptionDto>();
        // CreateMap<CreateSubscriptionDto, Subscription>();
        // CreateMap<UpdateSubscriptionDto, Subscription>();

        // Mapeamentos para Payment (quando implementarmos)
        // CreateMap<Payment, PaymentDto>();
        // CreateMap<CreatePaymentDto, Payment>();
        // CreateMap<UpdatePaymentDto, Payment>();
    }
}
