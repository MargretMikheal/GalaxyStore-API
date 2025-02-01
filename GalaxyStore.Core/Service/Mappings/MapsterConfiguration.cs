using GalaxyStore.Core.Helper;
using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.DTOs.InvoiceDtos;
using GalaxyStore.Domain.DTOs.ProductDtos;
using GalaxyStore.Domain.DTOs.SupplierDtos;
using GalaxyStore.Domain.Identity;
using GalaxyStore.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GalaxyStore.Core.Service.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMappings()
        {
            // Map ApplicationUser -> UserDto
            TypeAdapterConfig<ApplicationUser, UserDto>.NewConfig()
                .Map(dest => dest.UserId, src => src.Id)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Name, src => src.Name);

            // Map UserDto -> ApplicationUser
            TypeAdapterConfig<UserDto, ApplicationUser>.NewConfig()
                .Map(dest => dest.Id, src => src.UserId)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Name, src => src.Name);

            // Map UpdateProfileDto -> ApplicationUser
            TypeAdapterConfig<UpdateProfileDto, ApplicationUser>.NewConfig()
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Name, src => src.Name);         

            TypeAdapterConfig<Supplier, SupplierDto>.NewConfig()
           .Ignore(dest => dest.Image)
           .Ignore(dest => dest.IdImage);

              TypeAdapterConfig<Product, ProductDto>.NewConfig()
           .Ignore(dest => dest.ProductPhoto);

              TypeAdapterConfig<Product, ProductDetailsDto>.NewConfig()
           .Ignore(dest => dest.ProductPhoto);


            TypeAdapterConfig<CreateInvoiceDto, Invoice>.NewConfig()
                .Map(dest => dest.Type, src => Domain.Enums.InvoiceType.Buying)
                .Ignore(dest => dest.CreationDate)
                .Ignore(dest => dest.TotalPay);


        }
    }
}
