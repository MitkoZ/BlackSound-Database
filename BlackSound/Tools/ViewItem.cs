using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackSound.Tools
{
    class ViewItem
    {
        public string KeyPressed { get; set; }
        public string Text { get; set; }
        public Action ActionMethod { get; set; }

        public ViewItem(string keyPressed, string text, Action actionMethod)
        {
            this.KeyPressed = keyPressed;
            this.Text = text;
            this.ActionMethod = actionMethod;
        }
    }
}
