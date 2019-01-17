using System;
using System.Collections.Generic;

using WebApiOData.V4.Samples.Models;

namespace Simple.OData.Client.Tests
{
    public class MyConverter
    {
        public static object ProductConverter(IDictionary<string, object> dictionary)
        {
            var entity = new Product();

            foreach (var kvp in dictionary)
            {
                switch (kvp.Key)
                {
                    case "Id":
                        entity.Id = (int) kvp.Value;
                        break;

                    case "Name":
                        entity.Name = kvp.Value as string;
                        break;

                    case "Price":
                        entity.Price = (double) kvp.Value;
                        break;
                }
            }

            return entity;
        }

        public static object MovieConverter(IDictionary<string, object> dictionary)
        {
            var entity = new Movie();

            foreach (var kvp in dictionary)
            {
                switch (kvp.Key)
                {
                    case "ID":
                        entity.ID = (int) kvp.Value;
                        break;

                    case "Title":
                        entity.Title = kvp.Value as string;
                        break;

                    case "Year":
                        entity.Year = (int) kvp.Value;
                        break;

                    case "DueDate":
                        entity.DueDate = (DateTimeOffset?) kvp.Value;
                        break;
                }
            }

            return entity;
        }
    }
}
