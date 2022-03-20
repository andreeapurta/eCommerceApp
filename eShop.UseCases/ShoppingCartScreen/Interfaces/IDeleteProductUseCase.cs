﻿using eShop.Business.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public interface IDeleteProductUseCase
    {
        Task<Order> Execute(int productId);
    }
}