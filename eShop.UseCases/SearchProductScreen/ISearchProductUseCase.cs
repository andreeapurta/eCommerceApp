﻿using eShop.Business.Models;
using System.Collections.Generic;

namespace eShop.UseCases.SearchProductScreen
{
    public interface ISearchProductUseCase
    {
        IEnumerable<Product> Execute(string filter = null);
    }
}