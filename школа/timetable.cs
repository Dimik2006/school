//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace школа
{
    using System;
    using System.Collections.Generic;
    
    public partial class timetable
    {
        public int id { get; set; }
        public string lesson1 { get; set; }
        public string lesson2 { get; set; }
        public string lesson3 { get; set; }
        public string lesson4 { get; set; }
        public string lesson5 { get; set; }
        public string lesson6 { get; set; }
        public System.DateTime date { get; set; }
        public int @class { get; set; }
    
        public virtual @class class1 { get; set; }
    }
}
