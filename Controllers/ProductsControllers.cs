using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using empty2802core.Entities;

namespace empty2802core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsControllers : ControllerBase
    {
        public DBContext DBContext { get; set; }
        public ProductsControllers(DBContext context)
        {
            DBContext = context;
        }

        [HttpGet(Name = "GetProducts")]
        public string Get()
        {
            var allProducts = DBContext.Products.ToArray();

            return JsonSerializer.Serialize(allProducts.ToList());
        }

        [HttpPost(Name = "AddProducts")]
        public async Task Post(Products product)
        {
            using var insertInfo = new DBContext();
            await insertInfo.Products.AddAsync(product);
            await insertInfo.SaveChangesAsync();
        }

        /// <summary>
        /// Поиск продуктов
        /// </summary>
        /// <param name="model">Продукт</param>
        /// <returns></returns>
        [HttpPost("Search")]
        public string Search(string searchWord)
        {
            if (string.IsNullOrWhiteSpace(searchWord))
            {
                return "Строка поиска пуста";
            }
            var allProducts = DBContext.Products
                    .Where(x => x.Name.Contains(searchWord)).ToList();

            return JsonSerializer.Serialize(allProducts.ToList());
        }

    }
}
