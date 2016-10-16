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

namespace LocalizationDemo
{
    public class LocalizedString
    {
        private Resource m_LocalizedResource;

        public LocalizedString()
        {
            m_LocalizedResource = new Resource();
        }

        public Resource LocalizedResource { get { return m_LocalizedResource; } }
    }
}
