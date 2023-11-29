namespace TransferDataBetweenLayersBenchmark;

/// <summary>Domain Entity </summary>
public sealed class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly? Birthdate { get; set; }
    public bool? Gender { get; set; } // 0 = Female, 1 = Male
    public string Email { get; set; } = null!;
    public int Score { get; set; }
}

/// <summary>WEB Response создаваемый путём копирования</summary>
public sealed class PersonResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly? Birthdate { get; set; }
    public bool? Gender { get; set; } // 0 = Female, 1 = Male
    public string Email { get; set; } = null!;
    public int Score { get; set; }
}

/// <summary>WEB Response создаваемый путём агрегации</summary>
/// <param name="person">источник данных</param>
public sealed class PersonResponseWrapper(Person person)
{
    public int Id => person.Id;
    public string FirstName => person.FirstName;
    public string LastName => person.LastName;
    public DateOnly? Birthdate => person.Birthdate;
    public bool? Gender => person.Gender;
    public string Email => person.Email;
    public int Score => person.Score;
}

/// <summary>WEB Response в виде структуры создаваемый путём агрегации</summary>
/// <param name="person">источник данных</param>
public readonly struct PersonResponseWrapperStruct(Person person)
{
    public int Id => person.Id;
    public string FirstName => person.FirstName;
    public string LastName => person.LastName;
    public DateOnly? Birthdate => person.Birthdate;
    public bool? Gender => person.Gender;
    public string Email => person.Email;
    public int Score => person.Score;
}

public readonly struct ContainerPersonResponse(IEnumerable<PersonResponse> source)
{
    public IEnumerable<PersonResponse> Persons => source;
}

public readonly struct ContainerPersonResponseWrapper(IEnumerable<PersonResponseWrapper> source)
{
    public IEnumerable<PersonResponseWrapper> Persons => source;
}

public readonly struct ContainerPersonResponseWrapperStruct(IEnumerable<PersonResponseWrapperStruct> source)
{
    public IEnumerable<PersonResponseWrapperStruct> Persons => source;
}