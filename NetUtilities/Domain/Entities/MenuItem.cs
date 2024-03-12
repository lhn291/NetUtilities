using System.Collections.Generic;

namespace NetUtilities.Domain.Entities
{
    public class MenuItem
    {
        public MenuItem(string name, string icon, List<MenuItem> menuItems = null)
        {
            Name = name;
            Icon = icon;
            MenuItems = menuItems ?? new List<MenuItem>();
        }

        public string Name { get; private set; }
        public string Icon { get; private set; }
        public List<MenuItem> MenuItems { get; private set; }
    }
}
