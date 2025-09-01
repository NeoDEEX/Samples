using CustomSection;
using NeoDEEX.Configuration;

Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.InvariantCulture;

Console.WriteLine("Fox Configuration Custom Section Example");

SimpleSectionAccess();
BindingSectionAccess();
CustomSectionAccess();

static void SimpleSectionAccess()
{
    Console.WriteLine("\nSimple FoxConfigurationSection sample...");
    var section = new FoxConfigurationSection("mySettings", true);

    var value = section.ConfigSection["stringProp"];
    Console.WriteLine($"stringProp={value}");

    var subObject = section.ConfigSection.GetSection("subObject");
    Console.WriteLine($"name: {subObject["name"]}");

    Console.WriteLine(section.ConfigSection["NonExistKey"] ?? "(null)");
}

static void BindingSectionAccess()
{
    Console.WriteLine("\nSimple FoxConfiguration<T> sample...");
    var section = new FoxConfiguration<MySettings>("mySettings");
    var mySettings = section.Section;
    Console.WriteLine(mySettings);

    if (mySettings.BoolProp == true)
    {
        Console.WriteLine("My boolean setting is true!");
    }
    else
    {
        Console.WriteLine("My boolan setting is false or not provided.");
    }
}

static void CustomSectionAccess()
{
    Console.WriteLine("\nComplex Custom Section sample...");
    var mySettings = new MySettings2Section().Settings;
    Console.WriteLine(mySettings);
}


