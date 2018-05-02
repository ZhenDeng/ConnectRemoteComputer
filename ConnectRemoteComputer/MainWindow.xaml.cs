using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace ConnectRemoteComputer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ComboBox cmb;
        private ComboBoxItem cbi;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String calendarText;
            String fullUserName;
            calendarText = (null == cbi)
                ? "@konekt.com.au"
                : cbi.Content.ToString();

            fullUserName = username.Text + calendarText;

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            //cmdkey /generic:"<server>" /user:"<user>" /pass:"<pass>"
            //mstsc /v:<server>
            //cmdkey /delete:target name
            //use cmdkey /list to check target name

            cmd.StandardInput.WriteLine("cmdkey /delete:TERMSRV/w" + computerid.Text);
            cmd.StandardInput.WriteLine("cmdkey /generic:\"w" + computerid.Text + "\" /user:\"" + fullUserName + "\"");
            cmd.StandardInput.WriteLine("mstsc /v:w" + computerid.Text);
            cmd.StandardInput.WriteLine("cmdkey /delete:TERMSRV/w" + computerid.Text);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        private void Suffix_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmb = (ComboBox)sender;
            cbi = (ComboBoxItem)cmb.SelectedItem;
        }

    }
}
