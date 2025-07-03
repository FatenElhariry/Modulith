using EShop.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Basket.Basket.Exceptions
{
    public class BasketNotFound : NotFoundException
    {
        public BasketNotFound(string userName): base("ShoppingCart", userName)
        { }
    }
}
