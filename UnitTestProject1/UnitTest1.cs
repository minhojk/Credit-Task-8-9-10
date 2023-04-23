using CreditTaskWin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestLogin()
        {
            var f = new Form1(); // assuming LoginForm is the name of the form containing the button
            f.txtUsername.Text = "testuser"; // set test username
            f.txtPassword.Text = "testpassword"; // set test password

            // Act
            f.button1_Click(null, null);
            var result = MessageBox.Show("Login Successful");

            // Assert
            Assert.AreEqual(result, "Login Successful");
        }
    }
}
