using GalaxyStore.Domain.DTOs.TransferDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Interfaces.Service
{
    public interface ITransferService
    {
        Task<ServiceResponse<List<string>>> TransferToStoreAsync(TransferToStoreDto dto);
        Task<ServiceResponse<string>> TransferToStockAsync(TransferToStockDto dto);
    }
}
