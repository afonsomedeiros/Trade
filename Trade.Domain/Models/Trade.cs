using Trade.Domain.Enums;

namespace Trade.Domain.Models;

public class Trade : ITrade
{
    public readonly int BASE_VALUE = 1_000_000;
    public double Value { get; private set; }

    public string ClientSector { get; private set; }

    public DateTime NextPaymentDate { get; private set; }
    public bool IsPoliticallyExposed { get; private set; }

    public Trade(double value, string clientSector, DateTime nextPaymentDate, bool isPoliticallyExposed){
        Value = value;
        ClientSector = clientSector;
        NextPaymentDate = nextPaymentDate;
        IsPoliticallyExposed = isPoliticallyExposed;
    }

    public Category GetCategory(DateTime ReferenceDate) {
        return (
            DateTime.Compare(NextPaymentDate, ReferenceDate) < 0,
            IsPoliticallyExposed,
            Value >= BASE_VALUE && "private".Equals(ClientSector.ToLower()),
            Value >= BASE_VALUE && "public".Equals(ClientSector.ToLower())
        ) switch {
            (true, _, _, _) => Category.EXPIRED,
            (_, true, _, _) => Category.PEP,
            (_, _, true, _) => Category.MEDIUMRISK,
            (_, _, _, true) => Category.HIGHRISK,
            _ => 0
        };
    }
}
