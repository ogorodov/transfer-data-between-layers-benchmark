# Демонстрационный код к статье ["Передача данных между слоями приложения"](https://ogorodov.su/transfer-data-between-layers-benchmark/)

BenchmarkDotNet v0.13.10, Windows 11 (10.0.22621.2715/22H2/2022Update/SunValley2)
AMD Ryzen 7 4800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
[Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


| Method                            | Categories      | Mean           | Error       | StdDev      | Median         | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|---------------------------------- |---------------- |---------------:|------------:|------------:|---------------:|------:|--------:|-------:|----------:|------------:|
| Copy                              | 1_Single        |     10.8810 ns |   0.3297 ns |   0.9511 ns |     10.8374 ns | 2.141 |    0.26 | 0.0306 |      64 B |        2.67 |
| Wrapper                           | 1_Single        |      5.0977 ns |   0.1535 ns |   0.3990 ns |      4.9580 ns | 1.000 |    0.00 | 0.0115 |      24 B |        1.00 |
| StructWrapper                     | 1_Single        |      0.0000 ns |   0.0000 ns |   0.0000 ns |      0.0000 ns | 0.000 |    0.00 |      - |         - |        0.00 |
| StructWrapperWithBoxing           | 1_Single        |      5.1689 ns |   0.1579 ns |   0.3660 ns |      5.1863 ns | 1.009 |    0.09 | 0.0115 |      24 B |        1.00 |
|                                   |                 |                |             |             |                |       |         |        |           |             |
| Copy_ToList                       | 2_Collection    |    465.1945 ns |   8.6852 ns |   8.1242 ns |    466.6431 ns |  6.08 |    0.11 | 0.9294 |    1944 B |       40.50 |
| Wrapper_ToList                    | 2_Collection    |    376.1475 ns |   6.4246 ns |   5.6953 ns |    374.5939 ns |  4.91 |    0.10 | 0.4511 |     944 B |       19.67 |
| StructWrapper_ToList              | 2_Collection    |    202.8333 ns |   3.6953 ns |   5.7531 ns |    201.9537 ns |  2.66 |    0.09 | 0.1643 |     344 B |        7.17 |
| Copy_ToArray                      | 2_Collection    |    417.4285 ns |   8.1768 ns |   8.0307 ns |    417.8225 ns |  5.45 |    0.15 | 0.9103 |    1904 B |       39.67 |
| Wrapper_ToArray                   | 2_Collection    |    327.9560 ns |   6.3002 ns |  14.0913 ns |    327.2578 ns |  4.21 |    0.10 | 0.4320 |     904 B |       18.83 |
| StructWrapper_ToArray             | 2_Collection    |    155.6579 ns |   3.1110 ns |   5.4486 ns |    156.0848 ns |  2.02 |    0.08 | 0.1452 |     304 B |        6.33 |
| Copy_Enumerable                   | 2_Collection    |    310.6267 ns |   9.3890 ns |  26.3277 ns |    305.0387 ns |  3.99 |    0.26 | 0.7877 |    1648 B |       34.33 |
| Wrapper_Enumerable                | 2_Collection    |    252.0892 ns |  10.1900 ns |  29.2369 ns |    240.3358 ns |  3.09 |    0.20 | 0.3097 |     648 B |       13.50 |
| StructWrapper_Enumerable          | 2_Collection    |     76.5785 ns |   1.3769 ns |   1.2206 ns |     76.4518 ns |  1.00 |    0.00 | 0.0229 |      48 B |        1.00 |
|                                   |                 |                |             |             |                |       |         |        |           |             |
| Copy_ToList_ToJson                | 3_Serialization | 10,123.0963 ns | 201.8311 ns | 207.2657 ns | 10,037.2955 ns |  1.08 |    0.02 | 1.1597 |    2432 B |        2.14 |
| Wrapper_ToList_ToJson             | 3_Serialization |  9,813.8867 ns |  62.0587 ns |  51.8218 ns |  9,798.2773 ns |  1.05 |    0.01 | 0.6714 |    1432 B |        1.26 |
| StructWrapper_ToList_ToJson       | 3_Serialization |  9,372.9839 ns | 182.9106 ns | 268.1080 ns |  9,395.2698 ns |  1.01 |    0.03 | 0.6714 |    1432 B |        1.26 |
| Copy_ToArray_ToJson               | 3_Serialization |  9,737.6832 ns |  91.1023 ns |  85.2172 ns |  9,731.3034 ns |  1.04 |    0.02 | 1.1292 |    2392 B |        2.11 |
| Wrapper_ToArray_ToJson            | 3_Serialization |  9,683.4811 ns | 159.1691 ns | 141.0992 ns |  9,622.8691 ns |  1.03 |    0.02 | 0.6561 |    1392 B |        1.23 |
| StructWrapper_ToArray_ToJson      | 3_Serialization |  9,194.7654 ns |  88.6703 ns |  78.6039 ns |  9,193.9598 ns |  0.98 |    0.01 | 0.6561 |    1392 B |        1.23 |
| Copy_Enumerable_ToJson            | 3_Serialization |  9,843.1698 ns | 191.5686 ns | 188.1460 ns |  9,826.0506 ns |  1.05 |    0.02 | 1.0071 |    2136 B |        1.88 |
| Wrapper_Enumerable_ToJson         | 3_Serialization |  9,482.9134 ns | 114.0124 ns | 101.0690 ns |  9,466.2743 ns |  1.01 |    0.02 | 0.5341 |    1136 B |        1.00 |
| StructWrapper_Enumerable_ToJson   | 3_Serialization |  9,387.6717 ns |  97.3481 ns |  86.2966 ns |  9,377.3071 ns |  1.00 |    0.00 | 0.5341 |    1136 B |        1.00 |
|                                   |                 |                |             |             |                |       |         |        |           |             |
| Copy_ToArray_ToJson_Mapperly      | 4_Mappers       |  9,989.5762 ns | 167.3367 ns | 156.5268 ns |  9,923.9166 ns |  1.05 |    0.02 | 1.1292 |    2392 B |        2.11 |
| Copy_ToArray_ToJson_Mapster       | 4_Mappers       |  9,944.6010 ns | 102.7893 ns |  91.1201 ns |  9,941.7305 ns |  1.05 |    0.01 | 1.1292 |    2392 B |        2.11 |
| Copy_ToArray_ToJson_Automapper    | 4_Mappers       | 11,673.5906 ns | 230.6903 ns | 315.7715 ns | 11,617.6270 ns |  1.24 |    0.03 | 1.1597 |    2456 B |        2.16 |
| Copy_Enumerable_ToJson_Mapperly   | 4_Mappers       |  9,608.3611 ns |  64.5006 ns |  53.8609 ns |  9,614.1281 ns |  1.01 |    0.01 | 1.0071 |    2136 B |        1.88 |
| Copy_Enumerable_ToJson_Mapster    | 4_Mappers       |  9,954.0026 ns | 122.5017 ns | 108.5946 ns |  9,952.7435 ns |  1.05 |    0.01 | 1.0071 |    2136 B |        1.88 |
| Copy_Enumerable_ToJson_Automapper | 4_Mappers       | 11,462.0146 ns |  70.4611 ns |  62.4619 ns | 11,457.8041 ns |  1.21 |    0.01 | 1.0376 |    2200 B |        1.94 |
| Wrapper_Enumerable_ToJson_2       | 4_Mappers       |  9,483.3234 ns |  56.8430 ns |  47.4664 ns |  9,486.2503 ns |  1.00 |    0.00 | 0.5341 |    1136 B |        1.00 |
