using System.ComponentModel.DataAnnotations;

namespace BusinessSolutions.Web.Application.ViewModels;

public class FilterViewModel
{
    [Display(Name = "Provider Id")]
    public int? ProviderId { get; set; }

    [Display(Name = "Provider Name")]
    public string? ProviderName { get; set; }
    [Display(Name = "Order Number")]
    public string? OrderNumber { get; set; }
    [Display(Name = "Start Date")]
    public DateTime? StartDate { get; set; }
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }
    [Display(Name = "Item Name")]
    public string? ItemName { get; set; }
    [Display(Name = "Item Unit")]
    public string? ItemUnit { get; set; }
}