using System.ComponentModel.DataAnnotations;
using DataLayer.EfClasses;

namespace ServiceLayer.AdminServices;

public interface IChangePriceOfferService
{
    Book OrgBook { get; }

    PriceOffer GetOriginal(int id);
    
    ValidationResult AddRemovePriceOffer(PriceOffer promotion);
}