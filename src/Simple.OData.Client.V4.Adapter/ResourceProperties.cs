using Microsoft.OData;

namespace Simple.OData.Client.V4.Adapter;

public class ResourceProperties(ODataResource resource)
{
	public ODataResource Resource { get; } = resource;
	public string TypeName { get; set; }
#if ODATA_7
	public IEnumerable<ODataProperty> PrimitiveProperties => Resource.Properties;
#else
	public IEnumerable<ODataPropertyInfo> PrimitiveProperties => Resource.Properties;
#endif
	public IDictionary<string, ODataCollectionValue> CollectionProperties { get; set; }
	public IDictionary<string, ODataResource> StructuralProperties { get; set; }
}
