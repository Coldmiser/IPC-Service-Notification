using System;
using System.Windows.Forms;

namespace TaskTrayApplication
{
    public class TaskTrayApplicationContext : ApplicationContext
    {
        NotifyIcon notifyIcon = new NotifyIcon();
        Configuration configWindow = new Configuration();
        //Configuration aboutWindow = new About();

        public TaskTrayApplicationContext()
        {
            MenuItem configMenuItem = new MenuItem("Configuration", new EventHandler(ShowConfig));
            //MenuItem.aboutMenuItem = new MenuItem("About", new EventHandler(ShowAbout));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));

            notifyIcon.Icon = TaskTray.Properties.Resources.AppIcon;
            notifyIcon.DoubleClick += new EventHandler(ShowMessage);
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { configMenuItem, exitMenuItem });
            //notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { aboutMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
        }

        void ShowMessage(object sender, EventArgs e)
        {
            // Only show the message if the settings say we can.
            if (TaskTray.Properties.Settings.Default.ShowMessage)
                MessageBox.Show("Hello World");
        }

        void ShowConfig(object sender, EventArgs e)
        {
            // If we are already showing the window meerly focus it.
            if (configWindow.Visible)
                configWindow.Focus();
            else
                configWindow.ShowDialog();
        }

        //void ShowAbout(object sender, EventArgs e)
        //{
        //    // If we are already showing the window meerly focus it.
        //    if (aboutWindow.Visible)
        //        aboutWindow.Focus();
        //    else
        //        aboutWindow.ShowDialog();
        //}

        void Exit(object sender, EventArgs e)
        {
            // We must manually tidy up and remove the icon before we exit.
            // Otherwise it will be left behind until the user mouses over.
            notifyIcon.Visible = false;

            Application.Exit();
        }
    }
}
