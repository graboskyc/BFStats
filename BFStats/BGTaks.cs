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
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;

namespace BFStats
{
    public class BGTaks
    {
        public static void AddBGTaks()
        {
            PeriodicTask periodicTask = new PeriodicTask("BF3Stats");
            periodicTask.Description = "Gets stats in background hourly";

            try
            {
                if (ScheduledActionService.Find(periodicTask.Name) != null)
                {
                    ScheduledActionService.Remove("BF3Stats");
                }
                ScheduledActionService.Add(periodicTask);
            }
            catch (InvalidOperationException ex)
            {
                if (ex.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }

                if (ex.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }

        }
    }
}
