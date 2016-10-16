/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelsLocator xmlns:vm="clr-namespace:Wp7Recipe_10_2_MVVM.ViewModels"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

namespace Wp7Recipe_10_2_MVVM.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelsLocator in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelsLocator xmlns:vm="clr-namespace:Wp7Recipe_10_2_MVVM.ViewModels"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class ViewModelsLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelsLocator class.
        /// </summary>
        public ViewModelsLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view models
            ////}
            ////else
            ////{
            ////    // Create run time view models
            ////}
        }

        public static void Cleanup()
        {
            ClearMainPageViewModel();
        }

        #region MainPageViewModel
        private static MainPageViewModel _mainPageViewModel;

        /// <summary>
        /// Gets the MainPageViewModel property.
        /// </summary>
        public static MainPageViewModel MainPageViewModelStatic
        {
            get
            {
                if (_mainPageViewModel == null)
                {
                    CreateMainPageViewModel();
                }

                return _mainPageViewModel;
            }
        }

        /// <summary>
        /// Gets the MainPageViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                return MainPageViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the MainPageViewModel property.
        /// </summary>
        public static void ClearMainPageViewModel()
        {
            _mainPageViewModel.Cleanup();
            _mainPageViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the MainPageViewModel property.
        /// </summary>
        public static void CreateMainPageViewModel()
        {
            if (_mainPageViewModel == null)
            {
                _mainPageViewModel = new MainPageViewModel();
            }
        }
        #endregion
    }
}