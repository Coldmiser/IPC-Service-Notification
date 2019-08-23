/********************************** Module Header **********************************\ 
Module Name:  Program.cs 
Project:      Service/Notifier Client/Server application
Copyright (c) Microsoft Corporation. 
 
Information on how this application checks for updates can be found here:
https://www.codeproject.com/Articles/18683/Creating-a-Tasktray-Application
https://stackoverflow.com/questions/995195/how-can-i-make-a-net-windows-forms-application-that-only-runs-in-the-system-tra
https://docs.microsoft.com/en-us/windows/win32/shell/notification-area

 
This source is subject to the Microsoft Public License. 
See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL. 
All other rights reserved. 
 
THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,  
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED  
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE. 
\***********************************************************************************/

#region Using directives 
using System;
using System.Windows.Forms;
#endregion

namespace TaskTrayApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Instead of running a form, we run an ApplicationContext.
            Application.Run(new TaskTrayApplicationContext());
        }
    }
}