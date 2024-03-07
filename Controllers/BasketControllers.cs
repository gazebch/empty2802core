using empty2802core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace empty2802core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketControllers : ControllerBase
    {
        public DBContext DBContext { get; set; }
        public BasketControllers(DBContext context)
        {
            DBContext = context;
        }

        //[Authorize]
        [HttpPost(Name = "AddBasketProduct")]
        public string Add(Guid productId, string jtwToken, int countProduct)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(jtwToken);
            var userId = token.Claims.FirstOrDefault(x => x.Type == "UserId").Value;

            if (!DBContext.Products.ToList().Any(x => x.Id == productId))
            {
                return "Попытка добавить несуществующий товар";
            }

            if (DBContext.Basket.Any(x => x.Product.Id == productId))
            {
                var positionProduct = DBContext.Basket.FirstOrDefault(x => x.Product.Id == productId);

                positionProduct.Count += countProduct;

                DBContext.SaveChanges();
            }
            else
            {
                var basket = new Basket 
                { 
                    Id = Guid.NewGuid(),
                    Product = DBContext.Products.FirstOrDefault(x => x.Id == productId),
                    Users = DBContext.Users.FirstOrDefault(x => x.Id == new Guid(userId)),
                    Count = countProduct
                };

                DBContext.Basket.Add(basket);

                DBContext.SaveChanges();
            }

            return "Успешно добавлено в корзину";
        }

        [HttpDelete(Name = "DeleteBasketProduct")]
        public async Task Delete(Guid ProductId)
        {
            if (ProductId != Guid.Empty && DBContext.Basket.Any(x => x.Id == ProductId))
            {
                var deleteInfo = DBContext.Basket.FirstOrDefault(x => x.Id == ProductId);
                DBContext.Basket.Remove(deleteInfo);
                await DBContext.SaveChangesAsync();
            }
        }
    }
}
