using GalaxyStore.Core.Interfaces.Repositories;
using GalaxyStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Core.Helper
{
    public class BarcodeGenerator
    {
        private readonly IUnitOfWork _unitOfWork;

        public BarcodeGenerator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateBarcodeAsync(int productId)
        {
            string yearCode = DateTime.UtcNow.Year.ToString().Substring(1, 3);

            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            string productOrder = product.Serial.PadLeft(4, '0');
            var lastItem = await _unitOfWork.Items
                .GetAllAsync(i => i.ProductId == productId);

            int lastItemCode = lastItem.Any() ? lastItem.Max(i => int.Parse(i.Barcode.Substring(7, 5))) : 0;
            string itemOrder = (lastItemCode + 1).ToString("D5");

            return $"{yearCode}{productOrder}{itemOrder}";
        }
    }
}
