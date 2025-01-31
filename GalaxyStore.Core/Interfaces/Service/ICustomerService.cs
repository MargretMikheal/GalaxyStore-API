using GalaxyStore.Domain.DTOs;
using GalaxyStore.Domain.DTOs.CustomerDtos;
using GalaxyStore.Domain.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<CustomerDto>>> GetAllCustomersAsync();
        Task<ServiceResponse<CustomerDto>> GetCustomerByIdAsync(int id);
        Task<ServiceResponse<CustomerDto>> AddCustomerAsync(CreateCustomerDto dto);
    }
}
