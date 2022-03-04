using System;
using System.Collections.Generic;

namespace TestUssd.Core
{
    public class UssdForm
    {
        public string Header { get; set; }
        public string Footer { get; set; }
        public List<string> Items { get; set; }

        public UssdForm(string header, string footer)
        {
            Header = header;
            Footer = footer;
            Items = new List<string>();
        }

        public UssdForm()
        {
            Items = new List<string>();
        }
        public UssdForm Add(string item)
        {
            Items.Add(item);
            return this;
        }

        public string Render()
        {
            var display = string.Empty;
            if (!string.IsNullOrWhiteSpace(Header))
            {
                display += Header + Environment.NewLine;
            }
            for (int i = 0; i < Items.Count; i++)
            {
                display += string.Format("{0}. {1}" + Environment.NewLine,
                    i + 1, Items[i]);
            }
            if (!string.IsNullOrWhiteSpace(Footer))
            {
                display += Footer;
            }
            return display;
        }
    }
}
