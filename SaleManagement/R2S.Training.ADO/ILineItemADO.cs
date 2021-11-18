﻿using R2S.Training.Entities;
using System.Collections.Generic;

namespace R2S.Training.ADO
{
    internal interface ILineItemADO
    {
        bool AddLineItem(LineItem item);
        List<LineItem> GetAllItemsByOrderId(int orderId);
    }
}
