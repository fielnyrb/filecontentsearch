using File_Content_Search.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Implementations
{
    public class PortNumberSetting
    {
        public string GetPortNumber()
        {
            string portNumberToBeReturned = "";

            using (var context = new MyContext())
            {
                // Query the database to get the value of the "portnumber" field
                var portNumber = context.Settings
                    .Where(s => s.Name == "portnumber")
                    .Select(s => s.Value)
                    .FirstOrDefault();

                if (portNumber == null)
                {
                    // If the port number is null, update database with default value: 50001
                    string defaultValue = "50001";
                    var setting = new Setting
                    {
                        Name = "portnumber",
                        Value = defaultValue
                    };
                    portNumberToBeReturned = setting.Value;
                    context.Settings.Add(setting);
                    context.SaveChanges();
                }
                else
                {
                    // Set the value of the TextBox to the retrieved port number
                    portNumberToBeReturned = portNumber.ToString();
                }
            }
            return portNumberToBeReturned;
        }

        public void UpdatePortNumber(string portNumber)
        {
            using (var context = new MyContext())
            {
                // Query the database to get the value of the "portnumber" field
                var setting = context.Settings
                    .Where(s => s.Name == "portnumber")
                    .FirstOrDefault();

                // Update the value of the "portnumber" field
                setting.Value = portNumber;
                context.SaveChanges();
            }
        }
    }
}
