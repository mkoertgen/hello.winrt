using System.Collections.Generic;
using System.Linq;
using Stylet;

namespace hello.winrt.Pages
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell
    {
        public ShellViewModel(IEnumerable<IFeatureTab> features)
        {
            DisplayName = "hello.winrt";
            Items.AddRange(features);
            ActiveItem = Items.FirstOrDefault();
        }
    }
}
