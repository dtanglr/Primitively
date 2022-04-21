using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Primitively.IntegrationTests.Types;

namespace Primitively.IntegrationTests.ValueSets;

public readonly partial record struct ValueSet1 : IValueSet<GuidId1>
{
    private ValueSet1(GuidId1 code, string display)
    {
        Code = code;
        Display = display;
    }

    public GuidId1 Code { get; }

    public string Display { get; }
}

public partial record struct ValueSet1
{
    public static class ValueSetCollection
    {
        private static readonly Lazy<List<ValueSet1>> _list = new(LoadValueSet(), LazyThreadSafetyMode.PublicationOnly);

        public static IReadOnlyList<ValueSet1> Get() => _list.Value.AsReadOnly();

        public static ValueSet1 Get(GuidId1 code) => _list.Value.Single(p => p.Code.Equals(code));

        public static bool Exists(GuidId1 code) => _list.Value.Exists(p => p.Code.Equals(code));

        private static Func<List<ValueSet1>> LoadValueSet() => () =>
        {
            var list = new List<ValueSet1>();
            var items = GetAll();
            list.AddRange(items);

            return list;
        };
    }

    private static IEnumerable<ValueSet1> GetAll()
    {
        yield return new ValueSet1(GuidId1.New(), "Test1");
        yield return new ValueSet1(GuidId1.New(), "Test2");
        yield return new ValueSet1(GuidId1.New(), "Test3");
        yield return new ValueSet1(GuidId1.New(), "Test4");
        yield return new ValueSet1(GuidId1.New(), "Test5");
        yield return new ValueSet1(GuidId1.New(), "Test6");
    }
}
