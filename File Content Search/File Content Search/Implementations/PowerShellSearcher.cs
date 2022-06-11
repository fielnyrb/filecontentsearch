using File_Content_Search.Interfaces;
using File_Content_Search.Structures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Content_Search.Implementations
{
    internal class PowerShellSearcher : IContentSearcher
    {

        ICharacterEscaper CharacterEscaper;

        public PowerShellSearcher(ICharacterEscaper characterEscaper)
        {
            CharacterEscaper = characterEscaper;
        }

        public List<FoundItem> Search(string searchString, string directory)
        {
            List<FoundItem> foundItems = new List<FoundItem>();

            // Thanks to DavidDr90 https://stackoverflow.com/questions/33654318/c-sharp-run-powershell-command-get-output-as-it-arrives
            using (PowerShell powerShell = PowerShell.Create())
            {
                // Source functions.
                powerShell.AddScript("gci '" + directory + "' -include '*.txt' -recurse ` | select-string -pattern '" + CharacterEscaper.Apply(searchString) + "' ` | Select-Object -Unique Path");

                // invoke execution on the pipeline (collecting output)
                Collection<PSObject> PSOutput = powerShell.Invoke();

                // loop through each output object item
                foreach (PSObject outputItem in PSOutput)
                {
                    // if null object was dumped to the pipeline during the script then a null object may be present here
                    if (outputItem != null)
                    {
                        foundItems.Add(new FoundItem($"{outputItem}"));
                    }
                }

                // check the other output streams (for example, the error stream)
                if (powerShell.Streams.Error.Count > 0)
                {
                    // error records were written to the error stream.
                    // Do something with the error
                    MessageBox.Show("PowerShell error!");
                }

                return foundItems;
            }
        }
    }
}
