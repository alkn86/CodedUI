using System;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using MSTestAllureAdapter;
using System.Collections.Generic;
using System.IO;

namespace CodedUITestProject1
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public static String testResultDir;
        public CodedUITest1()
        {
        }
        [TestInitialize]
        public void Init()
        {
            Directory.SetCurrentDirectory("\\out\\");
            testResultDir = Directory.GetCurrentDirectory();            
        }
        [ClassCleanup]
        public static void GatherTestResults()
        {            
            AllureAdapter adapter = new AllureAdapter();
            TRXParser parser = new TRXParser();
            //String currentDir = Directory.GetCurrentDirectory();
            Console.WriteLine(testResultDir);
            Directory.CreateDirectory(testResultDir + "\\results");
            string[] reportFiles = Directory.GetFiles("\\TestResults", "*.trx");

            try
            {
                if (reportFiles.Length == 0) throw new Exception(message: testResultDir+" No report files");
                for (int i = 0; i < reportFiles.Length; i++)
                {
                    IEnumerable<MSTestResult> testresults = parser.GetTestResults(reportFiles[i]);
                    adapter.GenerateTestResults(testresults, "\\results");
                }
            }
            catch (Exception e)
            {
                throw new Exception(message: e.Message);
            }
            Console.WriteLine("Finished");

        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            Console.WriteLine(Directory.GetCurrentDirectory());
            UIMap2Classes.UIRunningapplicationsWindow runAppWindow = new UIMap2Classes.UIRunningapplicationsWindow();
            UIMap2Classes.UIRunningapplicationsToolBar runningToolbar = runAppWindow.UIRunningapplicationsToolBar;
            WinButton fileExplorerButton = runningToolbar.UIFileExplorerButton;
            Mouse.Click(fileExplorerButton);

            UIMap1Classes.UIFileExplorerWindow window = new UIMap1Classes.UIFileExplorerWindow();
            window.SetFocus();
            Assert.IsTrue(window.Exists);

            UIMap1Classes.UIFileExplorerTitleBar titleBar = new UIMap1Classes.UIFileExplorerTitleBar(window);
            WinButton buttonClose = titleBar.UICloseButton;
            Mouse.Click(buttonClose);
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }        
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
