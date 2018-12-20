using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Simple.OData.Client
{
    public interface IClientWithResponse<T> : IDisposable
    {
        HttpResponseMessage ResponseMessage { get; }

        //Task<IEnumerable<T>> ReadAsCollectionAsync();

        Task<IEnumerable<T>> ReadAsCollectionAsync(CancellationToken cancellationToken = default(CancellationToken));

        //Task<IEnumerable<T>> ReadAsCollectionAsync(ODataFeedAnnotations annotations);

        Task<IEnumerable<T>> ReadAsCollectionAsync(ODataFeedAnnotations annotations, CancellationToken cancellationToken = default(CancellationToken));

        //Task<T> ReadAsSingleAsync();

        Task<T> ReadAsSingleAsync(CancellationToken cancellationToken = default(CancellationToken));

        //Task<U> ReadAsScalarAsync<U>();

        Task<U> ReadAsScalarAsync<U>(CancellationToken cancellationToken = default(CancellationToken));

        //Task<Stream> GetResponseStreamAsync();

        Task<Stream> GetResponseStreamAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
