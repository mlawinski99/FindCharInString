using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindCharInString
{
    unsafe class UnsafePtr
    {
        public char* ptr;
        unsafe public UnsafePtr(char* p)
        {
            ptr = p;
        }
    }
}
