using System.Collections.Generic;

using Simple.OData.Client.Extensions;
using Simple.OData.Client.Tests;

namespace Simple.OData.Client.IntegrationTests
{
    public class Converters
    {
        public static object PhotoConverter(IDictionary<string, object> dictionary)
        {
            var entity = new Photo();

            foreach (var kvp in dictionary)
            {
                switch (kvp.Key)
                {
                    case "Id":
                        TypeCaches.Global.TryConvert(kvp.Value, typeof(int), out var foo);
                        entity.Id = (int) foo;
                        break;

                    case "Name":
                        entity.Name = kvp.Value as string;
                        break;
                }
            }
            
            return entity;
        }
    }
}
