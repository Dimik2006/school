﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class schoolEntities2 : DbContext
    {
        public schoolEntities2()
            : base("name=schoolEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<account> account { get; set; }
        public virtual DbSet<administrator> administrator { get; set; }
        public virtual DbSet<assessment> assessment { get; set; }
        public virtual DbSet<assessmentForStudent> assessmentForStudent { get; set; }
        public virtual DbSet<attendance> attendance { get; set; }
        public virtual DbSet<attendancesForStudent> attendancesForStudent { get; set; }
        public virtual DbSet<@class> @class { get; set; }
        public virtual DbSet<classesForTeacher> classesForTeacher { get; set; }
        public virtual DbSet<director> director { get; set; }
        public virtual DbSet<homework> homework { get; set; }
        public virtual DbSet<student> student { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<teacher> teacher { get; set; }
        public virtual DbSet<timetable> timetable { get; set; }
    }
}
