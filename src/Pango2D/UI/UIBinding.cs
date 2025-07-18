using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pango2D.UI
{
    public class UIBinding
    {
        public string PropertyName { get; }
        public Func<object> ValueGetter { get; }

        public UIBinding(string propertyName, Func<object> valueGetter)
        {
            PropertyName = propertyName;
            ValueGetter = valueGetter;
        }
    }
}
