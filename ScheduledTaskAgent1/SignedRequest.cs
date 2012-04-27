using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;

namespace ScheduledTaskAgent1
{
    public class SignedRequest
    {
        public string ident { get; set; }
        public string time { get; set; }
        public string name { get; set; }
        public string clientident { get; set; }
        public string player { get; set; }
        public SignedRequest()
        {

        }
    }
}


