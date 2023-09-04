﻿using ElastichSearch.API.Models;

namespace ElastichSearch.API.DTOs
{
    public record ProductCreateDto(string Name, decimal Price, int Stock, ProductFeatureDto Feature)
    {
        public Product CreateProduct()
        {
            return new Product
            {
                Name = Name,
                Price = Price,
                Stock = Stock,
                Feature = new ProductFeature()
                {
                    Color =(EColor)int.Parse(Feature.Color),
                    Height = Feature.Height,
                    Width = Feature.Width
                }
            };

        }
    }
}
