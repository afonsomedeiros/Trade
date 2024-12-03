namespace Trade.Domain.Models;

public interface ITrade {
    double Value { get; }
    string ClientSector { get; }
    DateTime NextPaymentDate { get; }
    bool IsPoliticallyExposed { get; }
}