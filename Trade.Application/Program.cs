using System.ComponentModel;
using System.Globalization;


static void Main(string[] args){
    string? sDate = Console.ReadLine();
    DateTime ReferenceDate = Convert.ToDateTime(sDate, new CultureInfo("en-US"));
    int numberOfTrades = Convert.ToInt32(Console.ReadLine());
    
    var categories = processTrade(ReferenceDate, numberOfTrades);
    
    if (categories.Length > 0) {
        for (int i = 0; i < categories.Length; i++)
        {
            Console.WriteLine(categories[i]);
        }
    }
}

static string[] processTrade(DateTime referenceDate, int numberOfTrades) {
    var categories = new List<string>();
    for (int i = 1; i <= numberOfTrades; i++)
    {
        string[]? data = Console.ReadLine()?.Split(' ');

        if (data?.Length == 0)
            continue;
        
        double amount = Convert.ToDouble(data?[0]);
        string? sector = data?[1];
        DateTime nextPaymentDate = Convert.ToDateTime(data?[2], new CultureInfo("en-US"));
        bool isPoliticallyExposed = data?.Length == 4 && !string.IsNullOrEmpty(data?[3]) ? true : false;

        var trade = new Trade.Domain.Models.Trade(amount, sector, nextPaymentDate, isPoliticallyExposed);
        Trade.Domain.Enums.Category category = trade.GetCategory(referenceDate);

        var sCategory = GetEnumDescription(category);
        categories.Add(sCategory);
    }
    return categories.ToArray<string>();
}

static string GetEnumDescription(Enum value)
{
    var field = value.GetType().GetField(value.ToString());
    var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
    
    return attribute != null ? attribute.Description : value.ToString();
}

Main([]);