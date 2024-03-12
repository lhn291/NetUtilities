using NetUtilities.Domain.Entities;
using System.Collections.Generic;

namespace NetUtilities.Applications.Interfaces
{
    public interface IMenuService
    {
        List<MenuItem> GetHomeMenuItems();
    }
}
