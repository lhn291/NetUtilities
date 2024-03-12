using FastEnumUtility;
using NetUtilities.Applications.Interfaces;
using NetUtilities.Domain.Entities;
using NetUtilities.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetUtilities.Applications.Services
{
    public class MenuService : IMenuService
    {
        public List<MenuItem> GetHomeMenuItems()
        {
            var homeItems = Enum.GetValues<HomeMenuType>();
            return GetHomeMenuChildren(homeItems, string.Empty);
        }

        private List<MenuItem> GetHomeMenuChildren(HomeMenuType[] homeItems, string parentName)
        {
            var menuItems = homeItems
                .Where(c => c.GetLabel(2) == parentName)
                .Select(c => new MenuItem(
                    c.GetLabel(0),
                    c.GetLabel(1),
                    GetHomeMenuChildren(homeItems, c.GetLabel(0))
                ))
                .ToList();

            return menuItems;
        }

    }
}
