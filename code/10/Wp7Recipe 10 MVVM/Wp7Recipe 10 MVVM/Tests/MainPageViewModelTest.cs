using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wp7Recipe_10_2_MVVM.ViewModels;

namespace Wp7Recipe_10_2_MVVM.Tests
{
    [TestClass]
    public class MainPageViewModelTest : SilverlightTest
    {
        [TestMethod]
        public void TestSaveAlwaysFails()
        {
                MainPageViewModel vm = new MainPageViewModel();
                vm.Motivation = string.Empty;
                vm.SaveCommand.Execute(null);
                Assert.IsTrue(vm.State == MainPageViewModel.States.Saved);
        }

        [TestMethod]
        public void TestSaveOk()
        {
            MainPageViewModel vm = new MainPageViewModel();
            vm.Motivation = "Shopping";
            vm.Amount = 200;
            vm.SaveCommand.Execute(null);
            Assert.IsTrue(vm.State == MainPageViewModel.States.Saved);
        }
        
        [TestMethod]
        public void TestSaveKo()
        {
            MainPageViewModel vm = new MainPageViewModel();
            vm.Motivation = string.Empty;
            vm.SaveCommand.Execute(null);
            Assert.IsFalse(vm.State == MainPageViewModel.States.Saved);
        }

    }
}
