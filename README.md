# Демонстрационный код к статье ["Передача данных между слоями приложения"](https://ogorodov.su/transfer-data-between-layers-benchmark/)

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 4800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                            | Mean           | Ratio | Allocated | Alloc Ratio |
|---------------------------------- |---------------:|------:|----------:|------------:|
| Copy                              |     10.8810 ns | 2.141 |      64 B |        2.67 |
| Wrapper                           |      5.0977 ns | 1.000 |      24 B |        1.00 |
| StructWrapper                     |      0.0000 ns | 0.000 |         - |        0.00 |
| StructWrapperWithBoxing           |      5.1689 ns | 1.009 |      24 B |        1.00 |
|                                   |                |       |           |             |
| Copy_ToList                       |    465.1945 ns |  6.08 |    1944 B |       40.50 |
| Wrapper_ToList                    |    376.1475 ns |  4.91 |     944 B |       19.67 |
| StructWrapper_ToList              |    202.8333 ns |  2.66 |     344 B |        7.17 |
| Copy_ToArray                      |    417.4285 ns |  5.45 |    1904 B |       39.67 |
| Wrapper_ToArray                   |    327.9560 ns |  4.21 |     904 B |       18.83 |
| StructWrapper_ToArray             |    155.6579 ns |  2.02 |     304 B |        6.33 |
| Copy_Enumerable                   |    310.6267 ns |  3.99 |    1648 B |       34.33 |
| Wrapper_Enumerable                |    252.0892 ns |  3.09 |     648 B |       13.50 |
| StructWrapper_Enumerable          |     76.5785 ns |  1.00 |      48 B |        1.00 |
|                                   |                |       |           |             |
| Copy_ToList_ToJson                | 10,123.0963 ns |  1.08 |    2432 B |        2.14 |
| Wrapper_ToList_ToJson             |  9,813.8867 ns |  1.05 |    1432 B |        1.26 |
| StructWrapper_ToList_ToJson       |  9,372.9839 ns |  1.01 |    1432 B |        1.26 |
| Copy_ToArray_ToJson               |  9,737.6832 ns |  1.04 |    2392 B |        2.11 |
| Wrapper_ToArray_ToJson            |  9,683.4811 ns |  1.03 |    1392 B |        1.23 |
| StructWrapper_ToArray_ToJson      |  9,194.7654 ns |  0.98 |    1392 B |        1.23 |
| Copy_Enumerable_ToJson            |  9,843.1698 ns |  1.05 |    2136 B |        1.88 |
| Wrapper_Enumerable_ToJson         |  9,482.9134 ns |  1.01 |    1136 B |        1.00 |
| StructWrapper_Enumerable_ToJson   |  9,387.6717 ns |  1.00 |    1136 B |        1.00 |
|                                   |                |       |           |             |
| Copy_ToArray_ToJson_Mapperly      |  9,989.5762 ns |  1.05 |    2392 B |        2.11 |
| Copy_ToArray_ToJson_Mapster       |  9,944.6010 ns |  1.05 |    2392 B |        2.11 |
| Copy_ToArray_ToJson_Automapper    | 11,673.5906 ns |  1.24 |    2456 B |        2.16 |
| Copy_Enumerable_ToJson_Mapperly   |  9,608.3611 ns |  1.01 |    2136 B |        1.88 |
| Copy_Enumerable_ToJson_Mapster    |  9,954.0026 ns |  1.05 |    2136 B |        1.88 |
| Copy_Enumerable_ToJson_Automapper | 11,462.0146 ns |  1.21 |    2200 B |        1.94 |
| Wrapper_Enumerable_ToJson_2       |  9,483.3234 ns |  1.00 |    1136 B |        1.00 |
