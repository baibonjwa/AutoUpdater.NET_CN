using System;
using System.Globalization;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace AutoUpdaterTest
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //uncomment below line to see russian version

            //AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru");

            //If you want to open download page when user click on download button uncomment below line.

            //AutoUpdater.OpenDownloadPage = true;

            //Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

            //AutoUpdater.LetUserSelectRemindLater = false;
            //AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            //AutoUpdater.RemindLaterAt = 2;

            //Want to handle update logic yourself then uncomment below line.

            //AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;

            //AutoUpdater.Start("http://rbsoft.org/updates/right-click-enhancer.xml");
        }

        private static void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var dialogResult =
                        MessageBox.Show(
                            string.Format(
                                "现在有新的版本 {0} 可以使用. 当前版本为 {1}。您想现在更新该软件么?",
                                args.CurrentVersion, args.InstalledVersion), @"有可用的更新",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            //You can use Download Update dialog used by AutoUpdater.NET to download the update.

                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"现在没有可用的更新，请稍后再试。", @"没有可用的更新",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(
                       @"访问服务器时出现了一个问题，请检查您的网络连接，然后稍候再试。",
                       @"更新检查失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonCheckForUpdate_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start("http://rbsoft.org/updates/right-click-enhancer.xml");
        }
    }
}
