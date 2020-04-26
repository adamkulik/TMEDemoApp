using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TMEDemoApp.BusinessLogic.UniqueRandomGenerator;
using TMEDemoApp.BusinessLogic.UsedNumberProviders;

namespace TMEDemoApp.ViewModels
{
    public class NumbersViewModel : Screen
    {
        private List<int> _randomNumbers = new List<int>();
        private IUniqueGenerator generator;
        private IUsedNumbersProvider usedNumbersProvider;
        public NumbersViewModel()
        {
            string dbName = ConfigurationManager.AppSettings["dbName"];
            string serverName = ConfigurationManager.AppSettings["serverName"];
            string tableName = ConfigurationManager.AppSettings["tableName"];
            usedNumbersProvider = UsedNumbersProviderFactory.GetSQLUsedNumbersProvider(serverName, dbName, tableName);
            generator = RandomUniqueGeneratorFactory.GetRandomUniqueGeneratorSQL(1000000,9999999);
        }
        public string RandomNumbers
        {
            get { return String.Join(",",_randomNumbers); }
        }
        public List<int> RandomNumbersList
        {
            set
            {
                _randomNumbers = value;
                NotifyOfPropertyChange(() => RandomNumbers);
            }
        }
        public int RandomNumbersUsed
        {
            get
            {
                return usedNumbersProvider.UsedNumbersCount;
            }
            set
            {
                NotifyOfPropertyChange(() => RandomNumbersUsed);
                NotifyOfPropertyChange(() => RandomNumbersUsedPercentage);
            }
        }
        public int RandomNumbersUsedPercentage
        {
            get
            {
                return (int)Math.Round(((double)usedNumbersProvider.UsedNumbersCount / (generator.To - generator.From)) * 100);
            }
        }
        public int RandomNumbersCount { get; set; }
        public void GenerateRandomNumbers()
        {
            if (RandomNumbersCount == 0) return;
            RandomNumbersList = generator.GenerateRandomUniqueNumbers(usedNumbersProvider, RandomNumbersCount);
            RandomNumbersUsed = usedNumbersProvider.UsedNumbersCount;
        }

    }
}
