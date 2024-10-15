namespace Plex.NaturalStringOrdering;
public static class NaturalStringOrderingExtension
{
    public static IOrderedEnumerable<TSource> OrderByNatural<TSource>(this IEnumerable<TSource> source,
                                                                        Func<TSource, string?> keySelector,
                                                                        bool ignoreCase = true)
    {
        return source.OrderBy(keySelector, new NaturalStringComparer(ignoreCase));
    }
    public static IOrderedEnumerable<TSource> OrderByNaturalDescending<TSource>(this IEnumerable<TSource> source,
                                                            Func<TSource, string?> keySelector,
                                                            bool ignoreCase = true)
    {
        return source.OrderByDescending(keySelector, new NaturalStringComparer(ignoreCase));
    }
}
