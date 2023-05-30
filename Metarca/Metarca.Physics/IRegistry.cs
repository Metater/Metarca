using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metarca.Physics;

public interface IRegistry<T>
{
    public bool Add(T item);
    public bool Remove(T item);
}