using System.Linq;
using Simple.OData.Client.Extensions;
using Xunit;

namespace Simple.OData.Client.Tests
{
    public class TypeExtensionTests
    {
        [Fact]
        public void GetAllProperties_BaseType()
        {
            Assert.Single(typeof(Transport).GetAllProperties());
        }

        [Fact]
        public void GetAllProperties_DerivedType()
        {
            Assert.Equal(6, typeof(Ship).GetAllProperties().Count());
        }

        [Fact]
        public void GetDeclaredProperties_ExcludeExplicitInterface()
        {
            Assert.Equal(5, typeof(Address).GetAllProperties().Count());
        }

        [Fact]
        public void GetDeclaredProperties_BaseType()
        {
            Assert.Single(typeof(Transport).GetDeclaredProperties());
        }

        [Fact]
        public void GetDeclaredProperties_DerivedType()
        {
            Assert.Equal(5, typeof(Ship).GetDeclaredProperties().Count());
        }

        [Fact]
        public void GetNamedProperty_BaseType()
        {
            Assert.NotNull(typeof(Transport).GetNamedProperty("TransportID"));
        }

        [Fact]
        public void GetNamedProperty_DerivedType()
        {
            Assert.NotNull(typeof(Ship).GetNamedProperty("TransportID"));
            Assert.NotNull(typeof(Ship).GetNamedProperty("ShipName"));
        }

        [Fact]
        public void GetDeclaredProperty_BaseType()
        {
            Assert.NotNull(typeof(Transport).GetDeclaredProperty("TransportID"));
        }

        [Fact]
        public void GetDeclaredProperty_DerivedType()
        {
            Assert.Null(typeof(Ship).GetDeclaredProperty("TransportID"));
            Assert.NotNull(typeof(Ship).GetDeclaredProperty("ShipName"));
        }

        [Fact]
        public void GetMappedNames_DerivedType()
        {
            var mappings = typeof(Ship).GetMappedPropertiesWithNames().ToList();
            foreach (var t in mappings)
            {
                switch (t.Item1.Name)
                {
                    case "TransportID":
                        Assert.Equal("TransportID", t.Item2);
                        break;

                    case "ShipName":
                        Assert.Equal("ShipName", t.Item2);
                        break;

                    case "DataMapping":
                        Assert.Equal("Data", t.Item2);
                        break;

                    case "JsonMapping":
                        Assert.Equal("Json", t.Item2);
                        break;
                }
            }
        }
    }
}