using System;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace OS_Project1
{
    public partial class OS_with_MrStefanovic : ServiceBase
    {
        #region Members



        #endregion

        public OS_with_MrStefanovic()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        public void RunThreading()
        {
            ////var tasks = new[]
            ////{

            var filePath = string.Empty;

            for (int i = 0; i < 2; i++)
            {
                //    //new Task(() => OS_Thread());

                filePath = string.Format("c:\\ServiceTesting{0}.txt", i + 1);

                var thread = new Thread(() => OS_Thread());
                thread.Start();

                var thread1 = new Thread(() => RunThreads1(filePath));
                thread1.Start();

            }

            //Thread.Sleep(1);
            ////};
        }

        private void OS_Thread()
        {
            byte[] array1 = null;

            long bytes1 = GC.GetTotalMemory(false);

            array1 = new byte[1000 * 1000 * 3];
            array1[0] = 0;

            long bytes2 = GC.GetTotalMemory(false);

            Console.WriteLine(bytes2 - bytes1);
        }

        public void RunThreads1(string filePath)
        {
            var characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            var sb = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < 100000000; i++)
            {
                sb.Append(characters[random.Next(characters.Length)]);
            }

            if (!IsFileLocked(filePath))
                File.WriteAllText(filePath, sb.ToString());
        }

        private readonly object FileLock = new object();

        private void LogToFile(string errorLogPath, string message)
        {
            lock (FileLock)
            {
                using (var sw = new StreamWriter(errorLogPath, true))
                {
                    var text = string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyy--MMM-dd HH:mm:ss"), message, Environment.NewLine);

                    sw.Write(text);
                    sw.Flush();
                }
            }
        }

        private bool IsFileLocked(string fileName)
        {
            FileStream stream = null;
            var file = new FileInfo(fileName);

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            return false;
        }
    }
}
