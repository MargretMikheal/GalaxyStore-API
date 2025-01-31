using GalaxyStore.Domain.DTOs.SupplierDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface ISupplierService
    {
        Task<ServiceResponse<List<SupplierDto>>> GetAllSuppliersAsync();
        Task<ServiceResponse<SupplierDto>> GetSupplierByIdAsync(int id);
        Task<ServiceResponse<SupplierDto>> AddSupplierAsync(CreateSupplierDto dto);
    }
}
