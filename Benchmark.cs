using System.Text.Json;
using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using Mapster;
using IMapper = AutoMapper.IMapper;

namespace TransferDataBetweenLayersBenchmark;

[MemoryDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public class Benchmark
{
    private const string SingleObjectCategoryName = "1_Single";
    private const string CollectionCategoryName = "2_Collection";
    private const string CollectionSerializationCategoryName = "3_Serialization";
    private const string MappersCategoryName = "4_Mappers";

    private Person _person = null!;
    private Person[] _persons = null!;
    private MemoryStream _stream = null!;
    private IMapper _mapper = null!;

    [GlobalSetup]
    public void Setup()
    {
        _person = CreatePerson(short.MaxValue);

        _persons = new Person[25];
        for (var i = 0; i < _persons.Length; i++)
            _persons[i] = CreatePerson(6 + i);

        _stream = new MemoryStream(1024 * 1024);

        _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Person, PersonResponse>()).CreateMapper();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _stream.Close();
        _stream.Dispose();
        _stream = null!;
    }

    #region SingleObject
    
    [Benchmark]
    [BenchmarkCategory(SingleObjectCategoryName)]
    public PersonResponse Copy()
    {
        return new PersonResponse()
        {
            Id = _person.Id,
            FirstName = _person.FirstName,
            LastName = _person.LastName,
            Birthdate = _person.Birthdate,
            Gender = _person.Gender,
            Email = _person.Email,
            Score = _person.Score,
        };
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory(SingleObjectCategoryName)]
    public PersonResponseWrapper Wrapper()
    {
        return new PersonResponseWrapper(_person);
    }
    
    [Benchmark]
    [BenchmarkCategory(SingleObjectCategoryName)]
    public PersonResponseWrapperStruct StructWrapper()
    {
        return new PersonResponseWrapperStruct(_person);
    }
    
    [Benchmark]
    [BenchmarkCategory(SingleObjectCategoryName)]
    public object StructWrapperWithBoxing()
    {
        return new PersonResponseWrapperStruct(_person);
    }
    
    #endregion
    
    #region Collection
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int Copy_ToList()
    {
        return _persons.Select(o => new PersonResponse()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Birthdate = o.Birthdate,
                Gender = o.Gender,
                Email = o.Email,
                Score = o.Score,
            })
            .ToList()
            .Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int Wrapper_ToList()
    {
        return _persons.Select(o => new PersonResponseWrapper(o)).ToList().Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int StructWrapper_ToList()
    {
        return _persons.Select(o => new PersonResponseWrapperStruct(o)).ToList().Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int Copy_ToArray()
    {
        return _persons.Select(o => new PersonResponse()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Birthdate = o.Birthdate,
                Gender = o.Gender,
                Email = o.Email,
                Score = o.Score,
            })
            .ToArray()
            .Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int Wrapper_ToArray()
    {
        return _persons.Select(o => new PersonResponseWrapper(o)).ToArray().Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int StructWrapper_ToArray()
    {
        return _persons.Select(o => new PersonResponseWrapperStruct(o)).ToArray().Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int Copy_Enumerable()
    {
        return _persons.Select(o => new PersonResponse()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Birthdate = o.Birthdate,
                Gender = o.Gender,
                Email = o.Email,
                Score = o.Score,
            })
            .Sum(o=>o.Id);
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionCategoryName)]
    public int Wrapper_Enumerable()
    {
        return _persons.Select(o => new PersonResponseWrapper(o)).Sum(o=>o.Id);
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory(CollectionCategoryName)]
    public int StructWrapper_Enumerable()
    {
        return _persons.Select(o => new PersonResponseWrapperStruct(o)).Sum(o=>o.Id);
    }
    
    #endregion
    
    #region CollectionWithSerialization
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long Copy_ToList_ToJson()
    {
        var items = _persons.Select(o => new PersonResponse()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Birthdate = o.Birthdate,
                Gender = o.Gender,
                Email = o.Email,
                Score = o.Score,
            })
            .ToList();
    
        var container = new ContainerPersonResponse(items);
    
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long Wrapper_ToList_ToJson()
    {
        var items = _persons.Select(o => new PersonResponseWrapper(o)).ToList();
        
        var container = new ContainerPersonResponseWrapper(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long StructWrapper_ToList_ToJson()
    {
        var items = _persons.Select(o => new PersonResponseWrapperStruct(o)).ToList();
        
        var container = new ContainerPersonResponseWrapperStruct(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long Copy_ToArray_ToJson()
    {
        var items = _persons.Select(o => new PersonResponse()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Birthdate = o.Birthdate,
                Gender = o.Gender,
                Email = o.Email,
                Score = o.Score,
            })
            .ToArray();
    
        var container = new ContainerPersonResponse(items);
    
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long Wrapper_ToArray_ToJson()
    {
        var items = _persons.Select(o => new PersonResponseWrapper(o)).ToArray();
        
        var container = new ContainerPersonResponseWrapper(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long StructWrapper_ToArray_ToJson()
    {
        var items = _persons.Select(o => new PersonResponseWrapperStruct(o)).ToArray();
        
        var container = new ContainerPersonResponseWrapperStruct(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long Copy_Enumerable_ToJson()
    {
        var items = _persons.Select(o => new PersonResponse()
            {
                Id = o.Id,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Birthdate = o.Birthdate,
                Gender = o.Gender,
                Email = o.Email,
                Score = o.Score,
            });
        
        var container = new ContainerPersonResponse(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long Wrapper_Enumerable_ToJson()
    {
        var items = _persons.Select(o => new PersonResponseWrapper(o));
        
        var container = new ContainerPersonResponseWrapper(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory(CollectionSerializationCategoryName)]
    public long StructWrapper_Enumerable_ToJson()
    {
        var items = _persons.Select(o => new PersonResponseWrapperStruct(o));
        
        var container = new ContainerPersonResponseWrapperStruct(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    #endregion

    #region Mappers

    [Benchmark]
    [BenchmarkCategory(MappersCategoryName)]
    public long Copy_ToArray_ToJson_Mapperly()
    {
        var items = _persons.Select(MapperlyMapper.Map).ToArray();

        var container = new ContainerPersonResponse(items);

        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }

    [Benchmark]
    [BenchmarkCategory(MappersCategoryName)]
    public long Copy_ToArray_ToJson_Mapster()
    {
        var items = _persons.Select(o => o.Adapt<PersonResponse>()).ToArray();

        var container = new ContainerPersonResponse(items);

        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }

    [Benchmark]
    [BenchmarkCategory(MappersCategoryName)]
    public long Copy_ToArray_ToJson_Automapper()
    {
        var items = _persons.Select(o => _mapper.Map<PersonResponse>(o)).ToArray();

        var container = new ContainerPersonResponse(items);

        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }

    [Benchmark]
    [BenchmarkCategory(MappersCategoryName)]
    public long Copy_Enumerable_ToJson_Mapperly()
    {
        var items = _persons.Select(MapperlyMapper.Map);

        var container = new ContainerPersonResponse(items);

        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }

    [Benchmark]
    [BenchmarkCategory(MappersCategoryName)]
    public long Copy_Enumerable_ToJson_Mapster()
    {
        var items = _persons.Select(o => o.Adapt<PersonResponse>());

        var container = new ContainerPersonResponse(items);

        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }

    [Benchmark]
    [BenchmarkCategory(MappersCategoryName)]
    public long Copy_Enumerable_ToJson_Automapper()
    {
        var items = _persons.Select(o => _mapper.Map<PersonResponse>(o));

        var container = new ContainerPersonResponse(items);

        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }
    
    [Benchmark(Baseline = true)]
    [BenchmarkCategory(MappersCategoryName)]
    public long Wrapper_Enumerable_ToJson_2()
    {
        var items = _persons.Select(o => new PersonResponseWrapper(o));
        
        var container = new ContainerPersonResponseWrapper(items);
        
        _stream.Position = 0;
        JsonSerializer.Serialize(_stream, container);
        return _stream.Position;
    }

    #endregion

    #region Private

    private static readonly DateOnly Birthdate = new DateOnly(2023, 11, 26);

    private static Person CreatePerson(int id)
    {
        return new Person
        {
            Id = id,
            FirstName = $"FirstName_{id}",
            LastName = $"LastName_{id}",
            Birthdate = Birthdate.AddDays(id),
            Gender = id % 2 == 1,
            Score = short.MaxValue + id,
            Email = $"not-existed-email{id}@mail.ru"
        };
    }

    #endregion
}