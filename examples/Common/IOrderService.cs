using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IOrderService
    {
        decimal CalculateFinalOrderSum(long userId, decimal originalSum);
    }
}
