using ArandaSoftTest.CORE.DTOs;
using ArandaSoftTest.CORE.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArandaSoftTest.INFRASTRUCTURE.Mappings
{
    public class AutomapperProfile : Profile 
    {
        public AutomapperProfile()
        {
            // entidad origen y destino
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
