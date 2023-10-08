using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachSan.Security
{
    class KEYS
    {
        public void OpenSSL_Generate_Private_Key()
        {
            String file = Directory.GetCurrentDirectory() + @"\private.txt";
            Boolean file_exitst = File.Exists(file);

            if (file_exitst) File.Delete(file);

            Process process = new Process();
            process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            process.StartInfo.Arguments = "/c openssl genrsa -out private.txt 2048";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }

        public void OpenSSL_Generate_Public_Key()
        {
            String file = Directory.GetCurrentDirectory() + @"\public.txt";
            Boolean file_exitst = File.Exists(file);

            if (file_exitst) File.Delete(file);

            Process process = new Process();
            process.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            process.StartInfo.Arguments = "/c openssl rsa -in private.txt -outform PEM -pubout -out public.txt";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
        }

        public String Read_Private_Key_PEM()
        {
            try
            {
                String dics = Directory.GetCurrentDirectory();
                String pr_file_name = dics + @"\private.txt";
                String text = "";
                String[] lines = File.ReadAllLines(pr_file_name);

                int len = lines.Length;

                for (int i = 0; i < len; i++)
                    if (i != 0 && i < len - 1)
                        text += lines[i];

                return text;
            }
            catch (Exception e) { }
            return null;
        }

        public String Read_Public_Key_PEM()
        {
            try
            {
                String dics = Directory.GetCurrentDirectory();
                String pr_file_name = dics + @"\public.txt";
                String text = "";
                String[] lines = File.ReadAllLines(pr_file_name);

                int len = lines.Length;

                for (int i = 0; i < len; i++)
                    if (i > 0 && i < len - 1)
                        text += lines[i];

                return text;
            }
            catch (Exception e) { }
            return null;
        }


        public String Read_file_key(String file)
        {
            try
            {
                String dics = Directory.GetCurrentDirectory();
                int index = dics.IndexOf(@"\bin\Debug");
                dics = dics.Substring(0, index);
                String file_name = dics + @"\Keys\" + file + ".txt";
                String text = "";
                String[] lines = File.ReadAllLines(file_name);

                int len = lines.Length;

                foreach (String line in lines) text += line;

                return text;
            }
            catch (Exception e) { }
            return null;
        }

        public Boolean Save_Keys(String key, String file_name)
        {
            try
            {
                String dics = Directory.GetCurrentDirectory();
                int indexs = dics.IndexOf(@"\bin\Debug");
                String dics_filter = dics.Substring(0, indexs);
                String c_file_name = dics_filter + @"\Keys\" + file_name + ".txt";
                File.WriteAllText(c_file_name, key);
                return true;
            }
            catch (Exception e) { }
            return false;
        }


        public Boolean Delete_File(String file_name)
        {
            try
            {
                String dics = Directory.GetCurrentDirectory();
                int indexs = dics.IndexOf(@"\bin\Debug");
                String dics_filter = dics.Substring(0, indexs);
                String c_file_name = dics_filter + @"\Keys\" + file_name + ".txt";
                if(File.Exists(c_file_name)) File.Delete(c_file_name);
                return true;
            }
            catch (Exception e) { }
            return false;
        }
    }
}
