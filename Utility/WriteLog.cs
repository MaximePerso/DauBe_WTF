using System;
using System.IO;
using System.Reflection;
namespace DauBe_WTF.Utility
{
    public class WriteLog
    {
        private string m_exePath = string.Empty;

        public WriteLog(string logMessage, string TypeOfMessage)
        {
            LogWrite(logMessage, TypeOfMessage);
        }

        public void LogWrite(string logMessage, string TypeOfMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = "CommandLog.txt";
            if (TypeOfMessage == "Error")
                fileName = "ErrorLog.txt";
            try
            {

                {
                    using (StreamWriter w = File.AppendText(m_exePath + "\\" + fileName))
                    {
                        Log(logMessage, w);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
