# IT DEV RISK.

## Question 2:
A new category was created called PEP (politically exposed person). Also, a new bool property
IsPoliticallyExposed was created in the ITrade interface. A trade shall be categorized as PEP if
IsPoliticallyExposed is true. Describe in at most 1 paragraph what you must do in your design to account for this
new category.

## Answare question 2:
We need create a new Enum on our `Trade.Domain.Enums.Category` with a description:

```cs
public enum Category {
    ...
    [Description("Politically Exposed Person")]
    PEP = 4
}
```

After that we has to create a new property named `IsPoliticallyExposed` on `ITrade` interface and on `Trade` class. We add a new argument on constructor and create a validation for this category.

```cs
// Trade.Domain.Models.ITrade | ITrade.cs
public interface ITrade {
    ...
    bool IsPoliticallyExposed { get; }
}

// Trade.Domain.Models.Trade | Trade.cs
public class Trade : ITrade
{
    ...
    public bool IsPoliticallyExposed { get; private set; }

    public Trade(double value, string clientSector, DateTime nextPaymentDate, bool isPoliticallyExposed){
        ...
        IsPoliticallyExposed = isPoliticallyExposed;
    }

    public Category GetCategory(DateTime ReferenceDate) {
        return (
            ...
            IsPoliticallyExposed,
            ...
        ) switch {
            ...
            (_, true, _, _) => Category.PEP,
            ...
        };
    }
}

```

On `Main` method at `Program.cs` we has to create a entry point for our new information.

```cs
static string[] processTrade(DateTime referenceDate, int numberOfTrades) {
    var categories = new List<string>();
    for (int i = 1; i <= numberOfTrades; i++)
    {
        ...
        bool isPoliticallyExposed = data?.Length == 4 && !string.IsNullOrEmpty(data?[3]) ? true : false;

        var trade = new Trade.Domain.Models.Trade(amount, sector, nextPaymentDate, isPoliticallyExposed);
        ...
    }
    return categories.ToArray<string>();
}
```