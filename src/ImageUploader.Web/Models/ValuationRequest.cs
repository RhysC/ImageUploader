using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ImageUploader.Web.Models
{
    public class ValuationRequest
    {
        public string Name { get; set; }
        public bool IsCustomerOver18YearsOld { get; set; }
        public string DescriptionOfItemToSell { get; set; }
        public string MakeOrManufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public PropertyCondition Condition { get; set; }
        public string OtherDetails { get; set; }
        public HttpPostedFileBase[] Images { get; set; }
        public bool IsCustomerRighfulOwner { get; set; }
        public decimal AmountRequestedForProperty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public enum PropertyCondition
    {
        //From http://www.cashconverters.co.uk/we-buy/online-valuation-step-2/?storename=Lewisham&store=88&two 10 feb 2016 - RHYSC
        None = 0,
        [Display(Name = "Not working.")]
        NotWorking = 1,

        [Display(Name = "Used: - Fair Condition: - Please detail wear & tear, any faults, missing accessories etc.")]
        UsedFairCondition = 2,

        [Display(Name = "Used: - Good Condition: - signs of minimal wear and tear, fully functioning, with all or some of the item’s original accessories.")]
        UsedGoodCondition = 3,

        [Display(Name = "Like New: - boxed with all original accessories (if applicable). No wear and tear.")]
        LikeNew = 4,

        [Display(Name = "Brand New: - unopened, receipt, fully boxed etc.")]
        NewBoxedUnopened = 5
    }
}