using System.Windows.Controls;

namespace NicAlert.View
{
    public partial class PopupSplash : UserControl
    {
        public PopupSplash()
        {
            InitializeComponent();
            progressBar1.IsIndeterminate = true;
        }
    }
}
