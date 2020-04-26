using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using TMEDemoApp.BusinessLogic.UniqueRandomGenerator;
using TMEDemoApp.BusinessLogic.UsedNumberProviders;

namespace TMEDemoApp.ViewModels
{
    public class NumbersViewModel : Screen
    {
        private List<int> _randomNumbers = new List<int>();
        private IUniqueGenerator generator;
        private IUsedNumbersProvider usedNumbersProvider;
        private int _generateSteps = 5;
        private double _progressBar;
        public NumbersViewModel()
        {
            string dbName = ConfigurationManager.AppSettings["dbName"];
            string serverName = ConfigurationManager.AppSettings["serverName"];
            string tableName = ConfigurationManager.AppSettings["tableName"];
            usedNumbersProvider = UsedNumbersProviderFactory.GetSQLUsedNumbersProvider(serverName, dbName, tableName);
            generator = RandomUniqueGeneratorFactory.GetRandomUniqueGeneratorSQL(1000000,9999999);
            usedNumbersProvider.CachedMode = true;
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
            get
            {
                return _randomNumbers;
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
        public double ProgressBar
        {
            get
            {
                return _progressBar;
            }
            set
            {
                _progressBar = value;
                NotifyOfPropertyChange(() => ProgressBar);
            }
        }
        async public void GenerateRandomNumbers()
        {
         
            if (RandomNumbersCount == 0) return;
            int iterations = _generateSteps;
            int numbersLeft = RandomNumbersCount;
            List<int> generatedList = new List<int>();
            int step = RandomNumbersCount / _generateSteps;
            await Task.Run(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    if (numbersLeft < step)
                        step = numbersLeft;
                    generatedList.AddRange(generator.GenerateRandomUniqueNumbers(usedNumbersProvider, step));
                    ProgressBar = ((double)i / (double)iterations) * 100;
                    numbersLeft -= step;
                }
            });
            ProgressBar = 100;
            RandomNumbersList = generatedList;
            usedNumbersProvider.SyncCache();
            RandomNumbersUsed = usedNumbersProvider.UsedNumbersCount;
        }


    }
}
