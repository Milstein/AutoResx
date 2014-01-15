using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;


using EnvDTE;
using EnvDTE80;
using EnvDTE100;
using EnvDTE90;
using System.Windows.Forms; 
using System.Collections.Generic;
using System.IO;


namespace cuiliangbjgmailcom.AutoResx
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidAutoResxPkgString)]
    public sealed class AutoResxPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public AutoResxPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if ( null != mcs )
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.guidAutoResxCmdSet, (int)PkgCmdIDList.cmdidAutoResx);
                MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID );
                mcs.AddCommand( menuItem );
            }
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            //// Show a Message Box to prove we were here
            //IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            //Guid clsid = Guid.Empty;
            //int result;
            //Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(uiShell.ShowMessageBox(
            //           0,
            //           ref clsid,
            //           "AutoResx",
            //           string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.ToString()),
            //           string.Empty,
            //           0,
            //           OLEMSGBUTTON.OLEMSGBUTTON_OK,
            //           OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST,
            //           OLEMSGICON.OLEMSGICON_INFO,
            //           0,        // false
            //           out result));

            ProcessFiles();
        }

        // 参考：http://forums.asp.net/t/1280608.aspx


        private IList<ProjectItem> _sourceFiles = new List<ProjectItem>();
        private IList<string>  _errors = new List<string>();

        private void ProcessFiles()
        {
            DTE2 dte = (DTE2)GetService(typeof(DTE));

            // 关闭所有的当前窗口
            if (dte.ActiveDocument != null)
            {
                dte.ExecuteCommand("Window.CloseAllDocuments");    
            }
            

            //
            Projects ps = dte.Solution.Projects;
            foreach (Project p in ps)
            {
                if (p.FullName.ToLower().EndsWith(".csproj"))
                {
                    Console.WriteLine("Processing project :" + p.Name);

                    foreach (ProjectItem pi in p.ProjectItems)
                    {
                        GetFiles(pi);
                    }
                }
            }
            

            //Array projects = dte.ActiveSolutionProjects as Array;


            //if (projects.Length > 0)
            //{
            //    Project currentproject = projects.GetValue(0) as Project;

            //    foreach (ProjectItem pi in currentproject.ProjectItems)
            //    {
            //        GetFiles(pi);
            //    }
            //}

            
            foreach (var file in _sourceFiles)
            {
                

                Console.WriteLine("Processing file :" + file.Name);

                ProcessFile(dte, file);

                
                
            }

            if (_errors.Count > 0)
            {
                File.WriteAllLines("D:\\AutoResxError.txt", _errors);
                System.Diagnostics.Process.Start("NotePad.exe", "D:\\AutoResxError.txt");
            }
            MessageBox.Show("共处理了" + _sourceFiles.Count + "个文件");


        }

        /// <summary>
        /// 将符合条件的文件加入队列
        /// </summary>
        /// <param name="pi"></param>
        private void GetFiles(ProjectItem pi)
        {
            string fileName = pi.Name.ToLower();
            if (fileName.Contains("debug"))
            {
                return;
            }

            //删除原来的resx文件
            if (fileName.EndsWith(".aspx.resx") || fileName.EndsWith(".ascx.resx"))
            {
                try
                {
                    pi.Delete();
                }
                catch (Exception ex)
                {
                    //throw;
                }
                
                return;
            }

            //如果是代码文件，加入队列
            if (fileName.EndsWith(".aspx") || fileName.EndsWith(".ascx"))
            {
                _sourceFiles.Add(pi);
            }

            //base case
            if (pi.ProjectItems == null)
                return ;

            foreach (ProjectItem item in pi.ProjectItems)
            {
                GetFiles(item);
            }
        }


        private void ProcessFile(DTE2 dte, ProjectItem pi)
        {
            var window = pi.Open(EnvDTE.Constants.vsViewKindTextView);
            window.Activate();

            //
            // 清除Markup文件中的原来的resourcekey信息。
            //

            Find2 findWin = dte.Find as Find2;
            findWin.WaitForFindToComplete = true;
            findWin.FindReplace(vsFindAction.vsFindActionReplaceAll,
                "meta:resourceKey=\"[a-zA-Z0-9]+\"",
                (int) vsFindOptions.vsFindOptionsRegularExpression,
                "",
                vsFindTarget.vsFindTargetCurrentDocument, "", "", vsFindResultsLocation.vsFindResultsNone);

            dte.ExecuteCommand("Edit.FormatDocument");
            dte.ExecuteCommand("File.SaveAll");

            dte.ExecuteCommand("View.ViewDesigner");
            window.Activate();

            //
            // delete old resx file
            //
            string filePath = dte.ActiveDocument.FullName;

            string resxFile = Path.Combine(Path.GetDirectoryName(filePath),
                "App_LocalResources\\" + Path.GetFileName(filePath) + ".resx");

            try
            {
                //MessageBox.Show(resxFile);

                if (File.Exists(resxFile))
                {
                    File.Delete(resxFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Delete resx file failed:" + resxFile + " EX:" + ex.Message);
            }

            dte.ExecuteCommand("View.ViewDesigner");
            
            MessageBox.Show("Wait for view!"); //等待视图加载完成
            dte.ExecuteCommand("File.SaveAll");
            window.Activate();
            

            if (!dte.Commands.Item("Tools.GenerateLocalResource").IsAvailable)
            {
                dte.ExecuteCommand("View.ViewMarkup");
                dte.ExecuteCommand("View.ViewDesigner");
                dte.ExecuteCommand("File.SaveAll");
                MessageBox.Show("Wait for command again!");
            }

            if (dte.Commands.Item("Tools.GenerateLocalResource").IsAvailable)
            {
                try
                {
                    dte.ExecuteCommand("Tools.GenerateLocalResource");
                    //System.Threading.Thread.Sleep(10000); //等待资源生成
                    //MessageBox.Show("Wait resource generation.");

                    dte.ExecuteCommand("File.SaveAll");
                }
                catch (Exception ex)
                {
                    _errors.Add("File " + pi.Name + " EX:" + ex.Message);
                }

                //window.Activate();
                dte.ActiveWindow.Close(vsSaveChanges.vsSaveChangesYes);
            }
            else
            {
                _errors.Add("Process file " + pi.Name + " : NO COMMAND Available");
                MessageBox.Show("can not execute generate resource command");
            }


            
            //try
            //{
            //    dte.ExecuteCommand("Tools.GenerateLocalResource");
            //}
            //catch (Exception ex)
            //{
            //    _errors.Add("Process file " + pi.Name + " EX:" + ex.Message);
            //}


            //dte.ActiveWindow.Close(vsSaveChanges.vsSaveChangesYes);
        }
    }
}
