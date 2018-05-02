﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Test
{
    class List
    {
        public void foo(List<int> l)
        {
            //var l = new LinkedList<int>();

            l.Add(1);
            l.Add(2);
            l.Add(3);
            l.Add(4);

            int acum = 0;
            foreach (var item in Elems(l))
            {
                acum = acum + item;
            }
            Contract.Assert(acum >= 6);
        }
        static IEnumerable<int> Elems(ICollection<int> list)
        {
            foreach (var elem in list)
            {
                yield return elem;

            }
        }
    }
}
