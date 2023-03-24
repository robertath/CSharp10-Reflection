using System.Reflection;
using ReflectionBase;

Console.WriteLine("Using Reflection");

//GetConstructors();
//CreateInstances();
//CreateInstanceOfConfiguredType();
//GetSetPropertiesField();
InvokeMethods();

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("********End********");
Console.ReadLine();




static void GetConstructors()
{
    Console.WriteLine("Working with Constructors");
    var personType = typeof(Person);
    Console.WriteLine("*****");

    Console.WriteLine("Public Constructors");
    var personPublicConstructors = personType.GetConstructors();
    personPublicConstructors.ToList().ForEach(type => Console.WriteLine($"{type}"));
    
    Console.WriteLine("*****");
    Console.WriteLine("Private Constructors");
    var personPrivateConstructors = personType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
    personPrivateConstructors.ToList().ForEach(type => Console.WriteLine($"{type}"));

    Console.WriteLine("*****");
    Console.WriteLine("All Constructors");
    var personAllConstructors = personType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    personAllConstructors.ToList().ForEach(type => Console.WriteLine($"{type}"));

    Console.WriteLine("*****");
    Console.WriteLine("Selected constructor - private, param: string, int");
    var privatePersonConstructor = personType.GetConstructor(
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new Type[] { typeof(string), typeof(int) },
        null);
    Console.WriteLine(privatePersonConstructor);
}

