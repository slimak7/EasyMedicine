namespace ActiveSubstancesManagement.Helpers
{
    public class StatsCollector
    {
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private string _medicineID;

        private readonly string _date = DateTime.Now.ToString("dd-MM-yyyy");
        private readonly string _path = "./Stats";

        public StatsCollector(string medicineID)
        {
            _medicineID = medicineID;
        }

        public int NumberOfFoundKeywords { get; set; }
        public int NumberOfFoundSubstances { get; set; }
        
        private static object _lock = new object();

        public void Start()
        {
            _startTime = TimeSpan.FromMilliseconds(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        }
        public void End()
        {
            _endTime = TimeSpan.FromMilliseconds(DateTimeOffset.Now.ToUnixTimeMilliseconds());

            Thread thread = new Thread(() =>
            {
                CreateFolders();

                string text = "MedicineID: " + _medicineID + "\n";
                text += "Time elapsed: " + (_endTime - _startTime).TotalMilliseconds + " ms\n";
                text += "Number of keywords found: " + NumberOfFoundKeywords + "\n";
                text += "Number of substances found: " + NumberOfFoundSubstances;

                CreateFile(text);
            });
            thread.Start();

        }

        private void CreateFolders()
        {
            if (!Directory.Exists(_path))
            {
                lock (_lock)
                {
                    Directory.CreateDirectory(_path + "/" + _date);
                }
            }
            else if (!Directory.Exists(_path + "/" + _date)) 
            {
                lock (_lock)
                {
                    Directory.CreateDirectory(_path + "/" + _date);
                }                
            }
        }

        private void CreateFile(string text)
        {
            string path = Path.Combine(_path, _date);

            string fileName = "stats_" + _medicineID.Replace("-", "_");

            lock (_lock)
            {

                int fileNumber = 1;
                while (File.Exists(path + "/" + fileName + "_" + fileNumber + ".txt"))
                {
                    fileNumber++;
                }
                fileName += "_" + fileNumber;

                File.WriteAllText(path + "/" + fileName + ".txt", text);

            }
            
        }
    }
}
