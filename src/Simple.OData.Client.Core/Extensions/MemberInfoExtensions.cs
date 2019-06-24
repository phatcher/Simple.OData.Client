﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#pragma warning disable 1591

namespace Simple.OData.Client.Extensions
{
    public static class MemberInfoExtensions
    {
        private static readonly ConcurrentDictionary<MemberInfo, MappingInfo> cache = new ConcurrentDictionary<MemberInfo, MappingInfo>();

        public static bool IsNotMapped(this MemberInfo memberInfo)
        {
            return MappingInfo(memberInfo).IsNotMapped;
        }

        /// <summary>
        /// Extract a column name from the member's attributes
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static string GetMappedName(this MemberInfo memberInfo)
        {
            return MappingInfo(memberInfo).MappedName;
        }

        private static MappingInfo MappingInfo(MemberInfo memberInfo)
        {
            var info = cache.GetOrAdd(memberInfo, MappingInfoFactory);

            return info;
        }

        private static MappingInfo MappingInfoFactory(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes().ToList();

            return new MappingInfo
            {
                IsNotMapped = attributes.IsNotMapped(),
                MappedName = attributes.IsNotMapped() ? null : memberInfo.Name.GetMappedName(attributes)
            };
        }

        private static bool IsNotMapped(this IList<Attribute> attributes)
        {
            return attributes.Any(x => x.GetType().Name == "NotMappedAttribute" || x.GetType().Name == "JsonIgnoreAttribute");
        }

        private static string GetMappedName(this string name, IList<Attribute> attributes)
        {
            var supportedAttributeNames = new[]
            {
                "DataAttribute",
                "DataMemberAttribute",
                "ColumnAttribute",
                "JsonPropertyAttribute"
            };

            var propertyName = name;
            string attributeProperty;
            var mappingAttribute = attributes.FirstOrDefault(x => supportedAttributeNames.Any(y => x.GetType().Name == y));
            if (mappingAttribute != null)
            {
                attributeProperty = mappingAttribute.GetType().Name == "JsonPropertyAttribute" ? "PropertyName" : "Name";
            }
            else
            {
                attributeProperty = "PropertyName";
                mappingAttribute = attributes.FirstOrDefault(x => x.GetType().GetNamedProperty(attributeProperty) != null);
            }

            if (mappingAttribute != null)
            {
                var nameProperty = mappingAttribute.GetType().GetNamedProperty(attributeProperty);
                if (nameProperty != null)
                {
                    var propertyValue = nameProperty.GetValue(mappingAttribute, null);
                    if (propertyValue != null)
                        propertyName = propertyValue.ToString();
                }
            }

            return propertyName;
        }
    }

    public class MappingInfo
    {
        public bool IsNotMapped { get; set; }

        public string MappedName { get; set; }
    }
}