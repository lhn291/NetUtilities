using System.ComponentModel.DataAnnotations;

namespace NetUtilities.Domain.Enums
{
    public enum SpaceTabEnum
    {
        [Display(Name = "2 Space Tab")]
        TwoSpaceTab,
        [Display(Name = "3 Space Tab")]
        ThreeSpaceTab,
        [Display(Name = "4 Space Tab")]
        FourSpaceTab
    }
}
