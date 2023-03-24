using ReflectionGenericsBase;
using ReflectionMagic;
using System.Reflection;

Console.WriteLine("Learning Reflection using Generics!");

UsingReflectionMagic();


Console.ReadLine();


static void UsingReflectionMagic()
{
    var person = new Person("Kevinn");
    var privateField = person.GetType().GetField(
        "_aPrivateField",
        BindingFlags.Instance | BindingFlags.NonPublic);

    privateField?.SetValue(person, "New private field value");


    person.AsDynamic()._aPrivateField = "Updated value via Reflection Magic";
}

static void IoCContainerExample() { 
    var iocContainer = new IoCContainer();
    iocContainer.Register<IWaterService, TapWaterService>();
    var waterService = iocContainer.Resolve<IWaterService>();

    //iocContainer.Register<IBeanService<Catimor>, ArabicaBeanService<Catimor>>();
    iocContainer.Register(typeof(IBeanService<>), typeof(ArabicaBeanService<>));

    iocContainer.Register<ICoffeeService, CoffeeService>();
    var coffeeService = iocContainer.Resolve<ICoffeeService>();

}


static void StartWithGenerics()
{
    var myList = new List<Person>();
    Console.WriteLine(myList.GetType().Name);
    Console.WriteLine(myList.GetType());

    var myDictionary = new Dictionary<string, int>();
    Console.WriteLine(myDictionary.GetType());

    var dictionaryType = myDictionary.GetType();
    dictionaryType.GenericTypeArguments.ToList()
        .ForEach(item => Console.WriteLine(item.ToString()));

    dictionaryType.GenericTypeArguments.ToList()
        .ForEach(item => Console.WriteLine(item.ToString()));


    var openDictionaryType = typeof(Dictionary<,>);
    openDictionaryType.GenericTypeArguments.ToList()
        .ForEach(item => Console.WriteLine(item.ToString()));

    openDictionaryType.GetGenericArguments().ToList()
        .ForEach(item => Console.WriteLine(item.ToString()));

    var createdInstance = Activator.CreateInstance(typeof(List<Person>));
    Console.WriteLine($"Created instance of {createdInstance.GetType()}");

    //var openResultType = typeof(Result<>);
    //var closedResultType = openResultType.MakeGenericType(typeof(Person));
    //var createdResult = Activator.CreateInstance(closedResultType);
    //Console.WriteLine($"Created result {createdResult?.GetType()}");

    var openResultType = Type.GetType("ReflectionGenericsBase.Result`1");
    var closedResultType = openResultType.MakeGenericType(Type.GetType("ReflectionGenericsBase.Person"));
    var createdResult = Activator.CreateInstance(closedResultType);
    Console.WriteLine($"Created result {createdResult?.GetType()}");

    var methodInfo = closedResultType.GetMethod("AlterAndReturnValue");
    Console.WriteLine(methodInfo);

    var genericMethodInfo = methodInfo.MakeGenericMethod(typeof(Employee));
    genericMethodInfo.Invoke(createdResult, new object[] { new Employee() });
}


