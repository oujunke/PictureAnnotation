using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Servers
{
    public class CmdServer
    {
        private Process _cmdProcess;
        public CmdServer()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            _cmdProcess = new Process();
            _cmdProcess.StartInfo = startInfo;
            _cmdProcess.OutputDataReceived += _cmdProcess_OutputDataReceived;
        }

        private void _cmdProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
