using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    class MenuDishComparer : IEqualityComparer<MenuDish>
    {
        public bool Equals(MenuDish x, MenuDish y)
        {
            if (x.dishId == y.dishId)
                return true;

            return false;
        }

        public int GetHashCode(MenuDish obj)
        {
            return obj.dishId.GetHashCode();
        }
    }
}
