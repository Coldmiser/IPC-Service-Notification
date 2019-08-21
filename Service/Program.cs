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
using Squirrel;
using System;
using System.Threading.Tasks;
#endregion

namespace Service
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can put this on a button click if you prefer, but this will automatically check & update
            // Modified to use a discard:  https://stackoverflow.com/questions/22629951/suppressing-warning-cs4014-because-this-call-is-not-awaited-execution-of-the
            _ = CheckForUpdates();
            Console.WriteLine("Hello World!");
        }

        // This method was modified and made static:  https://stackoverflow.com/questions/2505181/error-an-object-reference-is-required-for-the-non-static-field-method-or-prop
        static private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager(@"C:\Temp\Releases"))
            {
                await manager.UpdateApp();
            }
        }

    }
}
