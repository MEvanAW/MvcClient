﻿using MvcClient.Dtos.Basket;
using MvcClient.Models.Basket;

namespace MvcClient.Application
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketListModel>> Filter(BasketFilterDto basketFilterDto);
    }
}