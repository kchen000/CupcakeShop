using CupcakeShop.Domain;
using CupcakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CupcakeShop.Extensions
{
    
    public static class ExtensionMethods
    {
        public static CupcakeModel ToModel(this Cupcake cupcake)
        {
            //convert entity to model
            var model = new CupcakeModel();
            model.Id = cupcake.Id;
            model.Name = cupcake.Name;
            model.Price = cupcake.Price;
            model.CreatedOn = (DateTime?)cupcake.CreatedOn;
            model.LastModified = (DateTime?)cupcake.LastModified;
            model.Ingredients = cupcake.Ingredients;
            model.NumberOfIngredients = cupcake.Ingredients.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(x => !string.IsNullOrEmpty(x))
                .ToList().Count;
            model.ImageFileName = cupcake.ImageFileName;
            model.Featured = cupcake.Featured;

            return model;
        }

        public static Cupcake ToEntity(this CupcakeModel model)
        {
            //convert model to entity
            var cupcake = new Cupcake();
            cupcake.Id = model.Id;
            cupcake.Name = model.Name;
            cupcake.Price = model.Price;
            cupcake.Ingredients = model.Ingredients;
            cupcake.LastModified = DateTime.Now;
            if (cupcake.Id == 0)
            {
                cupcake.CreatedOn = DateTime.Now;
            }
            if (!string.IsNullOrEmpty(model.ImageFileName))
            {
                cupcake.ImageFileName = model.ImageFileName;
            }
            cupcake.Featured = model.Featured;

            return cupcake;
        }
    }
}