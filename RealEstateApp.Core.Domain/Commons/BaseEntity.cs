﻿namespace RealEstateApp.Core.Domain.Commons
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? Created {  get; set; }
        public DateTime? LastModified {  get; set; }
       
    }
}
