using FlaUI.Core;
using FlaUI.UIA3;

namespace WpfTest
{
    public class UITestsFixture : IDisposable
    {
        public UIA3Automation Automation { get; set; }
        public Application Application { get; set; }

        public UITestsFixture()
        {
            Automation = new UIA3Automation();
            Application = Application.Launch("C:\\Users\\natal\\Desktop\\labs\\lab8\\Wpf\\SimpleWpf.csproj");
        }

        public void Dispose()
        {
            Application.Close();
            Application.Dispose();
            Automation.Dispose();
        }
    }
}
