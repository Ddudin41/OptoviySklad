//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OptSklad
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSklad
    {
        public int SkladID { get; set; }
        public int ProductID { get; set; }
        public double Count { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Sklad Sklad { get; set; }
    }
}
