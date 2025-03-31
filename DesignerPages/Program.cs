using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignerPages
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = @"C:\Designer Pages";
            string absolutePath = @"C:\Designer";
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                Console.WriteLine("Files in directory:");
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    Console.WriteLine(fileName);
                    List<string> designlist = new List<string>();
                    designlist.Add("\"Controler\",\"id\"");
                    string fileContent = File.ReadAllText(file);
                    //Console.WriteLine("Contents of the file:");
                    //Console.WriteLine(fileContent);
                    string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        if (line.Trim().StartsWith("protected"))
                        {
                            //Console.WriteLine(line.Trim());
                            int index = line.IndexOf("WebControls");
                            if (index != -1)
                            {
                                string result = line.Substring(index + "WebControls".Length+1).Trim();
                                result = result.TrimEnd(';');
                                string[] parts = result.Split(new[] { ' ' }, StringSplitOptions.None);
                               // Console.WriteLine(result);
                                //Console.WriteLine(parts[0]);
                                //Console.WriteLine(parts[1]);
                                designlist.Add("\"" + parts[0]+ "\",\"" + parts[1] + "\"");

                            }
                        }
                    }
                    Console.WriteLine("Design List:");
                    foreach (var item in designlist)
                    {
                        Console.WriteLine(item);
                    }
                    File.WriteAllLines($"{absolutePath}\\{fileName}{System.DateTime.Now:MM/dd/yyyy}.csv", designlist);
                }
            }
            catch (DirectoryNotFoundException dirEx)
            {
                Console.WriteLine("Directory not found: " + dirEx.Message);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
