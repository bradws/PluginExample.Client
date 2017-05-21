using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;
//using Prism.Events;

using System.ComponentModel.Composition;
using PluginExample.Common;
using System.ComponentModel.Composition.Hosting;

namespace PluginExample.Client
{
    /// <summary>
    /// View Model for the MainWindow view.
    /// </summary>
    public class MainWindowVM : BindableBase
    {
        #region Constructor
        public MainWindowVM(  )
        {
            //  1.  Save the arguments
            //this.eventAggregator = eventAggregator;

            //  2.  Instantiate any properties
            this.filenames = new List<ITextToFilenames>();
            this.deletionMethods = new ObservableCollection<DeletionMethod>();

            //  3.  Setup Commands
            this.SaveCommand = new DelegateCommand(this.Save);
            this.SelectionChangedCommand = new DelegateCommand<System.Collections.IList>(this.SelectionChanged);

            //  4.  Setup default values
            this.ApplicationDescription = "This program deletes files and/or folders.\nEach tab item represents an area of text you can input, and the filenames will be generated below the text."
                + "\nYou then simply press the 'delete' button.";

            //  5.  Setup the Plugins (i.e. MEF)
            this.setupPlugins();
        }

        #endregion

        #region Public Properties

        private string applicationDescription;
        public string ApplicationDescription
        {
            get { return applicationDescription; }
            set { applicationDescription = value; }
        }

        private ObservableCollection<DeletionMethod> deletionMethods;
        public ObservableCollection<DeletionMethod> DeletionMethods
        {
            get { return deletionMethods; }
            set
            {
                deletionMethods = value;
                RaisePropertyChanged( nameof(DeletionMethods) );
            }
        }

        #endregion Properties

        #region Commands

        public ICommand SaveCommand { get; private set; }
        private void Save()
        {
            throw new Exception("Save not yet implemented.");
        }

        public ICommand SelectionChangedCommand { get; private set; }
        /// <summary>
        /// Change the selection of the currently-selected DeletionMethod
        /// </summary>
        /// <param name="newlySelectedDeletionMethod">An IList of size=1 containing the DeletionMethod type that is to be the newly selected one</param>
        private void SelectionChanged(System.Collections.IList newlySelectedDeletionMethod)
        {
            this.selectedDeletionMethod = (DeletionMethod)newlySelectedDeletionMethod[0];
        }
        #endregion Commands

        #region Private Properties

        [ImportMany(typeof(ITextToFilenames))]
        private List<ITextToFilenames> filenames;

        private DeletionMethod selectedDeletionMethod;

        #endregion Private Properties

        #region Private Methods

        private void setupPlugins()
        {
            //  1.  Populate all the plugins from disk into the property 'filenames'
            AggregateCatalog ag = new AggregateCatalog();
            DirectoryCatalog dc = new DirectoryCatalog(@".");       //  Reads the Plugins from the folder where the executable resides
            ag.Catalogs.Add(dc);

            AssemblyCatalog catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(ag);

            container.SatisfyImportsOnce(this); // 'SatisfyImportsOnce' means hook everything up at once

            //  2.  Now we have all the plugins data, populate the 'DeletionMethods' property
            foreach (ITextToFilenames ttf in this.filenames)
            {
                DeletionMethod dm = new DeletionMethod(ttf.Title, ttf.Description, ttf.GetFilenames);
                dm.CheckForFilesClicked += CheckForFilesClicked;
                deletionMethods.Add(dm);
            }
        }

        private void CheckForFilesClicked(object sender, EventArgs e)
        {         
            
        }

        #endregion Private Methods
    }
}