static void CreateInstances()
{
    Console.WriteLine("Creating Instances");
    var personType = typeof(Person);
    Console.WriteLine("*****");

    var personAllConstructors = personType.GetConstructors(
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

    Console.WriteLine("*****");
    Console.WriteLine("*****");
    Console.WriteLine("Invoke first Constructor");
    var person1 = personAllConstructors[0].Invoke(null);

    Console.WriteLine("*****");
    Console.WriteLine("Invoke first Constructor with parameter");
    var person2 = personAllConstructors[1].Invoke(new object[] { "Roberta" });

    Console.WriteLine("*****");
    var person3 = personAllConstructors[2].Invoke(new object[] { "Roberta", 39 });
    Console.WriteLine(person2);

    Console.WriteLine("*****");
    Console.WriteLine("Create a instance with Activator");
    var person4 = Activator.CreateInstance(
            "ReflectionBase",
            "ReflectionBase.Person")
        .Unwrap();

    Console.WriteLine("*****");
    Console.WriteLine("Create a instance with Activator using parameters");
    var person5 = Activator.CreateInstance(
        "ReflectionBase",
        "ReflectionBase.Person",
        true,
        BindingFlags.Instance | BindingFlags.Public,
        null,
        new object[] { "Roberta" },
        null,
        null);

    Console.WriteLine("*****");
    Console.WriteLine("Create a instance with Activator using parameter as object");
    var personTypeFromString = Type.GetType("ReflectionBase.Person");
    var person6 = Activator.CreateInstance(
        personTypeFromString,
        new object[] { "Roberta" });

    var person7 = Activator.CreateInstance(
        "ReflectionBase",
        "ReflectionBase.Person",
        true,
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new object?[] { "Roberta", 37 },
        null,
        null);

    Console.WriteLine("*****");
    Console.WriteLine("Create a instance with Assembly");
    var assembly = Assembly.GetExecutingAssembly();
    var person8 = assembly.CreateInstance("ReflectionBase.Person");
}

static void CreateInstanceOfConfiguredType()
{
    Console.WriteLine("*****");
    Console.WriteLine("Get instance by by type");
    Console.WriteLine("*****");
    Console.WriteLine();

    var actualTypeFromConfiguration = Type.GetType(GetTypeFromConfiguration());
    var iTalkInstance = Activator.CreateInstance(actualTypeFromConfiguration) as ITalk;
    iTalkInstance.Talk("Hello baby");

    Console.WriteLine("*****");
    Console.WriteLine("Working with Constructors");
    dynamic dynamicITalkInstance = Activator.CreateInstance(actualTypeFromConfiguration);
    dynamicITalkInstance.Talk("Hello Dynamic Baby");
    //dynamicITalkInstance.SomeProperty = "Age";
}

static void GetSetPropertiesField()
{
    Console.WriteLine("*****");
    Console.WriteLine("Get / Set properties and fields");
    Console.WriteLine("*****");
    Console.WriteLine();
    
    var personType = typeof(Person);

    var personForManipulation = Activator.CreateInstance(
        "ReflectionBase",
        "ReflectionBase.Person",
        true,
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new object?[]{"Roberta", 37},
        null, 
        null)?.Unwrap();


    var nameProperty = personForManipulation.GetType().GetProperty("Name");
    nameProperty.SetValue(personForManipulation, "Roberta Hoffmann");


    var ageField = personForManipulation?.GetType().GetField("age");
    ageField.SetValue(personForManipulation, 40);

    var privateField = personForManipulation.GetType().GetField(
        "_aPrivateField",
        BindingFlags.Instance | BindingFlags.NonPublic);
    privateField.SetValue(personForManipulation, "updated private field value");


    personForManipulation?.GetType().InvokeMember(
        "Name",
        BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
        null,
        personForManipulation,
        new[] { "Thamara" });


    personForManipulation?.GetType().InvokeMember(
        "_aPrivateField",
        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField,
        null,
        personForManipulation,
        new[] { "second update for private field name" });

    Console.WriteLine(personForManipulation);
}

static string GetTypeFromConfiguration()
{
    return "ReflectionBase.Person";
}

static void InvokeMethods()
{
    Console.WriteLine("*****");
    Console.WriteLine("Get / Set methods");
    Console.WriteLine("*****");
    Console.WriteLine();

    var personType = typeof(Person);

    var personForManipulation = Activator.CreateInstance(
        "ReflectionBase",
        "ReflectionBase.Person",
        true,
        BindingFlags.Instance | BindingFlags.NonPublic,
        null,
        new object?[] { "Roberta", 37 },
        null,
        null)?.Unwrap();

    var talkMethod = personForManipulation?.GetType().GetMethod("Talk");
    talkMethod?.Invoke(personForManipulation, new[] { "something to say" });

    personForManipulation?.GetType().InvokeMember(
        "Yell",
        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
        null,
        personForManipulation,
        new[] { "something to yell" });


}

static void CodeFromSecondModule()
{

    var name = "Roberta";
    var stringType1 = name.GetType();
    var stringType2 = typeof(string);
    Console.WriteLine(stringType1);
    Console.WriteLine(stringType2);

    Console.WriteLine($"Internal Types:");
    var currentAssembly = Assembly.GetExecutingAssembly();
    var typesFromCurrentAssembly = currentAssembly.GetTypes();
    foreach (var type in typesFromCurrentAssembly)
    {
        Console.WriteLine(type.Name);
    }

    var oneTypeFromCurrentAssembly = currentAssembly.GetType("ReflectionBase.Person");
    if (oneTypeFromCurrentAssembly != null)
    {
        foreach (var constructor in oneTypeFromCurrentAssembly.GetConstructors())
        {
            Console.WriteLine(constructor);
        }
        foreach (var method in oneTypeFromCurrentAssembly.GetMethods(
                     BindingFlags.Public | BindingFlags.NonPublic))
        {
            Console.WriteLine($"{method}, public: {method.IsPublic}");
        }

        foreach (var field in oneTypeFromCurrentAssembly.GetFields(
                     BindingFlags.Instance | BindingFlags.NonPublic))
        {
            Console.WriteLine(field);
        }
    }

    //Console.WriteLine($"Extenal Types:");
    //var externalAssembly = Assembly.Load("System.Text.Json");
    //var typesFromExternalAssembly = externalAssembly.GetTypes();
    //typesFromExternalAssembly.ToList().ForEach(type =>
    //    Console.WriteLine($"{type.Name} --- {type.FullName} "));
    //var oneTypeFromExternalAssembly = externalAssembly.GetType("System.Text.Json.JsonProperty");

    //var modulesFromExternalAssembly = externalAssembly.GetModules();
    //var oneModuleFromExternalAssembly = externalAssembly.GetModule("System.Text.Json.dll");

    //var typesFromModuleFromExternalAssembly = oneModuleFromExternalAssembly?.GetTypes();
    //var oneTypeFromModuleFromExternalAssembly = oneModuleFromExternalAssembly
    //    .GetType("System.Text.Json.JsonProperty");

    Console.ReadLine();
}

