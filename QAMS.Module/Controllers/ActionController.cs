using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using QAMS.Module.BusinessObjects.NPObjects;
using QAMS.Module.BusinessObjects.TestManagement;

namespace QAMS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ActionController : ViewController
    {
        public ActionController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            #region Access the Master Object from a Nested List View
            if (View is ListView)
                if ((View as ListView).CollectionSource is PropertyCollectionSource)
                {
                    PropertyCollectionSource collectionSource =
                        (PropertyCollectionSource)(View as ListView).CollectionSource;
                    collectionSource.MasterObjectChanged += OnMasterObjectChanged;
                    if (collectionSource.MasterObject != null)
                        UpdateMasterObject(collectionSource.MasterObject);
                }
            #endregion
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            #region Access the Master Object from a Nested List View
            if (View is ListView)
                if ((View as ListView).CollectionSource is PropertyCollectionSource)
                {
                    PropertyCollectionSource collectionSource =
                        (PropertyCollectionSource)(View as ListView).CollectionSource;
                    collectionSource.MasterObjectChanged -= OnMasterObjectChanged;
                }
            #endregion
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        #region Access the Master Object from a Nested List View
        object masterObjectOfNestedListView = null;
        void UpdateMasterObject(object masterObject)
        {
            masterObjectOfNestedListView = masterObject;
            //Use the master object as required             
        }
        void OnMasterObjectChanged(object sender, EventArgs e)
        {
            UpdateMasterObject(((PropertyCollectionSource)sender).MasterObject);
        }

        #endregion

        private TestCaseAssignRunResultNP _TCA;
        private void popTestCaseAssignRunResultNP_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _TCA = ObjectSpace.CreateObject<TestCaseAssignRunResultNP>();
            e.View = Application.CreateDetailView(ObjectSpace, _TCA, false);
            e.DialogController.SaveOnAccept = false;
        }


        private void popTestCaseAssignRunResultNP_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_TCA != null
                && _TCA.Project != null
                && _TCA.TestCases != null
                && _TCA.TestCases.Count > 0)
            {
                TestRun testRun = null;
                if (masterObjectOfNestedListView is TestRun)
                    testRun = masterObjectOfNestedListView as TestRun;

                foreach (BusinessObjects.TestManagement.TestCase item in _TCA.TestCases.ToList())
                {
                    TestRunResult newRunResult = ObjectSpace.CreateObject<TestRunResult>();
                    newRunResult.TestRun = testRun;
                    newRunResult.TestCase = item;
                    testRun.TestRunResults.Add(newRunResult);
                }

                this.ObjectSpace.CommitChanges();
            }
        }

        private UserAssignRunResultNP _UAR;
        private void popUserAssignRunResultNP_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _UAR = ObjectSpace.CreateObject<UserAssignRunResultNP>();
            e.View = Application.CreateDetailView(ObjectSpace, _UAR, false);
            e.DialogController.SaveOnAccept = false;
        }


        private void popUserAssignRunResultNP_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_UAR != null
                && _UAR.RunAssigned != null)
            {
                foreach (TestRunResult selectedRunResult in this.View.SelectedObjects.OfType<TestRunResult>().ToList())
                {
                    selectedRunResult.RunAssigned = _UAR.RunAssigned;
                }

                this.ObjectSpace.CommitChanges();
            }
        }
    }
}
