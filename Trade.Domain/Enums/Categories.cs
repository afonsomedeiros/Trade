using System.ComponentModel;

namespace Trade.Domain.Enums;

public enum Category {
    [Description("EXPIRED")]
    EXPIRED = 1,
    [Description("HIGHRISK")]
    HIGHRISK = 2,
    [Description("MEDIUMRISK")]
    MEDIUMRISK = 3,
    [Description("Politically Exposed Person")]
    PEP = 4
}