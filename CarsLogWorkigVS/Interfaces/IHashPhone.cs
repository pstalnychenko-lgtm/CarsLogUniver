using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasPhone
    {
        string Phone { get; }
        void ChangePhone(string newPhone);
    }
}
