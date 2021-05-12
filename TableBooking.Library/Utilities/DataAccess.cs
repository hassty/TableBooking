using System;
using System.Collections.Generic;
using System.Text;

namespace TableBooking.Core.Utilities
{
    public class DataAccess : IDataAccess
    {
        public void LoadData()
        {
            Console.WriteLine("Loading data");
        }

        public void SaveData(string name)
        {
            Console.WriteLine($"Saving {name}");
        }
    }
}