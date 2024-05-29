namespace iTicket.Application.Interfaces.AutoMapper
{
    public interface IMapper
    {
        TDestination Map<TDestination, TSource>(TSource source, string? ignore = null);
        IList<TDestination> Map<TDestination, TSource>(IList<TSource> sources, string? ignore = null);

        TDestination Map<TDestination>(object source, string? ignore = null);
        IList<TDestination> Map<TDestination>(IList<object> sources, string? ignore = null);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
