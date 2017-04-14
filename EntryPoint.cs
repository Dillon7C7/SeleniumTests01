// By Dillon Dommer
// April 09 2017

using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace DillonTestCaseAutomation
{
    class EntryPoint
    {
        static ChromeOptions chrOptions;
        static IWebDriver driver;

        static void NavtoTab(int choice)     // Navigate to a menu item under 'My Info'
        {
            string menu_string = "";

            switch (choice)                   // Refers to the menu item under 'My Info'
            {
                case 8:
                    menu_string = "Report to";
                    break;
                case 9:
                    menu_string = "Qualifications";
                    break;
                default:
                    Console.WriteLine("Error!!!");
                    break;
            }

            IWebElement menu_tab;

            try
            {
                menu_tab = driver.FindElement(By.CssSelector("#sidenav > li:nth-child(" + choice + ") > a:nth-child(1)"));

                if (menu_tab.Displayed)
                {
                    GreenMessage("'" + menu_string + "' menu item found.");
                }

                WhiteMessage("Switching to " + menu_string + " tab...");
                menu_tab.Click();
                Thread.Sleep(500);
            }
            catch (NoSuchElementException)
            {
                RedMessage("'" + menu_string + "' menu item not found!");
            }
        }



        // Run a little login script, and navigate to the "My Info" module. This will set the stage for the rest of
        // the tests in this file.
        static void Login()
        {
            IWebElement username, password, loginBtn, loginCheck, myInfo;
            string UserAndPass = "jasmine.morgan";

            try
            {
                username = driver.FindElement(By.Name("txtUsername"));

                if (username.Displayed)
                {
                    GreenMessage("Username field is visible");
                }
                WhiteMessage("Entering username...");
                username.SendKeys(UserAndPass);

            }
            catch (NoSuchElementException)
            {
                RedMessage("Username field is not visible");
            }

            try
            {
                password = driver.FindElement(By.Name("txtPassword"));

                if (password.Displayed)
                {
                    GreenMessage("Password field is visible");
                }
                WhiteMessage("Entering password...");
                password.SendKeys(UserAndPass);

            }
            catch (NoSuchElementException)
            {
                RedMessage("Password field is not visible");
            }

            try
            {
                loginBtn = driver.FindElement(By.Name("Submit"));
                if (loginBtn.Displayed)
                {
                    GreenMessage("Login button is visible");
                }
                WhiteMessage("Clicking login button...");
                loginBtn.Click();
                Thread.Sleep(500);
            }
            catch (NoSuchElementException)
            {
                RedMessage("Login button is not visible");
            }

            try
            {
                loginCheck = driver.FindElement(By.Id("welcome")); // if the "welcome" is visible, then we have
                                                                   // successfully logged in
                if (loginCheck.Displayed)
                {
                    GreenMessage("Login successful");
                }
            }
            catch (NoSuchElementException)
            {
                RedMessage("Login failed");
            }

            try
            {
                myInfo = driver.FindElement(By.CssSelector("#menu_pim_viewMyDetails > b:nth-child(1)"));

                if (myInfo.Displayed)
                {
                    GreenMessage("'My Info' Module found!");
                }

                WhiteMessage("Navigating to the 'My info' module...");
                myInfo.Click();
                Thread.Sleep(800);
            }
            catch (NoSuchElementException)
            {
                RedMessage("'My Info' Module not found!");
            }

            Console.WriteLine();
        }

        static void TC31901()  // We should be able to see a list of supervisor(s), and no subordinates.
        {
            WhiteMessage("Test Case 3.1.9 01 - Report To - View a list of Supervisors\n");
            Thread.Sleep(1000);

            IWebElement supervisor, empty;

            NavtoTab(8);           // Navigate to the appropriate tab

            try
            {
                supervisor = driver.FindElement(By.ClassName("supName"));

                if (supervisor.Displayed)
                {
                    GreenMessage("There is at least one supervisor.");
                }
            }
            catch (NoSuchElementException)
            {
                RedMessage("There are no supervisors!");
                PassOrFail("Test Case 3.1.9 01", false);
                return;
            }
            try
            {
                empty = driver.FindElement(By.CssSelector("#sub_list > tbody:nth-child(2) > tr:nth-child(1) > td:nth-child(2)"));

                if (empty.Displayed)                // Check to see if there are any subordinates
                {
                    GreenMessage("There are no subordinates.");
                }
            }
            catch (NoSuchElementException)
            {
                RedMessage("There is at least one subordinate!");
                PassOrFail("Test Case 3.1.9 01 - ", false);
                return;
            }

            PassOrFail("Test Case 3.1.9 01 - ", true);
        }

        static void TC31903()           // Test to see if we have can edit the 'Assigned Supervisor' field
        {                               // by clicking on an element in the table under it
            WhiteMessage("Test Case 3.1.9 03 - Report To - Restrictions (Assigned Supervisors)\n");
            Thread.Sleep(1000);

            IWebElement supervisor;

            NavtoTab(8);               // Navigate to the appropriate tab

            try
            {
                supervisor = driver.FindElement(By.ClassName("supName"));

                if (supervisor.Displayed)
                {
                    GreenMessage("'Assigned Supervisors' field is present.");
                }
                WhiteMessage("Clicking on the HTML table under 'Assigned Supervisors...'");
                supervisor.Click();
            }
            catch (NoSuchElementException)
            {
                RedMessage("Error!");
                PassOrFail("Test Case 3.1.9 03", false);
                return;

            }

            PassOrFail("Test Case 3.1.9 03 - ", true);
        }

        static void TC311001()    // Test to see if the 'Add' button works under 'Work Experience' under 'Qualifications'
        {
            WhiteMessage("Test Case 3.1.10 01 Qualifications - ‘Add’ button functionality (Work Experience)");
            Thread.Sleep(1000);

            IWebElement addBtn, cancelBtn;

            NavtoTab(9);                    // Navigate to the appropriate tab

            try
            {                               // Click on the 'Add' button
                addBtn = driver.FindElement(By.Id("addWorkExperience"));

                if (addBtn.Displayed)
                {
                    GreenMessage("'Add' button found!");
                }
                WhiteMessage("Clicking on the 'Add' button under 'Work Experience'...");
                addBtn.Click();
                Thread.Sleep(500);
            }
            catch (NoSuchElementException)
            {
                RedMessage("'Add' button not found!");
                PassOrFail("Test Case 3.1.10 01", false);
                return;
            }

            try
            {                           // Look for the 'Cancel' button
                cancelBtn = driver.FindElement(By.Id("btnWorkExpCancel"));

                if (cancelBtn.Displayed)
                {
                    GreenMessage("'Cancel' button found!");
                }
            }

            catch (NoSuchElementException)
            {
                RedMessage("'Cancel' button not found!");
                PassOrFail("Test Case 3.1.10 01", false);
                return;
            }

            PassOrFail("Test Case 3.1.10 01 - ", true);
        }

        static void TC311002()  // Test to see if the 'Cancel' button works under 'Work Experience' under 'Qualifications'
        {
            WhiteMessage("Test Case 3.1.10 02 Qualifications - ‘Cancel’ button functionality (Work Experience)");
            Thread.Sleep(1000);

            IWebElement addBtn, cancelBtn;

            NavtoTab(9);                    // Navigate to the appropriate tab

            try
            {                               // Click on the 'Add' button
                addBtn = driver.FindElement(By.Id("addWorkExperience"));

                if (addBtn.Displayed)
                {
                    GreenMessage("'Add' button found!");
                }
                WhiteMessage("Clicking on the 'Add' button under 'Work Experience'...");
                addBtn.Click();
                Thread.Sleep(500);
            }
            catch (NoSuchElementException)
            {
                RedMessage("'Add' button not found!");
                PassOrFail("Test Case 3.1.10 02", false);
                return;
            }

            try
            {                           // Click on the 'Cancel' button
                cancelBtn = driver.FindElement(By.Id("btnWorkExpCancel"));

                if (cancelBtn.Displayed)
                {
                    GreenMessage("'Cancel' button found!");
                }
                WhiteMessage("Clicking on the 'Cancel' button under 'Work Experience...'");
                cancelBtn.Click();
                Thread.Sleep(500);
            }

            catch (NoSuchElementException)
            {
                RedMessage("'Cancel' button not found!");
                PassOrFail("Test Case 3.1.10 02", false);
                return;
            }

            PassOrFail("Test Case 3.1.10 02 - ", true);
        }

        static void TC311003()  // Test to see if the 'Save' button works under 'Work Experience' under 'Qualifications'
        {
            WhiteMessage("Test Case 3.1.10 03 Qualifications - ‘Save’ button functionality (Work Experience)");
            Thread.Sleep(1000);

            IWebElement addBtn, saveBtn, companyText, jobText;

            NavtoTab(9);                    // Navigate to the appropriate tab

            try
            {                               // Click on the 'Add' button
                addBtn = driver.FindElement(By.Id("addWorkExperience"));

                if (addBtn.Displayed)
                {
                    GreenMessage("'Add' button found!");
                }
                WhiteMessage("Clicking on the 'Add' button under 'Work Experience'...");
                addBtn.Click();
                Thread.Sleep(500);
            }
            catch (NoSuchElementException)
            {
                RedMessage("'Add' button not found!");
                PassOrFail("Test Case 3.1.10 03", false);
                return;
            }

            try
            {                                       // Enter text in required field
                companyText = driver.FindElement(By.Id("experience_employer"));
                if (companyText.Displayed)
                {
                    GreenMessage("'Company' input field detected!");
                }
                WhiteMessage("Entering company information...");
                companyText.SendKeys("company");

            }
            catch (NoSuchElementException)
            {
                RedMessage("'Company' input field not found!");
                PassOrFail("Test Case 3.1.10 03", false);
                return;
            }
            Thread.Sleep(1000);

            try
            {                                       // Enter text in required field
                jobText = driver.FindElement(By.Id("experience_jobtitle"));
                if (jobText.Displayed)
                {
                    GreenMessage("'Job Title' input field detected!");
                }
                WhiteMessage("Entering a job title...");
                jobText.SendKeys("job");

            }
            catch (NoSuchElementException)
            {
                RedMessage("'Job Title' input field not found!");
                PassOrFail("Test Case 3.1.10 03", false);
                return;
            }
            Thread.Sleep(1000);

            try
            {                           // Click on the 'Save' button
                saveBtn = driver.FindElement(By.Id("btnWorkExpSave"));

                if (saveBtn.Displayed)
                {
                    GreenMessage("'Save' button found!");
                }
                WhiteMessage("Clicking on the 'Save' button under 'Work Experience...'");
                saveBtn.Click();
                Thread.Sleep(800);
            }

            catch (NoSuchElementException)
            {
                RedMessage("'Save' button not found!");
                PassOrFail("Test Case 3.1.10 03", false);
                return;
            }

            PassOrFail("Test Case 3.1.10 03 - ", true);
        }

        static void TC311004()  // Test to see if the 'Delete' button works under 'Work Experience' under 'Qualifications'
        {
            WhiteMessage("Test Case 3.1.10 04 Qualifications - ‘Delete’ button functionality (Work Experience)");
            Thread.Sleep(1000);

            IWebElement checkBox, delBtn;

            NavtoTab(9);                    // Navigate to the appropriate tab

            try
            {
                checkBox = driver.FindElement(By.CssSelector("tr.odd:nth-child(1) > td:nth-child(1) > input:nth-child(6)"));

                if (checkBox.Displayed)
                {
                    GreenMessage("check box found!");
                }
            }
            catch (NoSuchElementException)
            {
                RedMessage("No check boxes found!");
                PassOrFail("Test Case 3.1.10 04", false);
                return;
            }

            // Get number of check box items
            IReadOnlyCollection<IWebElement> boxes = driver.FindElements(By.ClassName("chkbox1"));
            int numberOfBoxes = boxes.Count;

            WhiteMessage("Checking check box...");

            bool isChecked = bool.TryParse(checkBox.GetAttribute("checked"), out isChecked); // check to see if
                                                                                             // check box is checked
            if (!isChecked)
            {
                checkBox.Click();       // check it if it's not checked already
            }

            Thread.Sleep(1000);

            try
            {                               // Click on the 'Delete' button
                delBtn = driver.FindElement(By.Id("delWorkExperience"));

                if (delBtn.Displayed)
                {
                    GreenMessage("'Delete' button found!");
                }
                WhiteMessage("Clicking on the 'Delete' button under 'Work Experience'...");
                delBtn.Click();
                Thread.Sleep(1000);
            }
            catch (NoSuchElementException)
            {
                RedMessage("'Delete' button not found!");
                PassOrFail("Test Case 3.1.10 04", false);
                return;
            }
            boxes = driver.FindElements(By.ClassName("chkbox1"));

            if (boxes.Count == numberOfBoxes - 1)                // Do we have 1 less box? If so, test passed
            {
                PassOrFail("Test Case 3.1.10 04 - ", true);
            }
            else
            {
                PassOrFail("Test Case 3.1.10 04 - ", false);
            }
        }

        static void Main()
        {
            chrOptions = new ChromeOptions();
            chrOptions.AddArguments("--start-maximized"); // Make sure the browser is maximized
            driver = new ChromeDriver(chrOptions);        // Initialize the driver

            string url = "http://opensource.demo.orangehrmlive.com/";

            driver.Navigate().GoToUrl(url);               // Navigate to the test site URL

            Login();                                      // Get ready for the module testing
            Thread.Sleep(3000);

            WhiteMessage("Enter the test case that you want to run:");

            WhiteMessage("1. Test Case 3.1.9 01      2. Test Case 3.1.9 03");
            WhiteMessage("3. Test Case 3.1.10 01     4. Test Case 3.1.10 02");
            WhiteMessage("5. Test Case 3.1.10 03     6. Test Case 3.1.10 04");

            TC31901();                                    // Test case 3.1.9 01
            Thread.Sleep(3000);

            TC31903();                                    // Test case 3.1.9 03
            Thread.Sleep(3000);

            TC311001();                                   // Test case 3.1.10 01
            Thread.Sleep(3000);

            TC311002();                                   // Test case 3.1.10 02
            Thread.Sleep(3000);

            TC311003();                                   // Test case 3.1.10 03
            Thread.Sleep(3000);

            TC311004();                                   // Test case 3.1.10 04
            Thread.Sleep(3000);

            driver.Quit();
            Console.ReadLine();
        }

        private static void GreenMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        private static void RedMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        private static void WhiteMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        private static void PassOrFail(string msg, bool passOrFail)  // Print out if the test passed or failed
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(msg + " -- ");

            if (passOrFail)             // Test passed!
            {
                GreenMessage("PASS");
            }
            else
            {
                RedMessage("FAIL");     // Test failed!
            }
            Console.WriteLine();
        }
    }
}