using System;
using System.Collections.Generic;

namespace CoreWiki.Extensibility.TheFeistyGoat
{
    public class SpecialItem
    {
        public SpecialItem(string item, double regularPrice, double specialPrice)
        {
            Item = item;
            RegularPrice = regularPrice;
            SpecialPrice = specialPrice;
        }

        public string Item { get; set; }
        public double RegularPrice { get; set; }
        public double SpecialPrice { get; set; }

        public static List<SpecialItem> GetSpecials()
        {
            return new List<SpecialItem>()
            {
                new SpecialItem("Dozen Buffalo Wings", 9.99, 5.99),
                new SpecialItem("Sam Adams Summer Ale", 3.50, 1.50)
            };
        }
    }
}
