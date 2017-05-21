using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Prism.Mvvm;
using PluginExample.Common;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace PluginExample.Controls
{
    public class TabControlVM : BindableBase
    {
        public TabControlVM()
        {
            //  1.  Instantiate any properties
            this.deletionMethods = new List<DeletionMethod>();
            this.TabItems = new ObservableCollection<TabItem>();

            //  2.  Setup Commands

            //  3.  Setup default values

            StackPanel sp1 = new StackPanel();
            sp1.Children.Add(new Button() { Content = "mdfksngcmlfgnrjmcsgjrkolc" });

            TabItem ti1 = new TabItem();
            ti1.Name = "ti1";
            ti1.Header = "ti1a";
            ti1.Content = sp1;
            this.TabItems.Add(ti1);

            TabItem ti2 = new TabItem();
            ti2.Name = "ti2";
            ti2.Header = "ti2b";
            this.TabItems.Add(ti2);

            TabItem ti3 = new TabItem();
            ti3.Name = "ti3";
            ti3.Header = "ti3c";
            this.TabItems.Add(ti3);
        }

        #region Properties

        private List<DeletionMethod> deletionMethods;
        public List<DeletionMethod> DeletionMethods    // Populate this into the raw WPF TabControl
        {
            get { return deletionMethods; }
            set
            {
                SetProperty<List<DeletionMethod>>(ref deletionMethods, value);  // TODO: Not sure we need this line?
            }
        }

        private ObservableCollection<TabItem> _tabItems;
        public ObservableCollection<TabItem> TabItems
        {
            get { return _tabItems; }
            set { _tabItems = value; }
        }
        #endregion


    }
}
