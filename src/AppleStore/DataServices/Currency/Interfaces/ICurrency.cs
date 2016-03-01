﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleStore.DataServices.Currency.Interfaces
{
    public interface ICurrency
    {
        Task<Decimal> GetCurrency();
    }
}
