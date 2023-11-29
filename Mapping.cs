using Riok.Mapperly.Abstractions;

namespace TransferDataBetweenLayersBenchmark;

[Mapper]
public static partial class MapperlyMapper
{
    public static partial PersonResponse Map(Person source);
}