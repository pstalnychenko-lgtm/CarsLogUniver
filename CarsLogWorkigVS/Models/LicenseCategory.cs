using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkigVS.Models
{
    public class LicenseCategory 
    {
        private List<DateTime, CategoryName> FullCategories;

        private DateTime _dateOfIssue;

        private DateTime _expirationDate;

        private string _sityOfIssue;

        private string _serialNumber;

        private string _TrafficPoliceCenter;
    }
    public enum CategoryName
    {
            A,
            A1,
            B,
            B1,
            C,
            C1,
            D,
            D1,
            CE,
            C1E,
            BE,
            T1,
            T2
    }
}
