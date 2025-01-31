using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.CustomerDtos;
using GalaxyStore.Domain.Models;
using Mapster;
using System.ComponentModel.DataAnnotations;

namespace GalaxyStore.Core.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<List<CustomerDto>>> GetAllCustomersAsync()
        {
            var customers = await _unitOfWork.Partners
                .GetAllAsync(p => p is Customer);

            var customerDtos = customers.Adapt<List<CustomerDto>>();

            return new ServiceResponse<List<CustomerDto>>
            {
                Success = true,
                Data = customerDtos,
                Message = "Customers retrieved successfully."
            };
        }

        public async Task<ServiceResponse<CustomerDto>> GetCustomerByIdAsync(int id)
        {
            var customer = await _unitOfWork.Partners
                .FirstOrDefaultAsync(p => p is Customer && p.Id == id);

            if (customer == null)
            {
                return new ServiceResponse<CustomerDto>
                {
                    Success = false,
                    Message = "Customer not found."
                };
            }

            var customerDto = customer.Adapt<CustomerDto>();

            return new ServiceResponse<CustomerDto>
            {
                Success = true,
                Data = customerDto,
                Message = "Customer retrieved successfully."
            };
        }

        public async Task<ServiceResponse<CustomerDto>> AddCustomerAsync(CreateCustomerDto dto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto);

            if (!Validator.TryValidateObject(dto, validationContext, validationResults, true))
            {
                var validationErrors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                return new ServiceResponse<CustomerDto>
                {
                    Success = false,
                    Message = $"Validation errors: {validationErrors}"
                };
            }

            var existingCustomer = await _unitOfWork.Partners
                .FirstOrDefaultAsync(p => p is Customer && ((Customer)p).Phone == dto.Phone);

            if (existingCustomer != null)
            {
                return new ServiceResponse<CustomerDto>
                {
                    Success = false,
                    Message = "A customer with the same phone number already exists."
                };
            }

            var customer = dto.Adapt<Customer>();
            customer.CreationDate = DateTime.UtcNow;

            await _unitOfWork.Partners.AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            var customerDto = customer.Adapt<CustomerDto>();

            return new ServiceResponse<CustomerDto>
            {
                Success = true,
                Data = customerDto,
                Message = "Customer added successfully."
            };
        }
    }
}
