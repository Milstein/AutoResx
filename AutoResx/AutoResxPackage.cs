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
        private IList<ProjectItem> _resxFiles = new List<ProjectItem>();
        private IList<string>  _errors = new List<string>();
        private bool _removeOldTagsAndFiles = true; //是否清楚原来的resource文件
        int _waitForDesignViewSeconds = 10;
        int _waitForCommandSeconds = 5;
        int _waitForSaveSeconds = 2;
        private int _maxFileCount = int.MaxValue;

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
            IList<string> projects= new List<string>();
            foreach (Project p in ps)
            {
                projects.Add(p.Name);
            }

            if (projects.Count == 0)
            {
                MessageBox.Show("No project to process.");
                return;
            }

            OptionForm optionForm = new OptionForm(projects);
            if (optionForm.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Task canceled.");
                return;
            }

            _waitForCommandSeconds = optionForm.WaitForCommandReadySeconds;
            _waitForDesignViewSeconds = optionForm.WaitForDesignViewSeconds;
            _waitForSaveSeconds = optionForm.WaitForSaveSeconds;
            _maxFileCount = optionForm.MaxProcessFiles;
            _removeOldTagsAndFiles = optionForm.RemoveOldTags;

            foreach (Project p in ps)
            {
                if (p.Name.Equals(optionForm.SelectedProject, StringComparison.InvariantCultureIgnoreCase)) //do this only for cs project
                {
                    Console.WriteLine("Processing project :" + p.Name);

                    foreach (ProjectItem pi in p.ProjectItems)
                    {
                        GetSourceFiles(pi);
                    }
                }
            }

            int count = 0; //how many files proccessed
            foreach (var file in _sourceFiles)
            {

                Console.WriteLine("Processing file :" + file.Name);

                ProcessFile(dte, file);

                count ++;

                if (count >= _maxFileCount)
                {
                    break;
                }
            }

            if (_errors.Count > 0)
            {
                File.WriteAllLines("D:\\AutoResxError.txt", _errors);
                System.Diagnostics.Process.Start("NotePad.exe", "D:\\AutoResxError.txt");
            }
            MessageBox.Show(String.Format("Total {0} files processed. {1} errors.", count, _errors.Count));


        }

        /// <summary>
        /// 将符合条件的文件加入队列
        /// </summary>
        /// <param name="pi"></param>
        private void GetSourceFiles(ProjectItem pi)
        {
            string fileName = pi.Name.ToLower();
            //if (fileName.Contains("debug"))
            //{
            //    return;
            //}

            //删除原来的resx文件
            if (_removeOldTagsAndFiles &&
                (fileName.EndsWith(".aspx.resx") || fileName.EndsWith(".ascx.resx")))
            {
                _resxFiles.Add(pi);

                return;
            }

            //如果是代码文件，加入队列
            if (fileName.EndsWith(".aspx") || fileName.EndsWith(".ascx"))
            {
                _sourceFiles.Add(pi);
            }

            //对子目录进行处理
            if (pi.ProjectItems == null)
                return ;

            foreach (ProjectItem item in pi.ProjectItems)
            {
                GetSourceFiles(item);
            }
        }


        private void ProcessFile(DTE2 dte, ProjectItem pi)
        {
            

            var window = pi.Open(EnvDTE.Constants.vsViewKindTextView);
            window.Activate();
            
            //
            // delete old resx file
            //
            if (_removeOldTagsAndFiles)
            {
                //
                // 清除Markup文件中的原来的resourcekey信息。
                //
                Find2 findWin = dte.Find as Find2;
                findWin.WaitForFindToComplete = true;
                findWin.FindReplace(vsFindAction.vsFindActionReplaceAll,
                    "meta:resourceKey=\"[a-zA-Z0-9]+\"",
                    (int)vsFindOptions.vsFindOptionsRegularExpression,
                    "",
                    vsFindTarget.vsFindTargetCurrentDocument, "", "", vsFindResultsLocation.vsFindResultsNone);

                dte.ExecuteCommand("Edit.FormatDocument");
                dte.ExecuteCommand("File.SaveAll");

                dte.ExecuteCommand("View.ViewDesigner");
                window.Activate();


                //
                // delete resx file
                //
                foreach (ProjectItem resx in _resxFiles)
                {
                    if (resx.Name.Equals(pi.Name + ".resx", StringComparison.InvariantCultureIgnoreCase))
                    {
                        try
                        {
                            resx.Delete();
                        }
                        catch (Exception)
                        {
                        }
                        
                        _resxFiles.Remove(resx);
                        break;
                    }
                }
            }
            

            dte.ExecuteCommand("View.ViewDesigner");
            
            //MessageBox.Show("Wait for view!"); //等待视图加载完成
            WaitForm waitFrm = new WaitForm("Please wait Design View Ready...", _waitForDesignViewSeconds);
            waitFrm.ShowDialog();

            dte.ExecuteCommand("File.SaveAll");
            window.Activate();
            
            //IF still can not execute generateresouce command, do something and retray
            //如果此时仍然不能执行命令，那么做点啥事过一会儿再试试。
            if (!dte.Commands.Item("Tools.GenerateLocalResource").IsAvailable)
            {
                dte.ExecuteCommand("View.ViewMarkup");
                dte.ExecuteCommand("View.ViewDesigner");
                dte.ExecuteCommand("File.SaveAll");

                if (_waitForCommandSeconds > 0)
                {
                    waitFrm = new WaitForm("Wait for Command ready...", _waitForCommandSeconds);
                    waitFrm.ShowDialog();
                }
            }

            // generate resource and save
            if (dte.Commands.Item("Tools.GenerateLocalResource").IsAvailable)
            {
                try
                {
                    dte.ExecuteCommand("Tools.GenerateLocalResource");
                    dte.ExecuteCommand("File.SaveAll");

                    if (_waitForSaveSeconds > 0)
                    {
                        waitFrm = new WaitForm("Wait for save...", _waitForSaveSeconds);
                        waitFrm.ShowDialog();
                    }
                    
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
                //MessageBox.Show("can not execute generate resource command");
                dte.ActiveWindow.Close(vsSaveChanges.vsSaveChangesNo);
            }
        }
    }
}
