using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCrawler.Bot.Helpers
{
    public class PyConverter
    {
        public PyConverter()
        {
            this.Converters = new Dictionary<IntPtr, Func<PyObject, object>>();
        }

        private Dictionary<IntPtr, Func<PyObject, object>> Converters;

        public void Add(IntPtr type, Func<PyObject, object> func)
        {
            this.Converters.Add(type, func);
        }

        public object Convert(PyObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            PyObject type = obj.GetPythonType();
            Func<PyObject, object> func;
            var state = Converters.TryGetValue(type.Handle, out func);
            if (!state)
            {
                throw new Exception($"Type {type.ToString()} not recognized");
            }
            return func(obj);
        }
    }

}
