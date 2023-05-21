using System.Collections;
using Microsoft.Extensions.DependencyInjection;

namespace Primitively.Configuration;

public class PrimitivelyConfigurator : IPrimitivelyConfigurator
{
    private readonly IServiceCollection _services;

    public PrimitivelyConfigurator(IServiceCollection services) => _services = services;

    public ServiceDescriptor this[int index]
    {
        get
        {
            return _services[index];
        }
        set
        {
            _services[index] = value;
        }
    }

    public int Count => _services.Count;
    public bool IsReadOnly => _services.IsReadOnly;

    public void Add(ServiceDescriptor item) => _services.Add(item);

    public void Clear() => _services.Clear();

    public bool Contains(ServiceDescriptor item) => _services.Contains(item);

    public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => _services.CopyTo(array, arrayIndex);

    public IEnumerator<ServiceDescriptor> GetEnumerator() => _services.GetEnumerator();

    public int IndexOf(ServiceDescriptor item) => _services.IndexOf(item);

    public void Insert(int index, ServiceDescriptor item) => _services.Insert(index, item);

    public bool Remove(ServiceDescriptor item) => _services.Remove(item);

    public void RemoveAt(int index) => _services.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator() => _services.GetEnumerator();
}
