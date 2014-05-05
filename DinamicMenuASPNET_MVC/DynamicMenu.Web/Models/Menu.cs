using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DynamicMenu.Web.Models
{
    [Table("tbMenu")]
    public class Menu
    {
        private int menuFather = 0;

        public int MenuFather
        {
            get { return menuFather; }
            set { menuFather = value;}
        }

 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }

        public Menu(int id, string itemName, string _itemLink, int _menuFather)
        {
            Id = id;
            Name = itemName;
            Link = _itemLink;
            MenuFather = _menuFather;
        }
    }
}