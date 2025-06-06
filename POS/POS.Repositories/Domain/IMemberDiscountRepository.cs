﻿using POS.Domain.Models.DataModels;
using POS.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Repositories.Domain
{
    public interface IMemberDiscountRepository:IBaseRepository<MemberDiscountEntity>
    {
        bool UpdateAsync(MemberDiscountEntity entity);
        bool DeleteAsync(MemberDiscountEntity entity);
    }
}
