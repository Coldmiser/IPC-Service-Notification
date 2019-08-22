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
    class Program
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
