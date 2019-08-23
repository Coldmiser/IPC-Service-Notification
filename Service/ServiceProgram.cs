/********************************** Module Header **********************************\ 
Module Name:  Program.cs 
Project:      Service/Notifier Client/Server application
Copyright (c) Microsoft Corporation. 
 
Information on how this application checks for updates can be found here:
https://www.youtube.com/watch?v=W8Qu4qMJyh4

 
This source is subject to the Microsoft Public License. 
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
All other rights reserved. 
 
THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,  
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED  
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
\***********************************************************************************/

#region Using directives 
using System;
using System.Diagnostics;
using Topshelf;
#endregion

namespace Service
{
    class ServiceProgram
    {
        static void Main(string[] args)
        {
            // You can put this on a button click if you prefer, but this will automatically check & update
            // Modified to use a discard:  https://stackoverflow.com/questions/22629951/suppressing-warning-cs4014-because-this-call-is-not-awaited-execution-of-the

            AddVersionNumber();
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Heartbeat>(s =>
                {
                    s.ConstructUsing(Heartbeat => new Heartbeat());
                    s.WhenStarted(heartbeat => heartbeat.Start());
                    s.WhenStopped(heartbeat => heartbeat.Stop());
                });
                x.RunAsLocalSystem();
                x.SetServiceName("HeartbeatService");
                x.SetDisplayName("Heartbeat Service");
                x.SetDescription("This is the sample heartbeat service");
                x.StartAutomatically();
                x.EnableServiceRecovery(r =>
                {
                    //you can have up to three of these
                    r.RestartService(0);
                    r.RunProgram(7, "\"C:\\Windows\\System32\\SC.exe\"start heartbeatservice");
                    //the last one will act for all subsequent failures
                    r.RestartComputer(2, "A failure has occurred and this system will be restarted");

                    //should this be true for crashed or non-zero exits
                    r.OnCrashOnly();

                    //number of days until the error count resets
                    r.SetResetPeriod(1); // set the reset interval to one day
                });
            });
            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
            Console.WriteLine("Hello World!");
        }

        // Getting version information from:  https://youtu.be/W8Qu4qMJyh4?t=2048
        static private void AddVersionNumber()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            Console.WriteLine(versionInfo.FileVersion);
        }

        // This method was modified and made static:  https://stackoverflow.com/questions/2505181/error-an-object-reference-is-required-for-the-non-static-field-method-or-prop

    }
}
