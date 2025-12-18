using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using FluentAssertions;

namespace WpfTest
{
    public partial class UnitTest(UITestsFixture fixture) : IClassFixture<UITestsFixture>
    {
        private UITestsFixture _fixture = fixture;
                
        [Fact]
        public async Task SignIn_InputUnCorrectPasswordLogin_ShowMessageBox()
        {
            var window = _fixture.Application.GetMainWindow(_fixture.Automation);

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("SignInButton")).AsButton();
            var loginInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            var passwordInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();

            loginInput?.Text = "unCorrect";
            passwordInput?.Text = "unCorrect";

            button?.Invoke();

            var mesBoxText = window?.FindFirstDescendant(cf => cf.ByAutomationId("65535")).AsLabel();
            var mesBoxButton = window?.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();

            await Task.Delay(250);
            window?.CaptureToFile("test_3.png");
            await Task.Delay(250);

            mesBoxText?.Text.Should().Be("Пользователь с таким логином не зарегистрирован.");

            mesBoxButton?.Invoke();
            window?.Close();
        }

        [Fact]
        public async Task SignIn_WithOutInputData_ShowMessageBox()
        {
            var window = _fixture.Application.GetMainWindow(_fixture.Automation);

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("SignInButton")).AsButton();
            var loginInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            var passwordInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();

            loginInput?.Text = "";
            passwordInput?.Text = "";

            button?.Invoke();

            var mesBoxText = window?.FindFirstDescendant(cf => cf.ByAutomationId("65536")).AsLabel();
            var mesBoxButton = window?.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();

            await Task.Delay(250);
            window?.CaptureToFile("test_5.png");
            await Task.Delay(250);

            mesBoxText?.Text.Should().Be("Пользователь с таким логином не зарегистрирован.");

            mesBoxButton?.Invoke();
        }

        [Fact]
        public async Task SignIn_InputCorrectLoginUnCorrectPassword_ShowMessageBox()
        {
            var window = _fixture.Application.GetMainWindow(_fixture.Automation);

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("SignInButton")).AsButton();
            var loginInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            var passwordInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();

            loginInput?.Text = "banan";
            passwordInput?.Text = "unCorrect";

            button?.Invoke();

            var mesBoxText = window?.FindFirstDescendant(cf => cf.ByAutomationId("65535")).AsLabel();
            var mesBoxButton = window?.FindFirstDescendant(cf => cf.ByAutomationId("2")).AsButton();

            await Task.Delay(250);
            window?.CaptureToFile("test_2.png");
            await Task.Delay(250);

            mesBoxText?.Text.Should().Be("Пароль неверный");

            mesBoxButton?.Invoke();
        }

        [Fact]
        public async Task SignIn_InputCorrectUserLoginPassword_OpenWelcomePageUserRole()
        {
            var window = _fixture.Application.GetMainWindow(_fixture.Automation);

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("SignInButton")).AsButton();
            var loginInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            var passwordInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();

            loginInput?.Text = "koko";
            passwordInput?.Text = "12345";

            button?.Invoke();

            var welcomeLabel = window?.FindFirstDescendant(cf => cf.ByAutomationId("WelcomeTextBlock")).AsLabel();

            welcomeLabel?.Text.Should().Be($"Добро пожаловать, koko, Ваша роль - User");

            await Task.Delay(250);
            window?.CaptureToFile("test_1.png");
            await Task.Delay(250);

            var backButton = window?.FindFirstDescendant(cf => cf.ByAutomationId("BrowseBack")).AsButton();
            backButton?.Invoke();
        }

        [Fact]
        public async Task SignIn_InputCorrectAdministatorLoginPassword_OpenWelcomePageAdministatorRole()
        {
            var window = _fixture.Application.GetMainWindow(_fixture.Automation);

            var button = window?.FindFirstDescendant(cf => cf.ByAutomationId("SignInButton")).AsButton();
            var loginInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("LoginTextBox")).AsTextBox();
            var passwordInput = window?.FindFirstDescendant(cf => cf.ByAutomationId("PasswordBox")).AsTextBox();

            loginInput?.Text = "admin";
            passwordInput?.Text = "admin";

            button?.Invoke();

            var welcomeLabel = window?.FindFirstDescendant(cf => cf.ByAutomationId("WelcomeTextBlock")).AsLabel();

            welcomeLabel?.Text.Should().Be($"Добро пожаловать, admin, Ваша роль - Administator");

            await Task.Delay(250);
            window?.CaptureToFile("test_4.png");
            await Task.Delay(250);

            var backButton = window?.FindFirstDescendant(cf => cf.ByAutomationId("BrowseBack")).AsButton();
            backButton?.Invoke();
        }

    }
}
