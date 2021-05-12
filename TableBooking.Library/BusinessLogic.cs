using TableBooking.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TableBooking.Core
{
    public class BusinessLogic : IBusinessLogic
    {
        private IDataAccess _dataAccess;
        private ILogger _logger;

        public BusinessLogic(ILogger logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _dataAccess = dataAccess;
        }

        public void ProcessData()
        {
            _logger.Log("Starting the processing of data");
            Console.WriteLine("Processing of data");
            _dataAccess.LoadData();
            _dataAccess.SaveData("Info");
            _logger.Log("Finished processing of data");
        }
    }
}