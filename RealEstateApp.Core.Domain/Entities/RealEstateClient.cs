﻿using RealEstateApp.Core.Domain.Commons;

namespace RealEstateApp.Core.Domain.Entities
{
    public class RealEstateClient:BaseEntity
    {
        public string IdCliente { get; set; }
        public int IdRealEstate { get; set; }
    }
}
