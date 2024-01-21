using System.ComponentModel;

namespace FamilyFoundsApi.Domain.Enums;

public enum CategoriesEnum
{
    [Description("Artykuły spożywcze")]
    FOOD = 1,
    [Description("Artykuły chemiczne i higieniczne")]
    CHEMICAL_AND_HYGIENE = 2,
    [Description("Ubrania")]
    CLOTHES = 3,
    [Description("Rozrywka")]
    ENTERTAINMENT = 4,
    [Description("Transport")]
    TRANSPORT = 5,
    [Description("Sprzęt domowy i budowlany")]
    HOUSEHOLD = 6,
    [Description("Zdrowie i uroda")]
    HEALTH = 7,
    [Description("Rachunki")]
    BILLS = 8,
    [Description("Dzieci")]
    CHILDREN = 9,
    [Description("Inne")]
    OTHER = 10
}
