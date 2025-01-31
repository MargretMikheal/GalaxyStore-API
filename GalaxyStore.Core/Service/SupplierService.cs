using GalaxyStore.Core.Helper;
using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Core.Interfaces.Service;
using GalaxyStore.Domain.DTOs.SupplierDtos;
using GalaxyStore.Domain.Models;
using Mapster;
using System.ComponentModel.DataAnnotations;

namespace GalaxyStore.Core.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<List<SupplierDto>>> GetAllSuppliersAsync()
        {
            var partners = await _unitOfWork.Partners
                .GetAllAsync(p => p is Supplier);
            var suppliers = partners.OfType<Supplier>().ToList();

            var supplierDtos = suppliers.Select(supplier =>
            {
                var dto = supplier.Adapt<SupplierDto>();

                Console.WriteLine($"Image Path: {supplier.Image}");
                Console.WriteLine($"ID Image Path: {supplier.IdImagePath}");

                dto.Image = FileHelper.ReadFileAsBytes(supplier.Image);
                dto.IdImage = FileHelper.ReadFileAsBytes(supplier.IdImagePath); 

                Console.WriteLine($"Image: {dto.Image != null}");
                Console.WriteLine($"ID Image: {dto.IdImage != null}");

                return dto;
            }).ToList();

            return new ServiceResponse<List<SupplierDto>>
            {
                Success = true,
                Data = supplierDtos,
                Message = "Suppliers retrieved successfully."
            };
        }

        public async Task<ServiceResponse<SupplierDto>> GetSupplierByIdAsync(int id)
        {
            var partner = await _unitOfWork.Partners
                .FirstOrDefaultAsync(p => p is Supplier && p.Id == id);
            var supplier = partner as Supplier;

            if (supplier == null)
            {
                return new ServiceResponse<SupplierDto>
                {
                    Success = false,
                    Message = "Supplier not found."
                };
            }

            var supplierDto = supplier.Adapt<SupplierDto>();
            supplierDto.Image = FileHelper.ReadFileAsBytes(supplier.Image); 
            supplierDto.IdImage = FileHelper.ReadFileAsBytes(supplier.IdImagePath); 

            return new ServiceResponse<SupplierDto>
            {
                Success = true,
                Data = supplierDto,
                Message = "Supplier retrieved successfully."
            };
        }





        public async Task<ServiceResponse<SupplierDto>> AddSupplierAsync(CreateSupplierDto dto)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dto);

            if (!Validator.TryValidateObject(dto, validationContext, validationResults, true))
            {
                var validationErrors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                return new ServiceResponse<SupplierDto>
                {
                    Success = false,
                    Message = $"Validation errors: {validationErrors}"
                };
            }
            const string imageSubFolder = "partners/partnersPhoto";
            const string idImageSubFolder = "partners/partnersId";

            var imagePath = FileHelper.SaveFileAsync(dto.Image, imageSubFolder);
            var idImagePath = FileHelper.SaveFileAsync(dto.IdImage, idImageSubFolder);

            var supplier = dto.Adapt<Supplier>();
            supplier.CreationDate = DateTime.UtcNow;
            supplier.Image = await imagePath; 
            supplier.IdImagePath = await idImagePath; 

            await _unitOfWork.Partners.AddAsync(supplier);
            await _unitOfWork.CompleteAsync();

            var supplierDto = supplier.Adapt<SupplierDto>();

            return new ServiceResponse<SupplierDto>
            {
                Success = true,
                Data = supplierDto,
                Message = "Supplier added successfully."
            };
        }

    }
}
